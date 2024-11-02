using SharpDX.Direct3D11;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Student
{
    public class Graph
    {
        int size = 9; //Board is 9x9
        public Node[,] grid;

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
                    //Temp again. Debugging purposes.
                }
            }
        }

        public Drag FindRoute(SpelBräde sb)
        {
            foreach (Node node in grid)
            {
                node.FindNeighbors(sb, grid);
                Debug.WriteLine("Node " + node.ToString() +"'s neighbors:");
                foreach (Node neighbor in node.Neighbors)
                {
                    if(neighbor != null)
                        Debug.WriteLine(neighbor.ToString());
                }
            }


            //Insert bfs here. This will find our route from position to the other end of the board./
            Spelare jag = sb.spelare[0];
            Node start = grid[jag.position.X, jag.position.Y];

            List<Node> goals = new List<Node>();
            for (int i = 0; i < 9; i++)
            {
                goals.Add(grid[i, 8]);
            }

            Queue<Node> queue = new Queue<Node>();
            HashSet<Node> visited = new HashSet<Node>();
            Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>(); // Dictionary for storing node origin

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
                    }

                    // We return the move as the next step towards the goal
                    Debug.Print($"BFS Move {jag.position.ToString()} {step.ToString()}");
                    return new Drag
                    {
                        typ = Typ.Flytta,
                        point = step.Position
                    };
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


            Drag fallbackDrag = new Drag
            {
                typ = Typ.Flytta,
                point = start.Position
            };
            fallbackDrag.point.Y++;
            Debug.Print("Fallback Move");
            return fallbackDrag;
        }
    }
}
