using Azaliq.Data;
using Azaliq.Data.Models.Models;
using Azaliq.Services.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

[TestFixture]
public class CartServiceTests
{
    private Mock<ApplicationDbContext> _mockContext;
    private Mock<DbSet<CartItem>> _mockCartItems;
    private Mock<DbSet<Product>> _mockProducts;
    private List<CartItem> _cartItemsData;
    private List<Product> _productsData;
    private CartService _cartService;

    [SetUp]
    public void Setup()
    {
        _cartItemsData = new List<CartItem>();
        _productsData = new List<Product>();

        // Setup CartItems DbSet mock backed by list
        _mockCartItems = CreateDbSetMock(_cartItemsData);

        // Setup Products DbSet mock backed by list
        _mockProducts = CreateDbSetMock(_productsData);

        // Setup DbContext mock
        _mockContext = new Mock<ApplicationDbContext>();
        _mockContext.Setup(c => c.CartItems).Returns(_mockCartItems.Object);
        _mockContext.Setup(c => c.Products).Returns(_mockProducts.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Setup AddAsync to add item to list and return dummy EntityEntry
        _mockCartItems.Setup(m => m.AddAsync(It.IsAny<CartItem>(), It.IsAny<CancellationToken>()))
            .Returns((CartItem entity, CancellationToken token) =>
            {
                _cartItemsData.Add(entity);
                var dummyEntry = Mock.Of<EntityEntry<CartItem>>(e => e.Entity == entity);
                return new ValueTask<EntityEntry<CartItem>>(dummyEntry);
            });

        _cartService = new CartService(_mockContext.Object);
    }

    // Helper to create a mock DbSet<T> backed by a List<T>
    private static Mock<DbSet<T>> CreateDbSetMock<T>(List<T> list) where T : class
    {
        var queryable = list.AsQueryable();

        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IAsyncEnumerable<T>>()
            .Setup(d => d.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new AsyncEnumerator<T>(queryable.GetEnumerator()));

        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new AsyncQueryProvider<T>(queryable.Provider));
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        dbSetMock.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(entity => list.Remove(entity));
        dbSetMock.Setup(d => d.RemoveRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>(entities =>
        {
            foreach (var e in entities.ToList())
                list.Remove(e);
        });

        return dbSetMock;
    }

    [Test]
    public async Task AddToCartAsync_AddsNewItem_WhenNotExists()
    {
        var userId = "user1";
        var productId = 1;
        var quantity = 2;

        _productsData.Add(new Product
        {
            Id = productId,
            Name = "Flower",
            Price = 10,
            Quantity = 5,
            ImageUrl = "img.jpg"
        });

        await _cartService.AddToCartAsync(userId, productId, quantity);

        Assert.That(_cartItemsData.Count, Is.EqualTo(1));
        var cartItem = _cartItemsData.First();
        Assert.AreEqual(userId, cartItem.UserId);
        Assert.AreEqual(productId, cartItem.ProductId);
        Assert.AreEqual(quantity, cartItem.Quantity);
    }

    [Test]
    public async Task AddToCartAsync_IncrementsQuantity_WhenCartItemExists()
    {
        var userId = "user1";
        var productId = 1;

        _productsData.Add(new Product
        {
            Id = productId,
            Name = "Flower",
            Price = 10,
            Quantity = 5,
            ImageUrl = "img.jpg"
        });

        _cartItemsData.Add(new CartItem
        {
            UserId = userId,
            ProductId = productId,
            Quantity = 3
        });

        await _cartService.AddToCartAsync(userId, productId, 2);

        Assert.That(_cartItemsData.Count, Is.EqualTo(1));
        var cartItem = _cartItemsData.First();
        Assert.AreEqual(5, cartItem.Quantity); // 3 + 2
    }

    [Test]
    public async Task GetCartItemsAsync_ReturnsAllItemsForUser()
    {
        var userId = "user1";
        var productId = 1;

        _productsData.Add(new Product
        {
            Id = productId,
            Name = "Flower",
            Price = 20,
            Quantity = 10,
            ImageUrl = "image.jpg"
        });

        _cartItemsData.Add(new CartItem
        {
            Id = 101,
            UserId = userId,
            ProductId = productId,
            Quantity = 4
        });

        var items = await _cartService.GetCartItemsAsync(userId);

        Assert.That(items.Count, Is.EqualTo(1));
        var item = items.First();

        Assert.AreEqual(101, item.Id);
        Assert.AreEqual(productId, item.ProductId);
        Assert.AreEqual("Flower", item.ProductName);
        Assert.AreEqual("image.jpg", item.ProductImageUrl);
        Assert.AreEqual(20, item.Price);
        Assert.AreEqual(4, item.Quantity);
        Assert.AreEqual(10, item.StockQuantity);
    }

    [Test]
    public async Task RemoveFromCartAsync_RemovesItem_WhenExists()
    {
        var userId = "user1";
        var productId = 1;

        _productsData.Add(new Product { Id = productId });
        var cartItem = new CartItem
        {
            UserId = userId,
            ProductId = productId,
            Quantity = 1
        };
        _cartItemsData.Add(cartItem);

        await _cartService.RemoveFromCartAsync(userId, productId);

        Assert.IsFalse(_cartItemsData.Contains(cartItem));
    }

    [Test]
    public async Task ClearCartAsync_RemovesAllUserItems()
    {
        var userId = "user1";

        _cartItemsData.Add(new CartItem { UserId = userId, ProductId = 1, Quantity = 1 });
        _cartItemsData.Add(new CartItem { UserId = userId, ProductId = 2, Quantity = 3 });

        await _cartService.ClearCartAsync(userId);

        Assert.IsEmpty(_cartItemsData.Where(ci => ci.UserId == userId));
    }

    [Test]
    public async Task UpdateQuantityAsync_UpdatesQuantity_WhenPositive()
    {
        var userId = "user1";
        var productId = 1;

        _productsData.Add(new Product { Id = productId });

        var cartItem = new CartItem
        {
            UserId = userId,
            ProductId = productId,
            Quantity = 3
        };
        _cartItemsData.Add(cartItem);

        await _cartService.UpdateQuantityAsync(userId, productId, 5);

        Assert.AreEqual(5, cartItem.Quantity);
    }

    [Test]
    public async Task UpdateQuantityAsync_RemovesItem_WhenQuantityZeroOrLess()
    {
        var userId = "user1";
        var productId = 1;

        _productsData.Add(new Product { Id = productId });

        var cartItem = new CartItem
        {
            UserId = userId,
            ProductId = productId,
            Quantity = 3
        };
        _cartItemsData.Add(cartItem);

        await _cartService.UpdateQuantityAsync(userId, productId, 0);

        Assert.IsFalse(_cartItemsData.Contains(cartItem));
    }
}

internal class AsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;

    internal AsyncQueryProvider(IQueryProvider inner)
    {
        _inner = inner;
    }

    public IQueryable CreateQuery(Expression expression)
        => new AsyncEnumerable<TEntity>(expression);

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        => new AsyncEnumerable<TElement>(expression);

    public object Execute(Expression expression)
        => _inner.Execute(expression);

    public TResult Execute<TResult>(Expression expression)
        => _inner.Execute<TResult>(expression);

    // EF Core 7+ requires this signature, returning TResult directly
    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        => Execute<TResult>(expression);
}

internal class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    public AsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
    public AsyncEnumerable(System.Linq.Expressions.Expression expression) : base(expression) { }
    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    IQueryProvider IQueryable.Provider => new AsyncQueryProvider<T>(this);
}

internal class AsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;
    public AsyncEnumerator(IEnumerator<T> inner) { _inner = inner; }
    public ValueTask DisposeAsync() { _inner.Dispose(); return new ValueTask(); }
    public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());
    public T Current => _inner.Current;
}