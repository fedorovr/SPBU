using System;

namespace AdvancedWorld
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class CoupleAttribute : Attribute
    {
        public String Pair { get; } 
        public double Probability { get; }
        public String ChildType { get; }

        public CoupleAttribute(String pair, double probability, String childType)
        {
            Pair = pair;
            Probability = probability;
            ChildType = childType;
        }
    }
}
