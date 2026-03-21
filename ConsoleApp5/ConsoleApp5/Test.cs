using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Test
    {
        private string fname;
        private string lname;
        private int age;

        public Test(string ime, string vtime, int godini)
        {
            this.fname = ime;
            this.lname = vtime;
            this.age = godini;
        }

        public string Fname
        {
            get { return this.fname; }
            set { this.fname = value; }
        }
        public string Lname
        {
            get { return this.lname; }
            set { this.lname = value; }
        }
        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }
    }
}
