using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWorld
{
    [Couple(pair: "Student", probability: 0.2, childType: "Girl")]
    [Couple(pair: "Botan", probability: 0.5, childType: "Book")]
    sealed class SmartGirl : FemaleHuman
    {
        public SmartGirl(String name, int age)
            : base(name, age)
        {
        }

        public override void Print()
        {
            ConsoleColor defaultConsoleForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("Smart girl with name " + Name + ", age " + Age + ", sex " + Sex);
            Console.ForegroundColor = defaultConsoleForeground;

            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
