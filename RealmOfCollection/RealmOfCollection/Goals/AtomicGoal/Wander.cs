using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.behaviour;
using RealmOfCollection.entity.StaticEntitys;

namespace RealmOfCollection.Goals.AtomicGoal
{
    public class Wander : Goal
    {
        public Wander(Hunter hunter) : base(hunter) { }

        public override void Activate()
        {
            status = Status.Active;

            hunter.RemoveAllMovingBehaviours();
            hunter.Velocity = new Vector2D();
            hunter.SteeringBehaviors.Add(new WanderBehaviour(hunter, World.random));
            hunter.SteeringBehaviors.Add(new CollisionAvoidanceBehaviour(hunter, 20, hunter.MyWorld.Objects, 50));
        }

        public override void AddSubgoal(Goal g)
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();
            if(hunter.FoundUnIgnitedTorchThatAreFound())
            {
                status = Status.Completed;
            }

            return status;
        }

        public override void Terminate() { }

        public override string goalName()
        {
            return "WANDER";
        }
    }
}
