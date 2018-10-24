using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.behaviour;
using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.Graphs;
using RealmOfCollection.util;

namespace RealmOfCollection.Goals.AtomicGoal
{
    public class WalkPath : Goal
    {
        private ExploreTarget destination;

        public WalkPath(Hunter hunter, ExploreTarget destination) : base(hunter)
        {
            this.destination = destination;
        }

        public override void Activate()
        {
            status = Status.Active;

            hunter.RemoveAllMovingBehaviours();
            Path path = new Path(hunter.MyWorld);
            string start = path.getNearestVertex(hunter.Pos);
            string dest = path.getNearestVertex(destination.position);
            path.bestPath = path.FindBestPath(start, dest);
            hunter.SteeringBehaviors.Add(new PathFollowBehaviour(hunter, path));

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

            if(hunter.Pos.Distance(destination.position) <= 10)
            {
                status = Status.Completed;
            }

            return status;
        }

        public override void Terminate()
        {
            hunter.RemoveSteeringBehaviour(new PathFollowBehaviour());
        }
    }
}
