using System;
using System.Collections.Generic;
using System.Linq;

namespace World
{
    class God : IGod
    {
        private const string malePatronymic = "ович";
        private const string femalePatronymic = "овна";
        private List<Human> createdHumans = new List<Human>();
        private Random random = new Random();
        private List<Sex> sexValues = Enum.GetValues(typeof(Sex)).OfType<Sex>().ToList();
        private List<String> maleNames = new List<String> { "Артем", "Иван", "Глеб" } ;
        private List<String> femaleNames = new List<String> { "Анастасия", "Ксения", "Алиса" } ;

        public Human CreateHuman()
        {
            switch (createdHumans.Count)
            {
                case 0:
                    return CreateHuman(Sex.Male);
                case 1:
                    return CreateHuman(Sex.Female);
                default:
                    return CreateHuman(GetRandomSex());
            }
        }

        public Human CreateHuman(Sex sex)
        {
            Human human;
            String humanName = GetRandomName(sex);
            switch (random.Next(4))
            {
                case 0:
                    human = new Student(humanName, GetRandomAge(), sex, GetPatronymicName(GetRandomName(Sex.Male), sex));
                    break;
                case 1:
                    human = new Botan(humanName, GetRandomAge(), sex, GetPatronymicName(GetRandomName(Sex.Male), sex), GetRandomMark());
                    break;
                case 2:
                    if (sex == Sex.Male)
                    {
                        human = new Parent(humanName, 2 * GetRandomAge(), sex, GetRandomCountOfChildren());
                    }
                    else
                    {
                        human = new Student(humanName, GetRandomAge(), sex, GetPatronymicName(GetRandomName(Sex.Male), sex));
                    }
                    break;
                default:
                    if (sex == Sex.Male)
                    {
                        human = new CoolParent(humanName, 2 * GetRandomAge(), sex, GetRandomCountOfChildren(), GetRandomMoney());
                    }
                    else
                    {
                        human = new Botan(humanName, GetRandomAge(), sex, GetPatronymicName(GetRandomName(Sex.Male), sex), GetRandomMark());
                    }
                    break;
            }
            createdHumans.Add(human);
            return human;
        }

        public Human CreatePair(Human human)
        {
            if (human is Botan)
            {
                Botan botan = human as Botan;
                return new CoolParent(GetParentName(botan), botan.Age + GetRandomAge(), Sex.Male, 1, (int)Math.Pow(10.0, botan.AverageMark));
            }
            else if (human is CoolParent)
            {
                CoolParent coolParent = human as CoolParent;
                Sex botanSex = GetRandomSex();
                return new Botan(GetRandomName(botanSex), coolParent.Age - GetRandomAge(), botanSex, GetPatronymicName(coolParent.Name, botanSex), Math.Log10(coolParent.Money));
            }
            else if (human is Student)
            {
                Student student = human as Student;
                return new Parent(GetParentName(student), student.Age + GetRandomAge(), Sex.Male, 1);
            }
            else if (human is Parent)
            {
                Parent parent = human as Parent;
                Sex studentSex = GetRandomSex();
                return new Student(GetRandomName(studentSex), parent.Age - GetRandomAge(), studentSex, GetPatronymicName(parent.Name, studentSex));
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public int this[int index] {
            get {
                CoolParent coolParent = createdHumans[index] as CoolParent;
                return (coolParent?.Money).GetValueOrDefault(0);
            }
        }

        public int GetTotalMoney() {
            int totalMoney = 0;
            for (int i = 0; i < createdHumans.Count; i++) {
                totalMoney += this[i];
            }
            return totalMoney;
        }

        private String GetRandomName(Sex sex)
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

        private String GetParentName(Student student)
        {
            switch (student.Sex)
            {
                case Sex.Male:
                    return student.Patronymic.Replace(malePatronymic, "");
                case Sex.Female:
                    return student.Patronymic.Replace(femalePatronymic, "");
                default:
                    throw new ArgumentException();
            }
        }

        private String GetPatronymicName(String parentName, Sex childSex)
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

        private int GetRandomAge()
        {
            return random.Next(20, 40);
        }

        private int GetRandomCountOfChildren()
        {
            return random.Next(12);
        }

        private int GetRandomMoney()
        {
            return random.Next(10000);
        }

        private double GetRandomMark()
        {
            return random.NextDouble() * 2.0 + 3.0;
        }

        private Sex GetRandomSex()
        {
            return sexValues[random.Next(sexValues.Count)];
        }
    }
}
