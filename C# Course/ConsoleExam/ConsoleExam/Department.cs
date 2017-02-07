using System;
using System.Threading;

namespace ConsoleExam
{
    internal sealed class Department
    {
        public ManualResetEvent ExamStartEvent { get; private set; }
        public String Name { get; private set; }
        private Object locker = new Object();
        private Random random = new Random();
        private const int MinEvaluationTime = 1000;
        private const int MaxEvaluationTime = 3000;
        
        public Department(String name)
        {
            Name = name;
            ExamStartEvent = new ManualResetEvent(false);
        }

        public void StartExam()
        {
            ExamStartEvent.Set();
        }

        public void FinishExam()
        {
            ExamStartEvent.Reset();
        }

        public void ListenStudent(Student student)
        {
            lock (locker)
            {
                Console.WriteLine("Student " + student.Name + " " + student.Surname + " arrived");
                Thread.Sleep(random.Next(MinEvaluationTime, MaxEvaluationTime));
                Console.WriteLine("Student has mark " + random.Next(2, 6));
            }
        }

    }
}
