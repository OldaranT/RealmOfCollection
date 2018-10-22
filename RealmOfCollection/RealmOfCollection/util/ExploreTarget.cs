using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.util
{
    public class ExploreTarget
    {
        public readonly Vector2D position;

        public bool visited;


        public ExploreTarget(double x, double y)
        {
            position = new Vector2D(x, y);
        }

        public bool Equals(ExploreTarget other)
        {
            if(other == null)
            {
                return false;
            }
            return position == other.position;
        }
    }
}
