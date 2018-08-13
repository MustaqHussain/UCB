using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UcbWeb.Models
{
    public class AdminMenuModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }
    }
}