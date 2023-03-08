using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class GameField
    {
        public void RemoveFilledRows(Map map, IPoint point)
        {
            List<IPoint> points = new();
            int mapWidth = map.GetMapWidth();
            int mapHeight = map.GetMapHeight();
            Utility.GoToStartingPoint(point, mapWidth - 1, mapHeight - 1);

            for (int y = 0; y < mapHeight; y++)
            {
                point.UpdatePosition(0, -y);
                for (int x = 0; x < mapWidth - 2; x++)
                {
                    if (!Pieces.PlacedPieces[point.X, point.Y]) return;

                    points.Add(point);

                    point.UpdatePosition(1, 0);
                }
                ClearCurrentRow(points);
                MovePiecesDown(point);
            }
        }
        public void MovePiecesDown(IPoint point)
        {
            int width = Pieces.PlacedPieces.GetLength(0);
            int height = Pieces.PlacedPieces.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    bool isCurrentPositionOccupied = Pieces.PlacedPieces[x, y];
                    bool isNextPositionEmpty = !Pieces.PlacedPieces[x, y + 1];

                    if (isCurrentPositionOccupied && isNextPositionEmpty)
                    {
                        point.SetPosition(x, y);
                        Utility.ClearCurrentPoint(point);
                        point.UpdatePosition(0, 1, '*');
                        Pieces.PlacedPieces[x, y] = false;
                        Pieces.PlacedPieces[x, y + 1] = isCurrentPositionOccupied;
                        break;
                    }
                }
            }
        }

        public void ClearCurrentRow(List<IPoint> points)
        {
            foreach (var point in points)
            {
                point.UpdatePosition(-1, 0, ' ');
                Pieces.PlacedPieces[point.X, point.Y] = false;
            }
            points.Clear();
        }

        public bool CheckHorizontalMovementValidity(List<IPoint> points, int mapWidth, int delta)
        {
            foreach (var point in points)
            {
                if (Utility.IsXPointOutOfRange(mapWidth, point.X + delta) ||
                    Pieces.PlacedPieces[point.X + delta, point.Y]) return false;
            }
            return true;
        }
    }
}
