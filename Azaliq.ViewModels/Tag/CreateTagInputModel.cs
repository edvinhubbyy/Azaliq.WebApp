using System.ComponentModel.DataAnnotations;
using static Azaliq.GCommon.ValidationConstants.ProductTag;

namespace Azaliq.ViewModels.Tag
{
    public class CreateTagInputModel : TagBase
    {

        [Required(ErrorMessage = TagRequiredErrorMessage)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = TagErrorMessage)]
        public string Name { get; set; } = null!;
        
    }
}
