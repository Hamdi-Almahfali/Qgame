﻿using SharpDX.Direct3D11;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;

namespace Student
{
    public class Graph
    {
        int size = 9; //Board is 9x9
        public Node[,] grid;
        List<Node> myGoals = new List<Node>();
        List<Node> opponentGoals = new List<Node>();

        public Graph()
        {
            grid = new Node[size, size];
            Initialize();
        }
        public void Initialize()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = new Node(i, j);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                myGoals.Add(grid[i, 8]);
                opponentGoals.Add(grid[i, 0]);
                //Could just initialize the lists with these values but I have no clue how lol
            }
        }

        public Drag MakeMove(SpelBräde sb)
        {
            foreach (Node node in grid)
            {
                node.FindNeighbors(sb, grid);
            }

            //Insert bfs here. This will find our route from position to the other end of the board./
            Spelare jag = sb.spelare[0];
            Node myStart = grid[jag.position.X, jag.position.Y];

            Spelare fiende = sb.spelare[1];
            Node enemyStart = grid[fiende.position.X, fiende.position.Y];

             // Dictionary for storing node origin

            var myResult = FindRoute(sb, myStart, jag, myGoals);
            var enemyResult = FindRoute(sb, enemyStart, fiende, opponentGoals);

            if(myResult.pathLength >= enemyResult.pathLength)
            {
                Debug.Print($"Enemy path is{enemyResult.pathLength}, our path is {myResult.pathLength}.");
                return new Drag
                {
                    typ = Typ.Flytta,
                    point = myResult.nextStep.Position,
                };
            }
            else
            {
                Debug.Print("Our path is shorter");
                return new Drag
                {
                    typ = Typ.Flytta,
                    point = myResult.nextStep.Position,
                };
            }
        }

        public (int pathLength, Node nextStep) FindRoute(SpelBräde sb, Node start, Spelare spelare, List<Node> goals)
        {
            Queue<Node> queue = new Queue<Node>();
            HashSet<Node> visited = new HashSet<Node>();
            Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
            
            int pathLength = 1;
            queue.Enqueue(start);
            visited.Add(start);
            cameFrom[start] = null;

           

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                // Check if the goal is reached
                if (goals.Contains(current))
                {
                    // Here we use the dictionary to trace back the node at the start
                    Node step = current;
                    while (cameFrom[step] != start)
                    {
                        step = cameFrom[step];
                        ++pathLength;
                    }

                    // We return the move as the next step towards the goal
                    Debug.Print($"BFS Move! Pos: {spelare.position.ToString()}, Goal: {step.ToString()}");
                    Debug.Print($"Path length: {pathLength}");
                    return (pathLength, step);
                }

                foreach (Node neighbor in current.Neighbors)
                {
                    if (neighbor == null) continue;

                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        cameFrom[neighbor] = current;
                    }
                }
            }

            Node fallBackNode = new Node(start.Position.X, start.Position.Y + 1);

            Debug.Print("Fallback Move!");
            return (pathLength, fallBackNode);
        }
    }
}
