using MvvmDemoCore.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmDemoCore.Core.Service
{
    class CollectionService
    {
        //In-memory collection: ObservableCollection<Employee>
        private static List<Employee> _employeeList;

        public CollectionService()
        {
            _employeeList = new List<Employee>()
            {
                new Employee{Id=101, Name="Syed", Age=25}
            };
        }

        public List<Employee> GetAll()
        {
            return _employeeList;
        }

        public bool Add(Employee newEmployee)
        {
            if (newEmployee.Age < 21 || newEmployee.Age > 58)
                throw new ArgumentException("Invalid age limit for employee");


            _employeeList.Add(newEmployee);
            return true;
        }

        public bool Update(Employee employee)
        {
            bool IsUpdated = false;
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Id == employee.Id)
                {
                    _employeeList[i].Name = employee.Name;
                    _employeeList[i].Age = employee.Age;
                    IsUpdated = true;
                    break;
                }
            }
            return IsUpdated;
        }

        public bool Delete(int id)
        {
            bool IsDeleted = false;
            for (int i = 0; i < _employeeList.Count; i++)
            {
                if (_employeeList[i].Id == id)
                {
                    _employeeList.RemoveAt(i);
                    IsDeleted = true;
                    break;
                }
            }
            return IsDeleted;
        }

        public Employee Search(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }
    }
}
