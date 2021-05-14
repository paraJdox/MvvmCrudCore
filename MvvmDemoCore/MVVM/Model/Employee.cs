using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MvvmDemoCore.MVVM.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Employee : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
