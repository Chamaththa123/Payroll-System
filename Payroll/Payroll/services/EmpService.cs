using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Payroll.models;
using Payroll.interfaces;

namespace Payroll.services
{
    public class EmpService : IEmpService
    {
        private readonly IConfiguration _config;

        // Constructor that accepts IConfiguration to get the connection string
        public EmpService(IConfiguration configuration)
        {
            _config = configuration;
        }

        // Method to retrieve a list of employees from the database
        public List<EmpModel> getEmp()
        {
            // Initialize a new list to hold employee data
            List<EmpModel> empList = new List<EmpModel>();

            // Establish a connection to the database using the connection string
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("EmployeeAppCon").ToString()))
            {
                // Create a SqlDataAdapter to execute the SQL query
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM employee", con);
                // Create a DataTable to hold the results of the query
                DataTable dt = new DataTable();
                // Fill the DataTable with data from the database
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i]; // Get the current row

                        // Map the DataRow to an EmpModel object
                        EmpModel employee = new EmpModel
                        {
                            IdEmployee = Convert.ToInt32(row["IdEmployee"]),
                            Full_Name = Convert.ToString(row["Full_Name"]),
                            EPF_Id = Convert.ToString(row["EPF_Id"]),
                            NIC = Convert.ToString(row["NIC"]),
                            BirthDay = Convert.ToDateTime(row["BirthDay"]),
                            Contact = Convert.ToString(row["Contact"]),
                            SalaryType = Convert.ToString(row["SalaryType"]),
                            SalaryAmount = Convert.ToDecimal(row["SalaryAmount"]),
                            Designation_IdDesignation = Convert.ToInt32(row["Designation_IdDesignation"]),
                            Status = Convert.ToInt32(row["Status"])
                        };
                        // Add the employee object to the list
                        empList.Add(employee);
                    }
                }
            }
            return empList;
        }
    }
}
