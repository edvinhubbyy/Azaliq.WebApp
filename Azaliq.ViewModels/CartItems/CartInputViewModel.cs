using Azaliq.Data.Models.Models;
using Azaliq.Data.Models.Models;
using Azaliq.Data.Models.Models.Enum;
using Azaliq.ViewModels.Store;
using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Cart;

namespace Azaliq.ViewModels.CartItems
{
    public class CartInputViewModel : CartIndexViewModel
    {
        [Required]
        [MaxLength(FullNameMaxLength)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(PhoneMaxLength)]
        public string Phone { get; set; } = null!;

        [Required]
        public CountryCode CountryCode { get; set; }

        [Required(ErrorMessage = "Please select a delivery option.")]
        public DeliveryOptions? DeliveryOption { get; set; }

        public List<StoreDropDownModel> Stores { get; set; } = new List<StoreDropDownModel>();

        public int? PickupStoreId { get; set; }  // Nullable to allow no selection

        [MaxLength(AddressMaxLength)]
        public string? Address { get; set; }

        [MaxLength(CityMaxLength)]
        public string? City { get; set; }

        [MaxLength(ZipCodeMaxLength)]
        public string? ZipCode { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new();

        public decimal Total => Items.Sum(i => i.Subtotal);
    }

}
