using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem
{
    public class LoginVM
    {
        private LoginModel _loginModel;
        private readonly Action _closeWindowAction;
        public LoginVM(Action closeWindowAction)
        {
            _loginModel = new LoginModel();
            _closeWindowAction = closeWindowAction;
        }
        public LoginVM()
        {

        }
        public string Username {
            set
            {
                _loginModel.Username = value;
            }
        }
        public string Password
        {
            set
            {
                _loginModel.Password = value;
            }
        }

        public LoginCommand LoginBtnCommand
        {
            get { return new LoginCommand(_loginModel, _closeWindowAction); }
        }


    }
}
