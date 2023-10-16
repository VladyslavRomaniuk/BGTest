using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BgLocal.Models {
    public class ApplicationUser : IdentityUser {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Birth date")]
        public DateOnly BirthDate { get; set; }

        [Required]
        public string Adress { get; set; }
    }
}
