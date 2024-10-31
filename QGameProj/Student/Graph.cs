using SharpDX.Direct3D11;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student
{
    internal class Graph
    {
        int size = 8; //Board is 9x9, but arrays start at 0.
        public Node[,] grid;

        Graph()
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
        }

        public void FindRoute()
        {
            //Insert bfs here. This will find our route from position to the other end of the board.
        }
    }
}
