using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.util
{
    class Wall2D
    {
        protected Vector2D A, B, N;

        public Wall2D()
        {

        }

        public Wall2D(Vector2D A, Vector2D B, Vector2D N)
        {
            CalculateNormal();
        }

        protected void CalculateNormal()
        {
            Vector2D temp = B.Sub(A);
            temp = temp.Normalize();
            N.X = -temp.Y;
            N.Y = temp.X;
        }

        public void Render(Graphics g)
        {
            Pen p = new Pen(new SolidBrush(Color.Black));
            g.DrawLine(p, (float)A.X, (float)A.Y, (float)B.X, (float)B.Y);
        }
    }
}
