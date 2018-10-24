using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic.FuzzySets
{
    public class RightShoulder : FuzzySet
    {
        public RightShoulder(double peak, double left, double right) : base((peak + right + peak) / 2)
        {
            PeakPoint = peak;
            LeftOffset = left;
            RightOffset = right;
        }

        public override double CalculateDOM(double val)
        {
            //check for case where the offset may be zero
            if (RightOffset == 0 && val.Equals(PeakPoint) || LeftOffset == 0.0 && (PeakPoint.Equals(val)))
            {
                return 1.0;
            }
            //find DOM if left of center
            if ((val <= PeakPoint) && (val > (PeakPoint - LeftOffset)))
            {
                double grad = 1.0 / LeftOffset;
                return grad * (val - (PeakPoint - LeftOffset));
            }
            //find DOM if right of center
            if (val > PeakPoint && (val <= PeakPoint + RightOffset))
            {
                return 1.0;
            }
            //out of range of this FLV, return zero
            else
            {
                return 0.0;
            }
        }
    }
}
