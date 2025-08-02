using Azaliq.Data.Models.Models.Enum;
using Azaliq.ViewModels.Store;
using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Cart;
using static Azaliq.GCommon.ValidationConstants.General;

namespace Azaliq.ViewModels.CartItems
{
    public class CartCheckoutInfoInputViewModel : CartIndexViewModel
    {
        [Required]
        [MaxLength(FullNameMaxLength)]
        [RegularExpression(FullNameRegex)]
        [Display(Name = FullNameDisplay)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLength)]
        [RegularExpression(EmailRegex, ErrorMessage = EmailRegexErrorMessage)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(PhoneMaxLength)]
        [Display(Name = PhoneDisplay)]
        [RegularExpression(PhoneRegex, ErrorMessage = PhoneRegexErrorMessage)]
        public string Phone { get; set; } = null!;

        [Required]
        public CountryCode CountryCode { get; set; }

        [Required(ErrorMessage = DeliveryOptionRequiredErrorMessage)]
        [Display(Name = DeliveryOptionDisplayName)]
        public DeliveryOptions? DeliveryOption { get; set; }

        public List<StoreDropDownModel> Stores { get; set; } 
            = new List<StoreDropDownModel>();

        public int? PickupStoreId { get; set; }  // Nullable to allow no selection

        [MaxLength(AddressMaxLength)]
        [RegularExpression(AddressRegex, ErrorMessage = AddressRegexErrorMessage)]
        public string? Address { get; set; }

        [MaxLength(CityMaxLength)]
        [RegularExpression(CityRegex, ErrorMessage = CityRegexErrorMessage)]
        public string? City { get; set; }

        [MaxLength(ZipCodeMaxLength)]
        [Display(Name = ZipCodeDisplay)]
        [RegularExpression(ZipCodeRegex, ErrorMessage = ZipCodeRegexErrorMessage)]
        public string? ZipCode { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new();

        public decimal Total => Items.Sum(i => i.Subtotal);
    }

}
