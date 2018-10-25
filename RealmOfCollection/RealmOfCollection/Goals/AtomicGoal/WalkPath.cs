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
        private Vector2D destination;
        public WalkPath(Hunter hunter, Vector2D destination) : base(hunter)
        {
            this.destination = destination;
        }

        public override void Activate()
        {
            status = Status.Active;
            
            hunter.RemoveAllMovingBehaviours();
            hunter.Velocity = new Vector2D();

            Path path = new Path(hunter.MyWorld);
            string start = path.getNearestVertex(hunter.Pos);
            string dest = path.getNearestVertex(destination);
            path.bestPath = path.FindBestPath(start, dest);

            hunter.SteeringBehaviors.Add(new PathFollowBehaviour(hunter, path));

        }

        public override void AddSubgoal(Goal g)
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            if(hunter.Pos.Distance(destination) < 15d)
            {
                status = Status.Completed;
            }

            return status;
        }

        public override void Terminate()
        {
            hunter.RemoveSteeringBehaviour(new PathFollowBehaviour());
        }

        public override string goalName()
        {
            return "WALK PATH";
        }
    }
}
