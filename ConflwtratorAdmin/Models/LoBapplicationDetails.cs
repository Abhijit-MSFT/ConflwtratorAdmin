using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ConflwtratorAdmin.Models
{
    public partial class LoBapplicationDetails
    {
        public int AppId { get; set; }
        public Guid TeamsAppId { get; set; }
        public string AppName { get; set; }
        [NotMapped]
        public IFormFile AppLogo { get; set; }
        public string AppDescription { get; set; }
        public string AppLogoUrl { get; set; }
        public string AppPreviewUrl { get; set; }
        public Guid ConfId { get; set; }

        public virtual ApplicationConfiguration Conf { get; set; }
    }
}
