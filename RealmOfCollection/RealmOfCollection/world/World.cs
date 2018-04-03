﻿using RealmOfCollection.behaviour;
using RealmOfCollection.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection
{
    public class World
    {
        private List<BaseGameEntity> entities = new List<BaseGameEntity>();
        public Vehicle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2D CursorPos { get; set; }

        public Random random { get; set; }

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            random = new Random();
            populate();
            CreateObjects();
        }

        private void populate()
        {
            Vehicle v = new Vehicle(new Vector2D(10,10), this, false);
            v.VColor = Color.Blue;
            Vehicle v2 = new Vehicle(new Vector2D(250, 100), this, false);
            v2.VColor = Color.Green;
            Vehicle v3 = new Vehicle(new Vector2D(100, 100), this, false);
            v3.VColor = Color.Pink;
            entities.Add(v);
            entities.Add(v2);
            entities.Add(v3);

            Target = new Vehicle(new Vector2D(100, 60), this, true);
            Target.VColor = Color.DarkRed;
            //Target.Scale = 50;
            Target.Pos = new Vector2D(200, 100);
        }

        private void CreateObjects()
        {
            entities.Add(new SqaureObject(new Vector2D(300, 300), this, new Vector2D(50, 50)));
        }

        public void Update(float timeElapsed)
        {
            foreach (BaseGameEntity me in entities)
            {
                //if(me is MovingEntity)
                //{
                //    Console.WriteLine("it works");
                //}

                if (double.IsNaN(me.Pos.X))
                {
                    Console.WriteLine();
                }
                var movingEntity = me as MovingEntity;

                if (movingEntity == null)
                    continue;

                movingEntity.SB = new SeekBehaviour(movingEntity); // restore later
                //movingEntity.SB = new WanderBehaviour(movingEntity, 100, 0.01, 200, random); // restore later;
                //Console.WriteLine("Target Position X: " + Target.Pos.X + " and Y: " + Target.Pos.Y);
                movingEntity.Update(timeElapsed);
            }  
        }

        public bool CheckObstruction(Vector2D pos, float size)
        {
            throw new NotImplementedException();
            // foreach door alle static entities
            // 
        }

        public void Render(Graphics g)
        {
            entities.ForEach(e => e.Render(g));

            Target.Render(g);
        }
    }
}
