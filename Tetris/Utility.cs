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
        public static bool DetectCollision(List<Point> points)
        {
            foreach (Point point in points)
            {
                bool yOutOfRange = IsYPointOutOfRange(point);

                bool isPieceBelow = Pieces.PlacedPieces[point.X, point.Y + 1];
                if (yOutOfRange || isPieceBelow)
                {
                    AddPlacedPiecePositions(points);
                    return true;
                }
            }
            return false;
        }

        private static void AddPlacedPiecePositions(List<Point> points)
        {
            foreach (Point point in points)
            {
                Pieces.PlacedPieces[point.X, point.Y] = true;
            }
        }

        public static bool IsYPointOutOfRange(Point point)
        {
            return point.Y <= Point.StartingY || point.Y >= Point.StartingY + Map.Height - 1;
        }

        public static bool IsXPointOutOfRange(int x)
        {
            return x >= Point.StartingX || x <= Point.StartingX - Map.Width;
        }

        public static bool IsXPointOutOfRange(int x, out int currentX)
        {
            currentX = 0;
            if (x >= Point.StartingX)
            {
                currentX = 1;
                return true;
            }
            else if(x <= Point.StartingX - Map.Width)
            {
                currentX = -1;
                return true;
            }
            return false;
        }


        public static void ClearPiece(List<Point> points)
        {
            foreach (Point point in points)
            {
                ClearCurrentPosition(point);
            }
        }

        public static void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public static void SetCursorPosition(int x, int y, char character)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(character);
        }


        public static void DrawCharacter(int x, int y, char character)
        {
            SetCursorPosition(x, y);
            Console.Write(character);
        }

        public static void DrawCharacter(Point point, char character)
        {
            SetCursorPosition(point.X, point.Y);
            Console.Write(character);
        }


        public static void SetPosition(int x, int y)
        {
            SetCursorPosition(x, y);
        }

        public static void ClearCurrentPosition(Point point)
        {
            SetCursorPosition(point.X, point.Y);
            Console.Write(' ');
        }

        public static void ClearCurrentPosition(int x, int y)
        {
            SetCursorPosition(x, y);
            Console.Write(' ');
        }

    }
}
