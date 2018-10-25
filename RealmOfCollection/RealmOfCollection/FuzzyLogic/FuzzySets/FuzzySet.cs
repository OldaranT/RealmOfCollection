using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic.FuzzySets
{
    public abstract class FuzzySet
    {

        //the values that define the shape of this FLV
        protected double PeakPoint;
        protected double LeftOffset;
        protected double RightOffset;

        // The degree of membership of this set.
        protected double DOM;
        

        //this is the maximum of the set's membership function. For instamce, if
        //the set is triangular then this will be the peak point of the triangular.
        //if the set has a plateau then this value will be the mid point of the 
        //plateau. This value is set in the constructor to avoid run-time
        //calculation of mid-point values.
        protected double RepresentativeValue;

        public FuzzySet(double repVal)
        {
            DOM = 0.0;
            RepresentativeValue = repVal;
        }

        //return the degree of membership in this set of the given value. NOTE,
        //this does not set m_dDOM to the DOM of the value passed as the parameter.
        //This is because the centroid defuzzification method also uses this method
        //to determine the DOMs of the values it uses as its sample points.
        public abstract double CalculateDOM(double val);

        //if this fuzzy set is part of a consequent FLV, and it is fired by a rule 
        //then this method sets the DOM (in this context, the DOM represents a
        //confidence level)to the maximum of the parameter value or the set's 
        //existing m_dDOM value
        public void ORwithDOM(double val)
        {
            if (val > DOM)
            {
                DOM = val;
            }
        }

        public double GetDOM()
        {
            return DOM;
        }
        public void SetDOM(double val)
        {
            DOM = val;
        }

        public void ClearDOM()
        {
            DOM = 0.0;
        }

        public double GetRepresentativeValue()
        {
            return RepresentativeValue;
        }
    }
}
