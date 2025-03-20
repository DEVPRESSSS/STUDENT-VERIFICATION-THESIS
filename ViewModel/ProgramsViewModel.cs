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

        public ICommand AddProgramCommand { get; }
        public ICommand UpdateProgramCommand { get; }
        public ICommand DeleteProgramCommand { get; }
        public ICommand LoadProgramCommand { get; }
        public ICommand SearchCommand { get; set; }


        public ProgramsViewModel(ApplicationDbContext context)
        {

            _context = context;

            ProgramCollection = new ObservableCollection<ProgramEntity>();
            AddProgramCommand = new RelayCommand(async _ => await AddProgramAsync(), _ => canAddProgram());
            UpdateProgramCommand = new RelayCommand(async _ => await UpdateProgramAsync(), _ => Selected_program != null);
            DeleteProgramCommand = new RelayCommand(async _ => await DeleteProgramAsync(), _ => Selected_program != null);
            LoadProgramCommand = new RelayCommand(async _ => await LoadProgramsAsync());
            LoadProgramCommand.Execute(null);
            SearchCommand = new RelayCommand(async _ => await SearchProgramAsync(), _ => !string.IsNullOrWhiteSpace(SearchTerm));

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



        //Search 
        private string _searchTerm;

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                if (string.IsNullOrWhiteSpace(_searchTerm))
                {
                    _ = LoadProgramsAsync();
                }
                else
                {

                    _ = SearchProgramAsync();
                }
            }
        }

        private async Task SearchProgramAsync()
        {
            try
            {

                using(var context = new ApplicationDbContext())
                {

                    var query = context.Programs.AsQueryable();

                    query = query.Where(p =>
                        EF.Functions.Like(p.Name, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.ProgramID, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Acronym, $"%{SearchTerm}%")
                    );

                    var result = await query.ToListAsync();

                    // Update the ObservableCollection
                    ProgramCollection.Clear();
                    foreach (var professor in result)
                    {
                        ProgramCollection.Add(professor);
                    }


                }






            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CloseCurrentActiveWindow()
        {
            var activeWindow = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(window => window.IsActive);

            if (activeWindow != null)
            {
                activeWindow.Close();
            }
        }



        //Insert Method 
        private async Task AddProgramAsync()
        {
            if (string.IsNullOrWhiteSpace(Name)|| string.IsNullOrWhiteSpace(Acronym))
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
        private async Task UpdateProgramAsync()
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
                var existingProgram = await _context.Programs
                .FirstOrDefaultAsync(s => (s.Name == Selected_program.Name || s.Acronym == Selected_program.Acronym) &&
                                          s.ProgramID != Selected_program.ProgramID);

                if (existingProgram != null)
                {
                    MessageBox.Show("Program name or acronym already exists. Please use a different one.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                existing_departments.Acronym = Selected_program.Acronym;
                existing_departments.Name = Selected_program.Name;

                _context.Programs.Update(existing_departments);
                await _context.SaveChangesAsync();

                MessageBox.Show("Program updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseCurrentActiveWindow();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Oops there is an error:{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                Clear();
            }




        }


        //Delete Program
        private async Task DeleteProgramAsync()
        {
            MessageBoxResult confiramtion = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confiramtion == MessageBoxResult.Yes)
            {
                if (Selected_program != null)
                {

                    _context.Programs.Remove(Selected_program);
                    await _context.SaveChangesAsync();

                    ProgramCollection.Remove(Selected_program);

                }

            }




            
        }


        //Load Programs
        private async Task LoadProgramsAsync()
        {


            using (var context = new ApplicationDbContext())
            {

                ProgramCollection.Clear();

                var departments = await context.Programs.ToListAsync();

                foreach (var item in departments)
                {

                    ProgramCollection.Add(item);
                }
                OnPropertyChanged(nameof(ProgramCollection));

            }
        }


        //Clear Input

        private void Clear()
        {
            Acronym = string.Empty;
            Name = string.Empty;

        }

        //Boolean method to flag if is valid
        private bool canAddProgram() => true;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
