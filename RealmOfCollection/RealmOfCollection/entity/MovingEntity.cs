using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.entity
{

    abstract class MovingEntity : BaseGameEntity
    {

        protected Vector2D Heading { get; set; }
        protected Vector2D Side { get; set; }
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }

        public SteeringBehaviour SB { get; set; }

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 150;
            Velocity = new Vector2D();
        }

        public override void Update(float timeElapsed)
        {
            Vector2D SteeringForce = SB.Calculate();

            //Acceleration
            Vector2D acceleration = SteeringForce.divide(Mass);

            //Update velocity
            Velocity.Add(acceleration.Multiply(timeElapsed));

            //Check on maxspeed
            Velocity.truncate(MaxSpeed);

            //Update position
            Pos.Add(Velocity.Multiply(timeElapsed));

            //Update heading
            if (Velocity.LengthSquared() > 0.00000001)
            {
                Heading = Velocity.Normalize();
                Side = Heading.Perp();
            }

            //treat the screen as a toroid


            //Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}
