using RealmOfCollection.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.entity.StaticEntitys
{
    public class TorchObject : StaticEntity
    {
        public bool onFire = false;

        public TorchObject(Vector2D pos, World w, Vector2D size) : base(pos, w, size)
        {
        }

        public bool Equals(TorchObject other)
        {
            if (other == null)
            {
                return false;
            }
            return Pos == other.Pos;
        }

        public override void Render(Graphics g)
        {

            float x = (float)Pos.X - 6;
            float y = (float)Pos.Y - 13;
            g.FillRectangle(new SolidBrush(Color.Brown), x, y, 12f, 36f);
            if (!onFire)
            {
                g.FillEllipse(new SolidBrush(Color.Black), x - 2, y - 2, 16f, 16f);
            }
            else
            {
                Brush brush = new SolidBrush(Color.Gold);
                float size = 16f;
                for (int i = -2, j = -2; i <= 14; i += 4, j -= 4)
                {
                    g.FillEllipse(brush, x + i, y + j, size, size);
                    size -= 2;
                }
                //g.FillEllipse(brush, x - 2, y - 2, 16f, 16f);
                //g.FillEllipse(brush, x + 2, y - 6, 14f, 14f);
                //g.FillEllipse(brush, x + 6, y - 10, 12f, 12f);
                //g.FillEllipse(brush, x + 10, y - 14, 10f, 10f);
                //g.FillEllipse(brush, x + 14, y - 18, 8f, 8f);

            }
        }
    }
}
