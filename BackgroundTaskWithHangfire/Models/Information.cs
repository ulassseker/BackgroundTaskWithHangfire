using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundTaskWithHangfire.Models
{
    public class Information
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string filename { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public double price { get; set; }
        public int rating { get; set; }
    }
}
