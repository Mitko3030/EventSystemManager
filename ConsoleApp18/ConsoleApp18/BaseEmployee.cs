using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    public abstract class BaseEmployee
    {
        private string employeeID;
        private string employeeName;
        private string employeeAddress;



        public BaseEmployee(string employeeID, string employeeName, string employeeAddress)
        {
            this.employeeID = employeeID;
            this.employeeName = employeeName;
            this.employeeAddress = employeeAddress;
        }
        public string EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }
        public string EmployeeName
            { get { return employeeName; } set { employeeName = value; } }
        public string EmployeeAddress
            { get { return employeeAddress; } set { employeeAddress = value; } }


        public string Show()
        {
            return $"{this.EmployeeID} - {this.EmployeeName} - {this.EmployeeAddress}";
        }


        public abstract double CalculateSalary(int workingHours);
        

        public abstract string GetDeparment();
      
    }
}
