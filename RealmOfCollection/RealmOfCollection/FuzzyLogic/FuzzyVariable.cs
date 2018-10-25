using RealmOfCollection.FuzzyLogic.FuzzySets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic
{
    public class FuzzyVariable
    {
        private Dictionary<string, FuzzySet> MemberSets = new Dictionary<string, FuzzySet>();

        //disallow copies
        private FuzzyVariable(FuzzyVariable fv)
        {
            throw new NotImplementedException();
        }

        public FuzzyVariable()
        {
            MinRange = 0;
            MaxRange = 100;
        }

        private double MinRange;
        private double MaxRange;

        private void AdjustRange(double min, double max)
        {
            if(min < MinRange)
            {
                MinRange = min;
            }
            if(max > MaxRange)
            {
                MaxRange = max;
            }
        }



        //the following methods create instances of the sets named in the method
        //name and adds them to the member set map. Each time a set of any type is
        //added the m_dMinRange and m_dMaxRange are adjusted accordingly. All of the
        //methods return a proxy class representing the newly created instance. This
        //proxy set can be used as an operand when creating the rule base.
        public FzSet AddLeftShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            MemberSets.Add(name, new LeftShoulder(peak, peak - minBound, maxBound - peak));

            //adjust range if necessary
            AdjustRange(minBound, maxBound);

            return new FzSet(MemberSets.FirstOrDefault(t => t.Key == name).Value);
        }

        public FzSet AddRightShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            MemberSets.Add(name, new RightShoulder(peak, peak - minBound, maxBound - peak));

            //adjust range if necessary
            AdjustRange(minBound, maxBound);

            return new FzSet(MemberSets.FirstOrDefault(t => t.Key == name).Value);
        }

        public FzSet AddTriangularSet(string name, double minBound, double peak, double maxBound)
        {
            MemberSets.Add(name, new Triangle(peak, peak - minBound, maxBound - peak));

            //adjust range if necessary
            AdjustRange(minBound, maxBound);

            return new FzSet(MemberSets.FirstOrDefault(t => t.Key == name).Value);
        }

        //fuzzify a value by calculating its DOM in each of this variable's subsets
        public void Fuzzify(double val)
        {
            //make sure the value is within the bounds of this variable
            if ((val < MinRange) || (val > MaxRange))
            {
                throw new Exception("<FuzzyVariable::Fuzzify>: value out of range");
            }

            foreach (KeyValuePair<string, FuzzySet> pair in MemberSets)
            {
                pair.Value.SetDOM(pair.Value.CalculateDOM(val));
            }
        }

        //defuzzify the variable using the MaxAv method
        public double DeFuzzifyMaxAv()
        {
            double bottom = 0;
            double top = 0;

            foreach (KeyValuePair<string, FuzzySet> pair in MemberSets)
            {
                bottom += pair.Value.GetDOM();
                top += pair.Value.GetRepresentativeValue() * pair.Value.GetDOM();
            }

            if (bottom == 0)
            {
                return 0;
            }

            return top / bottom;
        }
    }
}

