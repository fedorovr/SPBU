using System;

namespace AdvancedWorld
{
    [Couple(pair: "Student", probability: 0.7, childType: "Girl")]
    [Couple(pair: "Botan", probability: 0.3, childType: "SmartGirl")]
    public sealed class Girl : FemaleHuman
    {
        public Girl(String name, int age)
            : base(name, age)
        {
        }

        public override void Print()
        {
            ConsoleColor defaultConsoleForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("Girl with name " + Name + ", age " + Age + ", sex " + Sex);
            Console.ForegroundColor = defaultConsoleForeground;

            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
