using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.ViewModels.Review
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        
        public string UserName { get; set; } = null!;
        
        public string Comment { get; set; } = null!;
        
        public int Rating { get; set; }
        
        public DateTime CreatedOn { get; set; }
    }
}
