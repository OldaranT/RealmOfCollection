using RealmOfCollection.entity.MovingEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Goals
{
    public enum Status
    {
        Inactive = 0,
        Active = 1,
        Completed = 2,
        Failed = 3   
    }
    abstract class Goal
    {
        public Status status { get; set; }
        public Player player { get; set; }
        public Stack<Goal> subgoals { get; set; }

        public Goal(Player player)
        {
            this.player = player;
        }

        public abstract void Activate();
        public abstract int Process();
        public abstract void Terminate();
        public abstract bool HandleMessage(string s);
        public abstract void AddSubgoal(Goal g);

        
    }
}
