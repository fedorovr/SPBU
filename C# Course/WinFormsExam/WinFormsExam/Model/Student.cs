using System;
using System.Threading;

namespace WinFormsExam.Model
{
    public sealed class Student
    {
        public int ID { get; private set; }
        public String Name { get; private set; }
        public String Surname { get; private set; }
        private Random random = new Random();
        private Department department;
        private const int MinPreparationTime = 1000;
        private const int MaxPreparationTime = 10000;

        public Student(Department department, String name, String surname, int id)
        {
            this.department = department;
            Name = name;
            Surname = surname;
            ID = id;
        }

        public void PassExam()
        {
            department.ExamStartEvent.WaitOne();
            Thread.Sleep(random.Next(MinPreparationTime, MaxPreparationTime));
            department.ListenStudent(this);
        }
    }
}
