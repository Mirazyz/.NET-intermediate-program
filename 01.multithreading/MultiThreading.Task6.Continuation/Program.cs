/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // feel free to add your code

            var sucessfullTask = new Task(() => Console.WriteLine("Main task"));
            var faltedTask = new Task(() => throw new Exception());
            var ts = new CancellationTokenSource();
            var ct = ts.Token;
            var cancelledTask = new Task(async () =>
            {
                await Task.Delay(100);
            }, ct);

            // a.
            sucessfullTask.ContinueWith((parent) =>
            {
                Console.WriteLine("Execute regardless of the parent result. Task: success.");
            });

            faltedTask.ContinueWith((parent) =>
            {
                Console.WriteLine("Execute regardless of the parent result. Task: falted.");
            });

            cancelledTask.ContinueWith((parent) =>
            {
                Console.WriteLine("Execute regardless of the parent result. Task: cancelled.");
            });

            // b.
            sucessfullTask.ContinueWith((result) =>
            {
                Console.WriteLine("Execute when parent falted. Task: success.");
            }, TaskContinuationOptions.OnlyOnFaulted);

            faltedTask.ContinueWith((result) =>
            {
                Console.WriteLine("Execute when parent falted. Task: falted");
            }, TaskContinuationOptions.OnlyOnFaulted);

            cancelledTask.ContinueWith((result) =>
            {
                Console.WriteLine("Execute when parent falted. Task: cancelled");
            }, TaskContinuationOptions.OnlyOnFaulted);

            // c.
            sucessfullTask.ContinueWith((result) =>
            {
                if (result.IsFaulted)
                {
                    Console.WriteLine($"Execute when parent faled in the same thread. Task: success.");
                }
            }, ct, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current);

            faltedTask.ContinueWith((result) =>
            {
                if (result.IsFaulted)
                {
                    Console.WriteLine($"Execute when parent faled in the same thread. Task: falted.");
                }
            }, ct, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current);

            cancelledTask.ContinueWith((result) =>
            {
                if (result.IsFaulted)
                {
                    Console.WriteLine($"Execute when parent faled in the same thread. Task: cancelled.");
                }
            }, ct, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current);

            // d.
            sucessfullTask.ContinueWith((result) =>
            {
                Console.WriteLine("Execute when parent is cancelled. Task: success.");
            }, ct, TaskContinuationOptions.OnlyOnCanceled, TaskScheduler.Default);
            
            faltedTask.ContinueWith((result) =>
            {
                Console.WriteLine("Execute when parent is cancelled. Task: falted.");
            }, ct, TaskContinuationOptions.OnlyOnCanceled, TaskScheduler.Default);

            cancelledTask.ContinueWith((result) =>
            {
                Console.WriteLine("Execute when parent is cancelled. Task: cancelled.");
            }, ct, TaskContinuationOptions.OnlyOnCanceled, TaskScheduler.Default);

            
            sucessfullTask.Start();
            faltedTask.Start();
            cancelledTask.Start();
            ts.CancelAfter(10);

            cancelledTask.Wait();

            Console.WriteLine($"success: {sucessfullTask.Status}");
            Console.WriteLine($"fail: {faltedTask.Status}");
            Console.WriteLine($"cancel: {cancelledTask.Status}");

            Console.ReadLine();
        }
    }
}