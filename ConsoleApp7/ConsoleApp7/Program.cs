using System.Reflection;
using System.Runtime.InteropServices;

namespace ConsoleApp7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n=int.Parse(Console.ReadLine());
            Family family = new Family();
            
            for(int i=0; i<n; i++)
            {
                
                string[] data = Console.ReadLine().Split();
                string name = data[0];
                int age = int.Parse(data[1]);
                Person person1=new Person(name, age);
                family.AddMember(person1);
            }
            family.GetSortedFamily();
            
            family.PrintFamily();
            

        }
    }
}
