using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string facultyNumber { get; set; }
        public int role { get; set; }
        public DateTime? created { get; set; }
        public DateTime? activeUntil { get; set; }
        public int userId { get; set; }
    }
}
