using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    public class Person
    {
        private string firstName;
        private string lastName;
        private int age;

        public Person(string fname, string lname, int age)
        {
            this.firstName = fname;
            this.lastName = lname;
            this.age = age;
        }
            

        public string FirstName
        {
            get { return firstName; } 
            set {  firstName = value; }
        }
        public string Lname
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public override string ToString()
        {
            return $"{this.firstName} {this.lastName} is a {this.age} years old";
        }
    }
}
