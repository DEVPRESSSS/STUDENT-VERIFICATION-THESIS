using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {

        private readonly ApplicationDbContext _context;
        public AddStudent(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            this.DataContext = new StudentViewModel(context);
        }

      
        private void Close_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Automation_Click(object sender, RoutedEventArgs e)
        {
            BulkInsertStudent obj = new BulkInsertStudent(_context);
            obj.ShowDialog();   
        }

        private void Gmail_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Gmail_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void IDnumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Age_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Name_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && e.Key == Key.Space)
            {
                // Check if last character is already a space
                if (textBox.Text.EndsWith(" "))
                {
                    e.Handled = true; // Block the space
                }
            }
        }

        private void Name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[a-zA-Z.,\s]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("^[a-zA-Z]+$"); // Only letters are allowed
            return regex.IsMatch(text);
        }

        private static bool IsNumAllowed(string text)
        {
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(text);
        }

        private void Address_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Address_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Contact_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Contact_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumAllowed(e.Text);

        }

        private void IDnumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumAllowed(e.Text);

        }

        private static readonly Regex NameRegex = new Regex(@"^(?!.*\s{2})[A-Za-z]+(?:\s[A-Za-z]+)*(?:\s[A-Za-z]+\.)?$");
        private bool IsNameAllowed(string text)
        {
            // Check if the text matches the regular expression
            return NameRegex.IsMatch(text);
        }
    }
}
