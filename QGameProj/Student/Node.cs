using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Student
{
    public class Node
    {
        int[] horizontalNeighbors = {0, 1, 0, -1};
        int[] verticalNeighbors = { 1, 0, -1, 0 };
        Point position;

        public Node(int x, int y) 
        {
            position.X = x;
            position.Y = y;
        }

        List<Node> neighbors = new List<Node>();
        

        public Point Position { get { return position; } }

        public override string ToString()
        {
            return $"({position.X}, {position.Y})";
        }

        public void FindNeighbors()
        {
            for (int i = 0; i < 3; i++) 
            {
                int xNeighbor = position.X + horizontalNeighbors[i];
                int yNeighbor = position.Y + verticalNeighbors[i];
                
                if(xNeighbor < 8 && xNeighbor > 0 && yNeighbor < 8 && yNeighbor > 0)
                {
                    Node neighbor = new Node(xNeighbor, yNeighbor);
                    neighbors.Add(neighbor);

                    //Insert wall check here
                    CheckForWallLeft();
                    CheckForWallRight();
                    CheckForWallAbove();
                    CheckForWallBelow();
                }
            }
        }

        public void CheckForWallLeft()
        {
            //Check for wall at nieghbors[0]. Reference the walls list in graph
        }
        public void CheckForWallRight()
        {
            //Check for wall at nieghbors[1]
        }
        public void CheckForWallAbove()
        {
            //Check for wall at nieghbors[2]
        }
        public void CheckForWallBelow()
        {
            //Check for wall at nieghbors[3]
        }
    }

}
