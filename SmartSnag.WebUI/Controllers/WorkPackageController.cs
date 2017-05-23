using SmartSnag.Domain.Abstract;
using SmartSnag.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSnag.WebUI.Controllers
{
    public class WorkPackageController : Controller
    {
        private IWorkPackageRepository repository;
        public WorkPackageController(IWorkPackageRepository workPackageRepository)
        {
            repository = workPackageRepository;
        }
        
        public ActionResult Index()
        {
            return View(repository.WorkPackages);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WorkPackage workPackage)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = repository.SaveWorkPackageToDb(workPackage) ? "Success" : "Oops";

                return RedirectToAction("Create", workPackage);

            }
            else
            {
                ModelState.AddModelError("", "All fields are required");
            }
            return View("Create");
        }


        public ActionResult Edit(int workPackageID = 1)
        {

            WorkPackage workPackage = repository.WorkPackages.FirstOrDefault(m => m.WorkPackageID == workPackageID);
            return View(workPackage);
          
        }

        [HttpPost]
        public ActionResult Edit(WorkPackage workPackage)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = repository.SaveWorkPackageToDb(workPackage) ? "Success" : "Oops";
                return View("Edit", workPackage);
            }
            else
            {
                ModelState.AddModelError("", "All fields are required");
            }
            return View("Edit");
        }

        public ActionResult Delete(int workPackageID)
        {
            repository.DeleteWorkPackageFromDb(workPackageID);
                return RedirectToAction("Edit");
        }

        public PartialViewResult List()
        {
            return PartialView(repository.WorkPackages.OrderBy(m => m.WorkPackageCode));
        }

        public ActionResult ResetForm()
        {
            ModelState.Clear();
            return View("Create");
        }

    }
}
