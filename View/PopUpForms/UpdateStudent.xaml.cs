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
    /// Interaction logic for UpdateStudent.xaml
    /// </summary>
    public partial class UpdateStudent : Window
    {

        private readonly ApplicationDbContext _context;
        public UpdateStudent(ApplicationDbContext context)
        {
            InitializeComponent();

            _context = context;

       
             DataContext = new StudentViewModel(_context);

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(NameTxt.Text) &&
               !string.IsNullOrWhiteSpace(Agetxt.Text) &&
               !string.IsNullOrWhiteSpace(IDtxt.Text) &&
               !string.IsNullOrWhiteSpace(Address.Text) &&
               !string.IsNullOrWhiteSpace(Contactxt.Text))
            {
                this.Close();

            }
            else
            {

                MessageBox.Show(" Please fill out all fields correctly.",
                               "Validation Error",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }
        }

        private void Address_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Gmail_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Gmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Contactxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Contactxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumAllowed(e.Text);

        }

        private void IDtxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void IDtxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumAllowed(e.Text);

        }

        private void Agetxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumAllowed(e.Text);

        }

        private void Agetxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void NameTxt_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void NameTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[a-zA-Z.,\s]+$");
            e.Handled = !regex.IsMatch(e.Text);

        }


        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("^[a-zA-Z]+$"); 
            return regex.IsMatch(text);
        }

        private static bool IsNumAllowed(string text)
        {
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(text);
        }
    }
}
