using RealmOfCollection.behaviour;
using RealmOfCollection.Graphs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealmOfCollection
{
    public partial class Form1 : Form
    {
        World world;
        System.Timers.Timer timer;
        bool up;
        bool down;
        bool left;
        bool right;

        public const float timeDelta = 0.8f;

        public Form1()
        {
            InitializeComponent();

            up = false;
            down = false;
            left = false;
            right = false;

            world = new World(w: dbPanel1.Width, h: dbPanel1.Height);
            world.player.MousePosition = new Vector2D(0, 0);

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            world.Update(timeDelta);
            dbPanel1.Invalidate();

            if (up)
            {
                world.player.Pos.Y -= 1;
            }

            if (down)
            {
                world.player.Pos.Y += 1;
            }

            if (left)
            {
                world.player.Pos.X -= 1;
            }

            if (right)
            {
                world.player.Pos.X += 1;
            }
        }
        
        Pen p = new Pen(new SolidBrush(Color.Black) , 5);
        Pen p2 = new Pen(new SolidBrush(Color.LimeGreen), 1);

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            world.Render(e.Graphics);
        }

        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            string source = world.player.path.getNearestVertex(world.player.Pos);
            string destination = world.player.path.getNearestVertex(new Vector2D(e.X, e.Y));

            if (source != "notfound" && destination != "notfound")
            {
                Vertex path = world.player.path.FindBestPath(source, destination);
                world.player.SteeringBehaviors = new List<SteeringBehaviour>();
                world.player.SteeringBehaviors.Add(new PathFollowBehaviour(world.player));
            }

           //world.player.Pos = new Vector2D(e.X, e.Y);
        }

        private void dbPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            world.player.MousePosition = new Vector2D(e.X, e.Y);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.G)
            {
                world.showGraph = !world.showGraph;
            }

            if (e.KeyData == Keys.H)
            {
                world.showEntityInfo = !world.showEntityInfo;
            }

            if (e.KeyData == Keys.W)
            {
                up = true;
            }

            if (e.KeyData == Keys.S)
            {
                down = true;
            }

            if (e.KeyData == Keys.A)
            {
                left = true;
            }

            if (e.KeyData == Keys.D)
            {
                right = true;
            }

            switch (e.KeyData)
            {
                case Keys.W:
                case Keys.A:
                case Keys.S:
                case Keys.D:
                    world.player.path.bestPath = null;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.W)
            {
                up = false;
            }
            if (e.KeyData == Keys.S)
            {
                down = false;
            }
            if (e.KeyData == Keys.A)
            {
                left = false;
            }
            if (e.KeyData == Keys.D)
            {
                right = false;
            }
        }
    }
}
