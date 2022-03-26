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
            Loaded += onWindowLoaded;
            InitializeComponent();
            StudentData.initializeStudentData();
        }

        private void onWindowLoaded(object sender, RoutedEventArgs e)
        {
            degreeDropdown.ItemsSource = Enum.GetValues(typeof(Degree)).Cast<Degree>();
            statusDropdown.ItemsSource = Enum.GetValues(typeof(StudentStatus)).Cast<StudentStatus>();
        }

        private void clearAllTextBoxes()
        {
            foreach(var control in MainGrid.Children)
            {
                if(control is TextBox)
                {
                    TextBox textBox = control as TextBox;
                    textBox.Text = "";
                    continue;
                }

                if (control is ComboBox)
                {
                    ComboBox comboBox = control as ComboBox;
                    comboBox.SelectedIndex = -1;
                    continue;
                }
            }
        }

        private void fillTextBoxesWithStudentData(Student student)
        {
            foreach (var control in MainGrid.Children)
            {
                if (control is TextBox)
                {
                    TextBox textBox = control as TextBox;
                    handleFillingTextBox(textBox, student);
                    continue;
                }

                if (control is ComboBox)
                {
                    ComboBox comboBox = control as ComboBox;
                    handleFillingComboBox(comboBox, student);
                    continue;
                }
            }

        }

        private void setIsEnabledOnAllGridChildren(bool trueOrFalse)
        {
            foreach (var control in MainGrid.Children)
            {
                UIElement element = control as UIElement;
                element.IsEnabled = trueOrFalse;
            }
        }

        private void handleFillingTextBox(TextBox textBox, Student student)
        {
            string[] delimiter = new string[] { "TextBox" };
            string fieldName = textBox.Name.Split(delimiter, StringSplitOptions.None)[0];
            textBox.Text = student.GetType().GetProperty(fieldName).GetValue(student, null).ToString();
        }

        private void handleFillingComboBox(ComboBox comboBox, Student student)
        {
            string[] delimiter = new string[] { "Dropdown" };
            string fieldName = comboBox.Name.Split(delimiter, StringSplitOptions.None)[0];
            comboBox.SelectedIndex = (int) student.GetType().GetProperty(fieldName).GetValue(student, null);
        }

        private void enterTestMode_Click(object sender, RoutedEventArgs e)
        {
            Student sampleStudent = StudentData.allStudents.First();
            fillTextBoxesWithStudentData(sampleStudent);
            exitTestMode.Visibility = Visibility.Visible;
        }

        private void exitTestMode_Click(object sender, RoutedEventArgs e)
        {
            clearAllTextBoxes();
            enterTestMode.Visibility = Visibility.Hidden;
        }
    }
}
