using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class GameField
    {
        private static readonly int x = Point.StartingX - Map.Width / 2;
        private static readonly int y = Point.StartingY + 1;

        public static Point PieceSpawnPoint = new Point(x, y);
        public static Point FirstPiecePosition = new Point(x, y);

        private Point MapStartingPoint = new Point(Point.StartingX - (Map.Width - 1), Point.StartingY + (Map.Height - 1));

        public void ClearRowsThatAreFull()
        {
            List<Point> pointList = new();
            for (int i = 0; i < Map.Height; i++)
            {
                var rowPos = CheckEveryRowFirstPosition(i);

                if (rowPos == -1) continue;

                bool rowFull = IsRowFull(rowPos);

                if (!rowFull) continue;

                for (int x = 0; x < Map.Width - 1; x++)
                {
                    pointList.Add(new Point(MapStartingPoint.X + x, MapStartingPoint.Y - rowPos));
                }

            }
            ClearRows(pointList);
            pointList.Clear();
        }

        private bool IsRowFull(int rowPos)
        {
            for (int i = 0; i < Map.Width - 1; i++)
            {
                bool condition = Pieces.PlacedPieces[MapStartingPoint.X + i, MapStartingPoint.Y - rowPos];

                if (!condition) return false;
            }
            return true;
        }

        private void ClearRows(List<Point> pointList)
        {
            if (pointList.Count > 0 && pointList.Count % (Map.Width - 1) == 0)
            {
                ClearPieces(pointList);
                MovePiecesDown();
            }
        }

        private int CheckEveryRowFirstPosition(int inputY)
        {
            int mapHeight = Map.Height;
            for (int i = inputY; i < mapHeight; i++)
            {
                if (Pieces.PlacedPieces[MapStartingPoint.X, MapStartingPoint.Y - i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void MovePiecesDown()
        {
            int mapWidth = Map.Width;
            int mapHeight = Map.Height;

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth - 1; x++)
                {
                    int newX = MapStartingPoint.X + x;
                    int newY = MapStartingPoint.Y - y;
                    bool isOccupied = Pieces.PlacedPieces[newX, newY];
                    bool isSpaceAboveOccupied = Pieces.PlacedPieces[newX, newY - 1];

                    if (isOccupied || !isSpaceAboveOccupied) continue;

                    Utility.ClearCurrentPosition(newX, newY - 1);
                    Utility.DrawCharacter(newX, newY, Game.PieceChar);
                    Pieces.PlacedPieces[newX, newY - 1] = false;
                    Pieces.PlacedPieces[newX, newY] = true;
                }
            }
        }

        public void ClearPieces(List<Point> points)
        {
            foreach (Point point in points)
            {
                Utility.ClearCurrentPosition(point);
                Pieces.PlacedPieces[point.X, point.Y] = false;
            }
        }

        public bool CanWeMoveLeftOrRight(List<Point> points, int delta)
        {
            foreach (Point point in points)
            {
                if (Utility.IsXPointOutOfRange(point.X + delta) ||
                    Pieces.PlacedPieces[point.X + delta, point.Y]) return false;
            }
            return true;
        }

    }
}
