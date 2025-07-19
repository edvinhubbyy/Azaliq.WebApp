using Azaliq.Data.Models.Models.Enum;
using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Cart;

namespace Azaliq.ViewModels.CartItems
{
    public class CartInputViewModel : CartIndexViewModel
    {

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(PhoneMaxLength)]
        public string Phone { get; set; } = null!;

        [Required]
        public CountryCode CountryCode { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(CityMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(ZipCodeMaxLength)]
        public string ZipCode { get; set; } = null!;

        public List<CartItemViewModel> Items { get; set; } = new();

        public decimal Total => Items.Sum(i => i.Subtotal);


    }
}
