using Azaliq.Data.Models.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azaliq.GCommon.ValidationConstants.Store;

namespace Azaliq.ViewModels.Store
{
    public class CreateStoreLocationInputModel
    {
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
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Please select a country code")]
        public CountryCode CountryCode { get; set; }
    }

}
