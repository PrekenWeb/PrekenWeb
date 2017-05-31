using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prekenweb.Website.Areas.Website.Controllers;
using Prekenweb.Website.Areas.Website.Models;
using Prekenweb.Website.Lib.Cache;

namespace Website.Tests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HomeControllerTests : ControllerTestsBase
    {
        private IMailingRepository _mailingRepository;
        private ITekstRepository _tekstRepository;
        private IPrekenRepository _prekenRepository;
        private ISpotlightRepository _spotlightRepository;
        private IPrekenwebCache _cache;

        private HomeController _homeController;

        [TestInitialize]
        public void Initialize()
        {
            _mailingRepository = new MailingRepository(Context);
            _spotlightRepository = new SpotlightRepository(Context);
            _prekenRepository = new PrekenRepository(Context);
            _tekstRepository = new TekstRepository(Context);

            _cache = new TestPrekenwebCache();
        }

        [TestMethod]
        public async Task Index_Action_Returns_Preken()
        {
            // Arrange
            _homeController = GetController(false, new HomeController(_mailingRepository, _tekstRepository, _prekenRepository, _spotlightRepository, _cache, UserManager, HuidigeGebruiker));

            // Act
            var result = (ViewResult)await _homeController.Index() ;

            // Assert
            Assert.IsNotNull(result.Model as HomeIndex, "Index view is null");
            Assert.IsTrue(((HomeIndex)result.Model).NieuwePreken.Preken.Any(), "Geen preken op homepage");
        }

        [TestCleanup]
        public void Cleanup()
        {

        }
    }
}
