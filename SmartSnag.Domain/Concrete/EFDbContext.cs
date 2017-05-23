using SmartSnag.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSnag.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() :base("DefectDbConnection") { }
        public DbSet<Company> Companies { get; set; }
        public DbSet<WorkPackage> WorkPackages { get; set; }
    }
}
