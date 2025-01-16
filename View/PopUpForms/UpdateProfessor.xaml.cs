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
    /// Interaction logic for UpdateProfessor.xaml
    /// </summary>
    public partial class UpdateProfessor : Window
    {
        public UpdateProfessor(ApplicationDbContext context)
        {
            InitializeComponent();
            this.DataContext = new ProfessorViewModel(context);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(Professor.Text) &&
                !string.IsNullOrEmpty(Gmail.Text) &&
                !string.IsNullOrEmpty(Age.Text) &&
                !string.IsNullOrEmpty(Address.Text))
            {
                this.Close();

            }
            else
            {
                MessageBox.Show("Please ensure all fields are filled correctly:\n" +
                                "- Professor and Gmail should not be empty.\n" +
                                "- Age must be a numeric value.\n" +
                                "- Address should not be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Professor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);

        }

        private void Professor_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void Gmail_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Address_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void Age_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
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

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("^[a-zA-Z]+$"); // Only letters are allowed
            return regex.IsMatch(text);
        }

    }
}
