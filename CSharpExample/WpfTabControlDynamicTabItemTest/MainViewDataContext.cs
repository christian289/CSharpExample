using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WpfTabControlDynamicTabItemTest
{
    public class MainViewDataContext
    {
        public MainViewDataContext()
        {
            MyCommand1 = new Command(ExecuteMethod1, CanExecuteMethod1);
            MyCommand2 = new Command(ExecuteMethod2, CanExecuteMethod2);
            MyTab = new ObservableCollection<MyData>();
        }

        public ICommand MyCommand1 { get; set; }
        public ICommand MyCommand2 { get; set; }
        public ObservableCollection<MyData> MyTab { get; set; }

        private bool CanExecuteMethod2(object arg)
        {
            return true;
        }

        private void ExecuteMethod2(object obj)
        {
            MyTab.Clear();
        }

        private bool CanExecuteMethod1(object arg)
        {
            return true;
        }

        private void ExecuteMethod1(object obj)
        {
            for (int i = 1; i <= 2; i++)
            {
                MyTab.Add(new MyData
                {
                    Header = $"tabitem{i}"
                });
            }
        }
    }

    public class MyData
    {
        public string Header { get; set; }
    }

    public class Command : ICommand
    {
        Action<object> _executeMethod;
        Func<object, bool> _canexecuteMethod;

        public Command(Action<object> executeMethod, Func<object, bool> canexecuteMethod)
        {
            this._executeMethod = executeMethod;
            this._canexecuteMethod = canexecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _executeMethod(parameter);
        }
    }
}
