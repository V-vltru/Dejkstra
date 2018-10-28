namespace Dejkstra
{
    using System.Collections.Generic;
    using System.Linq;

    public class DejkstraAlgorim
    {
        public Point[] Points { get; private set; }

        public Edge[] Edges { get; private set; }

        public Point BeginPoint { get; private set; }

        public DejkstraAlgorim(Point[] pointsOfgrath, Edge[] rebraOfgrath)
        {
            Points = pointsOfgrath;
            Edges = rebraOfgrath;
        }

        /// <summary>
        /// Запуск алгоритма расчета
        /// </summary>
        /// <param name="beginPoint"></param>
        public void AlgoritmRun(Point beginPoint)
        {
            if (this.Points.Count() == 0 || this.Edges.Count() == 0)
            {
                throw new DekstraException("Массив вершин или ребер не задан!");
            }

            this.BeginPoint = beginPoint;
            this.BeginPoint.CurrentPathValue = 0;
            this.OneStep(this.BeginPoint);

            foreach (Point point in Points)
            {
                Point anotherP = GetAnotherUncheckedPoint();
                if (anotherP != null)
                {
                    OneStep(anotherP);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Метод, делающий один шаг алгоритма. Принимает на вход вершину
        /// </summary>
        /// <param name="beginPoint"></param>
        public void OneStep(Point beginPoint)
        {
            foreach (Point nextp in this.GetNeighbours(beginPoint))
            {
                if (nextp.IsChecked == false)//не отмечена
                {
                    float newmetka = beginPoint.CurrentPathValue + GetEdge(nextp, beginPoint).Weight;
                    if (nextp.CurrentPathValue > newmetka)
                    {
                        nextp.CurrentPathValue = newmetka;
                        nextp.PreviousPathPoint = beginPoint;
                    }
                }
            }

            beginPoint.IsChecked = true;//вычеркиваем
        }

        /// <summary>
        /// Поиск соседей для вершины. Для неориентированного графа ищутся все соседи.
        /// </summary>
        /// <param name="currpoint"></param>
        /// <returns></returns>
        private IEnumerable<Point> GetNeighbours(Point currpoint)
        {
            IEnumerable<Point> firstpoints = from ff in Edges where ff.FirstPoint == currpoint select ff.SecondPoint;
            IEnumerable<Point> secondpoints = from sp in Edges where sp.SecondPoint == currpoint select sp.FirstPoint;
            IEnumerable<Point> totalpoints = firstpoints.Concat(secondpoints);
            return totalpoints;
        }

        /// <summary>
        /// Получаем ребро, соединяющее 2 входные точки
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Edge GetEdge(Point a, Point b)
        {//ищем ребро по 2 точкам
            IEnumerable<Edge> edges = from reb in Edges
                                      where (reb.FirstPoint == a & reb.SecondPoint == b) || (reb.SecondPoint == a & reb.FirstPoint == b)
                                      select reb;

            if (edges.Count() > 1 || edges.Count() == 0)
            {
                throw new DekstraException("Не найдено ребро между соседями!");
            }
            else
            {
                return edges.First();
            }
        }

        /// <summary>
        /// Получаем очередную неотмеченную вершину, "ближайшую" к заданной.
        /// </summary>
        /// <returns></returns>
        private Point GetAnotherUncheckedPoint()
        {
            IEnumerable<Point> pointsuncheck = from p in Points where p.IsChecked == false select p;
            if (pointsuncheck.Count() != 0)
            {
                float minVal = pointsuncheck.First().CurrentPathValue;
                Point minPoint = pointsuncheck.First();
                foreach (Point p in pointsuncheck)
                {
                    if (p.CurrentPathValue < minVal)
                    {
                        minVal = p.CurrentPathValue;
                        minPoint = p;
                    }
                }
                return minPoint;
            }
            else
            {
                return null;
            }
        }

        public List<Point> MinPath1(Point end)
        {
            List<Point> listOfpoints = new List<Point>();
            Point tempp = new Point("temporary");
            tempp = end;
            while (tempp != this.BeginPoint)
            {
                listOfpoints.Add(tempp);
                tempp = tempp.PreviousPathPoint;
            }

            return listOfpoints;
        }
    }
}
