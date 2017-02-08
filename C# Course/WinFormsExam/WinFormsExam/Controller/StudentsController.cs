using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WinFormsExam.Model;
using WinFormsExam.View;

namespace WinFormsExam.Controller
{
    public sealed class StudentsController
    {
        private IStudentsView view;

        private Random random = new Random();
        private List<Thread> studentThreads = new List<Thread>();
        private String DepartmentName = "Department #1";
        private int MinStudnetsCount = 10;
        private int MaxStudnetsCount = 30;
        private Department department = null;

        public StudentsController(IStudentsView view)
        {
            this.view = view;
        }

        public void CreateStudents()
        {
            if (department == null)
            {
                department = new Department(DepartmentName, this);
                StudentFactory studentFactory = new StudentFactory(department);
                for (int i = 0; i < random.Next(MinStudnetsCount, MaxStudnetsCount); i++)
                {
                    var newStudentThread = new Thread(studentFactory.GetRandomStudent().PassExam);
                    studentThreads.Add(newStudentThread);
                    newStudentThread.Start();
                }
            }
        }

        public void StartExam()
        {
            if (department == null)
            {
                CreateStudents();
            }
            department.StartExam();
            new Thread(CheckIsAliveStudents).Start();
        }

        public void AddStudentToList(Student student) 
        {
            view.AddStudentToList(student);
        }

        public void SetStudentMark(Student student, int mark)
        {
            view.SetStudentMark(student, mark);
        }

        private void CheckIsAliveStudents()
        {
            while (studentThreads.Any(thread => thread.IsAlive))
            {
                Thread.Sleep(1000);
            }
            view.ReportExamFinished();
        }
    }
}
