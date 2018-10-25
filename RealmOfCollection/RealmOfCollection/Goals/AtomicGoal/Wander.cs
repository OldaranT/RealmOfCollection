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
        public Wander(Hunter hunter) : base(hunter)
        {
        }

        public override void Activate()
        {
            hunter.RemoveAllMovingBehaviours();
            hunter.Velocity = new Vector2D();
            hunter.SteeringBehaviors.Add(new WanderBehaviour(hunter, 2500, 50, 0.001, World.random));
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
            Console.WriteLine("Wandering...");
            ActivateIfInactive();
            if(hunter.FoundUnIgnitedTorch())
            {
                status = Status.Completed;
            }
            return status;

        }

        public override void Terminate()
        {

        }
    }
}
