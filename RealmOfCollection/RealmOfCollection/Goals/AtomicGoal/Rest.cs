using RealmOfCollection.behaviour;
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

        private int timer;
        public Rest(Hunter player) : base(player)
        {

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

            //Check if the hunter is out of stamina. If so rest.
            EvaluateStamina();

            return status;
        }

        public override void Terminate() { }

        public void EvaluateStamina()
        {

            if (hunter.stamina < Hunter.STAMINA_LIMIT)
            {
               hunter.stamina += 0.5d;
            }
            else
            {
                if (hunter.stamina > Hunter.STAMINA_LIMIT)
                {
                    hunter.stamina = Hunter.STAMINA_LIMIT;
                }
                status = Status.Completed;
            }
        }
    }
}
