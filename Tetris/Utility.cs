using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Utility
    {
        // Constant For Now, Use Map.GetWidth and Map.GetHeight.
        public static bool[,] PlacedPieces = new bool[100, 100];

        public static void Check(Map map, IPoint point)
        {
            List<IPoint> points = new();
            int mapWidth = map.GetMapWidth();
            int mapHeight = map.GetMapHeight();
            GoToStartingPoint(point, mapWidth -1, mapHeight - 1);
            for (int y = 0; y < mapHeight; y++)
            {
                point.UpdatePosition(0, -y);
                for (int x = 0; x < mapWidth - 2; x++)
                {
                    if (!PlacedPieces[point.X, point.Y]) return;

                    points.Add(point);

                    point.UpdatePosition(1, 0);
                }
                ClearRow(points);
            }
        }

        private static void ClearRow(List<IPoint> points)
        {
            foreach (var point in points)
            {
                point.UpdatePosition(-1, 0, ' ');
            }
            points.Clear();
        }

        public static void GetMapPoint(Map map, IPoint point, int x, int y)
        {
            int mapWidth = map.GetMapWidth();
            int mapHeight = map.GetMapHeight();
            GoToStartingPoint(point, mapWidth, mapHeight);

            point.UpdatePosition(x - 1, -y - 1, 'L');
            CheckPoint(point, mapWidth, mapHeight);

        }

        public static void CheckPoint(IPoint point, int mapWidth, int mapHeight)
        {
            bool xOutOfRange = point.X >= Point.StartingX - 2 || point.X <= Point.StartingX - mapWidth;
            bool yOutOfRange = point.Y <= Point.StartingY || point.Y >= Point.StartingY + mapHeight;

            if (xOutOfRange || yOutOfRange)
            {
                throw new Exception($"The point ({point.X}, {point.Y}) is outside of the game's boundaries. " +
                                 $"The X coordinate should be between {Point.StartingX - mapWidth + 1} and {Point.StartingX}. " +
                                 $"The Y coordinate should be between {Point.StartingY} and {Point.StartingY + mapHeight}.");
            }
        }

        public static bool DetectCollision(List<IPoint> points, int mapHeight)
        {
            foreach (var point in points)
            {
                bool yOutOfRange = IsYPointOutOfRange(mapHeight, point);

                bool isPieceBelow = PlacedPieces[point.X, point.Y + 1];
                if (yOutOfRange || isPieceBelow)
                {
                    AddPlacedPiecesPositions(points);
                    return true;
                }
            }
            return false;
        }

        private static void AddPlacedPiecesPositions(List<IPoint> points)
        {
            foreach (var point in points)
            {
                PlacedPieces[point.X, point.Y] = true;
            }
        }

        public static bool IsYPointOutOfRange(int mapHeight, IPoint point)
        {
            return point.Y <= Point.StartingY || point.Y >= Point.StartingY + mapHeight - 1;
        }

        public static bool IsXPointOutOfRange(int mapWidth, int x)
        {
            return x >= Point.StartingX - 1 || x <= Point.StartingX - mapWidth;
        }

        public static bool CheckHorizontalMovementValidity(List<IPoint> points, int mapWidth, int delta)
        {
            foreach (var point in points)
            {
                if (IsXPointOutOfRange(mapWidth, point.X + delta)) return false;
            }
            return true;
        }

        public static void GoToStartingPoint(IPoint point, int mapWidth, int mapHeight)
        {
            IPoint newPoint = point;
            newPoint.SetPositionToStartingPoint();
            newPoint.UpdatePosition(-mapWidth, mapHeight);
        }

        public static void ClearCurrentPoint(IPoint point)
        {
            point.UpdatePosition(0, 0, ' ');
        }

        public static void ClearPiece(List<IPoint> points)
        {
            foreach (var point in points)
            {
                ClearCurrentPoint(point);
            }
        }

        public static void ClearCurrentPoint()
        {
            Console.Write(' ');
        }

    }
}
