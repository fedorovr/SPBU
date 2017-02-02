using System;

namespace AdvancedWorld
{
    [Couple(pair: "Girl", probability: 0.7, childType: "Girl")]
    [Couple(pair: "PrettyGirl", probability: 1.0, childType: "PrettyGirl")]
    [Couple(pair: "SmartGirl", probability: 0.5, childType: "Girl")]
    public sealed class Student : MaleHuman
    {
        public String Patronymic { get; }

        public Student(String name, int age, String patronymic)
            : base(name, age)
        {
            Patronymic = patronymic;
        }

        public override void Print()
        {
            ConsoleColor defaultConsoleForeground = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("Student with name " + Name + " " + Patronymic + ", age " + Age + ", sex " + Sex);
            Console.ForegroundColor = defaultConsoleForeground;
            
            Console.ForegroundColor = defaultConsoleForeground;
        }
    }
}
