using System;
using WGraphClasses;

namespace PriorityQ
{
    public class PriorityQueue
    {
        Edge[] pq;
        int counter = 0;
        public PriorityQueue(int size=10)
        {
            pq = new Edge[size];
        }
        public void AddItem(Edge item)
        { 
            pq[counter] = item;
            counter++;
        }

        public Edge GetItem()
        {
            
            

            for (int i = 1; i < count(); i++)
            {
                if (pq[i] != null)
                {
                    if (pq[0].weight > pq[i].weight)
                    {
                        Edge swap = pq[0];
                        pq[0] = pq[i];
                        pq[i] = swap;
                    }
                }
            }
           
            counter--;
            return pq[0];
        }

        public int count()
        {
            return counter;
        }
    }
}
