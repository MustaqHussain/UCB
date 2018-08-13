using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UcbWeb.Models;

namespace UcbWeb.ViewModels
{
    public class StaffNominatedManagerSearchVM
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsNominatedManager { get; set; }

        public bool IsDeputyNominatedManager { get; set; }

        public List<StaffModel> MatchList { get; set; }

        public string Message { get; set; }
    }
}