using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystem_Finance_Department_.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string CurrentSO { get; set; }
        public string PreviousSO { get; set; }
        public string Dateofjoining { get; set; }
    }
}