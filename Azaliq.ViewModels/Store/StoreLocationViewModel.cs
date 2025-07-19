using Azaliq.Data.Models.Models.Enum;

namespace Azaliq.ViewModels.Store
{
    public class StoreLocationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string GoogleMapsUrl { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Phone { get; set; }

        public CountryCode CountryCode { get; set; }
    }
}
