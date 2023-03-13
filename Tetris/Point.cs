using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Point
    {

        public int X { get; set; }
        public int Y { get; set; }

        public static int StartingX
        {
            get { return Console.WindowWidth / 2; }
        }
        public static int StartingY
        {
            get { return Console.WindowHeight / 2 - 10; }
        }

        public static Point StartingPosition
        {
            get { return new Point(StartingX, StartingY); }
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point()
        {
            X = Point.StartingX;
            Y = Point.StartingY;
        }

        public void SetPositionToStartingPoint()
        {
            X = StartingX;
            Y = StartingY;
        }

        public static List<Point> GetListOfPoints(bool[,] pieceData, Point startingPoint)
        {
            List<Point> listOfPoints = new();
            for (int i = 0; i < pieceData.GetLength(0); i++)
            {
                for (int j = 0; j < pieceData.GetLength(1); j++)
                {
                    var condition = pieceData[i, j];

                    if (!condition) continue;

                    listOfPoints.Add(new Point(startingPoint.X + j, startingPoint.Y + i));
                }
            }
            return listOfPoints;
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }
    }
}
