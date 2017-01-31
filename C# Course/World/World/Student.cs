using System;

namespace World
{
    class Student : Human
    {
        public String Patronymic { get; }

        public Student(String name, int age, Sex sex, String patronymic)
            : base(name, age, sex)
        {
            Patronymic = patronymic;
        }

        public override void Print(ConsoleColor backgroundColor)
        {
            ConsoleColor defaultConsoleForeground = Console.ForegroundColor;
            ConsoleColor defaultConsoleBackground = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine("Created new student with name " + Name + " " + Patronymic + ", age " + Age + ", sex " + Sex);
            Console.ForegroundColor = defaultConsoleForeground;

            Console.BackgroundColor = defaultConsoleBackground;
            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
