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

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 5000;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            world.Update(timeDelta);
            dbPanel1.Invalidate();
        }

        Rectangle Border = new Rectangle(10, 10, 1180, 700);

        Rectangle Border2 = new Rectangle(200, 200, 100, 100);
        Pen p = new Pen(new SolidBrush(Color.Black) , 5);
        Pen p2 = new Pen(new SolidBrush(Color.LimeGreen), 1);

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(p, Border);
            e.Graphics.DrawRectangle(p2, Border2);
            world.Render(e.Graphics);
        }

        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            world.Target.Pos = new Vector2D(e.X, e.Y);
        }

        private void dbPanel1_MouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
