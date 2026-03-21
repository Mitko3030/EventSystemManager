using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{
    public class Hat
    {
        private string type;
        private string color;
        private double price;

        public Hat(string type, string color, double price)
        {
            Type = type;
            Color = color;
            Price = price;
            throw new NotImplementedException();
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value < 101)
                {
                    price = value;
                }
                else
                {
                    throw new ArgumentException("Invalid hat price!");
                }
            }
        }



        public override string ToString()
        {
            return $"Hat {Type} with color {Color} costs {Price:F2}";
            throw new NotImplementedException();
        }

    }
}
