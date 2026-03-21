namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n=int.Parse(Console.ReadLine());
            List<Test> tests = new List<Test>();
            for (int i = 0; i < n; i++)
            {
                string[] inputs = Console.ReadLine().Split();
                string fname=inputs[0];
                string lname=inputs[1];
                int age = int.Parse(inputs[2]);
                Test tester=new Test(fname, lname, age);
                tests.Add(tester);  
            }
        }
    }
}
