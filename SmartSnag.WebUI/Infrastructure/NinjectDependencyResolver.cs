﻿using Ninject;
using SmartSnag.Domain.Abstract;
using SmartSnag.Domain.Concrete;
using SmartSnag.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSnag.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void AddBindings()
        {
            kernel.Bind<ICompanyRepository>().To<EfCompanyRepository>();
            kernel.Bind<IWorkPackageRepository>().To<EfWorkPackageRepository>();
        }
    }
}