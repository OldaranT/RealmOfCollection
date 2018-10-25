using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.Goals.AtomicGoal;

namespace RealmOfCollection.Goals.CompositeGoals
{
    public class GetResources : CompositeGoal
    {
        public GetResources(Hunter hunter) : base(hunter)
        {
        }

        public override void Activate()
        {
            AddSubgoal(new GetTinderbox(hunter));
            AddSubgoal(new WalkPath(hunter, hunter.MyWorld.beginning.position));
            status = Status.Active;
        }

        public override bool HandleMessage(string s)
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {

            //Console.WriteLine("Goal: Get resources");
            ActivateIfInactive();
            if (hunter.tinder >= Hunter.TINDERBOX_CAPACITY)
            {
                status = Status.Completed;
                return status;
            }

            status = ProcessSubgoals();

            return status;
        }

        public override void Terminate()
        {
            RemoveAllSubGoals();
        }
    }
}
