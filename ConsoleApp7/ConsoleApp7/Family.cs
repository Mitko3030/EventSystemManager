using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public class Family
    {
        private List<Person> members = new List<Person>();

        public void AddMember(Person person)
        {
            members.Add(person);
        }


        public List<Person> GetSortedFamily()
        {
            return members.OrderBy(p => p.Name).ToList(); 
        }

        /*public string PrintFamily(List<Person> a, int n)
        {
            string result = "";
            for (int i = 0; i < n; i++)
            {
                result += a[i].Name + " " + a[i].Age + "\n";
            }
            return result;
        }*/


        public void PrintFamily()
        {
            foreach (var member in GetSortedFamily())
            {
                Console.WriteLine($"{member.Name} {member.Age}");
            }
        }
    }
}
