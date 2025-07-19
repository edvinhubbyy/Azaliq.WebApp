namespace Azaliq.Data.Models.Models
{
    public class Review
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; } = false;
    }

}
