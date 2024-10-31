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
        Point position;


        private Point position;
        private List<bool> sides;


        public Point Position { get { return position; } }

        public override string ToString()
        {
            return $"({position.X}, {position.Y})";
        }

    }

}
