using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartSnag.Domain.Entities
{
    [MetadataType(typeof(WorkPackageMetadata))]
    [Table("WorkPackage")]
    public partial class WorkPackage
    {

        public int WorkPackageID { get; set; }      
        public int? WorkPackageCode { get; set; }
        public string WorkPackageDescription { get; set; }
        public int? CompanyID { get; set; }
        public virtual Company Company { get; set; }
    }

    public class WorkPackageMetadata
    {
        [Key]
        [Display(Name = "Work Package ID")]
        public int? WorkPackageID { get; set; }
        [Display(Name = "Work package code")]
        [Required(ErrorMessage ="Package code is required")]
        public int WorkPackageCode { get; set; }
        [Display(Name ="Package description")]
        [Required(ErrorMessage ="Package description is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage ="{0} must have {2}-{1} characters")]
        public string WorkPackageDescription { get; set; }
        public int? CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        
        public virtual Company Company { get; set; }


    }

}
