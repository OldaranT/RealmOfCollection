using RealmOfCollection.behaviour;
using RealmOfCollection.entity.StaticEntitys;
using RealmOfCollection.Graphs;
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
        public float radius { get; set; }
        public Path path { get; set; }
        private List<SteeringBehaviour> movingBehaviours;


        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 10;
            arriveSpeed = MaxSpeed;
            Max_Force = 25;
            Velocity = new Vector2D();
            Heading = new Vector2D();
            Vector2D temp = Heading;
            Side = temp.PerpRightHand();
            SteeringBehaviors = new List<SteeringBehaviour>();
            SteeringForce = new Vector2D();
            radius = 15;
            path = new Path(MyWorld);
            movingBehaviours = new List<SteeringBehaviour>();

            //List all moving behaviours. 
            movingBehaviours.Add(new ArriveBehaviour());
            movingBehaviours.Add(new SeekBehaviour());
            movingBehaviours.Add(new WanderBehaviour());
            movingBehaviours.Add(new PathFollowBehaviour());
            movingBehaviours.Add(new ExploreBahviour());

        }

        public override void Update(float timeElapsed)
        {
            SteeringForce = SteeringForce.Zero();

            try
            {
                foreach (SteeringBehaviour SB in SteeringBehaviors)
                {
                    SteeringForce += SB.Calculate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("MovingEntity: " + e.Message);
            }


            if (SteeringForce.isZero())
            {
                Velocity = Velocity.Zero();
            }

            SteeringForce = Vector2D.truncate(SteeringForce, Max_Force);

            SteeringForce = SteeringForce / Mass;
            SteeringForce = SteeringForce.Multiply(timeElapsed);

            //Update velocity and truncate
            Velocity = Vector2D.truncate(Velocity + SteeringForce, arriveSpeed);

            Pos = Pos + Velocity;

            //Update heading
            if (Velocity.LengthSquared() > 0.00000001)
            {
                Heading = Velocity.Normalize();
                Side = Heading.PerpRightHand();
            }

            //treat the screen as a toroid
            Vector2D.WrapAround(this.Pos, MyWorld.Width, MyWorld.Height);
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }

        public void RemoveSteeringBehaviour(SteeringBehaviour type)
        {
            SteeringBehaviors.RemoveAll(SB => SB.GetType() == type.GetType());
        }

        public void RemoveAllMovingBehaviours()
        {
            movingBehaviours.ForEach(MB => { SteeringBehaviors.RemoveAll(SB => SB.GetType().Equals(MB.GetType())); });
        }
    }
}
