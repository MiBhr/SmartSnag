using SmartSnag.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSnag.Domain.Entities;

namespace SmartSnag.Domain.Concrete
{
    public class EfWorkPackageRepository : IWorkPackageRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<WorkPackage> WorkPackages
        {
            get  {return context.WorkPackages; }
        }

        public bool SaveWorkPackageToDb(WorkPackage workPackage)
        {
            WorkPackage dbEntry = context.WorkPackages.FirstOrDefault(m => m.WorkPackageID == workPackage.WorkPackageID);

          
            if (dbEntry != null)
            {   // sprawdzenie czy w bazie danych nie ma juz wpisu o takim workpackCode
                if (dbEntry.WorkPackageCode == workPackage.WorkPackageCode)
                    return false;
                dbEntry.WorkPackageCode = workPackage.WorkPackageCode;
                dbEntry.WorkPackageDescription = workPackage.WorkPackageDescription;
                context.SaveChanges();
                return true;
            }
            try
            {
                context.WorkPackages.Add(workPackage);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
  
        }

        public bool DeleteWorkPackageFromDb(int workPackageID)
        {
            WorkPackage dbEntry = context.WorkPackages.Find(workPackageID);

            try
            {
                context.WorkPackages.Remove(dbEntry);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

            
        }

      

        
    }
}
