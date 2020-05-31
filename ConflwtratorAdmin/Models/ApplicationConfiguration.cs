using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConflwtratorAdmin.Models
{
    public partial class ApplicationConfiguration
    {
        public ApplicationConfiguration()
        {
            LoBapplicationDetails = new List<LoBapplicationDetails>();
            NewsFeed = new List<NewsFeed>();
        }

        public Guid ConfId { get; set; }
        public string UserName { get; set; }
        public string UserEmailId { get; set; }
        public string AppDisplayName { get; set; }
        public string AppDescription { get; set; }
        public bool AppStatus { get; set; }
        [NotMapped]
        public IFormFile AppBanner { get; set; }
        public string AppBannerUrl { get; set; }
        [NotMapped]
        public IFormFile AppLogo { get; set; }
        public string AppLogoUrl { get; set; }
        public string FontColor { get; set; }
        public string FontFamily { get; set; }
        public bool IsWeatherApp { get; set; }
        public bool IsNewsFeedEnabled { get; set; }

        public virtual List<LoBapplicationDetails> LoBapplicationDetails { get; set; }
        public virtual List<NewsFeed> NewsFeed { get; set; }
    }
}
