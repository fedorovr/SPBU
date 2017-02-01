using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedWorld
{
    public class HumanCreator
    {
        private static Random random = new Random();
        private static string malePatronymic = "ович";
        private static string femalePatronymic = "овна";
        private static List<String> maleNames = new List<String> { "Артем", "Иван", "Глеб" };
        private static List<String> femaleNames = new List<String> { "Анастасия", "Ксения", "Алиса" };

        public static IHasName CreateChildWithName(Type childType, String childName, String childPatronymic)
        {
            var constructor = childType.GetConstructors().First();
            switch (constructor.GetParameters().Length)
            {
                case 1:
                    return (IHasName)Activator.CreateInstance(childType, childName);
                case 2:
                    return (IHasName)Activator.CreateInstance(childType, childName, GetRandomAge());
                case 3:
                    return (IHasName)Activator.CreateInstance(childType, childName, GetRandomAge(), childPatronymic);
                default:
                    throw new ArgumentException("Can't construct instance for type " + childType);
            }
        }

        public static String GetChildPatronymic(Type childType, String fatherName)
        {
            if (childType.IsSubclassOf(typeof(MaleHuman)))
            {
                return fatherName + malePatronymic;
            }
            else if (childType.IsSubclassOf(typeof(FemaleHuman)))
            {
                return fatherName + femalePatronymic;
            }
            else
            {
                return "";
            }
        }

        public static Type GetChildType(CoupleAttribute firstRelation, CoupleAttribute secondRelation)
        {
            if (firstRelation.ChildType != secondRelation.ChildType)
            {
                throw new Exception("Parents have different child expectations");
            }
            return Type.GetType(typeof(HumanCreator).Namespace + '.' + firstRelation.ChildType);
        }

        public static String GetChildName(Human human)
        {
            try
            {
                var namingMethod = human.GetType().GetMethods().First(x => x.ReturnType == typeof(String));
                return namingMethod.Invoke(human, null) as String;
            }
            catch (Exception e)
            {
                throw new Exception("Child naming is not supported by the human", e);
            }
        }

        public static Human GetMaleHuman()
        {
            String humanName = GetRandomName(Sex.Male);
            switch (random.Next(2))
            {
                case 0:
                    return new Student(humanName, GetRandomAge(), GetPatronymicName(GetRandomName(Sex.Male), Sex.Male));
                default:
                    return new Botan(humanName, GetRandomAge(), GetPatronymicName(GetRandomName(Sex.Male), Sex.Male));
            }
        }

        public static Human GetFemaleHuman()
        {
            String humanName = GetRandomName(Sex.Female);
            switch (random.Next(3))
            {
                case 0:
                    return new Girl(humanName, GetRandomAge());
                case 1:
                    return new SmartGirl(humanName, GetRandomAge());
                default:
                    return new PrettyGirl(humanName, GetRandomAge());
            }
        }

        private static String GetRandomName(Sex sex)
        {
            switch (sex)
            {
                case Sex.Male:
                    return maleNames[random.Next(maleNames.Count)];
                case Sex.Female:
                    return femaleNames[random.Next(femaleNames.Count)];
                default:
                    throw new ArgumentException();
            }
        }

        private static int GetRandomAge()
        {
            return random.Next(20, 40);
        }

        private static String GetPatronymicName(String parentName, Sex childSex)
        {
            switch (childSex)
            {
                case Sex.Male:
                    return parentName + malePatronymic;
                case Sex.Female:
                    return parentName + femalePatronymic;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
