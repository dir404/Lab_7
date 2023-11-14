using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericСalculator
{
    public class Calculator<T>
    {
        public delegate T AddDelegate(T a, T b);
        public delegate T SubtractDelegate(T a, T b);
        public delegate T MultiplyDelegate(T a, T b);
        public delegate T DivideDelegate(T a, T b);

        public AddDelegate Add { get; set; }
        public SubtractDelegate Subtract { get; set; }
        public MultiplyDelegate Multiply { get; set; }
        public DivideDelegate Divide { get; set; }

        public Calculator(AddDelegate add, SubtractDelegate subtract, MultiplyDelegate multiply, DivideDelegate divide)
        {
            Add = add;
            Subtract = subtract;
            Multiply = multiply;
            Divide = divide;
        }

        public T PerformAddition(T a, T b)
        {
            return Add(a, b);
        }

        public T PerformSubtraction(T a, T b)
        {
            return Subtract(a, b);
        }

        public T PerformMultiplication(T a, T b)
        {
            return Multiply(a, b);
        }

        public T PerformDivision(T a, T b)
        {
            if (EqualityComparer<T>.Default.Equals(b, default(T)))
            {
                throw new DivideByZeroException("Ділення на нуль.");
            }
            return Divide(a, b);
        }
    }

    class Program
    {
        static void Main()
        {
           
            Calculator<int> intCalculator = new Calculator<int>((a, b) => a + b, (a, b) => a - b, (a, b) => a * b, (a, b) => a / b);

            Console.WriteLine("Додавання: " + intCalculator.PerformAddition(5, 3));
            Console.WriteLine("Віднімання: " + intCalculator.PerformSubtraction(5, 3));
            Console.WriteLine("Множення: " + intCalculator.PerformMultiplication(5, 3));
            Console.WriteLine("Ділення: " + intCalculator.PerformDivision(5, 3));

            Calculator<double> doubleCalculator = new Calculator<double>((a, b) => a + b, (a, b) => a - b, (a, b) => a * b, (a, b) => a / b);

            Console.WriteLine("Додавання: " + doubleCalculator.PerformAddition(5.0, 3.0));
            Console.WriteLine("Віднімання: " + doubleCalculator.PerformSubtraction(5.0, 3.0));
            Console.WriteLine("Множення: " + doubleCalculator.PerformMultiplication(5.0, 3.0));
            Console.WriteLine("Ділення: " + doubleCalculator.PerformDivision(5.0, 3.0));

            Console.ReadLine();
        }
    }

}
