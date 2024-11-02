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
        return graph.FindRoute(bräde);
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