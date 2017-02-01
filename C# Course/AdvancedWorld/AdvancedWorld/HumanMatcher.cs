using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWorld
{
    class HumanMatcher
    {
        private static Random random = new Random();

        public static IHasName Couple(Human first, Human second)
        {
            if (first.Sex == second.Sex)
            {
                throw new ParentsEqualGenderException();
            }

            CoupleAttribute firstRelation = GetAttribute(first, second);
            CoupleAttribute secondRelation = GetAttribute(second, first);
            double matchProbability = firstRelation.Probability * secondRelation.Probability;
            if (matchProbability > random.NextDouble())
            {
                String childName = HumanCreator.GetChildName(second);
                Type childType = HumanCreator.GetChildType(firstRelation, secondRelation);
                String childPatronymic = HumanCreator.GetChildPatronymic(childType, first.Sex == Sex.Male ? first.Name : second.Name);
                return HumanCreator.CreateChildWithName(childType, childName, childPatronymic);
            }
            else
            {
                return null;
            }
        }

        private static CoupleAttribute GetAttribute(Human first, Human second)
        {
            var enumerator = new CoupleAttributeEnumerator(first.GetType());
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Pair.Equals(second.GetType().Name))
                {
                    return enumerator.Current;
                }
            }
            throw new ArgumentException();
        }
    }
}
