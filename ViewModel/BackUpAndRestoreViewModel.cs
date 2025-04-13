using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command;
using STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.ViewModel
{


    class BackUpAndRestoreViewModel
    {

        public ICommand BackupCommand { get; }
        public ICommand RestoreCommand { get; }
        private ApplicationDbContext _context;
        public BackUpAndRestoreViewModel(ApplicationDbContext context)
        {
            _context = context;

            BackupCommand = new RelayCommand(_ => BackupData());
            RestoreCommand = new RelayCommand(_ => RestoreData());
        }

        private void BackupData()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Backup files (*.bak)|*.bak|All files (*.*)|*.*",
                InitialDirectory = @"C:\GradeVerification"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string backupFilePath = saveFileDialog.FileName;
                try
                {
                    // Use your actual connection string and database name.
                    string connectionString = "Data Source=DESKTOP-QRF1854\\SQLEXPRESS;Initial Catalog=GradeVerification;Integrated Security=True;TrustServerCertificate=True;";
                    string databaseName = "GradeVerification";

                    // SQL command to backup the database
                    string backupCommandText = $@"
                        BACKUP DATABASE [{databaseName}]
                        TO DISK = '{backupFilePath}'
                        WITH INIT, FORMAT;
                    ";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(backupCommandText, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Backup completed successfully!", "Backup", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during backup: " + ex.Message, "Backup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RestoreData()
        {
            // Let the user select a backup file.
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Backup files (*.bak)|*.bak|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string backupFilePath = openFileDialog.FileName;
                try
                {
                    string connectionString = "Server=(local)\\SQLEXPRESS;Initial Catalog=umdb;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;";
                    string databaseName = "umdb";

                    string setSingleUser = $@"ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                    string restoreCommandText = $@"RESTORE DATABASE [{databaseName}] FROM DISK = '{backupFilePath}' WITH REPLACE;";
                    string setMultiUser = $@"ALTER DATABASE [{databaseName}] SET MULTI_USER;";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(setSingleUser + restoreCommandText + setMultiUser, connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Restore completed successfully!", "Restore", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during restore: " + ex.Message, "Restore Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
