using Microsoft.Xna.Framework;
using Student;
using System;
using System.Collections.Generic;

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
        Spelare jag = bräde.spelare[0];
        Point start = jag.position;
        Point goal = new Point(start.X, 8);

        Queue<Node> queue = new Queue<Node>();
        HashSet<Point> visited = new HashSet<Point>();

        queue.Enqueue(new Node(start.X, start.Y));
        visited.Add(start);

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();

            // Check if the goal is reached
            if (current.Position.Y == goal.Y)
            {
                Drag drag = new Drag
                {
                    typ = Typ.Flytta,
                    point = current.Position
                };
                return drag;
            }

            foreach (Point neighbor in GetValidNeighbors(current.Position, bräde))
            {
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(new Node(neighbor.X, neighbor.Y));
                    visited.Add(neighbor);
                }
            }
        }

        Drag fallbackDrag = new Drag
        {
            typ = Typ.Flytta,
            point = start
        };
        fallbackDrag.point.Y++;
        return fallbackDrag;
    }


    private bool IsValidMove(Point current, Point next, SpelBräde bräde)
    {
        if (next.X < 0 || next.X >= 8 || next.Y < 0 || next.Y >= 8)
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
    private List<Point> GetValidNeighbors(Point current, SpelBräde bräde)
    {
        List<Point> neighbors = new List<Point>();

        Point[] directions = {
            new Point(-1, 0), // Left
            new Point(0, -1), // Up
            new Point(1, 0),   // Right
            new Point(0, 1)  // Down
        };

        foreach (Point direction in directions)
        {
            Point neighbor = new Point(current.X + direction.X, current.Y + direction.Y);

            if (IsValidMove(current, neighbor, bräde))
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag)
    {
        System.Diagnostics.Debugger.Break();
        return SökNästaDrag(bräde);
    }
}