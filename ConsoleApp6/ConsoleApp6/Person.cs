using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class Person
    {
        private string name;
        private int age;
        public List<BankAccount> Accounts { get; set; }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
       
        /*public string GetBalance(List<BankAccount>)
        {
            double totalBalance = 0;
            foreach (var account in accounts)
            {
                totalBalance += account.Balance;
            }
            return totalBalance.ToString();
        }*/
        public string GetBalance()
        {
            double totalBalance = 0;
            foreach (var account in this.Accounts)
            {
                totalBalance += account.Balance;
            }
            return totalBalance.ToString();
        }
    }
}
