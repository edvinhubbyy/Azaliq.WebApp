using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.Category;

namespace Azaliq.ViewModels.Category
{
    public class AddCategoryViewModel
    {
        [Required]
        [StringLength(50)]
        [RegularExpression(CategoryNameRegex, ErrorMessage = CategoryNameRegexErrorMessage)]
        public string Name { get; set; } = null!;
    }
}
