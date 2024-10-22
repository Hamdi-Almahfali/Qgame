using Microsoft.Xna.Framework;
using System;

namespace Student
{
    public class Node
    {
        Point position;

        public Point Position { get { return position; } }



        public override string ToString()
        {
            return $"({position.X}, {position.Y})";
        }

    }

}
