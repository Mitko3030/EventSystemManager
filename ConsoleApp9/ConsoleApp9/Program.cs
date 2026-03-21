using System;
using System.Reflection;

namespace ConsoleApp9
{
    internal class Program
    {
        static void Main(string[] args)
        {


            double length=double.Parse(Console.ReadLine());
            double width=double.Parse(Console.ReadLine());
            double height=double.Parse(Console.ReadLine());
            Box box1 = new Box(length, width, height);


            Type boxType = typeof(Box);
            FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine(fields.Count());

            Console.WriteLine($"Surface Area - {box1.CalculateSurfaceArea():F2}");
            Console.WriteLine($"Lateral Surface Area - {box1.CalculateLateralSurfaceArea():F2}");
            Console.WriteLine($"Volume - {box1.CalculateVolume():F2}");
            
        }
    }
}
