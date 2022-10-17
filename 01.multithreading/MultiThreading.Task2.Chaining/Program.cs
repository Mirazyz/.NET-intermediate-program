/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        const int arrayLength = 10;
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            var task1 = Task.Run(() => CreateRandomIntegers());
            var task2 = task1.ContinueWith((task) => MultiplyByRandom(task.Result));
            var task3 = task2.ContinueWith((task) => SortNumbers(task.Result));
            var task4 = task3.ContinueWith((task) => PrintAverage(task.Result));

            // feel free to add your code

            Task.WaitAll(task1, task2, task3, task4);

            Task.WhenAll(task1, task2, task3, task4);

            Console.ReadLine();
        }

        

        private static List<int> CreateRandomIntegers()
        {
            List<int> result = new List<int>();

            for(int i = 0; i < arrayLength; i++)
            {
                result.Add(rnd.Next());
            }

            Console.WriteLine("--- Create random integers ---");
            result.ForEach(n => Console.Write($"{n} "));
            Console.WriteLine();

            return result;
        }

        private static List<int> MultiplyByRandom(List<int> numbers)
        {
            var multiplyBy = rnd.Next();
            for (int i = 0; i < arrayLength; i++)
            {
                numbers[i] *= multiplyBy;
            }

            Console.WriteLine($"--- Multiply by: {multiplyBy} ---");
            numbers.ForEach(n => Console.Write($"{n} "));
            Console.WriteLine();

            return numbers;
        }

        private static List<int> SortNumbers(List<int> numbers)
        {
            numbers.Sort();

            Console.WriteLine("--- Sorting ---");
            numbers.ForEach(n => Console.Write($"{n} "));
            Console.WriteLine();

            return numbers;
        }

        private static void PrintAverage(List<int> result)
        {
            var average = result.Average();

            Console.WriteLine("--- Average --- ");
            Console.WriteLine(average);
        }
    }
}
