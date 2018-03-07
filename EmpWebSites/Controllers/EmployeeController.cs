using EmpWebSites.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpWebSites.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //hello atul
        public JsonResult Get_data()
        {
            EmployeeDataHandler objEmployeeDataHandler = new EmployeeDataHandler();
            List<Employee> Employeelist = new List<Employee>();
            DataSet ds = objEmployeeDataHandler.GetEmployee();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Employeelist.Add(
                    new Employee
                    {
                        Eid = Convert.ToString(dr["Eid"]),
                        Ename = Convert.ToString(dr["Ename"]),
                        Email = Convert.ToString(dr["Email"]),
                        Salary = Convert.ToString(dr["Salary"]),
                        mobile = Convert.ToString(dr["mobile"]),
                        UserName = Convert.ToString(dr["UserName"]),
                        Password = Convert.ToString(dr["Password"])
                    });
            }
            return Json(Employeelist, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Add_record(Employee objEmployee)
        {
            string res = string.Empty;
            List<Employee> Employeelist = new List<Employee>();
            try
            {
                EmployeeDataHandler objEmployeeDataHandler = new EmployeeDataHandler();
                String msg = CheckUserName(objEmployee.UserName, objEmployee.Eid);
                if (msg == "true")
                {
                    String id = objEmployeeDataHandler.AddEmployee(objEmployee);

                    DataSet ds = objEmployeeDataHandler.Get_ByID(id);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Employeelist.Add(
                            new Employee
                            {
                                Eid = Convert.ToString(dr["Eid"]),
                                Ename = Convert.ToString(dr["Ename"]),
                                Email = Convert.ToString(dr["Email"]),
                                Salary = Convert.ToString(dr["Salary"]),
                                mobile = Convert.ToString(dr["mobile"]),
                                UserName = Convert.ToString(dr["UserName"]),
                                Password = Convert.ToString(dr["Password"])
                            });
                    }
                    return Json(Employeelist, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                res = "Error";
            }

            return Json(Employeelist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_databyid(String id)
        {
            EmployeeDataHandler objEmployeeDataHandler = new EmployeeDataHandler();
            List<Employee> Employeelist = new List<Employee>();
            DataSet ds = objEmployeeDataHandler.Get_ByID(id);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Employeelist.Add(
                    new Employee
                    {
                        Eid = Convert.ToString(dr["Eid"]),
                        Ename = Convert.ToString(dr["Ename"]),
                        Email = Convert.ToString(dr["Email"]),
                        Salary = Convert.ToString(dr["Salary"]),
                        mobile = Convert.ToString(dr["mobile"]),
                        UserName = Convert.ToString(dr["UserName"]),
                        Password = Convert.ToString(dr["Password"])
                    });
            }
            return Json(Employeelist, JsonRequestBehavior.AllowGet);

        }

//Hiii atul
        public JsonResult update(String id, Employee objEmployee)
        {
            string res = string.Empty;
            List<Employee> Employeelist = new List<Employee>();
            try
            {
                // TODO: Add update logic here
                EmployeeDataHandler objEmployeeDataHandler = new EmployeeDataHandler();
                String msg = CheckUserName(objEmployee.UserName, objEmployee.Eid);
                if (msg == "true")
                {
                    bool flag = objEmployeeDataHandler.UpdateDetails(objEmployee);

                    if (flag)
                    {

                        DataSet ds = objEmployeeDataHandler.GetEmployee();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Employeelist.Add(
                                new Employee
                                {
                                    Eid = Convert.ToString(dr["Eid"]),
                                    Ename = Convert.ToString(dr["Ename"]),
                                    Email = Convert.ToString(dr["Email"]),
                                    Salary = Convert.ToString(dr["Salary"]),
                                    mobile = Convert.ToString(dr["mobile"]),
                                    UserName = Convert.ToString(dr["UserName"]),
                                    Password = Convert.ToString(dr["Password"])
                                });
                        }
                        return Json(Employeelist, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        res = "Error";
                    }
                }
                else
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                res = "Error";
            }

            return Json(Employeelist, JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult Delete(String id)
        {
            string msg = "";
            EmployeeDataHandler objEmployeeDataHandler = new EmployeeDataHandler();
            if (objEmployeeDataHandler.DeleteEmployee(id))
            {
                msg = "Employee Deleted Successfully";
               
            }
            else
            {
                msg = "Error";
            }
            //return RedirectToAction("Index");
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        public string CheckUserName(String user, string id)
        {
            String flag = string.Empty;
            EmployeeDataHandler objEmployeeDataHandler = new EmployeeDataHandler();
            List<Employee> Employeelist = new List<Employee>();

            if (id == null)
            {
                DataSet ds = objEmployeeDataHandler.GetEmployee();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    String UserName = Convert.ToString(dr["UserName"]);
                    if (user == UserName)
                    {
                        flag = "false";
                        return flag;
                    }
                }
            }
            else
            {
                DataSet ds = objEmployeeDataHandler.GetEmployeeByIDUpdate(id);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    String UserName = Convert.ToString(dr["UserName"]);
                    if (user == UserName)
                    {
                        flag = "false";
                        return flag;
                    }
                }
            }
            return flag = "true";
        }
    }
}
