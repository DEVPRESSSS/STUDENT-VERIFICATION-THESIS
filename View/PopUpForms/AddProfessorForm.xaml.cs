using DocumentFormat.OpenXml.Spreadsheet;
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
    /// Interaction logic for AddProfessorForm.xaml
    /// </summary>
    public partial class AddProfessorForm : Window
    {
        private readonly ApplicationDbContext _context;

        public AddProfessorForm(ApplicationDbContext context)
        {
            InitializeComponent();
            _context= context;
            this.DataContext = new ProfessorViewModel(_context);
        }

     
   

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void Gmail_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        

        private void ProfessorName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[a-zA-Z.,\s]+$");
            e.Handled = !regex.IsMatch(e.Text);

        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("^[a-zA-Z]+$"); 
            return regex.IsMatch(text);
        }

        private void Age_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumAllowed(e.Text);

        }

        private static bool IsNumAllowed(string text)
        {
            Regex regex = new Regex("^[0-9]+$"); 
            return regex.IsMatch(text);
        }

        private void Name_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && e.Key == Key.Space)
            {
                if (string.IsNullOrEmpty(textBox.Text) || textBox.Text.EndsWith(" "))
                {
                    e.Handled = true;

                }
            }
        }

        private void Gmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Gmail_LostFocus(object sender, RoutedEventArgs e)
        {
            string emailPattern = @"^[a-zA-Z0-9]+(\.{1}[a-zA-Z0-9]+)*@gmail\.com$";
            string emailInput = Gmail.Text.Trim();
            if (!Regex.IsMatch(emailInput, emailPattern) && !string.IsNullOrEmpty(emailInput))
            {
                MessageBox.Show("Invalid email address format. Email should only contain letters, numbers, dot (.) and at (@) symbols, and end with @gmail.com.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Gmail.Text = string.Empty;
            }
        }

        private void Address_PreviewKeyDown(object sender, KeyEventArgs e)
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
    }
}
