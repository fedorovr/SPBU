using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleExam
{
    internal sealed class Program
    {
        private static Random random = new Random();
        private static List<Thread> studentThreads = new List<Thread>();

        private static String DepartmentName = "Department #1";
        private static int MinStudnetsCount = 10;
        private static int MaxStudnetsCount = 30;

        static void Main(string[] args)
        {
            Department department = new Department(DepartmentName);
            StudentFactory studentFactory = new StudentFactory(department);
            for (int i = 0; i < random.Next(MinStudnetsCount, MaxStudnetsCount); i++)
            {
                var newStudentThread = new Thread(studentFactory.GetRandomStudent().PassExam);
                studentThreads.Add(newStudentThread);
                newStudentThread.Start();
            }
            Console.WriteLine("Press Enter to start exam or any other key to quit");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                department.StartExam();
            }
            while (studentThreads.Any(thread => thread.IsAlive)) 
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine("Exam finished");
        }
    }
}
