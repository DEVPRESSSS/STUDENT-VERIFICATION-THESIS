using Microsoft.EntityFrameworkCore;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.UsercontrolsView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class ProfessorViewModel:INotifyPropertyChanged
    {
        //Inject the applicationddbcontext

        private readonly ApplicationDbContext _context;

        //Declare observable collection of professors

        public ObservableCollection<ProfessorsEntity> ProfessorsCollection { get; private set; }


        public ICommand AddProfessorrsCommand { get; }
       // public ICommand UpdateProfessorrsCommand { get; }
        //public ICommand DeleteProfessorrsCommand { get; }
        public ICommand LoadProfessorrsCommand { get; }


        //Declare an Encapsulation 


        private ProfessorsEntity _selected_professor;


        public ProfessorsEntity Selected_professor
        {
            get => _selected_professor;

            set
            {

                _selected_professor = value;
                OnPropertyChanged();
            }
        }

    

        public ProfessorViewModel(ApplicationDbContext context)
        {
            _context = context;


            ProfessorsCollection = new ObservableCollection<ProfessorsEntity>();

            AddProfessorrsCommand = new RelayCommand(async _ => await AddProfessorAsync(), _ => CanExecuteAddStudent());
            LoadProfessorrsCommand = new RelayCommand(async _ => await LoadProfessorAsync());
            LoadProfessorrsCommand.Execute (null);
        }

        private async Task LoadProfessorAsync()
        {
            var prof = await _context.Professors.ToListAsync();

            ProfessorsCollection.Clear();
            foreach (var professor in prof)
            {

                ProfessorsCollection.Add(professor);
            }

            //OnPropertyChanged(nameof(Professors));

        }

        public async Task AddProfessorAsync()
        {
            try
            {
                var newProfessor = new ProfessorsEntity
                {
                    ProfessorID = "PROOF0021",
                    Name = "Test",
                    Age = 1,
                    Email = "montemorjeraldd@gmail.com",
                    Address = "montemorjeraldd@gmail.com",
                    CreatedAt = DateTime.Now,
                    DepartmentID = 3
                };

                _context.Professors.Add(newProfessor);
                await _context.SaveChangesAsync();

              //  ProfessorsCollection.Add(newProfessor);



                MessageBox.Show("Inserted Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                await LoadProfessorAsync();
                OnPropertyChanged(nameof(ProfessorsCollection));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding professor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //Boolean to flag if it is valid
        private bool CanExecuteAddStudent() => true;


        //Event handler
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }






    }
}
