using Azaliq.Data.Models.Models.Enum;
using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Store;
using static Azaliq.GCommon.ValidationConstants.General;

namespace Azaliq.ViewModels.Store
{
    public class EditStoreLocationInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = NameRequired)]
        [StringLength(NameMaxLength, ErrorMessage = NameLengthErrorMessage)]
        [RegularExpression(StoreNameRegex, ErrorMessage = StoreNameRegexErrorMessage)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = GoogleMapsErrorMessage)]
        [Url(ErrorMessage = GoogleMapsUrlErrorMessage)]
        [StringLength(GoogleMapsUrlLength, ErrorMessage = GoogleMapsUrlLengthErrorMessage)]
        [Display(Name = GoogleMapsDisplay)]
        public string GoogleMapsUrl { get; set; } = null!;

        [Required(ErrorMessage = AddressErrorMessage)]
        [StringLength(AddressMaxLength, ErrorMessage = AddressErrorMessageErrorMessage)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = PhoneErrorMessage)]
        [RegularExpression(PhoneNumberRegex, ErrorMessage = PhoneRegexErrorMessage)]
        [Display(Name = PhoneDisplay)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = CountryCodeErrorMessage)]
        [Display(Name = CountryCodeDisplay)]
        public CountryCode CountryCode { get; set; }
    }
}
