using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.entity
{
    public class Imp : MovingEntity
    {
        public Color VColor { get; set; }
        public World w { get; set; }
       

        public Imp(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D(2, 2);
            Scale = 15;
            this.w = w;
            VColor = Color.Black;
            radius = Scale;
        }

        public float calculateTargetAngle()
        {
            double Angle = 0;
            double DetlaX = w.player.Pos.X - Pos.X;
            double DeltaY = w.player.Pos.Y - Pos.Y;
            double Radiant = 180 / Math.PI;
            if (DetlaX == 0)
            {
                if (DeltaY > 0)
                {
                    Angle = 90;
                }
                else if (DeltaY < 0)
                {
                    Angle = 270;
                }
                else
                {
                    Angle = 0;
                }
            }
            else if (DeltaY == 0 && DetlaX < 0)
            {
                Angle = 180;
            }
            else
            {
                Angle = Math.Atan2(DeltaY, DetlaX);
                Angle *= Radiant;
            }
            return (float)Angle;
        }

        public float calculateVelocityAngle()
        {
            double Angle = 0;
            double DetlaX = Velocity.X;
            double DeltaY = Velocity.Y;
            double Radiant = 180 / Math.PI;
            if (DetlaX == 0)
            {
                if (DeltaY > 0)
                {
                    Angle = 90;
                }
                else if (DeltaY < 0)
                {
                    Angle = 270;
                }
                else
                {
                    Angle = 0;
                }
            }
            else if (DeltaY == 0 && DetlaX < 0)
            {
                Angle = 180;
            }
            else
            {
                Angle = Math.Atan2(DeltaY, DetlaX);
                Angle *= Radiant;
            }
            return (float)Angle;
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            Pen p = new Pen(VColor, 2);
            Pen PVelocity = new Pen(Color.Gold, 2);
            Pen PTarget = new Pen(Color.Red, 2);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            g.DrawLine(p, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));

            if (MyWorld.showEntityInfo)
            {
                //Point to Player where imps hide from.
                g.DrawPie(PTarget, new Rectangle((int)(leftCorner - (size / 2)), (int)(rightCorner - (size / 2)), (int)(size + size), (int)(size + size)), calculateTargetAngle(), 1.0f);

                //Point to where they are wandering to.
                g.DrawPie(PVelocity, new Rectangle((int)(leftCorner - (size / 2)), (int)(rightCorner - (size / 2)), (int)(size + size), (int)(size + size)), calculateVelocityAngle(), 1.0f);
            }


        }
    }
}
