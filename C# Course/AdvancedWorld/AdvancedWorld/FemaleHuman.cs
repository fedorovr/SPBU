using System;

namespace AdvancedWorld
{
    public abstract class FemaleHuman : Human
    {
        public FemaleHuman(String name, int age) 
            : base(name, age, Sex.Female)
        {
        }
    }
}
