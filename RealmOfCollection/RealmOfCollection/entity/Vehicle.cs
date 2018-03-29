using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.entity
{
    class Vehicle : MovingEntity
    {
        public Color VColor { get; set; }
        public World w { get; set; }

        public Vehicle(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 5;
            this.w = w;
            VColor = Color.Black;
        }
        
        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;



            Pen p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int) leftCorner, (int) rightCorner, (int) size, (int) size));
            g.DrawLine(p, (int) Pos.X, (int) Pos.Y, (int) Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
            g.DrawPie(p, new Rectangle((int)(leftCorner - (size/2)), (int)(rightCorner - (size / 2)), (int)(size + size), (int)(size + size)), 0.0f,30.0f);
            //g.DrawRectangle(p, new Rectangle((int)(leftCorner - (size / 2)), (int)(rightCorner - (size / 2)), (int)(size + size), (int)(size + size)));
        }
    }
}
