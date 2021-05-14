using MvvmDemoCore.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MvvmDemoCore.MVVM.View
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        EmployeeViewModel ViewModel;
        public EmployeeView()
        {
            InitializeComponent();
            ViewModel = new EmployeeViewModel();
            this.DataContext = ViewModel; //we're not setting the DataContext directly in the DataGrid, so that it is available to all the elements inside the Window
        }
    }
}
