using RealmOfCollection.FuzzyLogic.FuzzySets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic
{
    class FuzzyVariable
    {
        private Dictionary<string, FuzzySet> MemberSets = new Dictionary<string, FuzzySet>();

        //disallow copies
        private FuzzyVariable(FuzzyVariable fv)
        {
            throw new NotImplementedException();
        }

        public FuzzyVariable()
        {

        }

        //private double MinRange;
        //private double MaxRange;



    }
}
