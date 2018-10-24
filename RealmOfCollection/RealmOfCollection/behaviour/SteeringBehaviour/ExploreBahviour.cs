using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity;
using RealmOfCollection.Graphs;
using RealmOfCollection.util;

namespace RealmOfCollection.behaviour
{
    class ExploreBahviour : SteeringBehaviour
    {
        private float searchRadius;
        private bool findXT; //XT == explore target;
        private PathFollowBehaviour pfBehaviour;
        private ExploreTarget exploreTarget;

        public ExploreBahviour() : base() { }

        public ExploreBahviour(MovingEntity me, float searchRadius) : base(me)
        {
            this.searchRadius = searchRadius;
            pfBehaviour = new PathFollowBehaviour(me);
        }

        public override Vector2D Calculate()
        {
            Vector2D force = new Vector2D();
            if (!findXT)
            {
                getNextPointOfInterest();
            }

            if (exploreTarget != null && findXT)
            {
                //force = pfBehaviour.Calculate();
                getForce(ref force);
            }
            return force;
        }

        private void getNextPointOfInterest()
        {
            float oldDist = float.MaxValue;
            List<ExploreTarget> targets = movingEntity.MyWorld.exploreTargets;

            foreach(ExploreTarget target in targets)
            {
                if (target.visited)
                {
                    continue;
                }

                float distance = (float)movingEntity.Pos.Distance(target.position);

                if(distance < oldDist)
                {
                    exploreTarget = target;
                    oldDist = distance;
                }
            }

            Path path = pfBehaviour.path;
            string beginning = path.getNearestVertex(movingEntity.Pos);
            string destination;
            if(exploreTarget != null)
            {
                destination = path.getNearestVertex(exploreTarget.position);
            }
            else
            {
                exploreTarget = movingEntity.MyWorld.beginning;
                destination = path.getNearestVertex(exploreTarget.position);
            }

            if(beginning != "notfound" && destination != "notfound")
            {
                path.bestPath = path.FindBestPath(beginning, destination);
                findXT = true;
            }
        }

        private void getForce(ref Vector2D force)
        {
            force = pfBehaviour.Calculate();
            if (pfBehaviour.arrived)
            {
                pfBehaviour.arrived = false;
                exploreTarget.visited = true;

                if (!movingEntity.MyWorld.beginning.visited)
                {
                    findXT = false;
                }
                else
                {
                    force = new Vector2D();
                    movingEntity.Velocity = new Vector2D();
                    movingEntity.SteeringBehaviors = new List<SteeringBehaviour>();
                }

                exploreTarget = null;
                pfBehaviour.path.bestPath = null;
            }
        }

        public override void Draw(Graphics g)
        {
            pfBehaviour.Draw(g);
        }
    }
}
