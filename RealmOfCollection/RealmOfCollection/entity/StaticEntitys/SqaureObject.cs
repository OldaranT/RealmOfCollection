using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.entity.StaticEntitys
{
    public class SqaureObject : StaticEntity
    {
        public SqaureObject(Vector2D pos, World w, Vector2D size) : base(pos, w, size)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Silver), new Rectangle((int)Pos.X, (int)Pos.Y, (int)size.X, (int)size.Y));
            base.Render(g);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Update(float delta)
        {
            base.Update(delta);
        }
    }
}
