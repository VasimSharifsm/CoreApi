using WorkBench.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkBench.Repository;
using WorkBench.SpModels;
using MySql.Data.MySqlClient;

namespace WorkBench.DAL
{
    public class EmployeesDataAccessLayer
    {     
        public static class ConnectionString
        {
            public static string CName { get; } = "Server=127.0.0.1;Database=optisolapi;Uid=root;Pwd=root;persistsecurityinfo=True;";
        }
      
        string connectionString = ConnectionString.CName;

        public Employees GetEmployee(int Id)
        {
            Employees Employees = new Employees();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Employees WHERE Id= " + Id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employees.Id = Convert.ToInt32(rdr["id"]);
                    Employees.EmpName = rdr["empName"].ToString();
                    Employees.EmailId = rdr["emailId"].ToString();
                    //Employees.Department = Convert.ToInt32( rdr["Department"]);
                }
            }
            return Employees;
        }

       

        public Employees PostEmployee(Employees Employees)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", Employees.EmpName);
                cmd.Parameters.AddWithValue("@Email", Employees.EmailId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Employees;
            }
        }

        public Employees PutEmployee(Employees Employeeschanges)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Employeeschanges.Id);
                cmd.Parameters.AddWithValue("@name", Employeeschanges.EmpName);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return Employeeschanges;
            }
        }

        public Employees DeleteEmployee(int Id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return null;
            }
        }

        public IEnumerable<spgetempperformance> GetAllEmployeees()
        {
            try
            {
                List<spgetempperformance> lstEmployees = new List<spgetempperformance>();
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("getempperformance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        spgetempperformance Employees = new spgetempperformance();
                        Employees.EmpName = rdr["empname"].ToString();
                        //Employees.PerformanceDetails = (from x in lstEmployees
                        //                         select new PerformanceDetails
                        //                         {
                        //                             totaltask = Convert.ToInt32(rdr["totaltask"]),
                        //                             pendingtask = Convert.ToInt32(rdr["pendingtask"]),
                        //                             completedtask = Convert.ToInt32(rdr["completedtask"])
                        //                         }).ToList();

                        Employees.totaltask = Convert.ToInt32(rdr["totaltask"]);
                        Employees.pendingtask = Convert.ToInt32(rdr["pendingtask"]);
                        Employees.completedtask = Convert.ToInt32(rdr["completedtask"]);
                        //Employees.phoneno = rdr["phoneno"].ToString();

                        lstEmployees.Add(Employees);
                    }
                    con.Close();
                }
                return lstEmployees;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

   
      
    }
}
