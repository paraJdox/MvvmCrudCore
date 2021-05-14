using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MvvmDemoCore.Core.Command
{
    public class RelayCommand : ICommand //ICommand provides us a set of methods that we can use to invoke the methods(behaviors) that is present in our ViewModel
    {
        public event EventHandler CanExecuteChanged;
        private Action DoWork; //This delegate holds a reference of the methods (with the same signature) from the ViewModel

        public RelayCommand(Action work) //We pass our method (from the ViewModel) here
        {
            DoWork = work;
        }

        //Whenever we bind our control to an object of "RelayCommand", to make sure that our control is enabled or not "CanExecute()" is executed

        //To check/determine if a control is enable or not
        public bool CanExecute(object parameter)
        {
            return true;
        }


        //When CanExecute() is true (control is enabled), the user can interact with that control
        //When a user clicks a control (e.g. a button) Execute() is executed
        public void Execute(object parameter)
        {
            DoWork(); //A method from the ViewModel is Executed
        }
    }
}
