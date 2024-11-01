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

        public void FindRoute(SpelBräde sb)
        {
            foreach (Node node in grid)
            {
                node.FindNeighbors(sb);
                Debug.WriteLine("Node " + node.ToString() +"'s neighbors:");
                foreach (Node neighbor in node.Neighbors)
                {
                    if(neighbor != null)
                        Debug.WriteLine(neighbor.ToString());
                }
            }

                    
            //Insert bfs here. This will find our route from position to the other end of the board.
        }
    }
}
