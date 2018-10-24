using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity.MovingEntitys;

namespace RealmOfCollection.Goals.CompositeGoals
{
    public class ManageTorch : CompositeGoal
    {
        public ManageTorch(Hunter hunter) : base(hunter)
        {
        }

        public override void Activate()
        {
            throw new NotImplementedException();
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
