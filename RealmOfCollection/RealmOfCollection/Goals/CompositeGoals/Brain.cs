using System;
using System.Collections.Generic;
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
        }

        public override void Activate()
        {
            PickAJob();
            status = Status.Active;
        }

        public override bool HandleMessage(string s)
        {
            throw new NotImplementedException();
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
                int randomChoice = World.random.Next(2);

                // 0 = Explore
                // 1 = getResources
                Console.WriteLine("Pick: " + randomChoice);

                switch (randomChoice)
                {
                    case 0:
                        hunter.brain.AddGoal_Explore();
                        break;
                    case 1:
                        hunter.brain.AddGoal_GetResources();
                        break;

                }
            }
        }

        public void AddGoal_Explore()
        {
            if (!IsPresent<Explore>())
            {
                RemoveAllSubGoals();
                AddSubgoal(new Explore(hunter));

            }

        }

        public void AddGoal_GetResources()
        {
            if (!IsPresent<WalkPath>())
            {
                RemoveAllSubGoals();
                AddSubgoal(new WalkPath(hunter, hunter.MyWorld.beginning));

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
            Console.WriteLine("Hunter Stamina: " + hunter.stamina);
            //Console.WriteLine("Hunter TINDER: " + hunter.tinder);
            //if(hunter.tinder <= 3)
            //{
            //    subgoals.Peek().status = Status.Inactive;
            //    AddSubgoal(new GetTinderbox(hunter));
            //}

            if (hunter.stamina <= 3)
            {
                subgoals.Peek().status = Status.Inactive;
                AddSubgoal(new Rest(hunter));
            }

            //CheckStamina();

            timer = 0;

        }

        public void CheckStamina()
        {
            Console.WriteLine("Hunter Stamina: " + hunter.stamina);
            if (hunter.stamina <= 3)
            {
                subgoals.Peek().status = Status.Inactive;
                AddSubgoal(new Rest(hunter));
            }
        }
    }
}
