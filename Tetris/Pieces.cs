using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Pieces
    {
        public static List<bool[,]> AllPieces = new List<bool[,]>();

        public static void Initalize()
        {
            AllPieces.Add(longPiece);
            AllPieces.Add(square);
            AllPieces.Add(tShape);
            AllPieces.Add(zShape);
        }


        private static bool[,] longPiece = new[,]
        {
            { true, true, true, true},
            { false, false, false, false},
            { false, false, false, false},
            { false, false, false, false}
        };

        private static bool[,] square = new[,]
        {
            { false, false, false, false},
            { false, true, true, false},
            { false, true, true, false},
            { false, false, false, false}
        };

        private static bool[,] tShape = new[,]
        {
            { false, true, false, false},
            { true, true, true, false},
            { false, false, false, false},
            { false, false, false, false}
        };

        private static bool[,] zShape = new[,]
        {
            { true, true, false, false},
            { false, true, true, false},
            { false, false, false, false},
            { false, false, false, false}
        };

        public static bool[,] GetRandomPiece()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, Pieces.AllPieces.Count);
            return Pieces.AllPieces[1];
        }
        public static void RenderPiece(bool[,] pieceData, IPoint point, ref List<IPoint> points)
        {
            int startX = point.X;
            int startY = point.Y;
            int yOffset = 0;
            for (int i = 0; i < pieceData.GetLength(0); i++)
            {
                point.SetPosition(startX, startY + yOffset);
                bool hasPrinted = false;

                for (int j = 0; j < pieceData.GetLength(1); j++)
                {
                    bool condition = pieceData[i, j];

                    if (!condition)
                    {
                        point.UpdatePosition(1, 0);
                        continue;
                    }

                    point.UpdatePosition(1, 0, '*');
                    hasPrinted = true;
                    points.Add(new Point(point.X, point.Y));
                }

                if (hasPrinted)
                {
                    yOffset++;
                    point.UpdatePosition(-pieceData.GetLength(1), 1);
                }
            }
        }

    }
}
