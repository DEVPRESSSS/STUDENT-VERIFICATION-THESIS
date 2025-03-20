using DocumentFormat.OpenXml.InkML;
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
    /// Interaction logic for UpdateProgram.xaml
    /// </summary>
    public partial class UpdateProgram : Window
    {
        private readonly ApplicationDbContext _context;

        public UpdateProgram(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            DataContext = new ProgramsViewModel(_context);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {


            if(!string.IsNullOrWhiteSpace(Acronym.Text) && !string.IsNullOrWhiteSpace(NameTxt_Copy.Text))
            {
                this.Close();


            }
            else
            {

                MessageBox.Show("You can't close the form unless you fill the fields",
                                "Error", 
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void NameTxt_Copy_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void NameTxt_Copy_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);

        }
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("^[a-zA-Z]+$"); 
            return regex.IsMatch(text);
        }
    }
}
