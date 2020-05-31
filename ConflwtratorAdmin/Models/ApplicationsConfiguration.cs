using System;
using System.ComponentModel.DataAnnotations;

namespace ConflwtratorAdmin.Models
{
    public class ApplicationsConfiguration
    {
        [Key]
        public Guid ConfID { get; set; }
        public string UserName { get; set; }
        public string UserEmailID { get; set; }
        public string AppDisplayName { get; set; }
        public string AppDescription { get; set; }
        public string? AppStatus { get; set; } // new
        public string? AppBannerURL { get; set; }
        public string? LogoURL { get; set; }
        public string? Theme { get; set; }
        public string? FontColor { get; set; }
        public string? FontFamily { get; set; }
        public Guid? WeatherApp { get; set; }
        public Guid? Payslips { get; set; }
        public string? PayslipsLogoURL { get; set; }
        public Guid? Forms { get; set; }
        public string? FormsLogoURL { get; set; }
        public Guid? Leads { get; set; }
        public string? LeadsLogoURL { get; set; }
        public Guid? Career { get; set; }
        public string? CareerLogoURL { get; set; }
        public Guid? Discounts { get; set; }
        public string? DiscountsLogoURL { get; set; }
        public Guid? Kudos { get; set; }
        public string? KudosLogoURL { get; set; }
        public string? NewsFeedOne { get; set; }
        public string? NewsFeedTwo { get; set; }
        public string? NewsFeedThree { get; set; }
        public string? NewsFeedFour { get; set; }
        public string? NewsFeedFive { get; set; }
    }
}
