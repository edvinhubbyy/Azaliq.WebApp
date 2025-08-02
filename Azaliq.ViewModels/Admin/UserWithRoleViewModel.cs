namespace Azaliq.ViewModels.Admin
{
    public class UserWithRoleViewModel
    {
        public string Id { get; set; } = null!;
        
        public string Email { get; set; } = null!;
        
        public string UserName { get; set; } = null!;
        
        public bool IsManager { get; set; }

        public bool IsBanned { get; set; }
    }

}
