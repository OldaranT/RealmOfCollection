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
        Random random;
        private int timer;
        TorchObject torch;
        public IgniteTorch(Hunter hunter, TorchObject torch) : base(hunter)
        {
            random = World.random;
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
            ActivateIfInactive();
            if (hunter.tinder < Hunter.TINDER_USAGE)
            {
                Console.WriteLine("No tinder failed to ignite torch");
                //Not enough tinder, task failed.
                status = Status.Failed;
            } else
            {
                IgniteTheTorch();
            }

            return status;
        }

        public override void Terminate()
        {
        }

        public void IgniteTheTorch()
        {

            timer++;
            if (timer != 5) return;
            Console.WriteLine("Lets ignite Tinder: " + hunter.tinder);

            int hitDice = random.Next(3);

            hunter.tinder -= 10;
            Console.WriteLine("Hunter remeaning tinder: " + hunter.tinder);
            if (hitDice == 0)
            {
                torch.onFire = true;
                hunter.foundTorches.Remove(torch);
                status = Status.Completed;
            }
            else if( hunter.tinder < Hunter.TINDER_USAGE)
            {
                Console.WriteLine("I am stupid i wasted all my tinder" + hitDice);
                status = Status.Failed;
            }

            timer = 0;

        }
    }
}
