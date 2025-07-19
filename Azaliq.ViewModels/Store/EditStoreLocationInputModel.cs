using Azaliq.Data.Models.Models.Enum;
using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Store;

namespace Azaliq.ViewModels.Store
{
    public class EditStoreLocationInputModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Url]
        [MaxLength(GoogleMapsUrlLength)]
        public string GoogleMapsUrl { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(PhoneMaxLength)]
        public string Phone { get; set; } = null!;

        [Required]
        [EnumDataType(typeof(CountryCode))]
        public CountryCode CountryCode { get; set; }
    }
}
