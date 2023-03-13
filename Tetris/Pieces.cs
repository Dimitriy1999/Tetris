using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Pieces
    {

        public static int PieceWidth { get; private set; }

        public static List<bool[,]> AllPieces = new List<bool[,]>();
        public static bool[,] PlacedPieces = new bool[100, 100];

        public static List<bool[,]> IgnoreRotationList = new List<bool[,]>();
        public static void Initalize()
        {
            AllPieces.Add(longPiece);
            AllPieces.Add(square);
            AllPieces.Add(tShape);
            AllPieces.Add(zShape);
            AllPieces.Add(JBlock);
            AllPieces.Add(LBlock);
            AllPieces.Add(SBlock);
            IgnoreRotationList.Add(square);
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
            { false, true, true, false},
            { false, true, true, false},
            { false, false, false, false},
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
            { false, false, false, false},
        };

        private static bool[,] JBlock = new[,]
        {
            { true, false, false, false},
            { true, true, true, false},
            { false, false, false, false},
            { false, false, false, false},
        };

        private static bool[,] LBlock = new[,]
        {
            { false, false, false, true},
            { false, true, true, true},
            { false, false, false, false},
            { false, false, false, false},
        };

        private static bool[,] SBlock = new[,]
       {
            { false, false, true, true},
            { false, true, true, false},
            { false, false, false, false},
            { false, false, false, false},
        };


        public static bool[,] GetRandomPiece()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, Pieces.AllPieces.Count);
            return Pieces.AllPieces[randomNumber];
        }

        public static void RenderPiece(bool[,] pieceData, Point startingPoint)
        {
            for (int i = 0; i < pieceData.GetLength(0); i++)
            {
                for (int j = 0; j < pieceData.GetLength(1); j++)
                {
                    bool condition = pieceData[i, j];

                    if (!condition) continue;

                    Utility.DrawCharacter(startingPoint.X + j, startingPoint.Y + i, Game.PieceChar);
                }
            }
        }
    }
}
