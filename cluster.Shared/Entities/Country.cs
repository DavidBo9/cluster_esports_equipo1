﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cluster.Shared.Entities
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(80, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "País")]
        public string Name { get; set; }
    }
}
