using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic
{
    class FuzzyModule
    {
        private Dictionary<string, FuzzyVariable> variables;

        private List<FuzzyRule> rules = new List<FuzzyRule>();

        public enum DefuzzyType
        {
            max_av,
            centroid
        }
        public static int SampleNumberAmountForCentroid = 15;

        public FuzzyModule()
        {
            variables = new Dictionary<string, FuzzyVariable>();
        }
        
        //  zeros the DOMs of the consequents of each rule. Used by Defuzzify()
        private void SetConfidencesOfConsequentsToZero()
        {
            rules.ForEach(rule => rule.SetConfidenceOfConsequentToZero());
        }

        public FuzzyVariable CreateFuzzyVariable(string name)
        {
            variables.Add(name, new FuzzyVariable());
            return variables[name];
        }

        public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            rules.Add(new FuzzyRule(antecedent, consequence));
        }

        public void Fuzzify(string variableName, double value)
        {

        }

        public double DeFuzzify(string variableName)
        {
            return double.MaxValue;
        }
    }
}
