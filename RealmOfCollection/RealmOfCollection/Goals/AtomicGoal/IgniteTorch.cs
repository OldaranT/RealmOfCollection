using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity.MovingEntitys;
using RealmOfCollection.entity.StaticEntitys;

namespace RealmOfCollection.Goals.AtomicGoal
{
    public class IgniteTorch : Goal
    {
        Random RNGjesus;
        int timer = 0;
        TorchObject torch;
        public IgniteTorch(Hunter hunter, TorchObject torch) : base(hunter)
        {
            RNGjesus = World.random;
            this.torch = torch;
        }

        public override void Activate()
        {
            hunter.RemoveAllMovingBehaviours();
            hunter.Velocity = new Vector2D();
            status = Status.Active;
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
            timer++;
            if (timer != 15) return status;

            if (hunter.tinder < Hunter.TINDER_USAGE)
            {
                status = Status.Completed;
                return status;
            }

            int hitDice = RNGjesus.Next(1, 3);

            hunter.tinder -= 10;

            if (hitDice == 1)
            {
                torch.onFire = true;
                hunter.foundTorches.Remove(torch);
                status = Status.Completed;
            }

            return status;
        }

        public override void Terminate()
        {
        }
    }
}
