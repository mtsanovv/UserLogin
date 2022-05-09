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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentInfoSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainFormVM();
        }

        private void clearAllTextBoxes()
        {
            foreach(var control in personalDataGrid.Children)
            {
                if(control is TextBox)
                {
                    TextBox textBox = control as TextBox;
                    textBox.Text = "";
                    continue;
                }
            }

            foreach (var control in studentInfoGrid.Children)
            {
                if (control is TextBox)
                {
                    TextBox textBox = control as TextBox;
                    textBox.Text = "";
                    continue;
                }
            }
        }

        private void setIsEnabledOnAllGridChildren(bool trueOrFalse)
        {
            foreach (var control in personalDataGrid.Children)
            {
                UIElement element = control as UIElement;
                element.IsEnabled = trueOrFalse;
            }

            foreach (var control in studentInfoGrid.Children)
            {
                UIElement element = control as UIElement;
                element.IsEnabled = trueOrFalse;
            }
        }

        private void enterTestMode_Click(object sender, RoutedEventArgs e)
        {
            loadStudent();
            if(TestStudentsIfEmpty())
            {
                CopyTestStudents();
                CopyTestUsers();
                MessageBox.Show("No students found in DB!");
                return;
            }
            MessageBox.Show("Students found in DB!");
        }

        private void exitTestMode_Click(object sender, RoutedEventArgs e)
        {
            resetForm();
        }

        private bool TestStudentsIfEmpty()
        {
            StudentInfoContext context = new StudentInfoContext();
            IEnumerable<Student> queryStudents = context.Students;
            int countStudents = queryStudents.Count();
            if(countStudents == 0)
            {
                return true;
            }
            return false;
        }

        private void CopyTestStudents()
        {
            StudentInfoContext context = new StudentInfoContext();
            foreach (Student st in StudentData.allStudents)
            {
                context.Students.Add(st);
            }
            context.SaveChanges();
        }

        private void CopyTestUsers()
        {
            UserLogin.UserContext context = new UserLogin.UserContext();
            foreach (UserLogin.User user in UserLogin.UserData.TestUsers)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }

        public void loadStudent()
        {
            enterTestMode.Visibility = Visibility.Hidden;
            exitTestMode.Visibility = Visibility.Hidden;
        }

        public void resetForm()
        {
            clearAllTextBoxes();
            enterTestMode.Visibility = Visibility.Visible;
            exitTestMode.Visibility = Visibility.Visible;
        }
    }
}
