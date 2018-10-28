using System;
using System.Collections.Generic;
using Dejkstra;

namespace DejkstraDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Point[] points = new Point[]
            {
                new Point("1"),
                new Point("2"),
                new Point("3"),
                new Point("4"),
                new Point("5"),
                new Point("6")
            };

            Edge[] edges = new Edge[]
            {
                new Edge(points[0], points[1], 7),
                new Edge(points[0], points[2], 9),
                new Edge(points[0], points[5], 14),
                new Edge(points[1], points[3], 15),
                new Edge(points[1], points[2], 10),
                new Edge(points[2], points[5], 2),
                new Edge(points[2], points[3], 11),
                new Edge(points[4], points[3], 6),
                new Edge(points[4], points[4], 9)
            };

            DejkstraAlgorim dejkstraAlgorim = new DejkstraAlgorim(points, edges);
            dejkstraAlgorim.AlgoritmRun(points[0]);

            List<string> lines = PrintGraph.PrintAllMinPaths(dejkstraAlgorim);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            Console.ReadKey();
        }
    }
}
