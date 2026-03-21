using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Bank
    {
        private int id;
        private double balance;
        public Bank(int id)
        {
            Id = id;
            Balance = 0;
        }
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
            this.Balance -= amount;
        }
        public override string ToString()
        {
            return $"Account {this.id}, balance {this.balance}";
        }

        
        
    }
    public class TestClient
    {
        private Dictionary<int, Bank> accounts;
        public TestClient()
        {
            accounts = new Dictionary<int, Bank>();
        }
        public void ExecuteCommand(string input)
        {
            string[] commandParts = input.Split();
            string command = commandParts[0];
            if (command == "Create")
            {
                int createID = int.Parse(commandParts[1]);
                if (accounts.ContainsKey(createID))
                {
                    Console.WriteLine("Account already eexists.");
                }
                else
                {
                    accounts.Add(createID, new Bank(createID));
                }


            }
            else if (command == "Deposit")
            {
                int createID = int.Parse(commandParts[1]);
                double depositAmount = double.Parse(commandParts[2]);
                if (accounts.ContainsKey(createID))
                {
                    accounts[createID].Deposit(depositAmount);
                }
                else
                {
                    Console.WriteLine("Account does not exist");
                }

            }
            else if (command == "Withdraw")
            {
                int withdrawId = int.Parse(commandParts[1]);
                double withdrawAmount = double.Parse(commandParts[2]);
                if (accounts.ContainsKey(withdrawId))
                {
                    accounts[withdrawId].Withdraw(withdrawAmount);
                }
                else
                {
                    Console.WriteLine("Account does not exist");
                }
            }
            else if (command == "Print")
            {
                int printId = int.Parse(commandParts[1]);
                if (accounts.ContainsKey(printId))
                {
                    Console.WriteLine(accounts[printId]);
                }
                else
                {
                    Console.WriteLine("Account does not exist");

                }

            }

        }
    }

}
