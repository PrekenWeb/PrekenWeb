using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Moq;
using Prekenweb.Models;
using Prekenweb.Models.Identity;
using Prekenweb.Models.TestData;
using PrekenWeb.Security;
using Prekenweb.Website.Controllers;

namespace Website.UnitTests.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected IPrekenwebContext<Gebruiker> Context;
        protected IPrekenWebUserManager UserManager;
        protected IHuidigeGebruiker HuidigeGebruiker;

        protected ControllerTestsBase()
        {
            SetupDbContext();
            SetupUserManager();
            SetupHuidigeGebruiker();
        }

        private void SetupDbContext()
        {
            Context = new TestPrekenwebContext<Gebruiker>();

            var testDataProvider = new TestDataProvider();
            testDataProvider.Provision(Context);
        }

        private void SetupUserManager()
        {
            var userManagerMock = new Mock<IPrekenWebUserManager>();

            UserManager = userManagerMock.Object;
        }

        private void SetupHuidigeGebruiker()
        {
            var huidigeGebruikerMock = new Mock<IHuidigeGebruiker>();
            huidigeGebruikerMock.Setup(x => x.GetId(It.IsAny<IPrekenWebUserManager>(), It.IsAny<IPrincipal>())).Returns(Task.FromResult( TestDataProvider.TestGebruiker1.Id));
            HuidigeGebruiker = huidigeGebruikerMock.Object;
        }

        protected T GetController<T>(bool authenticated, T controller) where T : ApplicationController
        {
            var controllerContextMock = new Mock<ControllerContext>();
            var httpContextMock = new Mock<HttpContextBase>();

            if (authenticated)
            {
                controllerContextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(TestDataProvider.TestGebruiker1.Email);
                controllerContextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
                httpContextMock.Setup(context => context.User).Returns(new GenericPrincipal(new GenericIdentity(TestDataProvider.TestGebruiker1.Email), null));
            }
            else
            {
                controllerContextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns((string)null);
                controllerContextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(false);
                httpContextMock.Setup(context => context.User).Returns(new GenericPrincipal(new GenericIdentity(string.Empty), new string[0]));
            }

            httpContextMock.Setup(context => context.Cache).Returns(HttpRuntime.Cache);
            controllerContextMock.SetupGet(ctx => ctx.HttpContext).Returns(httpContextMock.Object);

            controller.ControllerContext = controllerContextMock.Object;

            return controller;
        }
    }
}