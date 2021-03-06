using System.Web.Mvc;
// <copyright file="ControleAlunosControllerTest.cs">Copyright ©  2017</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pbl.Controllers;

namespace Pbl.Controllers.Tests
{
    [TestClass]
    [PexClass(typeof(ControleAlunosController))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ControleAlunosControllerTest
    {

        [PexMethod(MaxBranches = 20000)]
        public ActionResult Update(
            [PexAssumeUnderTest]ControleAlunosController target,
            int idAluno,
            string nomeAluno,
            string cpfAluno,
            string matriculaAluno
        )
        {
            ActionResult result = target.Update(idAluno, nomeAluno, cpfAluno, matriculaAluno);
            return result;
            // TODO: adicionar declarações para método ControleAlunosControllerTest.Update(ControleAlunosController, Int32, String, String, String)
        }

        [PexMethod(MaxConstraintSolverTime = 2)]
        public ActionResult Index([PexAssumeUnderTest]ControleAlunosController target)
        {
            ActionResult result = target.Index();
            return result;
            // TODO: adicionar declarações para método ControleAlunosControllerTest.Index(ControleAlunosController)
        }

        [PexMethod(MaxConditions = 1000)]
        public ActionResult Create([PexAssumeUnderTest]ControleAlunosController target)
        {
            ActionResult result = target.Create();
            return result;
            // TODO: adicionar declarações para método ControleAlunosControllerTest.Create(ControleAlunosController)
        }
    }
}
