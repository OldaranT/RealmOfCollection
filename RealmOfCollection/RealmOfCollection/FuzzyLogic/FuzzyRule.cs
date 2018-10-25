using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic
{
    class FuzzyRule
    {
        //usually a composite of several fuzzy sets and operators
        private FuzzyTerm Antecedent;
        // consequence (usually a single fuzzy set, but can be several ANDed together)
        private FuzzyTerm Consequence;

        //it doesn't make sense to allow clients to copy rules
        private FuzzyRule(FuzzyRule fr) { }

        public FuzzyRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            Antecedent = antecedent;
            Consequence = consequence;
        }

        public void SetConfidenceOfConsequentToZero()
        {
            Consequence.ClearDOM();
        }


        //this method updates the DOM (the confidence) of the consequent term with
        //the DOM of the antecedent term. 
        public void Calculate()
        {
            Consequence.ORwithDOM(Antecedent.GetDOM());
        }
    }
}
