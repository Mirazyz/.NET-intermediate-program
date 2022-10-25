﻿/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static int number = 10;
        Semaphore semaphore = new Semaphore(1, 10);
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code
            
            CreateThreadJoin(10);
            ThreadPool.QueueUserWorkItem((callback) => CreateThreadJoin(11));

            Console.ReadLine();
        }

        static void CreateThreadJoin(int number)
        {
            if(number > 0)
            {
                new Thread(() =>
                {
                    Console.WriteLine(number);
                    number--;

                    CreateThreadJoin(number);
                }).Start();
            }
        }
    }
}
