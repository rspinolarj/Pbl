using AutoMoq;
using Moq;
using NUnit.Framework;
using Pbl.Controllers;

namespace Pbl.Controllers
{
    [TestFixture]
    public class ControleAlunosControllerTests
    {
        [Test]
        public void TestMethod1()
        {
            var mocker = new AutoMoqer();



            var controleAlunosController = mocker.Create<ControleAlunosController>();


        }
    }
}