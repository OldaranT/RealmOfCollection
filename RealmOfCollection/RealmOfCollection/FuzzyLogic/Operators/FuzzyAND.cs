using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic.Operators
{
    class FuzzyAND : FuzzyTerm
    {
        // an instance of this class may AND together up to 4 terms
        private List<FuzzyTerm> Terms = new List<FuzzyTerm>(4);

        private FuzzyAND(FuzzyAND fuzzyAND)
        {
            Terms = new List<FuzzyTerm>(4);
            foreach (var term in Terms)
            {
                Terms.Add(term.Clone());
            }
        }

        //ctor using two terms
        public FuzzyAND(FuzzyTerm term1, FuzzyTerm term2)
        {
            Terms.Add(term1.Clone());
            Terms.Add(term2.Clone());
        }

        public override void ClearDOM()
        {
            Terms.ForEach(t => t.ClearDOM());
        }

        public override FuzzyTerm Clone()
        {
            return new FuzzyAND(this);
        }

        public override double GetDOM()
        {
            double minDOM = double.PositiveInfinity;

            Terms.ForEach(t =>
            {
                if (t.GetDOM() < minDOM)
                {
                    minDOM = t.GetDOM();
                }
            });
            return minDOM;
        }

        public override void ORwithDOM(double val)
        {
            Terms.ForEach(t => t.ORwithDOM(val));
        }
    }
}
