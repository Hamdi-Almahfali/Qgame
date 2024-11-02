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

    public override Drag GörOmDrag(SpelBräde bräde, Drag drag)
    {
        System.Diagnostics.Debugger.Break();
        return SökNästaDrag(bräde);
    }
}