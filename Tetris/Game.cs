using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Game
    {
        int windowWidth = Console.WindowWidth / 2;
        int windowHeight = (Console.WindowHeight / 2) - 10;
        int mapHeight;
        int mapWidth;
        bool[,] piece;
        int currentX;

        Map map;
        Timer time;
        Timer pieceLandedTimer;
        List<IPoint> points;
        GameField gameField;
        public Game()
        {
            points = new();
            map = new(20, 20);
            time = new Timer();
            pieceLandedTimer = new Timer();
            gameField = new();
            mapHeight = map.GetMapHeight();
            mapWidth = map.GetMapWidth();
            Pieces.Initalize();
        }

        public void Run()
        {
            Console.CursorVisible = false;
            Point point = Initialize();
            while (true)
            {
                gameField.RemoveFilledRows(map, point);
                if (time.DurationPassed(0.5f))
                {
                    MoveDown();
                }

                //To Do: When an object collides give at least 1-2 seconds so that we can rotate around for a bit
                if (Utility.DetectCollision(points, mapHeight))
                {
                    points.Clear();
                    GenerateNewPiece(point);
                }
                ProcessKeys();
            }
        }

        //This function just fills out rows. 
        private void FillRow()
        {
            Point point = new Point(40, 24);
            for (int i = 41; i <= 58; i++)
            {
                Pieces.PlacedPieces[i, 24] = true;
                point.SetPosition(i, 24);
                point.UpdatePosition(0, 0, '*');
                if (i % 2 == 0)
                {
                    Pieces.PlacedPieces[i, 23] = true;
                    point.SetPosition(i, 23);
                    point.UpdatePosition(0, 0, '*');
                }
            }
        }

        private Point Initialize()
        {
            Point point = new(windowWidth, windowHeight);
            point.UpdatePosition(0, 0);
            map.CreateMap(point);
            SetSpawnPosition(point);
            return point;
        }

        private void MovePieceWithKeys(ConsoleKeyInfo keyPressed)
        {
            switch (keyPressed.Key)
            {
                case ConsoleKey.A:
                    currentX = -1;
                    break;

                case ConsoleKey.D:
                    currentX = 1;
                    break;

                default:
                    currentX = 0;
                    break;
            }
        }

        private void MoveDown()
        {
            for (int i = points.Count - 1; i >= 0; i--)
            {
                IPoint point = points[i];

                Utility.ClearCurrentPoint(point);
                point.UpdatePosition(0, 1, '*');
            }
        }

        private void MovePieceLeftAndRight()
        {
            if (!gameField.CheckHorizontalMovementValidity(points, mapWidth, currentX)) return;

            Utility.ClearPiece(points);

            foreach (var point in points)
            {
                point.UpdatePosition(currentX, 0, '*');
            }
        }

        private void GenerateNewPiece(IPoint point)
        {
            SetSpawnPosition(point);
        }

        private void SetSpawnPosition(IPoint point)
        {
            bool[,] pieceData = Pieces.GetRandomPiece();
            piece = pieceData;
            Utility.GoToStartingPoint(point, mapWidth / 2 + 2, 1);
            Pieces.RenderPiece(pieceData, point, ref points);
        }

        public void ProcessKeys()
        {
            if (!Console.KeyAvailable) return;

            var KeyPressed = Console.ReadKey(true);
            MovePieceWithKeys(KeyPressed);
            MovePieceLeftAndRight();
        }

        //Function works just add functionality later.
        private void TurnPiece()
        {
            int print = 0;
            bool[,]? pieceCopy = new bool[4, 4];
            for (int y = 0; y <= 3; y++)
            {
                for (int x = 0; x <= 3; x++)
                {
                    bool condition = piece[y, x];
                    pieceCopy[x, 3 - y] = condition;
                    if (condition)
                    {
                        Console.SetCursorPosition(1, 5 + print);
                        print++;
                        Console.WriteLine($"previous : ({x} , {y}) | After : ({3 - y}, {x})");
                    }
                }
            }
            Utility.ClearPiece(points);
            Pieces.RenderPiece(pieceCopy, points[0], ref points);
        }
    }

}
