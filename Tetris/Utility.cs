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

                bool isPieceBelow = Pieces.PlacedPieces[point.X, point.Y + 1];
                if (yOutOfRange || isPieceBelow)
                {
                    AddPlacedPiecePositions(points);
                    return true;
                }
            }
            return false;
        }

        private static void AddPlacedPiecePositions(List<IPoint> points)
        {
            foreach (var point in points)
            {
                Pieces.PlacedPieces[point.X, point.Y] = true;
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
