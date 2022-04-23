using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentInfoSystem
{
    public class LoginCommand : ICommand
    {
        private LoginModel _loginModel;
        private readonly Action _closeWindowAction;

        public LoginCommand(LoginModel loginModel, Action closeWindowAction)
        {
            _loginModel = loginModel;
            _closeWindowAction = closeWindowAction;
        }

        public void Execute(object parameter)
        {
            UserLogin.User userToLogIn = UserLogin.UserData.isUserPassCorrect(_loginModel.Username, _loginModel.Password);
            if (userToLogIn == null)
            {
                MessageBox.Show("Невалидно потребителско име или парола!");
                return;
            }
            if (userToLogIn.role != (int)UserLogin.UserRoles.STUDENT)
            {
                MessageBox.Show("Потребителят не е студент!");
                return;
            }
            MainWindow studentDataWindow = new MainWindow();
            studentDataWindow.Show();
            studentDataWindow.loadStudent();
            _closeWindowAction();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}