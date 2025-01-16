using Microsoft.IdentityModel.Tokens;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel;
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

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms
{
    /// <summary>
    /// Interaction logic for UpdateDepartment.xaml
    /// </summary>
    public partial class UpdateDepartment : Window
    {

        public UpdateDepartment(ApplicationDbContext context)
        {
            InitializeComponent();

            DataContext = new DepartmentViewModel(context);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameTxt.Text))
            {
                this.Close();
             
            }
            else
            {
                MessageBox.Show("You can't close it there is no Departname provided", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
               
            }
        }

    

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NameTxt.Text = "";
        }
    }
}
