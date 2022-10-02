using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21451193_HW05.Models
{
    public class Books
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public int BookPageCount { get; set; }
        public int BookPoint { get; set; }
        public string AuthorName { get; set; }
        public string TypeName { get; set; }
        public DateTime? BookStatusTaken { get; set; }
        public DateTime? BookStatusBrought { get; set; }
        public string BookStatus { get; set; }
    }
}