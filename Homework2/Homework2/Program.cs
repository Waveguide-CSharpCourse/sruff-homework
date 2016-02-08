using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> text = new List<string>();
            List<int> numbers = new List<int>();

            // Console.OpenStandardOutput();
            bool enteringData = true;
            while (enteringData)
            {
                Console.WriteLine("Enter data. Enter ? when finished.");
                string data = Console.ReadLine();
                if (data == "?")
                {
                    enteringData = false;
                    break;
                }
                int result;
                if (int.TryParse(data, out result))
                {
                    numbers.Add(result);
                }
                else
                {
                    text.Add(data);
                }
            }

            float avg = Average(numbers);
            int sum = Sum(numbers);
            int max = Max(numbers);
            int min = Min(numbers);

            Console.Write("Numbers entered: ");
            foreach (int value in numbers)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();

            Console.Write("Strings entered: ");
            foreach (string value in text)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Average value is: " + avg);
            Console.WriteLine("Sum is: " + sum);
            Console.WriteLine("Max is: " + max);
            Console.WriteLine("Min is: " + min);

            Console.ReadLine();
        }

        static float Average(List<int> nums)
        {
            float avg = (float)nums.Sum() / (float)nums.Count;
            return avg;
        }

        static int Sum(List<int> nums)
        {
            return nums.Sum();
        }

        static int Max(List<int> nums)
        {
            return nums.Max();
        }

        static int Min(List<int> nums)
        {
            return nums.Min();
        }
    }
}