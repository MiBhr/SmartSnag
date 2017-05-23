using SmartSnag.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSnag.Domain.Abstract
{
    public interface IWorkPackageRepository
    {
        IEnumerable<WorkPackage> WorkPackages { get; }

        bool SaveWorkPackageToDb(WorkPackage workPackage);

        bool DeleteWorkPackageFromDb(int workPackageID);
    }
}
