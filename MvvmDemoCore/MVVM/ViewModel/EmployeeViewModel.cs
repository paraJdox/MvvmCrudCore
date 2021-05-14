using MvvmDemoCore.Core.Command;
using MvvmDemoCore.MVVM.Model;
using MvvmDemoCore.Core.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MvvmDemoCore.MVVM.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {

        #region Fields
        //CollectionService _collectionService;
        private AdoNetService _adoNetService;
        #endregion

        #region Constructor
        public EmployeeViewModel()
        {
            //_collectionService = new CollectionService();
            _adoNetService = new AdoNetService();
            LoadData();

            //We're not using the field - currentEmployee, because a field will not send us a notification if it is changed,
            //so we use the CurrentEmployee Property instead

            //If we're making changes to the CurrentEmployee property, its setter will be called,
            //and OnPropertyChanged method will be executed, therefore creating the notification
            CurrentEmployee = new Employee();

            SaveCommand = new RelayCommand(Save);
            SearchCommand = new RelayCommand(Search);
            UpdateCommand = new RelayCommand(Update);
            DeleteCommand = new RelayCommand(Delete);
        }
        #endregion

        #region Properties
        public ObservableCollection<Employee> EmployeesList { get; set; } //All the employees in the collection
        public Employee CurrentEmployee { get; set; } //The property of the Employee model will be taken from the CurrentEmployee property, then bind it to the UI
        public string Message { get; set; } //The message displayed after a CRUD operation

        //These are the Command objects that will be binded to a control (these are the properties that will be databinded)
        //We don't need a setter for these properties, since a command is only OneWay binding (setter is used in TwoWay databinding)
        public RelayCommand SaveCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand UpdateCommand { get; }
        public RelayCommand DeleteCommand { get; }
        #endregion

        #region CRUDS
        private void LoadData()
        {
            EmployeesList = new ObservableCollection<Employee>(_adoNetService.GetAll());
        }
        public void Save()
        {
            try
            {
                var IsSaved = _adoNetService.Add(CurrentEmployee);
                LoadData();
                if (IsSaved)
                    Message = "Employee Saved";
                else
                    Message = "Save Operation Failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        public void Search()
        {
            //display in the textboxes the employees name and age if it matches the id
            try
            {
                var ObjEmployee = _adoNetService.Search(CurrentEmployee.Id);
                if (ObjEmployee != null)
                {
                    CurrentEmployee.Name = ObjEmployee.Name;
                    CurrentEmployee.Age = ObjEmployee.Age;
                }
                else
                {
                    Message = "Employee Not found";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update()
        {
            try
            {
                var IsUpdated = _adoNetService.Update(CurrentEmployee);
                if (IsUpdated)
                {
                    Message = "Employee Updated";
                    LoadData();
                }
                else
                {
                    Message = "Update Operation Failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        public void Delete()
        {
            try
            {
                var IsDelete = _adoNetService.Delete(CurrentEmployee.Id);

                if (IsDelete)
                {
                    Message = "Employee deleted";
                    LoadData();
                }
                else
                {
                    Message = "Delete operation failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
    }
}
