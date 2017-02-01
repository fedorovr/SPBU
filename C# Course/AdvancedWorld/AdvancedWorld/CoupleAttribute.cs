using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWorld
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    class CoupleAttribute : Attribute
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
