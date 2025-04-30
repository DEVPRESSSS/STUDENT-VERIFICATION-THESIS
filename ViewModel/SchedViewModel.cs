using Microsoft.EntityFrameworkCore;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{
    public class SchedViewModel
    {


         private readonly ApplicationDbContext _context;


          public ObservableCollection<ScheduleOfSubjects> ScheduleCollection { get; private set; }
          public ObservableCollection<ProfessorsEntity> ProfessorCollection  {get; private set; }
          public ObservableCollection<SubjectsEntity> SubjectCollection {get; private set; }

          public ICommand? UpsertCommand { get; }
          public ICommand? UpdateCommand { get; }
          public ICommand? DeleteCommand { get; }
          public ICommand? LoadSchedCommand { get; }
          public ICommand? CloseCommand { get; }

          public SchedViewModel(ApplicationDbContext context)
          {
                 _context = context;

                 ScheduleCollection = new ObservableCollection<ScheduleOfSubjects>();
                ProfessorCollection = new ObservableCollection<ProfessorsEntity>();
                SubjectCollection = new ObservableCollection<SubjectsEntity>();
                  UpsertCommand = new RelayCommand(async _ => await AddScheduleAsync(), _ => canAddSched());
                 UpdateCommand = new RelayCommand(async _ => await UpdateScheduleAsync(), _=> SelectedSched != null);
                 LoadSchedCommand = new RelayCommand(async _ => await LoadSchedAsync());
                 LoadSchedCommand.Execute(null);
                 DeleteCommand = new RelayCommand(async _ => await DeleteSched(), _=> SelectedSched!= null);
                 
                 CloseCommand = new RelayCommand(_ => CloseCurrentActiveWindow());


                 _ = LoadProfessorAsync();
                 _ = LoadSubjectsAsync();


          }
        /// <summary>
        /// Encapsulation
        /// </summary>


        //Selected Sched
        private ScheduleOfSubjects _selected_Sched;


        public ScheduleOfSubjects SelectedSched
        {

            get => _selected_Sched;

            set
            {


                _selected_Sched = value;

              

                OnPropertyChanged(nameof(SelectedSched));



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

                if(SearchTerm!= null)
                {
                    _ = SearchSchedAsync();

                }
                OnPropertyChanged();

            }
        }

        //Get the selected subject
        private SubjectsEntity _selected_Subject;


        public SubjectsEntity SelectedSubject
        {

            get => _selected_Subject;

            set
            {


                _selected_Subject = value;

             

                OnPropertyChanged(nameof(SelectedSubject));



            }
        }


        //Get the selected Professor

        private ProfessorsEntity _selected_Professor;


        public ProfessorsEntity SelectedProfessor
        {

            get => _selected_Professor;

            set
            {


                _selected_Professor = value;

        

                OnPropertyChanged(nameof(SelectedProfessor));




            }
        }



        //Get day
        private string _day;


        public string Day
        {

            get => _day;

            set
            {


                _day = value;

                OnPropertyChanged(nameof(Day));




            }
        }






        //Get time
        private string _time;


        public string Time
        {

            get => _time;

            set
            {


                _time = value;

                OnPropertyChanged(nameof(Time));




            }
        }



        //Get room
        private string _room;


        public string Room
        {

            get => _room;

            set
            {


                _room = value;

                OnPropertyChanged(nameof(Room));




            }
        }


        private async Task DeleteSched()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this record?","Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if(messageBoxResult == MessageBoxResult.Yes)
            {
                if (SelectedSched != null)
                {

                    _context.Remove(SelectedSched);
                    await _context.SaveChangesAsync();
                    ScheduleCollection.Remove(SelectedSched);

                }

            }
          
        }

        //This will load the sched

        private async Task LoadSchedAsync()
        {

            try
            {

                var listOfSched = await _context.Schedule.
                    Include(x=>x.Subject).
                    Include(x=>x.Professor).
                    ToListAsync();


                ScheduleCollection.Clear();


                foreach(var sched in listOfSched) { 
                
                
                        ScheduleCollection.Add(sched);
                
                }

            }

            catch(Exception ex)
            {

                MessageBox.Show($"{ex}");

                return;
            }
        }


        //Load Professor

        private async Task LoadProfessorAsync()
        {

            
                using(var context = new ApplicationDbContext())
                {


                    var listOfProf= await context.Professors.ToListAsync();


                     ProfessorCollection.Clear();


                    foreach (var prof in listOfProf)
                    {


                        ProfessorCollection.Add(prof);

                    }

                }

          


         
        }



        //Load subjects
        private async Task LoadSubjectsAsync()
        {

           

                using (var context = new ApplicationDbContext())
                {


                    var listOfProf = await context.Subjects.
                    ToListAsync();




                        foreach (var prof in listOfProf)
                            {


                                SubjectCollection.Add(prof);

                            }

                }



        }

        //Upsert excxtion means insert and update

        private async Task AddScheduleAsync()
        {
            if (!ValidateInputs())
            {
                return;
            }


            string ID = $"GRD-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
            var newSched = new ScheduleOfSubjects
            {
                ScheduleID = ID,
                SubjectID = SelectedSubject.SubjectID,
                Day = Day,
                Time = Time,
                Room = Room,
                ProfessorID = SelectedProfessor.ProfessorID
            };

            _context.Schedule.Add(newSched);
            await _context.SaveChangesAsync();
            ScheduleCollection.Add(newSched);
            MessageBox.Show("Schedule added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearInputs();
            CloseCurrentActiveWindow();
           
            _ = LoadSchedAsync();


        }

        private async Task UpdateScheduleAsync()
        {

            if (string.IsNullOrWhiteSpace(SelectedSched.Room) ||
               string.IsNullOrWhiteSpace(SelectedSched.Time) ||
               string.IsNullOrWhiteSpace(SelectedSched.Day)
              )
            {
               
                MessageBox.Show("All fields are required and cannot contain only white spaces.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }


            var existingID = await _context.Schedule
                .FirstOrDefaultAsync(x => x.ScheduleID == SelectedSched.ScheduleID);

            if (existingID != null)
            {
                existingID.SubjectID = SelectedSched.SubjectID;
                existingID.ProfessorID = SelectedSched.ProfessorID;
                existingID.Day = SelectedSched.Day;
                existingID.Time = SelectedSched.Time;
                existingID.Room = SelectedSched.Room;

                _context.Schedule.Update(existingID);
                await _context.SaveChangesAsync();

                MessageBox.Show("Schedule updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseCurrentActiveWindow();
            }
            else
            {
                MessageBox.Show("Schedule not found for updating.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            private void ClearInputs()
            {
                Day = string.Empty;
                Time = string.Empty;
                Room = string.Empty;
            }





        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Day) ||
                string.IsNullOrWhiteSpace(Time) ||
                string.IsNullOrWhiteSpace(Room) ||
                SelectedSubject == null ||
                SelectedProfessor == null)
            {
                MessageBox.Show("All fields are required and cannot contain only white spaces.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }


        private async Task SearchSchedAsync()
        {
            try
            {

                using (var context = new ApplicationDbContext())
                {

                    var query = context.Schedule
                     .Include(p => p.Subject)
                     .Include(p => p.Professor)
                     .AsQueryable();

                    query = query.Where(p =>
                        EF.Functions.Like(p.Subject.SubjectID, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Subject.SubjectName, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Subject.CourseCode, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Professor.Name, $"%{SearchTerm}%") ||
                        EF.Functions.Like(p.Professor.Name, $"%{SearchTerm}%")
                    );

                    var result = await query.ToListAsync();

                    // Update the ObservableCollection
                    ScheduleCollection.Clear();
                    foreach (var professor in result)
                    {
                        ScheduleCollection.Add(professor);
                    }


                }






            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool canAddSched() => true;

          public event PropertyChangedEventHandler? PropertyChanged;

          protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
          {
              PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
          }
        
    }
}
