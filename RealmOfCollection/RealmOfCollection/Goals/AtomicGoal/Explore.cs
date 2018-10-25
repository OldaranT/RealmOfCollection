using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.behaviour;
using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.entity.StaticEntitys;

namespace RealmOfCollection.Goals.CompositeGoals
{
    public class Explore : Goal
    {
        public Explore(Hunter hunter) : base(hunter)
        {
        }

        public override void Activate()
        {

            status = Status.Active;
            hunter.RemoveAllMovingBehaviours();
            hunter.Velocity = new Vector2D();
            hunter.SteeringBehaviors.Add(new ExploreBahviour(hunter, 75F));
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


            if (hunter.FoundUnIgnitedTorch())
            {
                status = Status.Completed;
            }

            return status;
        }

        public override void Terminate()
        {
            hunter.RemoveSteeringBehaviour(new ExploreBahviour());
        }
    }
}
