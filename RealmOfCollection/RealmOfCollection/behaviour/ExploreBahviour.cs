using System;
using System.Collections.Generic;
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
                getForce(ref force);
            }
            return force;
        }

        private void getNextPointOfInterest()
        {
            float oldDist = float.MaxValue;
            List<ExploreTarget> targets = ME.MyWorld.exploreTargets;

            foreach(ExploreTarget target in targets)
            {
                if (target.visited)
                {
                    continue;
                }

                float distance = (float)ME.Pos.Distance(target.position);

                if(distance < oldDist)
                {
                    exploreTarget = target;
                    oldDist = distance;
                }
            }

            Path path = pfBehaviour.path;
            string beginning = path.getNearestVertex(ME.Pos);
            string destination;
            if(exploreTarget != null)
            {
                destination = path.getNearestVertex(exploreTarget.position);
            }
            else
            {
                exploreTarget = ME.MyWorld.beginning;
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

                if (!ME.MyWorld.beginning.visited)
                {
                    findXT = false;
                }
                else
                {
                    force = new Vector2D();
                    ME.Velocity = new Vector2D();
                }

                exploreTarget = null;
                pfBehaviour.path.bestPath = null;
            }
        }
    }
}
