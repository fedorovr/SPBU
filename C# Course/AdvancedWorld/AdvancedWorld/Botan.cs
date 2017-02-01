using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWorld
{
    [Couple(pair: "Girl", probability: 0.7, childType: "SmartGirl")]
    [Couple(pair: "PrettyGirl", probability: 1.0, childType: "PrettyGirl")]
    [Couple(pair: "SmartGirl", probability: 0.8, childType: "Book")]
    class Botan : MaleHuman
    {
        public String Patronymic { get; }

        public Botan(String name, int age, String patronymic)
            : base(name, age)
        {
            Patronymic = patronymic;
        }

        public override void Print()
        {
            ConsoleColor defaultConsoleForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("Botan with name " + Name + " " + Patronymic + ", age " + Age + ", sex " + Sex);
            Console.ForegroundColor = defaultConsoleForeground;

            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
