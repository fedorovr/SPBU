using System;

namespace AdvancedWorld
{
    enum Sex { Male, Female };

    abstract class Human : IHasName
    {
        public Sex Sex { get; }
        public String Name { get; }
        public int Age { get; }

        public Human(String name, int age, Sex sex)
        {
            Name = name;
            Age = age;
            Sex = sex;
        }

        public abstract void Print();
    }
}