//
//  main.cpp
//  CS 260 Lab 8
//
//  Created by Jim Bailey on June 2, 2020.
//  Licensed under a Creative Commons Attribution 4.0 International License.
//
//  Transpiled into C# 6/5/2020 by Katie Strauss
//

using System;
using DGraphClasses;
using WGraphClasses;

namespace GraphsDriver
{
    class Driver
    {
        static void Main(string[] args)
        {
            ConnectTest();
            MinCostTest();

            Console.Write("Press Enter to close.");
            Console.Read();
        }


        static void ConnectTest()
        {
            Console.Write("This test displays a connection table for a directed graph\n\n");

            // create the graph
            DGraph tree1 = new DGraph();

            //add nodes
            tree1.AddNode('A');
            tree1.AddNode('C');
            tree1.AddNode('T');
            tree1.AddNode('Z');
            tree1.AddNode('X');
            tree1.AddNode('K');
            tree1.AddNode('Q');
            tree1.AddNode('J');
            tree1.AddNode('M');
            tree1.AddNode('U');

            // add some edges
            tree1.AddEdge('A', 'C');
            tree1.AddEdge('A', 'T');
            tree1.AddEdge('A', 'Z');
            tree1.AddEdge('X', 'C');
            tree1.AddEdge('C', 'X');
            tree1.AddEdge('C', 'K');
            tree1.AddEdge('T', 'Q');
            tree1.AddEdge('K', 'Q');
            tree1.AddEdge('Q', 'J');
            tree1.AddEdge('J', 'M');
            tree1.AddEdge('Z', 'X');
            tree1.AddEdge('U', 'M');
            tree1.AddEdge('K', 'X');

            // uncomment the next line to see your node list, edge list, and edge matrix
             Debug1(tree1);

            Console.Write("The breadth first min tree from A should be: \n");
            Console.Write(" A-Z A-Z A-T A-C Z-X T-Q C-K Q-J J-M  with U unreachable\n");
            Console.Write(" is: " + tree1.BreadthFirst('A') + "\n\n");

            Console.Write("Note that the order of nodes in your output might differ\n");
            Console.Write("what is important which nodes are reached from each start\n");
            Console.Write("The graph connect table should be: \n");
            Console.Write("A: Z X C K Q J M T \n");
            Console.Write("C: K X Q J M\n");
            Console.Write("T: Q J M\n");
            Console.Write("Z: X C K Q J M\n");
            Console.Write("X: C K Q J M\n");
            Console.Write("K: X C Q J M\n");
            Console.Write("Q: J M\n");
            Console.Write("J: M\n");
            Console.Write("M:\n");
            Console.Write("U: M \n\n");

            Console.Write("The graph connect table is: \n");
            Console.Write(tree1.ConnectTable() + "\n\n");

            Console.Write("End of testing connection table\n\n");
        }

        static void Debug1(DGraph tree1)
        {
            Console.Write("The list of nodes is: \n");
            Console.Write(tree1.ListNodes() + "\n\n");

            Console.Write("The graph edgelist is: \n");
            Console.Write(tree1.DisplayEdges() + "\n\n");

            Console.Write("The graph edge matrix is\n");
            Console.Write(tree1.DisplayMatrix() + "\n");
        }

        static void MinCostTest()
        {
            Console.Write("This test displays a minimum cost spanning tree for a weighted graph\n\n");

            // create the graph
            WGraph tree2 = new WGraph();

            // add nodes
            tree2.AddNode('A');
            tree2.AddNode('C');
            tree2.AddNode('T');
            tree2.AddNode('Z');
            tree2.AddNode('X');
            tree2.AddNode('K');
            tree2.AddNode('Q');
            tree2.AddNode('J');
            tree2.AddNode('M');
            tree2.AddNode('U');

            // add edges
            tree2.AddWEdge('A', 'C', 3);
            tree2.AddWEdge('A', 'T', 4);
            tree2.AddWEdge('A', 'Z', 2);
            tree2.AddWEdge('X', 'C', 4);
            tree2.AddWEdge('C', 'K', 8);
            tree2.AddWEdge('T', 'Q', 4);
            tree2.AddWEdge('K', 'Q', 3);
            tree2.AddWEdge('Q', 'J', 6);
            tree2.AddWEdge('J', 'M', 5);
            tree2.AddWEdge('Z', 'X', 6);

            // uncomment the next line to see your node list, edge list, and edge matrix
            Debug2(tree2);

            Console.Write("The min cost tree should be: \n");
            Console.Write("Q: Q-K Q-T T-A A-Z A-C C-X Q-J J-M \n");

            Console.Write("The min cost tree is: \n");
            Console.Write(tree2.MinCostTree('Q') + "\n");

            Console.Write("Done with testing min cost spanning tree \n\n");

        }

        static void Debug2(WGraph tree2)
        {
            Console.Write("The list of nodes is: \n");
            Console.Write(tree2.ListNodes() + "\n\n");

            Console.Write("The graph edgelist is: \n");
            Console.Write(tree2.DisplayEdges() + "\n\n");

            Console.Write("The graph edge matrix is\n");
            Console.Write(tree2.DisplayMatrix() + "\n");
        }
    }
}