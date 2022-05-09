using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    public static class UserData
    {
        private static List<User> _testUsers;
        public static List<User> TestUsers
        {
            get {
                ResetTestUserData();
                return _testUsers;
            }
            set { }
        }
        private static void ResetTestUserData()
        {
            if (_testUsers != null) return;
            _testUsers = new List<User>(3);
            for(int i = 0; i < _testUsers.Capacity; i++)
            {
                _testUsers.Add(new User());
                _testUsers[i].username = "Marin" + i;
                _testUsers[i].password = "Password" + i;
                if(i > 0)
                {
                    _testUsers[i].facultyNumber = "12121904" + i;
                }
                if(i == 0)
                {
                    _testUsers[i].role = (int) UserRoles.ADMIN;
                    continue;
                }

                _testUsers[i].role = (int)UserRoles.STUDENT;
                _testUsers[i].created = DateTime.Now;
                _testUsers[i].activeUntil = DateTime.MaxValue;
            }
        }

        public static User isUserPassCorrect(string username, string password)
        {
            UserContext context = new UserContext();
            IEnumerable<User> matchedUsers = (from user in context.Users where user.username.Equals(username) && user.password.Equals(password) select user);
            User userToReturn = matchedUsers.DefaultIfEmpty(null).First();
            return userToReturn;
        }

        public static void setUserActiveTo(string username, DateTime activeUntil)
        {
            UserContext context = new UserContext();
            User user = (from u in context.Users where u.username.Equals(username) select u).First();
            user.activeUntil = activeUntil;
            context.SaveChanges();
            Logger.logActivity("Changed active until for user '" + username + "'");
        }

        public static void assignUserRole(string username, UserRoles role)
        {
            UserContext context = new UserContext();
            User user = (from u in context.Users where u.username.Equals(username) select u).First();
            user.role = (int) role;
            context.SaveChanges();
            Logger.logActivity("Changed role for user '" + username + "'");
        }
    }
}
