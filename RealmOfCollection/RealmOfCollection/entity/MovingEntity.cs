using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.entity
{

    public abstract class MovingEntity : BaseGameEntity
    {

        public Vector2D Heading { get; set; }
        public Vector2D Side { get; set; }
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public float Max_Force { get; set; }
        public Vector2D SteeringForce;
        public float arriveSpeed { get; set; }
        public SteeringBehaviour SB { get; set; }
        public List<SteeringBehaviour> SteeringBehaviors { get; set; }
        public Vector2D OldPos { get; set; }


        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 10;
            arriveSpeed = MaxSpeed;
            Max_Force = 25;
            Velocity = new Vector2D();
            Heading = new Vector2D();
            Vector2D temp = Heading;
            Side = temp.Perp();
            SteeringBehaviors = new List<SteeringBehaviour>();
            SteeringForce = new Vector2D();
        }

        public override void Update(float timeElapsed)
        {
            SteeringForce.Zero();
            if (Vector2D.InsideRegion(this.Pos, 50, 50, 150, 150))
            {
                OldPos = Pos;
            }
            foreach (SteeringBehaviour SB in SteeringBehaviors)
            {
                SteeringForce += SB.Calculate();
            }

            SteeringForce = Vector2D.truncate(SteeringForce, Max_Force);

            SteeringForce = SteeringForce / Mass;
            //Acceleration
            //Vector2D acceleration = SteeringForce.divide(Mass);

            //Update velocity
            //Velocity.Add(acceleration.Multiply(timeElapsed));
            SteeringForce = SteeringForce.Multiply(timeElapsed);
            //Console.WriteLine("update steeringForce lenght: " + SteeringForce.Length());
            //Console.WriteLine("update velocity length before adding steering force: " + Velocity.Length());
            Velocity = Vector2D.truncate(Velocity + SteeringForce, arriveSpeed);
            //Console.WriteLine("update velocity length after adding steering force: " + Velocity.Length());

            //Check on maxspeed
            //Velocity.truncate(MaxSpeed);

            //Update position
            //Pos.Add(Velocity.Multiply(timeElapsed));

            Pos = Pos + Velocity;

            //Update heading
            if (Velocity.LengthSquared() > 0.00000001)
            {
                Heading = Velocity.Normalize();
                Side = Heading.Perp();
            }

            //treat the screen as a toroid
            Vector2D.WrapAround(this.Pos, MyWorld.Width, MyWorld.Height);
            //Console.WriteLine("Old Pos: " + OldPos + " Cur Pos: " + Pos);
            //if (!Vector2D.InsideRegion(this.Pos, 50,50,150,150))
            //{
            //    Console.WriteLine("I am outside");
            //    this.Pos = OldPos;
            //}
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}
