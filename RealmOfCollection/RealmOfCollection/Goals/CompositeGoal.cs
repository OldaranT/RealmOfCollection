using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOfCollection.entity.MovingEntitys;

namespace RealmOfCollection.Goals
{
    public abstract class CompositeGoal : Goal
    {
        public CompositeGoal(Hunter hunter) : base(hunter)
        {
            subgoals = new Stack<Goal>();
        }

        public override void AddSubgoal(Goal g)
        {
            subgoals.Push(g);
        }

        public void RemoveAllSubGoals()
        {
            foreach (Goal g in subgoals)
            {
                g.Terminate();
            }
            subgoals.Clear();
        }

        public Status ProcessSubgoals()
        {
            // Remove all completed and failed goals from the front of the subgoal list
            while (subgoals.Count > 0 &&
                   (subgoals.Peek().status == Status.Completed || subgoals.Peek().status == Status.Failed))
            {
                subgoals.Peek().Terminate();
                subgoals.Pop();
            }

            // no more subgoals to process, return "completed"
            if (subgoals.Count <= 0)
            {
                return Status.Completed;
            }

            // If any subgoals remains, process the one at the front of the list
            // Grab the status of the frontmost subgoal
            Status statusOfSubgoals = subgoals.Peek().Process();

            // We have to test for the special case where the frontmost subgoal
            // Reports "completed" and the subgoal list contains additional goals.
            // When this is the case, to ensure the parent keeps processing its
            // subgoal list, the "active" status is returned
            if (statusOfSubgoals == Status.Completed && subgoals.Count > 1)
            {
                return Status.Active;
            }

            return statusOfSubgoals;
        }
    }
}
