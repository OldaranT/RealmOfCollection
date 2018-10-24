using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic.FuzzySets
{
    public class Triangle : FuzzySet
    {

        public Triangle(double peak, double left, double right) : base(peak)
        {
            PeakPoint = peak;
            LeftOffset = left;
            RightOffset = right;
        }

        //this method calculates the degree of membership for a particular value
        public override double CalculateDOM(double val)
        {
            //test for the case where the triangle's left or right offsets are zero
            //(to prevent divide by zero errors below)
            if (RightOffset.Equals(0.0) && PeakPoint.Equals(val) ||
                LeftOffset.Equals(0.0) && PeakPoint.Equals(val))
            {
                return 1.0;
            }
            //find DOM if left of center
            if ((val <= PeakPoint) && (val >= (PeakPoint - LeftOffset)))
            {
                double grad = 1.0 / LeftOffset;
                return grad * (val - (PeakPoint - LeftOffset));
            }
            //find DOM if right of center

            if (val > PeakPoint && (val < (PeakPoint + RightOffset)))
            {
                double grad = 1.0 / -RightOffset;
                return grad * (val - PeakPoint) + 1.0;
            }
            //out of range of this FLV, return zero

            return 0.0;
        }
    }
}
