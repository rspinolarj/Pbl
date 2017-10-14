using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pbl.Controllers;

namespace PblTests.Controllers
{
    [TestClass]
    public class AcompanhamentoMedsControllerTests
    {
        private MockRepository mockRepository;



        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void TestMethod1()
        {


            AcompanhamentoMedsController acompanhamentoMedsController = this.CreateAcompanhamentoMedsController();


        }

        private AcompanhamentoMedsController CreateAcompanhamentoMedsController()
        {
            return new AcompanhamentoMedsController();
        }
    }
}