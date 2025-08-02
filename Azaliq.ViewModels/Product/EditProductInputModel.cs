using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Product;

namespace Azaliq.ViewModels.Product
{
    public class EditProductInputModel
    {

        [Required]
        public int Id { get; set; } // Needed to identify which product to update

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        [RegularExpression(NameRegex, ErrorMessage = NameRegexErrorMessage)]
        public string Name { get; set; } = null!;

        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = DescriptionLengthErrorMessage)]
        [RegularExpression(DescriptionRegex, ErrorMessage = DescriptionLengthErrorMessage)]
        public string? Description { get; set; }


        [Display(Name = ImageUrlDisplay)]
        [RegularExpression(ImageUrlRegex, 
            ErrorMessage = ImageUrlRegexErrorMessage)]
        public string? ImageUrl { get; set; }


        [Range(0.01, 100_000, ErrorMessage = PriceErrorMessage)]
        [RegularExpression(PriceRegex, ErrorMessage = PriceRegexErrorMessage)]
        public decimal Price { get; set; }


        [Required(ErrorMessage = QuantityRequiredError)]
        [Range(QuantityMinValue, QuantityMaxValue, ErrorMessage = QuantityErrorMessage)]
        public int Quantity { get; set; }


        [Display(Name = SameDayDeliveryRequiredDisplay)]
        public bool IsSameDayDeliveryAvailable { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CreateProductDropDownCategory>? Categories { get; set; }

        [Display(Name = TagsDisplayName)]
        public List<string>? SelectedTags { get; set; }

        [Display(Name = AllTagsDisplayName)]
        public IEnumerable<string> AllTags { get; set; } 
            = new HashSet<string>();
    }
}
