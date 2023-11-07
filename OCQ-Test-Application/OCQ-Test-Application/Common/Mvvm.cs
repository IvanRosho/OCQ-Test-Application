using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CC.Mvvm
{
    public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string propertyName = "")
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
	}
    public class DelegateCommand : ICommand
	{
		private Func<object, bool> _canExecute;

		private Action _execute;
		private Action<object> _executeWithParameter;

        #region Constructors
        private DelegateCommand() => _canExecute = new Func<object, bool>((parameter) => true);
        public DelegateCommand(Action execute) : this() => _execute = execute;
        public DelegateCommand(Action execute, Func<object, bool> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}
        public DelegateCommand(Action<object> executeWithParameter) : this() => _executeWithParameter = executeWithParameter;
        public DelegateCommand(Action<object> executeWithParameter, Func<object, bool> canExecute)
		{
			_executeWithParameter = executeWithParameter;
			_canExecute = canExecute;
		}
        #endregion

        public bool CanExecute(object parameter) => _canExecute(parameter);
		public void Execute(object parameter)
		{
			_execute?.Invoke();
			_executeWithParameter?.Invoke(parameter);
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}