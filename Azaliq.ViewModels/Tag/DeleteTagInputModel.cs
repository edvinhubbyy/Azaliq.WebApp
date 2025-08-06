namespace Azaliq.ViewModels.Tag
{
    public class DeleteTagInputModel
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<string> ProductNames { get; set; } = new List<string>();

    }
}
