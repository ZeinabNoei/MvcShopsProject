using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
   public  class RolesMetaData
    {
        [Key]
        public int RoleID { get; set; }

        [Display(Name = "Role Title")]
        [Required(ErrorMessage = "Please enter {0}")]
        public string RoleTitle { get; set; }

        [Display(Name = "Role system Title")]
        [Required(ErrorMessage = "Please enter {0}")]
        public string RoleName { get; set; }

    }
    //[MetadataType(typeof(RolesMetaData))]
    //public partial class Roles { }
}
