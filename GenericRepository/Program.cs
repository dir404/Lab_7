using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    public class Repository<T>
    {
        private List<T> items;

        public Repository()
        {
            items = new List<T>();
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public List<T> Find(Criteria<T> criteria)
        {
            List<T> result = new List<T>();

            foreach (var item in items)
            {
                if (criteria(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }

    
    public delegate bool Criteria<T>(T item);

    class Program
    {
        static void Main()
        {
            Repository<string> stringRepository = new Repository<string>();
            stringRepository.Add("apple");
            stringRepository.Add("banana");
            stringRepository.Add("orange");

            Criteria<string> criteria = s => s.StartsWith("a");
            List<string> result = stringRepository.Find(criteria);

            Console.WriteLine("Елементи, що задовольняють критерію:");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            Repository<int> intRepository = new Repository<int>();
            intRepository.Add(10);
            intRepository.Add(20);
            intRepository.Add(30);

            Criteria<int> criteriaInt = i => i > 15;
            List<int> resultInt = intRepository.Find(criteriaInt);

            Console.WriteLine("Елементи, що задовольняють критерію:");
            foreach (var item in resultInt)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }

}
