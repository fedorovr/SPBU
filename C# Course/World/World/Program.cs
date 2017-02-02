using System;
using System.IO;

namespace World
{
    internal sealed class Program
    {
        private const string Description = "There is a God console.";
        private const string Invitation = "Enter a number of humans to create.";
        private const string WeekendDeclaration = "Sorry, I don't work on Sundays.";
        private const string WrongInput = "Unable to parse input.";
        private const string OutputFile = "money.txt";

        static void Main(string[] args)
        {
            Console.WriteLine(Description);
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine(WeekendDeclaration);
                return;
            }
            Console.WriteLine(Invitation);
            var userInput = 0;
            var isInt = Int32.TryParse(Console.ReadLine(), out userInput);
            if (isInt)
            {
                God god = new God();
                for (int i = 0; i < userInput; i++)
                {
                    Human hooman = god.CreateHuman();
                    hooman.Print(Console.BackgroundColor);
                    god.CreatePair(hooman).Print(ConsoleColor.Gray);
                }
                DumpMoney(god);
            }
            else
            {
                Console.WriteLine(WrongInput);
            }
        }

        private static void DumpMoney(God god)
        {
            File.WriteAllText(OutputFile, god.GetTotalMoney().ToString());
        }
    }
}
