using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView
{
    /// <summary>
    /// Interaction logic for Professors.xaml
    /// </summary>
    public partial class Professors : UserControl
    {
        private readonly ApplicationDbContext _context;
        public Professors(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;

            this.DataContext= new ProfessorViewModel(context);
        }

        private void UsernameXZ_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UsernameXZ_GotFocus(object sender, RoutedEventArgs e)
        {
            UsernameXZ.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            AddProfessorForm s = new AddProfessorForm(_context);
            s.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateProfessor updateProfessor = new UpdateProfessor(_context)
            {
                DataContext = this.DataContext 
            };
            updateProfessor.ShowDialog();

        }
    }
}
