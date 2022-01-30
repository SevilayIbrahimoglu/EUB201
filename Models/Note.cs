using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Note
    {
        public int ID { get; set; }
        public String noteTitle { get; set; }
        public String note { get; set; }
        public String createDate { get; set; }
        public String lastUpdateDate { get; set; }

    }
}