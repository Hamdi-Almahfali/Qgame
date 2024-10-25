using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Student
{
    public class Node
    {

        public Node(int x, int y) 
        {
            position.X = x;
            position.Y = y;
        }
        public Point Position { get { return position; } }


        private Point position;
        private List<bool> sides;



        public override string ToString()
        {
            return $"({position.X}, {position.Y})";
        }
        public List<bool> getSides()
        {
            return sides;
        }
    }

}
