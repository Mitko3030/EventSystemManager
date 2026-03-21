
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            bank.Id=int.Parse(Console.ReadLine());
            bank.Balance=double.Parse(Console.ReadLine());
            bank.Deposit(20);
            bank.Withdraw(15);
            Console.WriteLine(bank.ToString());

        }                 
    }
}
