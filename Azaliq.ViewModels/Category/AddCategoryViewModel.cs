﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azaliq.ViewModels.Category
{
    public class AddCategoryViewModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
    }
}
