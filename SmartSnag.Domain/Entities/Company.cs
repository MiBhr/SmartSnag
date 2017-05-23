using SmartSnag.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartSnag.Domain.Entities
{
    [MetadataType(typeof(CompanyMetaData))]
    [Table("Company")]
    public partial class Company
    {
        private ICollection<WorkPackage> _workpackage;
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public CompanyCategory? CompanyCategory { get; set; }

        public int? WorkPackageID { get; set; }
        public virtual ICollection<WorkPackage> WorkPackage
        {
            get
            {
                return this._workpackage ?? (this._workpackage = new HashSet<WorkPackage>());
            }
        }
    }

    public class CompanyMetaData
    {
        [Key]
        [ScaffoldColumn(false)]
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company category is required")]
        [Display(Name = "Company category")]
        public CompanyCategory? CompanyCategory { get; set; }
        [NotMapped]
        public int? WorkPackageID { get; set; }

        public virtual ICollection<WorkPackage> WorkPackage { get; set; }
    }

    public enum CompanyCategory
    {
        SubContractor,
        DesignTeam,
        Client,
        PrincipalContractor
    }
}
