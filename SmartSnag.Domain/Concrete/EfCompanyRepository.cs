using SmartSnag.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSnag.Domain.Entities;


namespace SmartSnag.Domain.Concrete
{
    public class EfCompanyRepository : ICompanyRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Company> Companies
        {
            get { return context.Companies; }
        }

        public bool SaveCompanyToDb(Company company)
        {
            Company dbEntry = context.Companies
                .FirstOrDefault(c => c.CompanyName == company.CompanyName);

            if (dbEntry != null)
            {
                return false;
            }
                context.Companies.Add(company);
                context.SaveChanges();
                return true;
        }
    }
}
