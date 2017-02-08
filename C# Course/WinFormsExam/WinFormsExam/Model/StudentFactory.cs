using System;
using System.Collections.Generic;

namespace WinFormsExam.Model
{
    internal sealed class StudentFactory
    {
        private int idGenerator = 0;
        private Random random = new Random();
        private Department department;
        private List<String> names = new List<string> { "Evgeniy", "Anton", "Ivan", "Pyotr", "Anastasia", "Lolita", "Bonnie", "Lana" };
        private List<String> surnames = new List<string> { "Abramovich", "Demidovich", "Kozak", "Simpson", "Korotkevich", "Devich", "Johnson", "Williamson" };

        public StudentFactory(Department baseDepartment)
        {
            department = baseDepartment;
        }

        public Student GetRandomStudent()
        {
            String studentName = names[random.Next(names.Count)];
            String studentSurname = surnames[random.Next(surnames.Count)];
            return new Student(department, studentName, studentSurname, idGenerator++);
        }
    }
}
