using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    internal class LoginValidation
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        public static string currentUserUsername { get; private set; }
        public static UserRoles currentUserRole { get; private set; }
        private ActionOnError _actionOnError;
        public delegate void ActionOnError(string errorMsg);

        public LoginValidation (string username, string password, ActionOnError actionOnError)
        {
            _username = username;
            _password = password;
            _actionOnError = actionOnError;
        }
        public bool ValidateUserInput(ref User user)
        {
            if(_username.Equals(String.Empty))
            {
                _errorMessage = "The username entered is empty.";
                _actionOnError(_errorMessage);
                return false;
            }

            if(_password.Equals(String.Empty))
            {
                _errorMessage = "The password entered is empty.";
                _actionOnError(_errorMessage);
                return false;
            }

            if(_username.Length < 5)
            {
                _errorMessage = "The username entered is less than 5 characters.";
                _actionOnError(_errorMessage);
                return false;
            }

            if (_password.Length < 5)
            {
                _errorMessage = "The password entered is less than 5 characters.";
                _actionOnError(_errorMessage);
                return false;
            }

            User checkUser = UserData.isUserPassCorrect(_username, _password);
            if (checkUser != null)
            {
                user = new User();
                user.username = checkUser.username;
                user.password = checkUser.password;
                user.facultyNumber = checkUser.facultyNumber;
                user.role = checkUser.role;
                currentUserRole = (UserRoles) user.role;
                currentUserUsername = user.username;
                Logger.logActivity("Login successful!");
                return true;
            }

            _errorMessage = "Username and password combination is not known.";
            _actionOnError(_errorMessage);
            currentUserRole = UserRoles.ANONYMOUS;

            return false;
        }
    }
}
