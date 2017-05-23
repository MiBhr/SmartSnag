using Ninject.Activation;
using SmartSnag.Domain.Abstract;
using SmartSnag.Domain.Concrete;
using SmartSnag.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSnag.WebUI.Controllers
{
    public class CompanyController : Controller
    {
        private ICompanyRepository repository;
        private IWorkPackageRepository repoWp;

        public CompanyController(ICompanyRepository companyRepository, IWorkPackageRepository packageRepsitory)
        {
            repository = companyRepository;
            repoWp = packageRepsitory;
        }
        public ActionResult Index()
        {
            
            return View(repository.Companies);
        }

        public ActionResult Create()
        {
            ViewBag.WorkPackage = new SelectList(repoWp.WorkPackages.Where(m => m.CompanyID == null)  , "WorkPackageID", "WorkPackageDescription");
            //ViewBag.OverCategoryID = new SelectList(db.Kategorie, "CategoryID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Company company)
        {
            //nalezy umozliwic wyswietlanie tylko tych pakietów które nie zostay jeszcze przypisane do zadnego podwykonawcy
            // ma na to na celu uniemozliwienie przypisania tego samego pakietu do róznych podwykonawców
            ViewBag.WorkPackage = new SelectList(repoWp.WorkPackages.Where(m => m.CompanyID == null), "WorkPackageID", "WorkPackageDescription");

            var wp = new WorkPackage();

            // sprawdzenie czy przekazany parametr odnosi sie do kategorii SubContractor
            if (company.CompanyCategory == 0)
            {
                
                using (var context = new EFDbContext())
                {

                    //próba odczytania z bazy danych instanacji klasy workpackage o ID równego wartoci zmiennej workPackageID przekazanej 
                    //z formularza z akcji POST
                    try
                    {
                        wp = context.WorkPackages.Where(c => c.WorkPackageID == company.WorkPackageID).First();
                        
                    }
                    // w przypadku wystapienia wyjatku, informacja o bledzie przekazana jest do modelu, a w nastepstwie 
                    // wywolanie widoku poczatkowego Create
                    catch (Exception)
                    {
                        context.Dispose();
                        ModelState.AddModelError("", "For company category Subcontractor workpackage is required");
                        return View("Create");
                    }

                    context.Dispose();
                }
            }
            //uaktualnienie parametru company przechowywujacego instacje klasy Company poprzez dodanie konkretnej instancji klasy WorkPackage
            

            if (ModelState.IsValid)
            {
                //Dla kategorii Subcontractor po dodatniu nowej instanacji klasy workpackage do tabeli Company nalezy ta sama instancje
                //usunac z tabeli WorkPackage.
                if (company.CompanyCategory == 0)
                {
                    company.WorkPackage.Add(wp);
                    repoWp.DeleteWorkPackageFromDb(wp.WorkPackageID);
                }

                TempData["Message"] = repository.SaveCompanyToDb(company) ? "Success" : "Oops";

                return RedirectToAction("Create", company);
              
            } else {
                ModelState.AddModelError("", "All fields are required");
            }
            return View("Create");
        }

        public ViewResult Edit(int companyID)
        {
            return View();
        }

        public ActionResult ResetForm()
        {
            ViewBag.WorkPackage = new SelectList(repoWp.WorkPackages.Where(m => m.CompanyID == null), "WorkPackageID", "WorkPackageDescription");
            ModelState.Clear();
            return View("Create");
        }

        public PartialViewResult List()
        {
            return PartialView(repository.Companies);
        }
    }
}