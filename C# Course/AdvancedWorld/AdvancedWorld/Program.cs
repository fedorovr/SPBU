using System;

namespace AdvancedWorld
{
    internal sealed class Program
    {
        private const string Description = "There is a God console.";
        private const string Invitation = "Press Enter to create new couple or Q\\F10 to exit.";
        private const string WeekendDeclaration = "Sorry, I don't work on Sundays.";
        private const string MatchDescription = "Try to match 2 persons: ";
        private const string NoMatchDescription = "Unfortunately, there is no match between 2 persons...";
        private const string SuccessfulMatchDescription = "Yeah! There is a match and the child is: ";
        private const string SameGenderException = "Both parents have same gender, unable to get a couple.";
        
        static void Main(string[] args)
        {
            Console.WriteLine(Description);
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine(WeekendDeclaration);
                return;
            }
            Console.WriteLine(Invitation);

            bool shouldExit = false;
            while (!shouldExit)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Human maleHuman = HumanCreator.GetMaleHuman();
                    Human femaleHuman = HumanCreator.GetFemaleHuman();
                    Console.WriteLine(MatchDescription);
                    Console.Write('\t');
                    maleHuman.Print();
                    Console.Write('\t');
                    femaleHuman.Print();
                    IHasName child = null;
                    try {
                        child = HumanMatcher.Couple(maleHuman, femaleHuman);
                    }
                    catch (ParentsEqualGenderException)
                    {
                        Console.WriteLine(SameGenderException);
                    }
                    if (child == null)
                    {
                        Console.WriteLine(NoMatchDescription);
                    }
                    else
                    {
                        Console.WriteLine(SuccessfulMatchDescription);
                        Console.Write('\t');
                        child.Print();
                    }
                }
                else if (key.Key == ConsoleKey.Q || key.Key == ConsoleKey.F10)
                {
                    shouldExit = true;
                }
            }
        }
    }
}
