using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic
{
    public class FuzzyModule
    {
        private Dictionary<string, FuzzyVariable> Variables;

        private List<FuzzyRule> Rules = new List<FuzzyRule>();

        public enum DefuzzyType
        {
            max_av,
            centroid
        }
        public static int SampleNumberAmountForCentroid = 15;

        public FuzzyModule()
        {
            Variables = new Dictionary<string, FuzzyVariable>();
        }
        
        //  zeros the DOMs of the consequents of each rule. Used by Defuzzify()
        private void SetConfidencesOfConsequentsToZero()
        {
            Rules.ForEach(rule => rule.SetConfidenceOfConsequentToZero());
        }

        public FuzzyVariable CreateFuzzyVariable(string name)
        {
            Variables.Add(name, new FuzzyVariable());
            return Variables[name];
        }

        public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            Rules.Add(new FuzzyRule(antecedent, consequence));
        }

        public void Fuzzify(string variableName, double value)
        {
            if (Variables[variableName] == null)
            {
                throw new Exception("<FuzzyModule::Fuzzify>:key not found");
            };
            Variables[variableName].Fuzzify(value);
        }

        public double DeFuzzify(string variableName)
        {
            if (Variables[variableName] == null)
            {
                throw new Exception("<FuzzyModule::Fuzzify>:key not found");
            }



            // Clears the DOMs of all the consequents of all the rules
            SetConfidencesOfConsequentsToZero();

            // process the rules
            foreach (var curRule in Rules)
            {
                curRule.Calculate();
            }

            // now defuzzify the resultant conclusion using the MaxAv
            return Variables[variableName].DeFuzzifyMaxAv();
        }

        public double CalculateTinderUsage(double stamina, double level)
        {
            Fuzzify(FuzzyInitializer.Stamina_Name, stamina);
            Fuzzify(FuzzyInitializer.Experiance_Name, level);

            return DeFuzzify(FuzzyInitializer.TinderUsage_Name);
        }
    }
}
