using System;

namespace World
{
    class Parent : Human
    {
        public int CountOfChildren { get; }

        public Parent(String name, int age, Sex sex, int countOfChildren)
            : base(name, age, sex)
        {
            CountOfChildren = countOfChildren;
        }

        public override void Print(ConsoleColor backgroundColor)
        {
            ConsoleColor defaultConsoleForeground = Console.ForegroundColor;
            ConsoleColor defaultConsoleBackground = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine("Created new parent with name " + Name + ", age " + Age + ", sex " + Sex + ", and count of children " + CountOfChildren);

            Console.BackgroundColor = defaultConsoleBackground;
            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
