using RealmOfCollection.FuzzyLogic.FuzzySets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic
{
    class FzSet : FuzzyTerm
    {
        public FuzzySet Set;

        public FzSet(FuzzySet fs)
        {
            Set = fs;
        }

        private FzSet(FzSet con)
        {
            Set = con.Set;
        }
        
        public override FuzzyTerm Clone()
        {
            return new FzSet(this);
        }

        public override double GetDOM()
        {
            return Set.GetDOM();
        }

        public override void ClearDOM()
        {
            Set.ClearDOM();
        }

        public override void ORwithDOM(double val)
        {
            Set.ORwithDOM(val);
        }
    }
}
