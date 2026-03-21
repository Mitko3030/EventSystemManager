using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPTask
{
    public class Person
    {
        private string name;
        private int age;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }
        public void GetSorted(List<Person> list)
        {
                return list.Where(person => person.Age > 30).ToList();
        }
        public void PrintFamily()
        {
            foreach (var member in GetSorted())
            {
                Console.WriteLine($"{member.Name} {member.Age}");
            }
        }
        
    }
}