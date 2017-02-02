using System;

namespace World
{
    internal enum Sex { Male, Female };

    internal abstract class Human
    {
        public Sex Sex { get; }
        public String Name { get; }
        public int Age { get; }

        public Human(String name, int age, Sex sex) {
            Name = name;
            Age = age;
            Sex = sex;
        }

        public abstract void Print(ConsoleColor backgroundColor);
    }
}
