namespace Azaliq.Data.Models.Models
{
    public class ArchivedUser
    {
        public Guid Id { get; set; }
        public string OriginalUserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime ArchivedOn { get; set; }

        public ICollection<ArchivedOrder> Orders { get; set; } = new List<ArchivedOrder>();
    }
}
