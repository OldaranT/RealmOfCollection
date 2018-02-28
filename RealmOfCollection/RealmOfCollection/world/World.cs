using RealmOfCollection.behaviour;
using RealmOfCollection.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection
{
    class World
    {
        private List<MovingEntity> entities = new List<MovingEntity>();
        public Vehicle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            populate();
        }

        private void populate()
        {
            Vehicle v = new Vehicle(new Vector2D(10,10), this);
            v.VColor = Color.Blue;
            Vehicle v2 = new Vehicle(new Vector2D(250, 100), this);
            v2.VColor = Color.Green;
            Vehicle v3 = new Vehicle(new Vector2D(100, 100), this);
            v3.VColor = Color.Pink;
            entities.Add(v);
            entities.Add(v2);
            entities.Add(v3);

            Target = new Vehicle(new Vector2D(100, 60), this);
            Target.VColor = Color.DarkRed;
            //Target.Scale = 50;
            Target.Pos = new Vector2D(200, 100);
        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in entities)
            {
                me.SB = new SeekBehaviour(me); // restore later
                //Console.WriteLine("Target Position X: " + Target.Pos.X + " and Y: " + Target.Pos.Y);
                me.Update(timeElapsed);
            }  
        }

        public void Render(Graphics g)
        {
            entities.ForEach(e => e.Render(g));
            Target.Render(g);
        }
    }
}
