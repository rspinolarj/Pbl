using System.Web.Mvc;
// <copyright file="ControleDisciplinasControllerTest.cs">Copyright ©  2017</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pbl.Controllers;

namespace Pbl.Controllers.Tests
{
    [TestClass]
    [PexClass(typeof(ControleDisciplinasController))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ControleDisciplinasControllerTest
    {

        [PexMethod(MaxBranches = 20000, MaxConditions = 1000)]
        public ActionResult Index([PexAssumeUnderTest]ControleDisciplinasController target)
        {
            ActionResult result = target.Index();
            return result;
            // TODO: adicionar declarações para método ControleDisciplinasControllerTest.Index(ControleDisciplinasController)
        }
    }
}
