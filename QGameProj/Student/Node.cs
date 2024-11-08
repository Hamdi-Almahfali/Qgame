﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Student
{
    public class Node
    {
        int[] horizontalNeighbors = { 0, 1, 0, -1 };
        int[] verticalNeighbors = { 1, 0, -1, 0 };

        private Point position;
        private List<bool> sides;
        Node[] neighbors = new Node[4];

        public Point Position { get { return position; } }
        public Node[] Neighbors { get { return neighbors; } }

        public Node(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }    

        public List<bool> getSides()
        {
            return sides;
        }

        public override string ToString()
        {
            return $"({position.X}, {position.Y})";
        }

        public void FindNeighbors(SpelBräde sb, Node[,] grid)
        {
            for (int i = 0; i < 4; i++)
            {
                int xNeighbor = position.X + horizontalNeighbors[i];
                int yNeighbor = position.Y + verticalNeighbors[i];
                //Takes the values from the two arrays. Index 0 will always be the node above, 1 the node to the right, 2 the node below, and 3 the node to the left.

                if (xNeighbor <= 8 && xNeighbor >= 0 && yNeighbor <= 8 && yNeighbor >= 0)
                { 
                    //Check if neighbors exist on gameboard. If they do, add them to a neighbor list.
                    neighbors[i] = grid[xNeighbor, yNeighbor];
                }
            }

            //Temporary implementation to help with debugging at the moment. Will combine these together into one method later.
            CheckForWallAbove(sb);
            CheckForWallRight(sb);
            CheckForWallBelow(sb);
            CheckForWallLeft(sb);
            //we balled. no longer buggy
        }

        public void CheckForWallAbove(SpelBräde sb)
        {
            if (neighbors[0] == null)
                return;
            if (Position.Y == 8)
                return;
            //Checks if the bool at the position of the node is true. 
            if (sb.horisontellaVäggar[position.X, position.Y])
            {
                //If it is true, a wall is there, and we remove the neighbor from the list.
                neighbors[0] = null;
            }
        }
        
        public void CheckForWallRight(SpelBräde sb)
        {
            if (neighbors[1] == null)
                return;
            if (Position.X == 8)
                return;

            if (sb.vertikalaVäggar[position.X, position.Y])
            {
                neighbors[1] = null;
            }
        }
        
        public void CheckForWallBelow(SpelBräde sb)
        {
            if (neighbors[2] == null)
                return;
            if (Position.Y == 0)
                return;

            if (sb.horisontellaVäggar[neighbors[2].Position.X, neighbors[2].Position.Y])
            {
                neighbors[2] = null;
            }
        }

        public void CheckForWallLeft(SpelBräde sb)
        {
            if (neighbors[3] == null)
                return;
            if (Position.X == 0)
                return;

            if (sb.vertikalaVäggar[neighbors[3].Position.X, neighbors[3].Position.Y])
            {
                neighbors[3] = null;
            }
        }
    }

}
