using System;
using System.Collections.Generic;
using System.Text;
using Task.Data.Enums;

namespace Task.ViewModels.Users
{
    public  class ManageUserViewModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public int Gender { get; set; }
    }
}
