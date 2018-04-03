using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.entity
{
    public class Vehicle : MovingEntity
    {
        public Color VColor { get; set; }
        public World w { get; set; }
        public Vector2D MousePosition { get; set; }
        private bool Player;

        private float PieAngle = 45.0f;

        public Vehicle(Vector2D pos, World w, bool Player) : base(pos, w)
        {
            Velocity = new Vector2D(0, 0);
            Scale = 15;
            this.w = w;
            this.Player = Player;
            VColor = Color.Black;
        }

        public float calculateMouseAngle()
        {
            double Angle = 0;
            double DetlaX = MousePosition.X - Pos.X;
            double DeltaY = MousePosition.Y - Pos.Y;
            double Radiant = 180 / Math.PI;
            if (DetlaX == 0)
            {
                if(DeltaY > 0)
                {
                    Angle = 90;
                }
                else if( DeltaY < 0)
                {
                    Angle = 270;
                }
                else
                {
                    Angle = 0;
                }
            }else if(DeltaY == 0 && DetlaX < 0)
            {
                Angle = 180;
            }
            else
            {
                Angle = Math.Atan2(DeltaY , DetlaX);
                Angle *= Radiant;
            }
            return (float)Angle - (PieAngle/2);
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;



            Pen p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int) leftCorner, (int) rightCorner, (int) size, (int) size));
            g.DrawLine(p, (int) Pos.X, (int) Pos.Y, (int) Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
            if (Player)
            {

                Console.WriteLine(calculateMouseAngle());
                g.DrawPie(p, new Rectangle((int)(leftCorner - (size / 2)), (int)(rightCorner - (size / 2)), (int)(size + size), (int)(size + size)), calculateMouseAngle(), PieAngle);
            }
        }
    }
}
