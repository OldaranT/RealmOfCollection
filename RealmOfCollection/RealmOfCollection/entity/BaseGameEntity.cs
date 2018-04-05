using System;
using System.Drawing;

namespace RealmOfCollection
{
    public abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }
        public World MyWorld { get; set; }

        public BaseGameEntity(Vector2D pos, World w)
        {
            Pos = pos;
            MyWorld = w;
        }

        public abstract void Update(float delta);

        public virtual void Render(Graphics g)
        {
           //g.FillEllipse(Brushes.Blue, new Rectangle((int) Pos.X,(int) Pos.Y, 10, 10));
           //g.DrawEllipse(new Pen(Color.Red), new Rectangle((int)Pos.X, (int)Pos.Y, 50, 50));
        }
        
        
        
    }

    
}
