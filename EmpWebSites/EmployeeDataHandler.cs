using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using EmpWebSites.Models;
using System.Linq;
using System.Web;
using System.Data;

namespace EmpWebSites
{
    public class EmployeeDataHandler
    {

        private SqlConnection con;

        private SqlCommand cmd;

        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["Emp"].ToString();
            con = new SqlConnection(constring);
        }

        public String AddEmployee(Employee objEmployee)
        {
            connection();
            string query = "INSERT INTO Employee(Eid,Ename,Email,Salary,Mobile,UserName,Password) VALUES(@Eid,@Ename, @Email,@Salary,@Mobile,@UserName,@Password)";

            cmd = new SqlCommand(query, con);
            objEmployee.Eid = Guid.NewGuid().ToString();
            cmd.Parameters.AddWithValue("@Ename", objEmployee.Ename);
            cmd.Parameters.AddWithValue("@Email", objEmployee.Email);
            cmd.Parameters.AddWithValue("@Salary", objEmployee.Salary);
            cmd.Parameters.AddWithValue("@Mobile", objEmployee.mobile);
            cmd.Parameters.AddWithValue("@UserName", objEmployee.UserName);
            cmd.Parameters.AddWithValue("@Password", objEmployee.Password);
            cmd.Parameters.AddWithValue("@Eid", objEmployee.Eid);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return objEmployee.Eid;
            else
                return objEmployee.Eid;
        }

        public DataSet GetEmployee()
        {

            connection();
            String query = "SELECT * FROM Employee";
            SqlCommand com = new SqlCommand(query, con);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            con.Open();
            da.Fill(ds);
            con.Close();
            return ds;

        }

        public bool UpdateDetails(Employee objEmployee)
        {
            connection();
            //Guid empIdToupdate = new Guid(objEmployee.Eid);
            String query = " Update Employee  set Ename =@Ename,Email = @Email,Salary =@Salary,Mobile=@Mobile,UserName=@UserName,password=@password where Eid =@Eid";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Ename", objEmployee.Ename);
            cmd.Parameters.AddWithValue("@Email", objEmployee.Email);
            cmd.Parameters.AddWithValue("@Salary", objEmployee.Salary);
            cmd.Parameters.AddWithValue("@Mobile", objEmployee.mobile);
            cmd.Parameters.AddWithValue("@UserName", objEmployee.UserName);
            cmd.Parameters.AddWithValue("@Password", objEmployee.Password);
            cmd.Parameters.AddWithValue("@Eid", objEmployee.Eid);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteEmployee(String id)
        {
            connection();
            Guid empIdToDelete = new Guid(id);
            String query = "DELETE FROM Employee WHERE Eid='" + empIdToDelete.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public DataSet Get_ByID(String entityID)
        {
            try
            {
                connection();
                Guid empIdToupdate = new Guid(entityID);
                String query = "SELECT * FROM Employee WHERE Eid='" + empIdToupdate.ToString() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                con.Open();
                da.Fill(ds);
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataSet GetEmployeeByIDUpdate(String entityID)
        {
            try
            {
                connection();
                Guid empIdToupdate = new Guid(entityID);
                String query = "SELECT * FROM Employee WHERE Eid != '" + empIdToupdate.ToString() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                con.Open();
                da.Fill(ds);
                con.Close();
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}