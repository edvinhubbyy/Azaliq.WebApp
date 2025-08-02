using Azaliq.Data.Models.Models;
using Azaliq.Data.Models.Models;
using Azaliq.Data.Models.Models.Enum;
using Azaliq.ViewModels.Store;
using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Cart;

namespace Azaliq.ViewModels.CartItems
{
    public class CartCheckoutInfoInputViewModel : CartIndexViewModel
    {
        [Required]
        [MaxLength(FullNameMaxLength)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(PhoneMaxLength)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = null!;

        [Required]
        [Display(Name = "Country Code")]
        public CountryCode CountryCode { get; set; }

        [Required(ErrorMessage = "Please select a delivery option.")]
        [Display(Name = "Delivery Options")]
        public DeliveryOptions? DeliveryOption { get; set; }

        public List<StoreDropDownModel> Stores { get; set; } = new List<StoreDropDownModel>();

        public int? PickupStoreId { get; set; }  // Nullable to allow no selection

        [MaxLength(AddressMaxLength)]
        public string? Address { get; set; }

        [MaxLength(CityMaxLength)]
        public string? City { get; set; }

        [MaxLength(ZipCodeMaxLength)]
        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new();

        public decimal Total => Items.Sum(i => i.Subtotal);
    }

}
