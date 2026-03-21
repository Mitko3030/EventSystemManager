using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    public class ContractEmployee : BaseEmployee
    {
        private string employeeTask;
        private string employeeDepartment;



        public ContractEmployee(string employeeID, string employeeName, string employeeAddress,string employeeTask, string employeeDepartment)
            :base(employeeID, employeeName, employeeAddress)
        {
            this.employeeTask = employeeTask;
            this.employeeDepartment = employeeDepartment;
        }


        public string EmployeeTask
        {
            get { return employeeTask; }
            set { employeeTask = value; }
        }
        public string EmployeeDepartment
        {
            get { return employeeDepartment; }
            set { employeeDepartment = value; }
        }

        public string Show()
        {
            return $"{base.Show()} \n {this.employeeTask} - {this.EmployeeDepartment}";
        }

        public override double CalculateSalary(int workingHours)
        {
            return workingHours * 20 + 250;
        }

        public override string GetDeparment()
        {
            return this.employeeDepartment;
        }
    }
}
