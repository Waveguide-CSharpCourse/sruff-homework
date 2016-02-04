/*
    Waveguide C# Course
    Homework 1
    Steve Ruff
    2/4/2016
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            int inputLength = 0;    // initial value of user input
            List<String> stringList = new List<String>();
            while (inputLength < 4 || inputLength > 10) // loop until we receive between 4 and 10 values
            {
                    Console.Write("Please enter between 4 and 10 integers separated by spaces: ");
                    stringList = Console.ReadLine().Split(' ').ToList();
                    inputLength = stringList.Count;
            }
            // Convert the List<String> to List<int>
            List<int> intList = new List<int>();
            intList = stringList.Select(int.Parse).ToList();

            // Print the sum
            Console.WriteLine("Sum: " + intList.Sum());

            // Print the list in ascending order
            Console.WriteLine("Sort (ascending):");
            intList.Sort();
            PrintList(intList);

            // Print the list in descending order
            Console.WriteLine("Sort (descending):");
            intList.Reverse();
            PrintList(intList);

            // Print the average
            Console.WriteLine("Average: " + intList.Average());

            // Print the median
            if (intList.Count() % 2 != 0)
            {
                intList.Sort();
                Console.WriteLine("Median: " + intList[(intList.Count() - 1) / 2]);
            } else
            {
                intList.Sort();
                double middleMean = ((double)intList[(intList.Count() - 1)/2] + (double)intList[(intList.Count()/2)])/2;
                Console.WriteLine("Median: " + middleMean);
            }

            // Print the square of the sum
            Console.WriteLine("Square of the sum: " + Math.Pow(intList.Sum(), 2));

            // Print the square root of the sum
            Console.WriteLine("Square root of the sum: " + Math.Sqrt((double)intList.Sum()));
        }

        static void PrintList(List<int> L)
        {
            foreach (int i in L)
            {
                Console.WriteLine(i);
            }
        }
    }
}
