using System;

namespace DGraphClasses
{
    public class Node // the obj used to represent a node 
    {
        public char name = '\n'; // the name of this node
        public bool visited = false; // variable indicating if it has been visited
        public Edge connections = null; // null edge ptr
    }
    
    public class Edge // when using an edgelist you have a set of edges
    {
        public int endIndex = Int32.MaxValue; // end of the edge
        public Edge next = null; // null edge next ptr
        public int weight = -1; // weight of this edge
    }

    public class DGraph //graph class, this one has edgelists and connection matrix
    {
        private const int SIZE = 20; // set int size
        private int numNodes = 0; // set nodes
        private Node[] nodeList = new Node[SIZE]; // nodelist
        private int[,] edgeMatrix = new int[SIZE, SIZE];// set edge matrix

        //private methods
        //linear search for node return -1 if not found otherwise return it's index
        private int FindNode(char name)
        {
            for(int i = 0; i < numNodes; i++)
            {
                if (nodeList[i].name == name)
                    return i;
            }
            return -1;
        }

        private void ResetVisited()
        {
            for(int i = 0; i < numNodes; i++)
            {
                nodeList[i].visited = false;
            }
        }

        //public methods

        // add node to graph, fail if array full
        public void AddNode(char name)
        {
            // array is full panic!
            if(numNodes >= SIZE)
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

        public bool AddEdge(char starts, char ends, int weight=1)
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
            edgeMatrix[startIndex,endIndex] = weight;
            // create a new edge and add
            // to the start node’s list of edges
            Edge startEnd = new Edge();
            startEnd.endIndex = endIndex;
            startEnd.weight = weight;
            startEnd.next = nodeList[startIndex].connections;
            nodeList[startIndex].connections = startEnd;

            return true;
        }
        
        // return list of nodes as string
        public string ListNodes()
        {
            System.Text.StringBuilder returnlist = new System.Text.StringBuilder(); //make string
            for( int i = 0; i < numNodes;i++) //cycle through the nodelist appending to string
            {
                returnlist.Append(nodeList[i].name.ToString() + " ");
            }
            return returnlist.ToString(); // return string
        }

        //return list of edges per node.
        public string DisplayEdges()
        {
            System.Text.StringBuilder returnlist = new System.Text.StringBuilder(); // make return string
            for(int i = 0; i < numNodes; i++) // cycle through nodelist
            {
                returnlist.Append(nodeList[i].name.ToString() + ": "); //apppend first letter
                Edge ptr = nodeList[i].connections; // set ptr
                while(ptr != null)
                {
                    returnlist.Append(nodeList[ptr.endIndex].name.ToString() + "[" + ptr.weight + "] "); // add to string
                    ptr = ptr.next; // move ptr to next
                }
                returnlist.Append("\n"); // new line
            }
            return returnlist.ToString(); // return string
        }

        // display the adjacency matrix
        // as 0 for no connection and 1 for connection
        // standard output of 2D matrix
        public string DisplayMatrix()
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder(); // make new string
            output.Append(" "); // add space to string
            for (int k = 0; k < numNodes; k++)
            {
                output.Append(" " +nodeList[k].name); // add space and list all nodes
            }
            output.Append("\n"); // new line to start list
            for (int i = 0; i < numNodes; i++)
            {
                output.Append(nodeList[i].name + " "); // append node name space
                for (int j = 0; j < numNodes;j++)
                {

                    if(edgeMatrix[i,j] > 0) // check for true
                    {
                        output.Append(edgeMatrix[i, j] + " ");// add true or 1
                    }
                    else
                    {
                        output.Append(0 + " "); // add false or 0
                    }
                    
                }
                output.Append("\n"); // nl
            }
            return output.ToString(); // return string
        }

        public string BreadthFirst(char start) //remove from fifo visit neighbors
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
                if (tempFifo.Count != 0)
                {
                    tempNode = (Node)tempFifo.Dequeue();
                    ptr = tempNode.connections; // set ptr equal to this nodes first edge
                }

                if (ptr != null) // check if visited or end of ptr list
                {
                    while(ptr != null)
                    {   
                        if (nodeList[ptr.endIndex].visited != true)
                        {
                            nodeList[ptr.endIndex].visited = true;
                            output.Append(tempNode.name + "-" + nodeList[ptr.endIndex].name + "["+ptr.weight+"] "); // append output str, startchar - next char in ptr
                            tempFifo.Enqueue(nodeList[ptr.endIndex]);
                            
                        }
                        ptr = ptr.next; // move to next one.
                    }  
                }
            } while (tempFifo.Count != 0); // while fifo is not empty
         return output.ToString(); // return finished string
        }
        
        public string DepthFirst(char name) // stub not used yet
        {
            return "this is a stub";
        }

        //show what nodes is connected to this node
        public string ConnectTable()
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder(); // make new string
            for (int i = 0; i < SIZE; i++)
            {
                
                if (nodeList[i] != null)
                {
                    output.Append(nodeList[i].name + ": ");
                    output.Append(BreadthFirst(nodeList[i].name));
                    output.Append("\n");
                }
               
            }
            return output.ToString(); // spit out what went into string

        }

        //find what nodes are connected to whatever node char is sent in using matrix
        // preserving for possible future use

        /*
         *  System.Text.StringBuilder output = new System.Text.StringBuilder();
            System.Collections.Generic.List<char> tempList = new System.Collections.Generic.List<char>();
            int tempNodesIndex = FindNode(start);
            Node tempNode = nodeList[tempNodesIndex];
            bool stillmore = true;
            int countOfEdges = 1;
            while (stillmore)
            {
                if (edgeMatrix[tempNodesIndex, countOfEdges] == true)
                {
                    tempList.Add(nodeList[countOfEdges].name);
                    countOfEdges++;
                }
                else 
                    stillmore = false;
            }
            while(tempList.Count != 0)
            {
                output.Append(tempNode.name + "-" + tempList[0].ToString()+ " ");
                tempList.RemoveAt(0);
            }

            return output.ToString();
        */
    }
}
