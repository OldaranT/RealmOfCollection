using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic.FuzzySets
{
    class LeftShoulder : FuzzySet
    {
        public LeftShoulder(double peak, double left, double right) : base((peak - left + peak) / 2)
        {
            // The base class has peak and offset attributes because all the fuzzySets use it.
            PeakPoint = peak;
            LeftOffset = left;
            RightOffset = right;
        }

        public override double CalculateDOM(double val)
        {
            //test for the case where the left or right offsets are zero
            //(to prevent divide by zero errors below)
            if ((RightOffset == 0.0) && (PeakPoint.Equals(val))
                || (LeftOffset == 0.0) && (PeakPoint.Equals(val)))
            {
                return 1.0;
            } //find DOM if right of center
            else if ((val >= PeakPoint) && (val < (PeakPoint + RightOffset)))
            {
                double grad = 1.0 / -RightOffset;
                return grad * (val - PeakPoint) + 1.0;
            } //find DOM if left of center
            else if ((val < PeakPoint) && (val >= PeakPoint - LeftOffset))
            {
                return 1.0;
            } //out of range of this FLV, return zero
            else
            {
                return 0.0;
            }
        }
    }
}
