namespace Dejkstra
{
    using System.Collections.Generic;

    public class PrintGraph
    {
        public static List<string> PrintAllPoints(DejkstraAlgorim da)
        {
            List<string> retListOfPoints = new List<string>();
            foreach (Point p in da.Points)
            {
                retListOfPoints.Add(string.Format("point name={0}, point value={1}, predok={2}", p.Name, p.CurrentPathValue, p.PreviousPathPoint.Name ?? "нет предка"));
            }

            return retListOfPoints;
        }

        public static List<string> PrintAllMinPaths(DejkstraAlgorim da)
        {
            List<string> retListOfPointsAndPaths = new List<string>();
            foreach (Point p in da.Points)
            {
                if (p != da.BeginPoint)
                {
                    string s = string.Empty;
                    foreach (Point p1 in da.MinPath1(p))
                    {
                        s += string.Format("{0} ", p1.Name);
                    }

                    retListOfPointsAndPaths.Add($"Point = {p.Name}, MinPath from {da.BeginPoint.Name} = {s}");
                }
            }
            return retListOfPointsAndPaths;
        }
    }
}
