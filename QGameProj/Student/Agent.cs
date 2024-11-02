using Microsoft.Xna.Framework;
using Student;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Agent : BaseAgent
{
    [STAThread]
    static void Main()
    {
        Program.Start(new Agent());
    }

    public Agent() { }

    public override Drag SökNästaDrag(SpelBräde bräde)
    {
        Debug.WriteLine("----------------------------------------------------");
        Debug.WriteLine("----------------------------------------------------");
        Debug.WriteLine("----------------------------------------------------");
        Debug.WriteLine("----------------------------------------------------");
        Graph graph = new Graph();
        graph.Initialize();
        graph.FindRoute(bräde);


        Spelare jag = bräde.spelare[0];
        Node start = new Node(jag.position.X,jag.position.Y);

        List<Node> goals = new List<Node>();
        for (int i = 0; i < 9; i++)
        {
            Node goal = new Node(i, 8);
            goals.Insert(i,goal);
        }

        Queue<Node> queue = new Queue<Node>();
        HashSet<Node> visited = new HashSet<Node>();

        queue.Enqueue(new Node(start.Position.X, start.Position.Y));
        visited.Add(start);

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();

            // Check if the goal is reached
            if (goals.Contains(current))
            {
                Drag drag = new Drag
                {
                    typ = Typ.Flytta,
                    point = current.Position
                };
                return drag;
            }

            foreach (Node neighbor in current.Neighbors)
            {
                if (neighbor == null) continue;

                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(new Node(neighbor.Position.X, neighbor.Position.Y));
                    visited.Add(neighbor);
                }
            }
        }

        Drag fallbackDrag = new Drag
        {
            typ = Typ.Flytta,
            point = start.Position
        };
        fallbackDrag.point.Y++;
        return fallbackDrag;
    }


    private bool IsValidMove(Node current, Node next, SpelBräde bräde)
    {
        if (next.Position.X < 0 || next.Position.X >= 8 || next.Position.Y < 0 || next.Position.Y >= 8)
        {
            return false;
        }
        /// ------------->>> this is for when we implement wall checks <<<<---------------------
        //if (bräde.IsWallBetween(current, next))
        //{
        //    return false;
        //}
        return true;
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag)
    {
        System.Diagnostics.Debugger.Break();
        return SökNästaDrag(bräde);
    }
}