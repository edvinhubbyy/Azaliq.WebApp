namespace Azaliq.ViewModels.Archives
{
    public class ArchivedUserListItemViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime ArchivedOn { get; set; }
    }
}
