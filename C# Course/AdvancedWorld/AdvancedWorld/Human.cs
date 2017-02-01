using System;

namespace AdvancedWorld
{
    public enum Sex { Male, Female };

    public abstract class Human : IHasName
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