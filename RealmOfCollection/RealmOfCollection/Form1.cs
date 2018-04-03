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

        public const float timeDelta = 0.8f;

        public Form1()
        {
            InitializeComponent();

            world = new World(w: dbPanel1.Width, h: dbPanel1.Height);
            world.Target.MousePosition = new Vector2D(0, 0);

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            world.Update(timeDelta);
            dbPanel1.Invalidate();
        }

        Rectangle Border = new Rectangle(50, 50, 100, 100);
        
        Pen p = new Pen(new SolidBrush(Color.Black) , 5);
        Pen p2 = new Pen(new SolidBrush(Color.LimeGreen), 1);

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(p, Border);
            world.Render(e.Graphics);
        }

        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            world.Target.Pos = new Vector2D(e.X, e.Y);
        }

        private void dbPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            world.Target.MousePosition = new Vector2D(e.X, e.Y);
        }
    }
}
