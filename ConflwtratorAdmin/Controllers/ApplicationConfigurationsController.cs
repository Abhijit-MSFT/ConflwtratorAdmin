using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConflwtratorAdmin.DBContext;
using ConflwtratorAdmin.Models;
using ConflwtratorAdmin.Helper;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ConflwtratorAdmin.Controllers
{
    public class ApplicationConfigurationsController : Controller
    {
        private readonly ConflwTratorContext _context;
        private readonly IConfiguration _config;

        //[Obsolete]
        //private readonly IHostingEnvironment hostingEnvironment;

        [Obsolete]
        public ApplicationConfigurationsController(ConflwTratorContext context, IConfiguration config, IHostingEnvironment environment)
        {
            _context = context;
            _config = config;
        }

        // GET: ApplicationConfigurations
        public IActionResult Index()
         {
            var model = new ApplicationConfiguration();
            model.LoBapplicationDetails = new System.Collections.Generic.List<LoBapplicationDetails>() {
            new LoBapplicationDetails(){ AppName ="Payslips"},
            new LoBapplicationDetails(){ AppName ="Forms"},
            new LoBapplicationDetails(){ AppName ="Leads"},
            new LoBapplicationDetails(){ AppName ="Career"},
            new LoBapplicationDetails(){ AppName ="Discounts"},
            new LoBapplicationDetails(){ AppName ="Kudos"},
            new LoBapplicationDetails(){ AppName ="Benefits"}
            };
            return View(model);
        }

        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> IndexAsync(ApplicationConfiguration app)
        {
            try
            {
                string BannerImageURL = app.AppBanner.FileName != null ? await BlobStorageHelper.GetImageUrl(await GetFilePath(app.AppBanner)) : null;
                string LogoImageURl = app.AppLogo != null ? await BlobStorageHelper.GetImageUrl(await GetFilePath(app.AppLogo)) : null;
                foreach (var item in app.LoBapplicationDetails)
                {
                    item.TeamsAppId = Guid.Parse(_config[item.AppName]);
                    //item.AppName = i.AppName;
                    item.AppDescription = item.AppName;
                    item.AppLogoUrl = await BlobStorageHelper.GetImageUrl(await GetFilePath(item.AppLogo));
                }
                foreach(var item in app.NewsFeed)
                {
                    item.NewsUrl = item.NewsUrl;
                }
                string userMailID = User.Identity.Name;
                string userName = userMailID.Substring(0, (userMailID.IndexOf('@')));
                GetAppConfiguration(userMailID, _context);
                
                var apConfig = new ApplicationConfiguration
                {
                    UserName = userName,
                    UserEmailId = userMailID,
                    AppDisplayName = app.AppDisplayName,
                    AppDescription = app.AppDescription,
                    AppStatus = true,
                    AppBannerUrl = BannerImageURL,
                    AppLogoUrl = LogoImageURl,
                    FontColor = app.FontColor,
                    FontFamily = app.FontFamily,
                    IsWeatherApp = false,
                    IsNewsFeedEnabled = false,
                    LoBapplicationDetails = app.LoBapplicationDetails,
                    NewsFeed = app.NewsFeed
                    

                };

                _context.ApplicationConfiguration.Add(apConfig);
                _context.SaveChanges();

                ViewBag.Message = string.Format("Your data is recorded !");

                var model = new ApplicationConfiguration();
                    model.LoBapplicationDetails = new System.Collections.Generic.List<LoBapplicationDetails>() {
                        new LoBapplicationDetails(){ AppName ="Payslips"},
                        new LoBapplicationDetails(){ AppName ="Forms"},
                        new LoBapplicationDetails(){ AppName ="Leads"},
                        new LoBapplicationDetails(){ AppName ="Career"},
                        new LoBapplicationDetails(){ AppName ="Discounts"},
                        new LoBapplicationDetails(){ AppName ="Kudos"},
                        new LoBapplicationDetails(){ AppName ="Benefits"}
                        };
                return View(model);
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

        //[AllowAnonymous]
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public static void GetAppConfiguration(string emailId, ConflwTratorContext context)
        {
            //if (context.ApplicationConfiguration.Local.Count > 0)
            //{
            var appConfig = context.ApplicationConfiguration.Where(a => a.UserEmailId == emailId && a.AppStatus == true).First();
            if (appConfig != null)
            {
                appConfig.AppStatus = false;
                context.Entry(appConfig).State = EntityState.Modified;
                context.SaveChanges();

            }
            //}
        }

        [Obsolete]
        private async Task<string> GetFilePath(IFormFile file)
        {
            try
            {
                var uploads = Path.Combine(_config["BaseUri"], "uploads");
                var filePath = @"wwwroot/uploads/" + GetUniqueFileName(file.FileName);
                using (var fileStream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(fileStream);
                }

                return filePath;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
