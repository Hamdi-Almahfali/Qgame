using SharpDX.Direct3D11;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace Student
{
    public class Graph
    {
        public Node[,] grid;
        public List<Drag> illegalWalls;

        int size = 9; //Board is 9x9
        List<Node> myGoals = new List<Node>();
        List<Node> opponentGoals = new List<Node>();
        SpelBräde spelBräde;
        public Graph(SpelBräde spelBräde)
        {
            grid = new Node[size, size];
            Initialize();
            this.spelBräde = spelBräde;
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

        public Drag MakeMove(List<Drag> illegalMoves)
        {
            foreach (Node node in grid)
            {
                node.FindNeighbors(spelBräde, grid);
            }

            Spelare jag = spelBräde.spelare[0];
            Node myStart = grid[jag.position.X, jag.position.Y];

            Spelare fiende = spelBräde.spelare[1];
            Node enemyStart = grid[fiende.position.X, fiende.position.Y];

            var myResult = FindRoute(myStart, jag, myGoals);
            var enemyResult = FindRoute(enemyStart, fiende, opponentGoals);

            if(myResult.pathLength < enemyResult.pathLength || jag.antalVäggar == 0)
            {
                Debug.Print("Our path is shorter");
                Debug.Print($"BFS Move! Pos: {jag.position.ToString()}, Goal: {myResult.nextStep.ToString()}");
                return MoveDrag(myResult.nextStep.Position);
            }

            Drag move;

            if (enemyResult.nextStep.Position.X > enemyStart.Position.X)
            {
                move = PlaceVerticalWall(enemyStart.Position, myResult.nextStep.Position, illegalMoves);
            }
            else if (enemyResult.nextStep.Position.X < enemyStart.Position.X)
            {
                move = PlaceVerticalWall(enemyResult.nextStep.Position, myResult.nextStep.Position, illegalMoves);
            }
            else if (enemyResult.nextStep.Position.Y < enemyStart.Position.Y)
            {
                move = PlaceHorisontalWall(enemyResult.nextStep.Position, myResult.nextStep.Position, illegalMoves);
            }
            else
            {
                move = PlaceHorisontalWall(enemyStart.Position, myResult.nextStep.Position, illegalMoves);
            }

            // If chosen move is in the illegal moves list, fall back to a movement action
            return illegalMoves.Contains(move) ? MoveDrag(myResult.nextStep.Position) : move;
        }

        public (int pathLength, Node nextStep) FindRoute(Node start, Spelare spelare, List<Node> goals)
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

        public Drag MoveDrag(Point position)
        {
            return new Drag
            {
                typ = Typ.Flytta,
                point = position,
            };
        }

        public Drag PlaceVerticalWall(Point position, Point nextStep, List<Drag> illegalMoves)
        {
            if (position.Y != 8 && !spelBräde.vertikalaVäggar[position.X, position.Y +1])
            {
                return new Drag
                {
                    typ = Typ.Vertikal,
                    point = position
                };
            }
            else if (!spelBräde.vertikalaVäggar[position.X, position.Y - 1])
            {
                return new Drag
                {
                    typ = Typ.Vertikal,
                    point = new Point(position.X, position.Y - 1)
                };
            }
            else
            {
                return MoveDrag(nextStep);
            }
        }

        public Drag PlaceHorisontalWall(Point position, Point nextStep, List<Drag> illegalMoves)
        {
            if (position.X != 8 && !spelBräde.horisontellaVäggar[position.X + 1, position.Y])
            {
                return new Drag
                {
                    typ = Typ.Horisontell,
                    point = position
                };
            }
            else if (!spelBräde.horisontellaVäggar[position.X - 1, position.Y])
            {
                return new Drag
                {
                    typ = Typ.Horisontell,
                    point = new Point(position.X - 1, position.Y)
                };
            }
            else
            {
                return MoveDrag(nextStep);
            }
        }
    }
}
