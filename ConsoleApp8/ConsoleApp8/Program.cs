using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPTask
{
    public class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Person> list = new List<Person>();
            for (int i = 0; i < n; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                string name = inputs[0];
                int age = int.Parse(inputs[1]);
                Person novchovek = new Person(name, age);
                list.Add(novchovek);
            }
            List<Person> ageOverThirty = new List<Person>();
            foreach (Person person in list)
            {
                if (person.Age>30)
                {
                    ageOverThirty.Add(person);
                }
            }

            ageOverThirty.OrderByDescending(novchovek => novchovek.Name);
            foreach (Person person in ageOverThirty)
            {
                person.ToString();
            }
            ;
        }
    }
}
