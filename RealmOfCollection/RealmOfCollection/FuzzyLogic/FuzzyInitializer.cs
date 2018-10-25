using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.FuzzyLogic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic
{
    static class FuzzyInitializer
    {
        private static FuzzyVariable Stamina;
        private static FuzzyVariable Tinder;
        private static FuzzyVariable TinderUsage;

        public static readonly string Stamina_Name = "Stamina";
        public static readonly string Tinder_Name = "Tinder";
        public static readonly string TinderUsage_Name = "TinderUsage";

        private static FzSet Stamina_Low;
        private static FzSet Stamina_Average;
        private static FzSet Stamina_High;

        public static readonly string Stamina_Low_Name = "Stamina_Low";
        public static readonly string Stamina_Average_Name = "Stamina_Average";
        public static readonly string Stamina_High_Name = "Stamina_High";

        private static FzSet Experiance_Low;
        private static FzSet Experiance_Average;
        private static FzSet Experiance_High;

        public static readonly string Experiance_Low_Name = "Experiance_Low";
        public static readonly string Experiance_Average_Name = "Experiance_Average";
        public static readonly string xperiance_High_Name = "xperiance_High";

        private static FzSet TinderUsage_Low;
        private static FzSet TinderUsage_Average;
        private static FzSet TinderUsage_High;

        public static readonly string TinderUsage_Low_Name = "TinderUsage_Low";
        public static readonly string TinderUsage_Average_Name = "TinderUsage_Average";
        public static readonly string TinderUsage_High_Name = "TinderUsage_High";

        public static void InitializeRules(Hunter hunter)
    {
            FuzzyModule module = hunter.GetFuzzyModule();

            Stamina = module.CreateFuzzyVariable(Stamina_Name);
            Tinder = module.CreateFuzzyVariable(Tinder_Name);
            TinderUsage = module.CreateFuzzyVariable(TinderUsage_Name);

            Stamina_Low = Stamina.AddLeftShoulderSet(Stamina_Low_Name, 0, 25.0, 50.0);
            Stamina_Average = Stamina.AddTriangularSet(Stamina_Average_Name, 25.0, 50.0, 75.0);
            Stamina_High = Stamina.AddRightShoulderSet(Stamina_High_Name, 50.0, 75.0, 100.0);

            Experiance_Low = Tinder.AddLeftShoulderSet(Experiance_Low_Name, 0.0, 25, 50);
            Experiance_Average = Tinder.AddTriangularSet(Experiance_Average_Name, 25, 50, 75);
            Experiance_High = Tinder.AddRightShoulderSet(xperiance_High_Name, 50, 75, 100);

            TinderUsage_Low = TinderUsage.AddLeftShoulderSet(TinderUsage_Low_Name, 0, 2.5, 5);
            TinderUsage_Average = TinderUsage.AddTriangularSet(TinderUsage_Average_Name, 2.5, 5, 7.5);
            TinderUsage_High = TinderUsage.AddRightShoulderSet(TinderUsage_High_Name, 5, 7.5, 10);

            module.AddRule(new FuzzyAND(Stamina_Low, Experiance_Low), TinderUsage_High);
            module.AddRule(new FuzzyAND(Stamina_Low, Experiance_Average), TinderUsage_High);
            module.AddRule(new FuzzyAND(Stamina_Low, Experiance_High), TinderUsage_Average);

            module.AddRule(new FuzzyAND(Stamina_Average, Experiance_Low), TinderUsage_High);
            module.AddRule(new FuzzyAND(Stamina_Average, Experiance_Average), TinderUsage_Average);
            module.AddRule(new FuzzyAND(Stamina_Average, Experiance_High), TinderUsage_Low);

            module.AddRule(new FuzzyAND(Stamina_High, Experiance_Low), TinderUsage_Average);
            module.AddRule(new FuzzyAND(Stamina_High, Experiance_Average), TinderUsage_Average);
            module.AddRule(new FuzzyAND(Stamina_High, Experiance_High), TinderUsage_Low);
        }

    }
}
