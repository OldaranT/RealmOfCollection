using RealmOfCollection.entity.MovingEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Goals.AtomicGoal
{
    class Rest : Goal
    {
        public Rest(Player player) : base(player)
        {

        }
        public override void Activate()
        {
            throw new NotImplementedException();
        }

        public override void AddSubgoal(Goal g)
        {
            throw new NotImplementedException();
        }

        public override bool HandleMessage(string s)
        {
            throw new NotImplementedException();
        }

        public override int Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
