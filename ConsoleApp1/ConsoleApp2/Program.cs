namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person();
            person1.Name = Console.ReadLine();
            person1.Age=int.Parse(Console.ReadLine());
            person1.Introduction();
        }
    }
}
