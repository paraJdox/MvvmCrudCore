using MvvmDemoCore.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MvvmDemoCore.Core.Service
{
    class AdoNetService
    {
        //ado.net
        //add System.Data.SqlClient in the nuget package
        SqlConnection sqlCon;
        SqlCommand sqlCmd;

        public AdoNetService()
        {
            //ado.net
            sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["EMSConnection"].ConnectionString);
            sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlCon;
            sqlCmd.CommandType = CommandType.StoredProcedure;
        }

        public List<Employee> GetAll()
        {
            //ado.net
            List<Employee> employeeList = new List<Employee>();
            try
            {
                sqlCmd.Parameters.Clear();
                sqlCmd.CommandText = "udp_SelectAllEmployees";

                sqlCon.Open();
                var sqlDataReader = sqlCmd.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    Employee employee = null;

                    //reading multiple Employee records
                    while (sqlDataReader.Read())
                    {
                        employee = new Employee();
                        employee.Id = sqlDataReader.GetInt32(0);
                        employee.Name = sqlDataReader.GetString(1);
                        employee.Age = sqlDataReader.GetInt32(2);

                        employeeList.Add(employee);
                    }
                }
                sqlDataReader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sqlCon.Close();
            }
            return employeeList;
        }

        public bool Add(Employee newEmployee)
        {
            //ado.net
            bool IsAdded = false;
            if (newEmployee.Age < 21 || newEmployee.Age > 58)
                throw new ArgumentException("Invalid age limit for employee");

            try
            {
                sqlCmd.Parameters.Clear();
                sqlCmd.CommandText = "udp_InsertEmployee";
                sqlCmd.Parameters.AddWithValue("@Id", newEmployee.Id);
                sqlCmd.Parameters.AddWithValue("@Name", newEmployee.Name);
                sqlCmd.Parameters.AddWithValue("@Age", newEmployee.Age);

                sqlCon.Open();
                int NoOfRowsAffected = sqlCmd.ExecuteNonQuery();
                IsAdded = NoOfRowsAffected > 0; //If number of rows affected is > 0, that means a record is added
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sqlCon.Close();
            }
            return IsAdded;
        }

        public bool Update(Employee employee)
        {
            //ado.net
            bool IsUpdated = false;
            try
            {
                sqlCmd.Parameters.Clear();
                sqlCmd.CommandText = "udp_UpdateEmployee";
                sqlCmd.Parameters.AddWithValue("@Id", employee.Id);
                sqlCmd.Parameters.AddWithValue("@Name", employee.Name);
                sqlCmd.Parameters.AddWithValue("@Age", employee.Age);

                sqlCon.Open();
                int NoOfRowsAffected = sqlCmd.ExecuteNonQuery();
                IsUpdated = NoOfRowsAffected > 0; //If number of rows affected is > 0, that means a record is updated
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sqlCon.Close();
            }
            return IsUpdated;
        }

        public bool Delete(int id)
        {
            //ado.net
            bool IsDeleted = false;

            try
            {
                sqlCmd.Parameters.Clear();
                sqlCmd.CommandText = "udp_DeleteEmployee";
                sqlCmd.Parameters.AddWithValue("@Id", id);

                sqlCon.Open();
                int NoOfRowsAffected = sqlCmd.ExecuteNonQuery();
                IsDeleted = NoOfRowsAffected > 0; //If number of rows affected is > 0, that means a record is deleted
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sqlCon.Close();
            }

            return IsDeleted;
        }

        public Employee Search(int id)
        {
            //ado.net
            Employee employee = null;

            try
            {
                sqlCmd.Parameters.Clear();
                sqlCmd.CommandText = "udp_SelectEmployeeById";
                sqlCmd.Parameters.AddWithValue("@Id", id);

                sqlCon.Open();
                var sqlDataReader = sqlCmd.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    //reading a single Employee record
                    sqlDataReader.Read();
                    employee = new Employee();
                    employee.Id = sqlDataReader.GetInt32(0);
                    employee.Name = sqlDataReader.GetString(1);
                    employee.Age = sqlDataReader.GetInt32(2);
                }
                sqlDataReader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sqlCon.Close();
            }

            return employee;
        }
    }
}
