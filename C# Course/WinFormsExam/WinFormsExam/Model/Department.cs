using System;
using System.Threading;
using WinFormsExam.Controller;

namespace WinFormsExam.Model
{
    public sealed class Department
    {
        public ManualResetEvent ExamStartEvent { get; private set; }
        public String Name { get; private set; }
        private Object locker = new Object();
        private Random random = new Random();
        private const int MinEvaluationTime = 1000;
        private const int MaxEvaluationTime = 3000;
        private StudentsController controller;

        public Department(String name, StudentsController controller)
        {
            Name = name;
            this.controller = controller;
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
                controller.AddStudentToList(student);
                Thread.Sleep(random.Next(MinEvaluationTime, MaxEvaluationTime));
                controller.SetStudentMark(student, random.Next(2, 6));
            }
        }
    }
}
