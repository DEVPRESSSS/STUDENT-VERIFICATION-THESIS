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
    /// Interaction logic for AddProgram.xaml
    /// </summary>
    public partial class AddProgram : Window
    {
        public AddProgram(ApplicationDbContext context)
        {
            InitializeComponent();

            this.DataContext = new ProgramsViewModel(context);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //NameTxt.Text = "";

        }

        private void NameTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);

        }

        private void NameTxt_Copy_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);

        }

       
        private void NameTxt_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("^[a-zA-Z]+$"); // Only letters are allowed
            return regex.IsMatch(text);
        }
    }
}
