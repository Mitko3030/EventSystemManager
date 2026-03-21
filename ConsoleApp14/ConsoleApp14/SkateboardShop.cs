using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    public class SkateboardShop
    {
        private string name;
        private List<Skateboard> skateboards;

        public SkateboardShop(string name)
        {
            Name = name;
            this.skateboards = new List<Skateboard>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                // // Regex validation
                // Validator.ValidateCapitalSmall(value);

                if (!Regex.IsMatch(value, "^([A-Za-z])+$"))
                {
                    throw new ValidationException("Input values should be a capital or lower latin letter!");
                }

                this.name = value;
            }
        }

        public List<Skateboard> Skateboards
        {
            get
            {
                return this.skateboards;
            }
            set
            {
                this.skateboards = value;
            }
        }

        public void AddSkateboard(string model, double price)
        {
            Skateboard skateboard = new Skateboard(model, price);
            skateboards.Add(skateboard);
        }

        public double AveragePriceInRange(double start, double end)
        {
            //TODO: Добавете вашия код тук …
            throw new NotImplementedException();
        }

        public List<string> FilterSkateboardsByPrice(double price)
        {
            //TODO: Добавете вашия код тук …
            throw new NotImplementedException();
        }

        public List<Skateboard> SortAscendingByModel()
        {
            //TODO: Добавете вашия код тук …
            throw new NotImplementedException();
        }

        public List<Skateboard> SortDescendingByPrice()
        {
            //TODO: Добавете вашия код тук …
            throw new NotImplementedException();
        }

        public bool CheckSkateboardIsInShop(string model)
        {
            //TODO: Добавете вашия код тук …
            throw new NotImplementedException();
        }

        public string[] ProvideInformationAboutAllSkateboards()
        {
            return skateboards.Select(x => x.ToString()).ToArray();

            // string[] info = new string[skateboards.Count];

            // for (int i = 0; i < skateboards.Count; i++)
            // {
            //     info[i] = skateboards[i].ToString();
            // }

            // return info;
        }
    }
}