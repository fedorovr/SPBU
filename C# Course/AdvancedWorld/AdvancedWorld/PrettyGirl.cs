using System;

namespace AdvancedWorld
{
    [Couple(pair: "Student", probability: 0.4, childType: "PrettyGirl")]
    [Couple(pair: "Botan", probability: 0.1, childType: "PrettyGirl")]
    public sealed class PrettyGirl : FemaleHuman
    {
        public PrettyGirl(String name, int age)
            : base(name, age)
        {
        }

        public override void Print()
        {
            ConsoleColor defaultConsoleForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Pretty girl with name " + Name + ", age " + Age + ", sex " + Sex);
            Console.ForegroundColor = defaultConsoleForeground;

            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
