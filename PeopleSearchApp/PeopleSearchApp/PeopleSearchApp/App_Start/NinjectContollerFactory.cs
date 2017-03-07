using Ninject;
using PeopleSearchApp.Models;
using PeopleSearchApp.Repository;
using System;
using System.Web.Mvc;

namespace PeopleSearchApp.App_Start
{
      public class NinjectControllerFactory : DefaultControllerFactory
        {
            public IKernel Kernel { get; private set; }

            public NinjectControllerFactory()
            {
                this.Kernel = new StandardKernel();
                AddBindings();
            }

            protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
            {
                IController controller = null;

                if (controllerType != null)
                    controller = (IController)Kernel.Get(controllerType);

                return controller;
            }

            private void AddBindings()
            {
                Kernel.Bind<IRepository<User>>().To<Repository<User>> ();
            }
        }
    }
