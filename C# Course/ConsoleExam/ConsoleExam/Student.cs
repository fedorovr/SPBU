using System;
using System.Threading;

namespace ConsoleExam
{
    internal sealed class Student
    {
        public String Name { get; private set; }
        public String Surname { get; private set; }
        private Random random = new Random();
        private Department department;
        private const int MinPreparationTime = 1000;
        private const int MaxPreparationTime = 10000;
        
        public Student(Department department, String name, String surname)
        {
            this.department = department;
            Name = name;
            Surname = surname;
        }

        public void PassExam()
        {
            department.ExamStartEvent.WaitOne();
            Thread.Sleep(random.Next(MinPreparationTime, MaxPreparationTime));
            department.ListenStudent(this);
        }
    }
}
