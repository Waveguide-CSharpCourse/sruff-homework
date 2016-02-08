/*
    Waveguide C# Course
    Homework 2
    Steve Ruff
    2/8/2016
*/

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

            bool enteringData = true;
            // take user input
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

            // call functions to calculate required information and assign results to variables
            float avg = Average(numbers);
            int sum = Sum(numbers);
            int max = Max(numbers);
            int min = Min(numbers);

            // output the numbers entered
            Console.Write("Numbers entered: ");
            foreach (int value in numbers)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();

            // output the text entered
            Console.Write("Strings entered: ");
            foreach (string value in text)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();

            // output the calculated values
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