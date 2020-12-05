using System;

namespace WGraphClasses
{

    public class Node // the obj used to represent a node 
    {
        public char name = '\n'; // the name of this node
        public bool visited = false; // variable indicating if it has been visited
        public Edge connections = null; // null edge ptr
    }

    public class Edge // when using an edgelist you have a set of edges
    {
        public int startIndex = Int32.MaxValue;// startpoint of the edge
        public int endIndex = Int32.MaxValue; // end of the edge
        public int weight = -1; // weight of this edge
        public Edge next = null; // next connected edge ptr
    }

    public class WGraph //graph class, this one has edgelists and connection matrix
    {
        private const int SIZE = 20;// set int size
        private int numNodes = 0;// set nodes
        private Node[] nodeList = new Node[SIZE];// nodelist
        private bool[,] edgeMatrix = new bool[SIZE, SIZE];// set edge matrix

        //private methods
        private int FindNode(char name)
        {
            for (int i = 0; i < numNodes; i++)
            {
                if (nodeList[i].name == name)
                    return i;
            }
            return -1;
        }

        private void ResetVisited()
        {
            for (int i = 0; i < numNodes; i++)
            {
                nodeList[i].visited = false;
            }
        }

        //public methods

        public void AddNode(char name)
        {
            // array is full panic!
            if (numNodes >= SIZE)
            {
                throw new OverflowException("Graph size exceeded!");
            }
            // create node with name initialize with no edges not visited
            Node temp = new Node();
            temp.name = name;
            temp.visited = false;
            temp.connections = null;
            //add to list
            nodeList[numNodes++] = temp;
        }

        public bool AddWEdge(char starts, char ends, int weight)
        {
            // return false if link to self
            if (starts == ends)
                return false;
            // return false if either end does not exist
            int startIndex = FindNode(starts);
            int endIndex = FindNode(ends);
            if (startIndex == -1 || endIndex == -1)
                return false;
            // set the link in the edgeMatrix
            edgeMatrix[startIndex, endIndex] = true;
            // create a new edge and add
            // to the start node’s list of edges
            Edge startEnd = new Edge();
            startEnd.startIndex = starts;
            startEnd.endIndex = endIndex;
            startEnd.weight = weight;
            startEnd.next = nodeList[startIndex].connections;
            nodeList[startIndex].connections = startEnd;

            return true;
        }


        public string ListNodes()
        {
            System.Text.StringBuilder returnlist = new System.Text.StringBuilder(); //make string
            for (int i = 0; i < numNodes; i++) //cycle through the nodelist appending to string
            {
                returnlist.Append(nodeList[i].name.ToString() + " ");
            }
            return returnlist.ToString(); // return string
        }

        public string DisplayEdges()
        {
            System.Text.StringBuilder returnlist = new System.Text.StringBuilder(); // make return string
            for (int i = 0; i < numNodes; i++) // cycle through nodelist
            {
                returnlist.Append(nodeList[i].name.ToString() + ": "); //add to returnlist string
                Edge ptr = nodeList[i].connections; //switch ptr to node's connection
                while (ptr != null) // while this ptr isn't null
                {
                    returnlist.Append(nodeList[ptr.endIndex].name.ToString() + " "); // add to string
                    ptr = ptr.next; // switch to next ptr
                }
                returnlist.Append("\n"); // add spacing
            }
            return returnlist.ToString(); // return string
        }

        public string DisplayMatrix()
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder(); // make new string
            output.Append(" ");// add space to string
            for (int k = 0; k < numNodes; k++)
            {
                output.Append(" " + nodeList[k].name);// add space and list all nodes
            }
            output.Append("\n");// new line to start list
            for (int i = 0; i < numNodes; i++)
            {
                output.Append(nodeList[i].name + " "); // append node name space
                for (int j = 0; j < numNodes; j++)
                {

                    if (edgeMatrix[i, j])
                    {
                        output.Append(1 + " ");// add true or 1
                    }
                    else
                    {
                        output.Append(0 + " ");// add false or 0
                    }

                }
                output.Append("\n");// nl
            }
            return output.ToString();// return string
        }

        public string BreadthFirst(char start)//remove from fifo visit neighbors
        {
            ResetVisited(); // reset visited
            System.Text.StringBuilder output = new System.Text.StringBuilder(); // make string
            Node tempNode; // make tempnode
            System.Collections.Queue tempFifo = new System.Collections.Queue(); // make queue fifo
            tempFifo.Enqueue(nodeList[FindNode(start)]); // add node to que
            tempNode = (Node)tempFifo.Dequeue(); //set node to tempnode. 
            Edge ptr = tempNode.connections;
            do
            {
                if (ptr != null && nodeList[ptr.endIndex].visited == false) // check if visited or end of ptr list
                {
                    if (tempFifo.Count != 0)
                    {
                        tempNode = (Node)tempFifo.Dequeue();
                    }
                    output.Append(nodeList[ptr.endIndex].name + " "); // append output str, startchar - next char in ptr
                    tempNode.visited = true; // mark as visited
                    tempFifo.Enqueue(nodeList[ptr.endIndex]); // add node to fifo

                    while (ptr != null)
                    {
                        if (nodeList[ptr.endIndex].visited != true)
                        {
                            nodeList[ptr.endIndex].visited = true;
                            tempFifo.Enqueue(nodeList[ptr.endIndex]);

                        }
                        ptr = ptr.next; // move to next one.
                    }

                }
                else // if has visited this node, or ptr is null
                {
                    if (tempFifo.Count != 0) // if fifo is not empty
                    {
                        tempNode = (Node)tempFifo.Dequeue(); // pop from fifo, assign to tempnode to move to next ptr branch

                        ptr = tempNode.connections; // set ptr equal to this nodes first edge
                    }
                }


            } while (tempFifo.Count != 0); // while fifo is not empty

            return output.ToString(); // return finished string
        }

        public string DepthFirst(char name)// stub not used yet
        {
            return "this is a stub";
        }
        //show what nodes is connected to this node
        public string ConnectTable()
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder(); // make new string
            foreach (Node c in nodeList) // start iterating through nodes in node list.
            {
                if (c != null) // if it's not null still have more to go
                {
                    output.Append(BreadthFirst(c.name)); // append the route for this breathfirst search
                    output.Append("\n");
                }
                
            }
            return output.ToString(); // spit out what went into string
        }

        public string MinCostTree(char value) // mincosttree
        {
            ResetVisited();
            System.Text.StringBuilder output = new System.Text.StringBuilder(); // string
            System.Collections.Queue tempFifo = new System.Collections.Queue(); // queue
            int count = 0; // int
            Edge[] PQ = new Edge[SIZE]; // pq edge list
            Node tempnode = nodeList[FindNode(value)]; //assigns node found from value to tempnode
            tempnode.visited = true; // marks tempnode visited
            Edge ptr = tempnode.connections; // ptr
 
            
            for (int i = 0; i < SIZE; i++) 
            {

                if (ptr != null)  // making sure ptr not null
                {
                    tempFifo.Enqueue(nodeList[ptr.endIndex]); // add this node from edge of nodelist to enqueue 
                    output.Append(nodeList[FindNode(value)].name + "-" + nodeList[ptr.endIndex].name + " "); // add value's node - and next node into string
                    nodeList[FindNode(value)].visited = true; // mark true
                    PQ[i] = ptr; // assign edge ptr to pq
                    ptr = ptr.next; // next ptr
                }
            }
            // empties array into count
            while (PQ[0] != null)
            {
                count += PQ[0].weight;
                PQ[0] = null;
            }
            
            return output.ToString(); //outputs result
        }
    }
}