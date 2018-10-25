using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.entity.StaticEntitys;
using RealmOfCollection.Goals.AtomicGoal;
using RealmOfCollection.util;

namespace RealmOfCollection.Goals.CompositeGoals
{
    public class ManageTorch : CompositeGoal
    {

        public ManageTorch(Hunter hunter) : base(hunter)
        {
            Console.WriteLine("Created MANAGE TORCH goal");
        }

        public override void Activate()
        {
            //Console.WriteLine("Goal: Manage Torch");
            RemoveAllSubGoals();


            if ( hunter.foundTorches.Count > 0)
            {
                TorchObject torchObject = hunter.foundTorches.First();

                AddSubgoal(new IgniteTorch(hunter, torchObject));

                AddSubgoal(new WalkPath(hunter, torchObject.Pos));

                AddSubgoal(new GetResources(hunter));

            } else
            {
                if (AreAllExplorePointsVisited())
                {
                    AddSubgoal(new Wander(hunter));
                } else
                {
                    AddSubgoal(new Explore(hunter));
                }
            }

            status = Status.Active;
        }

        public override Status Process()
        {
            ActivateIfInactive();

            status = ProcessSubgoals();

            return status;
        }

        public override void Terminate()
        {
            RemoveAllSubGoals();
        }

        private bool AreAllExplorePointsVisited()
        {
            foreach (ExploreTarget exploreTarget in hunter.MyWorld.exploreTargets)
            {
                if(!exploreTarget.visited)
                {
                    return false;
                }
            }

            return true;
        }

        public override string goalName()
        {
            return "MANAGE TORCH";
        }
    }
}
