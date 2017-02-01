using System;

namespace AdvancedWorld
{
    public class Book : IHasName
    {
        public Book(String name)
        {
            Name = name;
        }

        public String Name { get; }

        public void Print()
        {
            ConsoleColor defaultConsoleForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("Book with name " + Name + " ");
            Console.ForegroundColor = defaultConsoleForeground;

            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
