using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.Goals.AtomicGoal;

namespace RealmOfCollection.Goals.CompositeGoals
{
    public class Brain : CompositeGoal
    {
        private int timer;

        public Brain(Hunter hunter) : base(hunter)
        {
            Console.WriteLine("Created BRAIN goal");
        }

        public override void Activate()
        {
            PickAJob();
            status = Status.Active;
        }

        public override Status Process()
        {
            ActivateIfInactive();

            Status subGoalStatus = ProcessSubgoals();

            if (subGoalStatus == Status.Completed || subGoalStatus == Status.Failed)
            {
                status = Status.Inactive;
            }

            EveluateTime();

            return status;
        }


        public void PickAJob()
        {
            if (status == Status.Inactive)
            {
                Console.WriteLine("Picking a job...");
                // Choose one of the three strategy-level composite goals at random
                int randomPick = World.random.Next(3);

                // 0 = Explore
                // 1 = getResources
                Console.WriteLine("Pick: " + randomPick);

                switch (randomPick)
                {
                    case 0:
                        hunter.brain.AddGoal_Managatorch();
                        break;
                    case 1:
                        hunter.brain.AddGoal_GetResources();
                        break;
                    case 2:
                        hunter.brain.AddGoal_Wander();
                        break;

                }
            }
        }

        public void AddGoal_Managatorch()
        {
            if (!IsPresent<ManageTorch>())
            {
                RemoveAllSubGoals();
                AddSubgoal(new ManageTorch(hunter));

            }

        }

        public void AddGoal_GetResources()
        {
            if (!IsPresent<GetResources>())
            {
                RemoveAllSubGoals();
                AddSubgoal(new GetResources(hunter));

            }

        }

        public void AddGoal_Wander()
        {
            if (!IsPresent<Wander>())
            {
                RemoveAllSubGoals();
                AddSubgoal(new Wander(hunter));

            }

        }

        private bool IsPresent<T>()
        {
            if (subgoals.Count > 0)
            {
                return subgoals.Peek().GetType() is T;
            }

            return false;
        }

        public override void Terminate()
        {
            RemoveAllSubGoals();
            status = Status.Inactive;
        }

        public void EveluateTime()
        {
            timer++;

            if (timer != 30) return;
            
            hunter.stamina -= 2d;
            //Console.WriteLine("Hunter Stamina: " + hunter.stamina);
            //Console.WriteLine("Hunter tinder: " + hunter.tinder);
            //Console.WriteLine("Hunter TINDER: " + hunter.tinder);

            if (hunter.stamina <= 3)
            {
                subgoals.Peek().status = Status.Inactive;
                AddSubgoal(new Rest(hunter));
            }
            EstemateTinderUsage();
            //CheckStamina();

            timer = 0;

        }

        public void CheckStamina()
        {
            //Console.WriteLine("Hunter Stamina: " + hunter.stamina);
            if (hunter.stamina <= 3)
            {
                subgoals.Peek().status = Status.Inactive;
                AddSubgoal(new Rest(hunter));
            }
        }

        public void Render(Graphics g)
        {
            string playerinfo = playerInfo();
            Font font = new Font("arial", 12);
            PointF pos = new PointF((float)hunter.Pos.X - (playerinfo.Length), (float)hunter.Pos.Y - 25);

            g.DrawString(playerinfo, font, Brushes.Green, pos);

            pos = new PointF((float)hunter.Pos.X + 16, (float)hunter.Pos.Y);
            g.DrawString(goalName(), font, Brushes.Black, pos);

            if (subgoals.Count > 0)
            {
                subgoals.Peek().Render(g, font, pos);
            }
        }

        public override string goalName()
        {
            return "Brain";
        }

        private string playerInfo()
        {
            return "STAMINA: " + hunter.stamina + " TINDER: " + hunter.tinder + " TINDER-USAGE: " + Hunter.TINDER_USAGE;
        }

        private void EstemateTinderUsage()
        {
            double tinderUsage = hunter.GetFuzzyModule().CalculateTinderUsage(hunter.stamina, hunter.level);
            Hunter.TINDER_USAGE = tinderUsage;
        }

       
    }
}
