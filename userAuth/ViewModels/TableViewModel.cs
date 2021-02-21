using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using userAuth.Models;

namespace userAuth.ViewModels
{
    public class TableViewModel
    {
        public string Email { get; set; }
        public string LastLog { get; set; }
        public string RegTime { get; set; }
        public string UserName { get; set; }
        public string Id { get; set; }
        public bool IsChecked { get; set; }
        public string Status { get; set; }
        public TableViewModel()
        {
        }
        public TableViewModel(User el)
        {
            Email = el.Email;
            LastLog = el.LastLog.ToString( "s" );
            RegTime = el.RegTime.ToString( "s" );
            Id = el.Id;
            IsChecked = false;
            Status = el.Blocked ? "Blocked" : "Unblocked";
            UserName = el.UserName;
        }
    }
}
