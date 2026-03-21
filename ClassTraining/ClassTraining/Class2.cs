using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTraining
{
    public class BankAccount
    {
        int id;
        double balance;
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
        public void BankAcc()
        {
            Console.WriteLine($"{id} {balance}");
        }
    }
}
