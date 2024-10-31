using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Student
{
    public class Node
    {
        int[] horizontalNeighbors = { 0, 1, 0, -1 };
        int[] verticalNeighbors = { 1, 0, -1, 0 };

        private Point position;
        private List<bool> sides;

        public Point Position { get { return position; } }

        public Node(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }

        List<Node> neighbors = new List<Node>();

        public List<bool> getSides()
        {
            return sides;
        }

        

        public override string ToString()
        {
            return $"({position.X}, {position.Y})";
        }

        public void FindNeighbors(SpelBräde sb)
        {
            for (int i = 0; i < 3; i++)
            {
                int xNeighbor = position.X + horizontalNeighbors[i];
                int yNeighbor = position.Y + verticalNeighbors[i];

                if (xNeighbor < 8 && xNeighbor > 0 && yNeighbor < 8 && yNeighbor > 0)
                {
                    Node neighbor = new Node(xNeighbor, yNeighbor);
                    neighbors.Add(neighbor);
                }
            }
            CheckForWallAbove(sb);
            CheckForWallRight(sb);
            CheckForWallBelow(sb);
            CheckForWallLeft(sb);
        }

        public void CheckForWallAbove(SpelBräde sb)
        {
            //Checks if the bool at the position of the node is true. If it is true, a wall is there, and we remove the neighbor from the list.
            if (sb.horisontellaVäggar[neighbors[0].Position.X, neighbors[0].Position.X])
            {
                neighbors.Remove(neighbors[0]);
            }
            //Check for wall at nieghbors[0]
        }
        
        public void CheckForWallRight(SpelBräde sb)
        {
            if (sb.horisontellaVäggar[neighbors[1].Position.X, neighbors[1].Position.X])
            {
                neighbors.Remove(neighbors[1]);
            }
            //Check for wall at nieghbors[1]
        }
        
        public void CheckForWallBelow(SpelBräde sb)
        {
            if (sb.horisontellaVäggar[neighbors[2].Position.X, neighbors[2].Position.X])
            {
                neighbors.Remove(neighbors[2]);
            }
            //Check for wall at nieghbors[2]
        }
        public void CheckForWallLeft(SpelBräde sb)
        {
            if (sb.horisontellaVäggar[neighbors[3].Position.X, neighbors[3].Position.X])
            {
                neighbors.Remove(neighbors[3]);
            }
            //Check for wall at nieghbors[3]. Reference the walls list in graph
        }
    }

}
