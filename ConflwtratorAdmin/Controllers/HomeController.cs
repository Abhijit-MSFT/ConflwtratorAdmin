using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConflwtratorAdmin.Models;
using ConflwtratorAdmin.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using ConflwtratorAdmin.Helper;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConflwtratorAdmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationConfigurationContext _context;
        private readonly string weatherAppID;
        private readonly string payslipAppID;
        private readonly string formsAppID;
        private readonly string careerAppID;
        private readonly string discountsAppID;
        private readonly string kudosAppID;
        private readonly string benefitsAppID;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, ApplicationConfigurationContext context)
        {
            _logger = logger;
            _context = context;
            weatherAppID = config["Weather"];
            payslipAppID = config["Payslips"];
            formsAppID = config["Forms"];
            careerAppID = config["Career"];
            discountsAppID = config["Discounts"];
            kudosAppID = config["Kudos"];
            benefitsAppID = config["Benefits"];
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(ApplicationsConfiguration app)
        {
            try
            {
                string BannerImageURL = await BlobStorageHelper.GetImageUrl(app.AppBannerURL);
                string LogoImageURl = await BlobStorageHelper.GetImageUrl(app.LogoURL);
                string PayslipsImageURL = await BlobStorageHelper.GetImageUrl(app.PayslipsLogoURL);
                string FormsImageURL = await BlobStorageHelper.GetImageUrl(app.FormsLogoURL);
                string LeadsImageURL = await BlobStorageHelper.GetImageUrl(app.LeadsLogoURL);
                string CareerImageURL = await BlobStorageHelper.GetImageUrl(app.CareerLogoURL);
                string DiscountsImageURL = await BlobStorageHelper.GetImageUrl(app.DiscountsLogoURL);
                string KudosImageURL = await BlobStorageHelper.GetImageUrl(app.KudosLogoURL);
                string userMailID = User.Identity.Name;
                string userName = userMailID.Substring(0, (userMailID.IndexOf('@')));
                GetAppConfiguration(userMailID, _context);
                var apConfig = new ApplicationsConfiguration
                {
                    UserName = userName,
                    UserEmailID = userMailID,
                    AppDisplayName = app.AppDisplayName,
                    AppDescription = app.AppDescription,
                    AppStatus = "Active",                
                    AppBannerURL = BannerImageURL,
                    LogoURL = LogoImageURl,
                    Theme = app.Theme,
                    FontColor = app.FontColor,
                    FontFamily = app.FontFamily,
                    WeatherApp = Guid.Parse(weatherAppID),
                    Payslips = Guid.Parse(payslipAppID),
                    PayslipsLogoURL = PayslipsImageURL,
                    Forms = Guid.Parse(formsAppID),
                    FormsLogoURL = FormsImageURL,
                    Leads = Guid.Parse(careerAppID),
                    LeadsLogoURL = LeadsImageURL,
                    Career = Guid.Parse(discountsAppID),
                    CareerLogoURL = CareerImageURL,
                    Discounts = Guid.Parse(discountsAppID),
                    DiscountsLogoURL = DiscountsImageURL,
                    Kudos = Guid.Parse(discountsAppID),
                    KudosLogoURL = KudosImageURL,
                    NewsFeedOne = app.NewsFeedOne,
                    NewsFeedTwo = app.NewsFeedTwo,
                    NewsFeedThree = app.NewsFeedThree,
                    NewsFeedFour = app.NewsFeedFour,
                    NewsFeedFive = app.NewsFeedFive
                };
                
                _context.AppConfiguration.Add(apConfig);
                _context.SaveChanges();
                
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static void GetAppConfiguration(string emailId, ApplicationConfigurationContext context )
        {
            var appConfig = context.AppConfiguration.Where(a => a.UserEmailID == emailId && a.AppStatus == "Active").First();
            if (appConfig != null)
            {
                appConfig.AppStatus = "Inactive";
                context.Entry(appConfig).State = EntityState.Modified;
                context.SaveChanges();

            }

        }
    }
}
