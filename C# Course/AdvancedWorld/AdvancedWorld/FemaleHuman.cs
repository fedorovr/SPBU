using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWorld
{
    abstract class FemaleHuman : Human
    {
        public FemaleHuman(String name, int age) 
            : base(name, age, Sex.Female)
        {
        }
    }
}
