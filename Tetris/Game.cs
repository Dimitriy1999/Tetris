using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Game
    {
        bool[,] piece;

        float timeForPiece = 0.5f;

        Map map;
        Timer time;
        GameField game;

        public static char PieceChar = '*';


        public Game()
        {
            map = new(20, 20);
            time = new Timer();
            game = new();
            Pieces.Initalize();
        }

        public void Run()
        {
            Console.CursorVisible = false;
            InitializeGame();
            PrintGameTutorialTexts();
            while (true)
            {
                game.ClearRowsThatAreFull();

                if (time.DurationPassed(timeForPiece))
                {
                    MovePieceDown();
                }

                List<Point> pointsList = Point.GetListOfPoints(piece, GameField.FirstPiecePosition);
                if (Utility.DetectCollision(pointsList))
                {
                    if (DidWeLose(pointsList)) break;

                    ResetTimeForPiece();
                    GenerateNewPiece();
                    GameField.FirstPiecePosition = GameField.PieceSpawnPoint.Clone();
                }
                ProcessKeys();
            }
            PrintLostMessage();
        }

        private static void PrintLostMessage()
        {
            Console.Clear();
            Utility.SetPosition(30, 15);
            Console.WriteLine("You Lost! Restart The Application To Play Again.");
            Console.ReadLine();
        }

        private bool DidWeLose(List<Point> points)
        {
            return points.Any(point => point.Y == GameField.PieceSpawnPoint.Y);
        }

        private void PrintGameTutorialTexts()
        {
            string welcomeMessage = "Welcome To Tetris";
            Utility.SetCursorPosition(Point.StartingPosition.X - welcomeMessage.Length - 1, Point.StartingY - 2);
            Console.Write(welcomeMessage);
            Utility.SetCursorPosition(Point.StartingPosition.X + 5, 5);
            Console.Write("Press \"A\" or \"D\" to Move Your PieceChar");
            Utility.SetCursorPosition(Point.StartingPosition.X + 5, 6);
            Console.Write("Press \"Space\" To Instantly Put Your PieceChar Down");
            Utility.SetCursorPosition(Point.StartingPosition.X + 5, 7);
            Console.Write("Press \"L\" To Rotate Your PieceChar");



        }

        private void ResetTimeForPiece()
        {
            timeForPiece = 0.5f;
        }

        private void FillRow()
        {
            Point point = new Point(40, 24);
            for (int i = 41; i <= 59; i++)
            {
                Pieces.PlacedPieces[i, 24] = true;
                Utility.SetPosition(i, 24);
                Utility.DrawCharacter(i, 24, '*');
                if (i % 2 == 0)
                {
                    Pieces.PlacedPieces[i, 23] = true;
                    Utility.SetPosition(i, 23);
                    Utility.DrawCharacter(i, 23, '*');
                    Pieces.PlacedPieces[i, 22] = true;
                    Utility.SetPosition(i, 22);
                    Utility.DrawCharacter(i, 22, '*');

                }
            }
        }

        private void InitializeGame()
        {
            map.CreateMap();
            GenerateNewPiece();
        }

        private int MovePieceWithKeys(ConsoleKeyInfo keyPressed)
        {
            int currentX = 0;
            switch (keyPressed.Key)
            {
                case ConsoleKey.A:
                    currentX = -1;
                    break;
                case ConsoleKey.L:
                    TurnPiece();
                    break;
                case ConsoleKey.D:
                    currentX = 1;
                    break;
                case ConsoleKey.Spacebar:
                    timeForPiece = 0;
                    break;
                default:
                    currentX = 0;
                    break;
            }
            return currentX;
        }

        private void MovePieceDown()
        {
            List<Point> listOfPoints = Point.GetListOfPoints(piece, GameField.FirstPiecePosition);

            GameField.FirstPiecePosition.Y += 1;

            Utility.ClearPiece(listOfPoints);
            foreach (var point in listOfPoints)
            {
                point.Y += 1;
                Utility.DrawCharacter(point, PieceChar);
            }
        }

        private void GenerateNewPiece()
        {
            SetSpawnPosition();
        }

        private void SetSpawnPosition()
        {
            bool[,] pieceData = Pieces.GetRandomPiece();
            piece = pieceData;
            Pieces.RenderPiece(pieceData, GameField.PieceSpawnPoint);
        }

        public void ProcessKeys()
        {
            if (!Console.KeyAvailable) return;

            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            int currentX = MovePieceWithKeys(keyPressed);
            MovePieceLeftAndRight(currentX);
        }

        private void MovePieceLeftAndRight(int currentX)
        {
            var pointList = Point.GetListOfPoints(piece, GameField.FirstPiecePosition);
            if (!game.CanWeMoveLeftOrRight(pointList, currentX)) return;

            Utility.ClearPiece(pointList);

            GameField.FirstPiecePosition.X += currentX;

            foreach (var point in pointList)
            {
                point.X += currentX;
                Utility.DrawCharacter(point, PieceChar);
            }
        }

        private void TurnPiece()
        {
            if (Pieces.IgnoreRotationList.Contains(piece)) return;

            List<Point> currentPoints = Point.GetListOfPoints(piece, GameField.FirstPiecePosition);

            Utility.ClearPiece(currentPoints);

            bool[,] pieceCopy = new bool[4, 4];

            for (int y = 0; y <= 3; y++)
            {
                for (int x = 0; x <= 3; x++)
                {
                    bool condition = piece[y, x];
                    pieceCopy[x, 3 - y] = condition;
                }
            }
            List<Point> points = Point.GetListOfPoints(pieceCopy, GameField.FirstPiecePosition);
            CheckRotatedPiece(points);
            Pieces.RenderPiece(pieceCopy, GameField.FirstPiecePosition);
            piece = pieceCopy;
        }

        private void CheckRotatedPiece(List<Point> points)
        {
            while (true)
            {
                var shiftDirection = CheckInRange(points);
                if (shiftDirection == 0) return;

                ShiftPieces(points, shiftDirection);
            }
        }

        private static void ShiftPieces(List<Point> pointList, int currentDirection)
        {
            GameField.FirstPiecePosition.X -= currentDirection;
            foreach (Point point in pointList)
            {
                point.X -= currentDirection;
            }
        }

        private int CheckInRange(List<Point> pointList)
        {
            foreach (Point point in pointList)
            {
                int currentDirection;

                if (Utility.IsXPointOutOfRange(point.X, out currentDirection)) return currentDirection;
            }
            return 0;
        }
    }

}
