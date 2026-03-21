using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegularExam
{
    public class HatShop
    {
        private string name;
        private List<Hat> hats;

        public HatShop(string name)
        {
            Name = name;
            hats = new List<Hat>();
            throw new NotImplementedException();
        }

        public string Name
        {
            get { return name; }
            set 
            {
                if (value.Length >= 6)
                {
                    name = value;
                }
                else { throw new ArgumentException("Invalid hat shop name!"); }
            }
        }


        
        public void AddHat(Hat hat)
        {
            hats.Add(hat);
            throw new NotImplementedException();
        }

       
        
        public bool SellHat(Hat hat)
        {
            if (hats.Contains(hat))
            {
                hats.Remove(hat);
                return true;
            }
            return false;
            throw new NotImplementedException();
        }

        

        public double CalculateTotalPrice()
        {
            double total = 0;
            foreach(var hat in hats)
            {
                total += hat.Price;
            }
            return total;
            throw new NotImplementedException();
        }


        public void RenameHatShop(string newName)
        {
            if (newName.Length >= 6)
            {
                name = newName;
            }
            else
            {
                throw new ArgumentException("Invalid shop name!");
            }
        }


        public void SellAllHats()
        {
            hats.Clear();
            throw new NotImplementedException();
        }

        public Hat GetHatWithHighestPrice()
        {
            
        
            double max = double.MinValue;
            foreach (var hat in hats)
            {
                if (hat.Price > max)
                {
                    max = hat.Price;
                }
            }

            foreach (Hat hat in hats)
            {
                if (hat.Price == max)
                {
                    return hat;
                }
            }

            throw new InvalidOperationException("No hats available.");
        



            throw new NotImplementedException();
        }
        

        public Hat GetHatWithLowestPrice()
        {
            double min = double.MaxValue;
            foreach (var hat in hats)
            {
                if (hat.Price < min)
                {
                    min = hat.Price;
                }
            }

            foreach (Hat hat in hats)
            {
                if (hat.Price == min)
                {
                    return hat;
                }
            }

            throw new InvalidOperationException("No hats available.");
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            if (hats.Count > 0)
            {
                Console.WriteLine($"Hat Shop {name} has {hats.Count} hat/s:");
                foreach (var hat in hats)
                {
                    Console.WriteLine($"Hat {hat.Type} with color {hat.Color} costs {hat.Price}");
                }
            }
            else
            {
                Console.WriteLine($"Hat Shop {name} has no available hats.");
            }
            throw new NotImplementedException();
        }
    }
    
}
