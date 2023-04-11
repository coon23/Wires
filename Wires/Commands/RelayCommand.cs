using System;
using System.Windows.Input;

namespace Wires.Commands
{
    internal class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Action<object> _toExecute;
        public Func<object, bool> _canExecute;


        public RelayCommand(Action<object> toExecute, Func<object, bool> canExecute)
        {
            _toExecute = toExecute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }

            return false;
        }

        public void Execute(object? parameter)
        {
            _toExecute(parameter);
        }


    }
}
