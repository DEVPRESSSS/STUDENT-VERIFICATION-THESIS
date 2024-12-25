using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.Command
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action<object?> _execute;

        private readonly Func<object?, bool>? _canExecute;


        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            _canExecute?.Invoke(parameter);

            return true;
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    }
}
