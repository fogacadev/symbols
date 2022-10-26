using System;
using System.Windows.Input;

namespace Symbols.Commands
{
    public class Command : ICommand
    {

        public Command(Action action)
        {
            Action = action;
            Func = new Func<bool>(() => true);
        }

        public Command(Action action, Func<bool> func)
        {
            Action = action;
            Func = func;
        }

        public event EventHandler CanExecuteChanged;


        private Action action;

        public Action Action
        {
            get { return action; }
            set { action = value; }
        }


        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }


        private Func<bool> func;

        public Func<bool> Func
        {
            get { return func; }
            set { func = value; }
        }

        public bool CanExecute(object parameter)
        {
            return Func.Invoke();
        }

        public void Execute(object parameter)
        {
            Action.Invoke();
        }
    }

    public class Command<T> : ICommand
    {

        public Command(Action<T> action)
        {
            Action = action;
            Func = new Func<bool>(() => true);
        }

        public Command(Action<T> action, Func<bool> func)
        {
            Action = action;
            Func = func;
        }

        public event EventHandler CanExecuteChanged;


        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private Action<T> action;

        public Action<T> Action
        {
            get { return action; }
            set { action = value; }
        }

        private Func<bool> func;

        public Func<bool> Func
        {
            get { return func; }
            set { func = value; }
        }

        public bool CanExecute(object parameter)
        {
            return Func.Invoke();
        }

        public void Execute(object parameter)
        {
            if (Action != null && parameter is T)
                Action((T)parameter);
        }
    }
}
