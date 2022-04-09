using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentInfoSystem
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public Login()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            UserLogin.User userToLogIn = UserLogin.UserData.isUserPassCorrect(Username, Password);
            if(userToLogIn == null)
            {
                MessageBox.Show("Невалидно потребителско име или парола!");
                return;
            }
            if(userToLogIn.role != (int) UserLogin.UserRoles.STUDENT)
            {
                MessageBox.Show("Потребителят не е студент!");
                return;
            }
            MainWindow studentDataWindow = new MainWindow();
            studentDataWindow.Show();
            studentDataWindow.loadStudent();
            this.Close();
        }
    }
}
