using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.Graphs
{
    class PathPriorityQueue // The BinaryHeap from week 4 but made GENERIC (Hope it works XD)
    {
        private int currentSize;
        private Path[] array;
        private static int DEFAULT_CAPACITY = 100;

        // Constructors
        public PathPriorityQueue()
        {
            currentSize = 0;
            array = new Path[DEFAULT_CAPACITY];
        }

        public PathPriorityQueue(Path[] collection) //Construct priorityQueue from another collection
        {
            currentSize = collection.Count();
            array = new Path[(currentSize + 2) * 11 / 10];

            int i = 1;
            foreach (Path element in collection)
                array[i++] = element;

            BuildHeap();
        }

        // Functions
        public Path FindMin()
        {
            return array[1];
        }

        public Path Element()
        {
            if (IsEmpty())
                throw new Exception();

            return array[1];
        }

        public bool Add(Path x)
        {
            if (currentSize + 1 == array.Count())
                DoubleArray();

            // Precolate up
            int hole = ++currentSize;
            array[0] = x;

            for (; x.cost < array[hole / 2].cost; hole /= 2)
                array[hole] = array[hole / 2];
            array[hole] = x;

            return true;
        }

        public void AddFreely(Path x) //Excersize 3
        {
            ++currentSize;
            array[currentSize] = x;
        }

        public Path Remove()
        {
            Path minItem = Element();

            array[1] = array[currentSize--];
            PrecolateDown(1);

            return minItem;
        }

        public void BuildHeap() //Excersize 3
        {
            for (int i = currentSize / 2; i > 0; i--)
                PrecolateDown(i);
        }

        public bool IsEmpty() //Added for Graph
        {
            return currentSize == 0;
        }

        private void PrecolateDown(int hole)
        {
            int child;
            Path temporary = array[hole];

            for (; hole * 2 <= currentSize; hole = child)
            {
                child = hole * 2;
                if (child != currentSize && array[child + 1].cost < array[child].cost)
                    child++;

                if (array[child].cost < temporary.cost)
                    array[hole] = array[child];
                else
                    break;
            }

            array[hole] = temporary;
        }

        public void PrintPreOrder() // Driver
        {
            if (currentSize > 0)
                PrintPreOrder(1, 0);
        }

        protected void PrintPreOrder(int place, int depth)
        {
            if (place <= currentSize)
            {
                for (int i = depth; i > 0; i--)
                    Console.Write(" ");

                Console.Write(array[place] + "\n");
                PrintPreOrder(place * 2, depth + 1);
                PrintPreOrder((place * 2) + 1, depth + 1);
            }
        }

        public void TestPrint() // To see index and value in array
        {
            for (int i = 0; i <= currentSize; i++)
            {
                Console.WriteLine("[{0}] => {1}", i, array[i]);
            }
        }

        public void DoubleArray() //Made this myself, not sure if correct
        {
            Path[] newArray = new Path[currentSize * 2];

            int i = 1;
            foreach (Path element in array)
            {
                newArray[i++] = element;
            }

            array = newArray;
        }

    }
}
