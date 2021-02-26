using AngularJSCRUDusingDotNetMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularJSCRUDusingDotNetMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Get_AllEmployee()
        {
            using (EmployeeDBEntities obj = new EmployeeDBEntities())
            {
                List<Employee> emp=obj.Employees.ToList();
                return Json(emp, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Get_EmployeeById(string Id)
        {
            using (EmployeeDBEntities obj= new EmployeeDBEntities())
            {
                int EmpId = int.Parse(Id);
                return Json(obj.Employees.Find(EmpId), JsonRequestBehavior.AllowGet);
            }
        }
        public string Insert_Employee(Employee employee)
        {
            if (employee!=null)
            {
                using (EmployeeDBEntities obj= new EmployeeDBEntities())
                {
                    obj.Employees.Add(employee);
                    obj.SaveChanges();
                    return "Employee Added Successfully";
                }
            }
            else
            {
                return "Employee not Added! Try Again!";
            }
        }
        public string Update_Employee(Employee employee)
        {
            if (employee != null)
            {
                using (EmployeeDBEntities obj = new EmployeeDBEntities())
                {
                    var Emp_ = obj.Entry(employee);

                    Employee EmpObj = obj.Employees.FirstOrDefault(x => x.Emp_Id == employee.Emp_Id);
                    EmpObj.Emp_Name = employee.Emp_Name;
                    EmpObj.Emp_Age = employee.Emp_Age;
                    EmpObj.Emp_City = employee.Emp_City;
                    obj.SaveChanges();

                    return "Employee Updated Successfully";
                }
            }
            else
            {
                return "Employee not Updated! Try Again!";
            }
        }
        public string Delete_Employee(Employee Emp)
        {
            if (Emp!=null)
            {
                using (EmployeeDBEntities obj= new EmployeeDBEntities())
                {
                    var Emp_ = obj.Entry(Emp);
                    if (Emp_.State==System.Data.Entity.EntityState.Detached)
                    {
                        obj.Employees.Attach(Emp);
                        obj.Employees.Remove(Emp);
                    }
                    obj.SaveChanges();
                    return "Employee Deleted Successfully..";
                }                
            }
            else
            {
                return "Employee not Deleted! Try Again!";
            }
        }

    }

}