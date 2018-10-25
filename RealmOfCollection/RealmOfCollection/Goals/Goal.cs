using RealmOfCollection.entity.MovingEntitys;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Goals
{
    public enum Status
    {
        Inactive = 0,
        Active = 1,
        Completed = 2,
        Failed = 3   
    }
    public abstract class Goal
    {
        public Status status { get; set; }
        protected Hunter hunter { get; set; }
        protected Stack<Goal> subgoals { get; set; }

        protected Goal(Hunter hunter)
        {
            this.hunter = hunter;
            status = Status.Inactive;
        }

        public abstract void Activate();
        public abstract Status Process();
        public abstract void Terminate();
        public abstract string goalName();
        public abstract void AddSubgoal(Goal g);
        public void ActivateIfInactive()
        {
            if (status == Status.Inactive)
            {
                Activate();
            }
        }


        public virtual void Render(Graphics g, Font font, PointF pos)
        {
            pos.X += 30;
            pos.Y += 20;

            g.DrawString(goalName(), font, Brushes.Blue, pos);

            if (subgoals != null && subgoals.Count > 0)
            {
                subgoals.Peek().Render(g, font, pos);
            }
        }


    }
}
