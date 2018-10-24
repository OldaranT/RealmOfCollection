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
        public GetTinderbox(Hunter hunter) : base(hunter)
        {
        }

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

        public override bool HandleMessage(string s)
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();
            EvaluateTinderBox();
            return status;
        }

        public override void Terminate()
        {
        }

        public void EvaluateTinderBox()
        {
            if(hunter.tinder < Hunter.TINDERBOX_CAPACITY)
            {
                hunter.tinder += 0.5d;
            }
            else
            {
                if(hunter.tinder > Hunter.TINDERBOX_CAPACITY)
                {
                    hunter.tinder = Hunter.TINDERBOX_CAPACITY;
                }
                status = Status.Completed;
            }
        }
    }
}
