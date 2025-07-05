using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string? Phone { get; set; }
    }
}
