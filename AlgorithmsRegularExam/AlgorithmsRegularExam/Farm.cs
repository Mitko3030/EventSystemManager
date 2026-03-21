using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsRegularExam
{
    public class Farm
    {

        private string name;
        private List<Animal> animals;

        public Farm(string name)
        {
            Name = name;
            animals = new List<Animal>();
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Animal> Animals
        {
            get { return animals; }
            set { animals = value; }
        }




        public void AddAnimal(string name, int weight)
        {
            Animal animal1 = new Animal(name, weight);
            animals.Add(animal1);
        }






        public double AverageWeight()
        {
            return animals.Average(a => a.Weight);
        }





        public List<string> FilterAnimalsByWeight(int weight)
        {
            return animals.Where(animal => animal.Weight == weight).Select(animal => animal.Type).ToList();
        }



        public List<Animal> SortAscendingByType()
        {
            return animals.OrderBy(animal => animal.Type).ToList();
        }


        public List<Animal> SortDescendingByWeight()
        {
            return animals.OrderByDescending(animal => animal.Weight).ToList();
        }


        public bool CheckAnimalIsInFarm(string name)
        {
            return animals.Any(a => a.Type.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public string[] ProvideInformationAboutAllAnimals()
        {
            return animals.Select(animal => animal.ToString()).ToArray();
        }
        public List<string> FilterAnimals(double weight)
        {
            return animals.Where(animal => animal.Weight <= weight).Select(animal => animal.Type).ToList();
        }
    }
}
