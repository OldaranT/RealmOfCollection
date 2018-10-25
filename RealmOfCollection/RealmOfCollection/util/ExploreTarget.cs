using System;
using System.Collections.Generic;
using System.Drawing;
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

        public void Render(Graphics g, Color color)
        {
            double leftCorner = position.X - 15;
            double rightCorner = position.Y - 15;
            double size = 15 * 2;
            Pen p = new Pen(color, 10);
            Pen PVelocity = new Pen(Color.Gold, 2);
            Pen PTarget = new Pen(Color.Red, 2);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        }
    }
}
