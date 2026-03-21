using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    public class FullTimeEmployee : BaseEmployee
    {
        private string employeePosition;
        private string employeeDepartment;


        public FullTimeEmployee(string employeeID, string employeeName, string employeeAddress, string employeePosition, string employeeDepartment)
            : base(employeeID, employeeName, employeeAddress)
        {
            this.employeePosition = employeePosition;
            this.employeeDepartment = employeeDepartment;
        }

        public string EmployeePosition
        {
            get { return employeePosition; } set { employeePosition = value; }
        }
        public string EmployeeDepartment
            { get { return employeeDepartment; } set { employeeDepartment = value; } }  


        public string Show()
        {
            return $"{base.Show()} \n {this.employeePosition} - {this.EmployeeDepartment}";
        }

        public override double CalculateSalary(int workingHours)
        {
            return workingHours * 10.80;
        }

        public override string GetDeparment()
        {
            return this.employeeDepartment;
        }
    }
}
