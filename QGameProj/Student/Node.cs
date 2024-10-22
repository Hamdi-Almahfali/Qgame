using Microsoft.Xna.Framework;
using System;

namespace Student
{
    public class Node
    {

        public Node(int x, int y) 
        {
            position.X = x;
            position.Y = y;
        }
        Point position;

        public Point Position { get { return position; } }



        public override string ToString()
        {
            return $"({position.X}, {position.Y})";
        }

    }

}
