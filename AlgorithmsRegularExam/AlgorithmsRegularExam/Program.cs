using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsRegularExam
{
    class Program
    {
        static Farm farm = new Farm("Farm1");

        static void Main(string[] args)
        {
            string line;

            while ("END" != (line = Console.ReadLine()))
            {
                string[] cmdArgs = line.Split(' ');

                switch (cmdArgs[0])
                {
                    case "Add":
                        AddAnimal(cmdArgs[1], int.Parse(cmdArgs[2]));
                        break;
                    case "AverageWeight":
                        AverageWeight();
                        break;
                    case "FilterAnimals":
                        FilterAnimalsByWeight(int.Parse(cmdArgs[1]));
                        break;
                    case "SortByType":
                        SortAscendingByType();
                        break;
                    case "SortByWeight":
                        SortDescendingByWeight();
                        break;
                    case "CheckAnimal":
                        CheckAnimalIsInFarm(cmdArgs[1]);
                        break;
                    case "Print":
                        ProvideInformationAboutAllAnimals();
                        break;
                }
            }
        }

        private static void ProvideInformationAboutAllAnimals()
        {
            string[] info = farm.ProvideInformationAboutAllAnimals();
            foreach (string item in info)
            {
                Console.WriteLine(item);
            }
        }

        private static void CheckAnimalIsInFarm(string type)
        {
            if (farm.CheckAnimalIsInFarm(type))
            {
                Console.WriteLine($"Animal {type} is in the farm.");
            }
            else
            {
                Console.WriteLine($"Animal {type} is not in the farm.");
            }
        }
        private static void SortDescendingByWeight()
        {
            farm.SortDescendingByWeight();
            Console.WriteLine("The animal with lowest weight is: " + farm.Animals[farm.Animals.Count - 1].Type);
        }
        private static void SortAscendingByType()
        {
            farm.SortAscendingByType();
            Console.WriteLine("First animal is: " + farm.Animals[0].Type);
        }
        private static void FilterAnimalsByWeight(int weight)
        {
            List<string> animals = farm.FilterAnimalsByWeight(weight);
            Console.WriteLine("Filtered animals: " + string.Join(", ", animals));
        }

        private static void AverageWeight()
        {
            double averageWeight = farm.AverageWeight();
            Console.WriteLine($"Average weight: {averageWeight:F2}");
        }

        private static void AddAnimal(string type, int weight)
        {
            farm.AddAnimal(type, weight);
            Console.WriteLine($"Added animal {type}.");
        }
    }
}