using System.Web.Mvc;
using Pbl.Controllers;
// <copyright file="ControleAlunosControllerFactory.cs">Copyright ©  2017</copyright>

using System;
using Microsoft.Pex.Framework;

namespace Pbl.Controllers
{
    /// <summary>A factory for Pbl.Controllers.ControleAlunosController instances</summary>
    public static partial class ControleAlunosControllerFactory
    {
        /// <summary>A factory for Pbl.Controllers.ControleAlunosController instances</summary>
        [PexFactoryMethod(typeof(ControleAlunosController))]
        public static ControleAlunosController Create(
            IDependencyResolver value_iDependencyResolver,
            IActionInvoker value_iActionInvoker,
            ITempDataProvider value_iTempDataProvider,
            UrlHelper value_urlHelper,
            ViewEngineCollection value_viewEngineCollection,
            ControllerContext value_controllerContext,
            TempDataDictionary value_tempDataDictionary,
            bool value_b,
            IValueProvider value_iValueProvider,
            ViewDataDictionary value_viewDataDictionary
        )
        {
            ControleAlunosController controleAlunosController
               = new ControleAlunosController();
            ((Controller)controleAlunosController).Resolver = value_iDependencyResolver;
            ((Controller)controleAlunosController).ActionInvoker = value_iActionInvoker;
            ((Controller)controleAlunosController).TempDataProvider =
              value_iTempDataProvider;
            ((Controller)controleAlunosController).Url = value_urlHelper;
            ((Controller)controleAlunosController).ViewEngineCollection =
              value_viewEngineCollection;
            ((ControllerBase)controleAlunosController).ControllerContext =
              value_controllerContext;
            ((ControllerBase)controleAlunosController).TempData = value_tempDataDictionary;
            ((ControllerBase)controleAlunosController).ValidateRequest = value_b;
            ((ControllerBase)controleAlunosController).ValueProvider = value_iValueProvider;
            ((ControllerBase)controleAlunosController).ViewData = value_viewDataDictionary;
            return controleAlunosController;

            // TODO: Edit factory method of ControleAlunosController
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
