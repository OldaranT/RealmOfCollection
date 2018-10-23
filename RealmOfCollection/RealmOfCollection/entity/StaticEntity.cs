using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.entity
{
    public abstract class StaticEntity : BaseGameEntity
    {
        public Vector2D size;
        public Vector2D center;
        public StaticEntity(Vector2D pos, World w, Vector2D size) : base(pos, w)
        {
            this.size = size;
            center = pos + (size / 2);
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
            base.Render(g);
            g.FillEllipse(new SolidBrush(Color.Black), new Rectangle((int)center.X - 5, (int)center.Y - 5, 10, 10));
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }
    }
}
