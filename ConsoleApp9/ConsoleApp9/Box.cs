using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.length = length;
            this.width = width;
            this.height = height;
        }

        public double Length
        {
            get { return length; }
            set { length = value; }
        }
        public double Width
        {
            get { return width; } 
            set { width = value; }
        }
        public double Height
        {
            get { return height; }
            set { height = value; }
        }


        

        public double CalculateSurfaceArea()
        {
            return 2 * (length * width + length * height + width * height);
        }

        public double CalculateLateralSurfaceArea()
        {
            return 2 * height * (length + width);
        }

        public double CalculateVolume()
        {
            return length * width * height;
        }
    }
}
