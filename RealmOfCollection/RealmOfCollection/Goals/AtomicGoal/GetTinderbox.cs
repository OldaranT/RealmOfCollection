using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity.MovingEntitys;

namespace RealmOfCollection.Goals.AtomicGoal
{
    public class GetTinderbox : Goal
    {
        private int timer;
        public GetTinderbox(Hunter hunter) : base(hunter) { }

        public override void Activate()
        {
            hunter.RemoveAllMovingBehaviours();
            hunter.Velocity = new Vector2D();
            status = Status.Active;
        }

        public override void AddSubgoal(Goal g)
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();
            EvaluateTinderBox();
            return status;
        }

        public override void Terminate() { }

        public void EvaluateTinderBox()
        {
            timer++;

            if (timer != 5) return;

            if (hunter.tinder <= Hunter.TINDERBOX_CAPACITY)
            {
                hunter.tinder += 10d;
            }
            else
            {
                if(hunter.tinder > Hunter.TINDERBOX_CAPACITY)
                {
                    hunter.tinder = Hunter.TINDERBOX_CAPACITY;
                }
                status = Status.Completed;
            }

            timer = 0;
        }

        public override string goalName()
        {
            return "GET TINDERBOX";
        }
    }
}
