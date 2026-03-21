using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class BankAccount
    {
        private int id;
        private double balance;
        public double Balance
        {
            get { return this.balance; }
            set { this.balance = value; }
        }
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }


        public void Deposit(double amount)
        {
            this.balance += amount;
        }
        public void Withdraw(double amount)
        {
            this.balance -= amount;
        }
        public override string ToString()
        {
            return $"{this.id}, {this.balance}";
        }
    }
}
