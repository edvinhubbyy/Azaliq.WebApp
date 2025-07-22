namespace Azaliq.ViewModels.Archives
{
    public class ArchivedUserDetailsViewModel
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<ArchivedOrderViewModel> Orders { get; set; } = new List<ArchivedOrderViewModel>();
    }
}
