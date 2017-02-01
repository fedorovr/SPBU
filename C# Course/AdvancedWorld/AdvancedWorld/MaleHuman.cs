using System;

namespace AdvancedWorld
{
    public abstract class MaleHuman : Human
    {
        public MaleHuman(String name, int age) 
            : base(name, age, Sex.Male)
        {
        }
    }
}
