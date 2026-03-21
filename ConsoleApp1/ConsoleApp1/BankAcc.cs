using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Bank
    {
        private int id;
        private double balance;
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public double Balance
        {
            get { return this.balance; }
            set { this.balance = value; }
        }

        public void Deposit(double amount)
        {
            this.Balance += amount;
        }
        public void Withdraw(double amount)
        {
            this.Balance-=amount;
        }
        public override string ToString()
        {
            return $"Account {this.id}, balance {this.balance}";
        }

    }
}
