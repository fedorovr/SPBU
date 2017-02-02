using System;

namespace World
{
    internal sealed class Botan : Student
    {
        public double AverageMark { get; }

        public Botan(String name, int age, Sex sex, String patronymic, double averageMark)
            : base(name, age, sex, patronymic)
        {
            AverageMark = averageMark;
        }

        public override void Print(ConsoleColor backgroundColor)
        {
            ConsoleColor defaultConsoleForeground = Console.ForegroundColor;
            ConsoleColor defaultConsoleBackground = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine("Created new botan with name " + Name + " " + Patronymic + ", age " + Age + 
                ", sex " + Sex + ", and average mark " + AverageMark);
            
            Console.BackgroundColor = defaultConsoleBackground;
            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
