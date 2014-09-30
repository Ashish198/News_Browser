using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsBrowser.Models
{
    public class CategoryProperty
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
    }
}