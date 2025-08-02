using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azaliq.GCommon.ValidationConstants.ProductTag;

namespace Azaliq.ViewModels.Tag
{
    public class TagEditInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = TagRequiredErrorMessage)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = TagErrorMessage)]
        public string Name { get; set; } = null!;
        
    }

}
