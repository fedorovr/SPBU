using System;

namespace World
{
    internal sealed class CoolParent : Parent
    {
        public int Money { get; }
    
        public CoolParent(String name, int age, Sex sex, int countOfChildren, int money)
            : base(name, age, sex, countOfChildren)
        {
            Money = money;
        }

        public override void Print(ConsoleColor backgroundColor)
        {
            ConsoleColor defaultConsoleForeground = Console.ForegroundColor;
            ConsoleColor defaultConsoleBackground = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = backgroundColor;

            Console.Write("Created new cool parent with name " + Name + ", age " + Age + ", sex " + Sex + ", count of children " + CountOfChildren + ", ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("and money " + Money);

            Console.BackgroundColor = defaultConsoleBackground;
            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
