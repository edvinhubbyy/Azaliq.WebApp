using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azaliq.Data.Models.Models;

namespace Azaliq.Data.Configurations
{
    public class Category
    {

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<Product> Products { get; set; }
            = new HashSet<Product>();

        public bool IsDeleted { get; set; } = false;

    }
}
