using System;

namespace ConsoleApp6

{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            person.Name = "Test";
            person.Age = 4;

            int accountsCount = int.Parse(Console.ReadLine());

            person.Accounts = new List<BankAccount>();
            for (int i = 0; i < accountsCount; i++)
            {
                BankAccount account = new BankAccount();
                account.Id = i;
                account.Balance = double.Parse(Console.ReadLine());

                person.Accounts.Add(account);
            }

            Console.WriteLine(person.GetBalance());
        }
    }
}