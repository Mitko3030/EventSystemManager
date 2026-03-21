namespace ConsoleApp11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s = " six impossible things before breakfast ";
            int i = s.IndexOf('i');
            Console.Write(i + " ");
            Console.WriteLine("\"" + s.Trim() + "\"");
            s.Replace(' ', '|');
            Console.WriteLine(s);
        }
    }
}
