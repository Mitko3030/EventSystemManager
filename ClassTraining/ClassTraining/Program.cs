using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassTraining;
namespace ClassTraining
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Person person1 = new Person();
            person1.Name=Console.ReadLine();
            person1.Age = int.Parse(Console.ReadLine());
            person1.IntroduceYourself();*/

            BankAccount account = new BankAccount();
            account.Id = int.Parse(Console.ReadLine());
            account.Balance =double.Parse(Console.ReadLine());
            account.BankAcc();
        }
    }
}
