using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class ProgramsViewModel:INotifyPropertyChanged
    {


        private readonly ApplicationDbContext _context;

        public ObservableCollection<ProgramEntity> ProgramCollection { get; private set; }

        //Crud Commands

        public ICommand AddProgramCommand { get; }
        public ICommand UpdateProgramCommand { get; }
        public ICommand DeleteProgramCommand { get; }
        public ICommand LoadProgramCommand { get; }


        public ProgramsViewModel(ApplicationDbContext context)
        {

            _context = context;

            ProgramCollection = new ObservableCollection<ProgramEntity>();
            AddProgramCommand = new RelayCommand(async _ => await AddDepartmentAsync(), _ => canAddDepartment());
            UpdateProgramCommand = new RelayCommand(async _ => await UpdateDepartmentAsync(), _ => Selected_program != null);
            DeleteProgramCommand = new RelayCommand(async _ => await DeleteDepartmentAsync(), _ => Selected_program != null);
            LoadProgramCommand = new RelayCommand(async _ => await LoadDepartmentsAsync());
            LoadProgramCommand.Execute(null);

        }





        //Get the selected departments

        private ProgramEntity _selected_program;


        public ProgramEntity Selected_program
        {
            get => _selected_program;

            set
            {

                _selected_program = value;
                OnPropertyChanged(nameof(Selected_program));
            }
        }


        //Department ID 

        private string _programID;

        public string ProgramID
        {

            get => _programID;


            set
            {

                _programID = value;

                OnPropertyChanged();
            }

        }

        //Acronym

        private string _acronym;

        public string Acronym
        {

            get => _acronym;


            set
            {

                
                    _acronym = value;
                    OnPropertyChanged(nameof(Acronym));
                
            }

        }

        //Name

        private string _name;

        public string Name
        {

            get => _name;


            set
            {

                _name = value;
                OnPropertyChanged(nameof(Name));
                
            }

        }


        //Insert Method 
        private async Task AddDepartmentAsync()
        {
            if (string.IsNullOrEmpty(Name)|| string.IsNullOrEmpty(Acronym))
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            var existingProgramName = await _context.Programs
                               .FirstOrDefaultAsync(s => s.Name == Name || s.Acronym == Acronym);

            if (existingProgramName != null)
            {
                MessageBox.Show($"Program Name or Acronym already exists. Please use a different one.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Clear();
                return;
            }


            try
            {

                string ID = $"PROGRAM-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                var obj = new ProgramEntity
                {
                    ProgramID = ID,
                    Acronym = Acronym,
                    Name = Name,


                    CreatedAt = DateTime.Now,


                };



                _context.Programs.Add(obj);

                await _context.SaveChangesAsync();
                ProgramCollection.Add(obj);



                OnPropertyChanged(nameof(ProgramCollection));
                MessageBox.Show("Program added success", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Clear();
            }

            catch (Exception ex)
            {


            }
          





        }

        //Update Department
        private async Task UpdateDepartmentAsync()
        {




            var existing_departments = await _context.Programs.FindAsync(Selected_program.ProgramID);

            if (existing_departments == null)
            {
                MessageBox.Show("Selected departments does not exist in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(Selected_program.Name) || string.IsNullOrWhiteSpace(Selected_program.Acronym))
            {
                MessageBox.Show("Please fill in all fields correctly.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                

                existing_departments.Acronym = Selected_program.Acronym;
                existing_departments.Name = Selected_program.Name;

                _context.Programs.Update(existing_departments);
                await _context.SaveChangesAsync();

                MessageBox.Show("Program updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                //Clear();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Oops there is an error:{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                Clear();
            }




        }


        //Delete Program
        private async Task DeleteDepartmentAsync()
        {

            if (Selected_program != null)
            {

                _context.Programs.Remove(Selected_program);
                await _context.SaveChangesAsync();

                ProgramCollection.Remove(Selected_program);

            }
        }


        //Load Programs
        private async Task LoadDepartmentsAsync()
        {

            ProgramCollection.Clear();

            var departments = await _context.Programs.ToListAsync();

            foreach (var item in departments)
            {
               
                ProgramCollection.Add(item);
            }
            OnPropertyChanged(nameof(ProgramCollection));


        }


        //Clear Input

        private void Clear()
        {
            Acronym = string.Empty;
            Name = string.Empty;

        }

        //Boolean method to flag if is valid
        private bool canAddDepartment() => true;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
