using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    public class Scale<T> where T : IComparable<T>
    {
        private T left;
        private T right;

        public Scale(T left, T right)
        {
            this.left = left;
            this.right = right;
        }


        public T GetHeavier()
        {
            int comparison = left.CompareTo(right);
            if (comparison < 0)
            {
                return right;
            }
            else if (comparison > 0)
            {
                return left;
            }
            else
            {
                return default(T);
            }
        }

        public T Left
        {
            get { return left; }
            set { left = value; }
        }
        public T Right
        { 
            get { return right; } 
            set { right = value; } 
        }
    }
}
