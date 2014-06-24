using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Framework.Client.Mvvm
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {

        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #region ICommand
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        #endregion

        #region factory methods
        public static ICommand CreateCommand(ref ICommand cmd, Action<object> execute)
        {
            return CreateCommand(ref cmd, execute, null);
        }
        public static ICommand CreateCommand(ref ICommand cmd, Action<object> execute, Predicate<object> canExecute)
        {
            return cmd ?? (cmd = new RelayCommand(execute, canExecute));
        }

        #endregion
    }
}
