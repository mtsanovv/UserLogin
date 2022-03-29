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

namespace WPFHello
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ListBoxItem[] newItems = new ListBoxItem[2];
            for(int i = 0; i < newItems.Length; i++)
            {
                newItems[i] = new ListBoxItem();
            }
            newItems[0].Content = "James";
            newItems[1].Content = "David";
            foreach(ListBoxItem item in newItems) {
                peopleListBox.Items.Add(item);
            }
            peopleListBox.SelectedItem = newItems[0];
        }

        private void btnHello_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtName.Text;
            if(userName.Length < 2)
            {
                MessageBox.Show("Името трябва да е с дължина от поне 2 символа!");
                return;
            }
            MessageBox.Show("Здрасти " + txtName.Text + "!!!\nТова е твоята първа програма на Visual Studio 2022!");
        }

        private void nFactorielBtn_Click(object sender, RoutedEventArgs e)
        {
            string nValue = nTextBox.Text;
            int n;
            if (!int.TryParse(nValue, out n))
            {
                MessageBox.Show("Cannot parse the value for n: invalid input!");
                return;
            }
            int nFactorial = 1;
            for(int i = n; i > 1; i--)
            {
                nFactorial *= i;
            }
            MessageBox.Show("n! is " + nFactorial);
        }

        private void nToYBtn_Click(object sender, RoutedEventArgs e)
        {
            string nValue = nTextBox.Text;
            string yValue = yTextBox.Text;
            int n, y;
            if (!int.TryParse(nValue, out n))
            {
                MessageBox.Show("Cannot parse the value for n: invalid input!");
                return;
            }

            if (!int.TryParse(yValue, out y))
            {
                MessageBox.Show("Cannot parse the value for y: invalid input!");
                return;
            }

            int result = (int) Math.Pow(n, y);
            MessageBox.Show("n ^ y is " + result);
        }

        private void currentSelectedPersonBtn_Click(object sender, RoutedEventArgs e)
        {
            string greetingMsg = (peopleListBox.SelectedItem as ListBoxItem).Content.ToString();
            MyMessage anotherWindow = new MyMessage();
            anotherWindow.Show();
        }
    }
}
