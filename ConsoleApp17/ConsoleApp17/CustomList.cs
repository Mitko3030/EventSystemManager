using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    public class CustomList<T> where T : IComparable<T>
    {
        private List<T> list = new List<T>();

        public void Add(T item)=> list.Add(item);

        public T Remove(int index)
        {
            T remove=list[index];
            list.RemoveAt(index);
            return remove;  
        }

        public bool Contains(T item)=> list.Contains(item);

        public void Swap(int index1, int indexw2)
        {
            T sth = list[index1];
            list[index1] = list[indexw2];
            list[indexw2] = sth;
        }

        public int CountGreaterThan(T element)
        {
            int count = 0;
            foreach (T item in list)
            {
                if (item.CompareTo(element) > 0)
                    count++;
            }
            return count;
        }
        public T Max() => list.Max();

        public T Min() => list.Min();

        public void Print()
        {
            foreach (T item in list)
                Console.WriteLine(item);
        }
    }
}
