using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.entity.StaticEntitys;
using RealmOfCollection.Goals.AtomicGoal;

namespace RealmOfCollection.Goals.CompositeGoals
{
    public class ManageTorch : CompositeGoal
    {
        private TorchObject torchObject;

        public ManageTorch(Hunter hunter, TorchObject torchObject) : base(hunter)
        {
            this.torchObject = torchObject;
        }

        public override void Activate()
        {
            AddSubgoal(new IgniteTorch(hunter, torchObject));

            AddSubgoal(new WalkPath(hunter, torchObject.Pos));

            if (hunter.tinder < Hunter.TINDER_USAGE) 
            {
                AddSubgoal(new GetTinderbox(hunter));

                AddSubgoal(new GetResources(hunter));
            } else
            {
            }
        }

        public override bool HandleMessage(string s)
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
