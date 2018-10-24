using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.behaviour;
using RealmOfCollection.entity.MovingEntitys;

namespace RealmOfCollection.Goals.CompositeGoals
{
    public class Explore : CompositeGoal
    {
        public Explore(Hunter hunter) : base(hunter)
        {
        }

        public override void Activate()
        {

            status = Status.Active;

            hunter.SteeringBehaviors.Add(new ExploreBahviour(hunter, 75F));
        }

        public override bool HandleMessage(string s)
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();


            return status;
        }

        public override void Terminate()
        {
        }
    }
}
