using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

/// <summary>
/// This Base ViewModel Implements INotifyPropertyChanged to all child ViewModels
/// </summary>
namespace MvvmDemoCore.MVVM.ViewModel
{
    /// <summary>
    /// This attribute finds all public PROPERTIES in the class and injects the PropertyChanged event to its setter.
    /// If a property changes, [AddINotifyPropertyChangedInterface] will execute PropertyChanged event for us.
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        //This is the event that is executed when any property (from a child class) changes its value
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
