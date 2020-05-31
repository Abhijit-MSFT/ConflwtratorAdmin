using System;
using System.Collections.Generic;

namespace ConflwtratorAdmin.Models
{
    public partial class NewsFeed
    {
        public int NewsId { get; set; }
        public string NewsUrl { get; set; }
        public Guid ConfId { get; set; }

        public virtual ApplicationConfiguration Conf { get; set; }
    }
}
