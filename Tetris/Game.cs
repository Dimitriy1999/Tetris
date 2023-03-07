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

        int currentX;

        Map map;
        Timer time;
        Timer pieceLandedTimer;
        List<IPoint> points;
        public Game()
        {
            points = new();
            map = new(20, 20);
            time = new Timer();
            pieceLandedTimer = new Timer();
            mapHeight = map.GetMapHeight();
            mapWidth = map.GetMapWidth();
            Pieces.Initalize();
        }

        public void Run()
        {
            Point point = new(windowWidth, windowHeight);
            point.UpdatePosition(0, 0);
            map.CreateMap(point);
            SetSpawnPosition(point);
            while (true)
            {
                Utility.Check(map, point);
                if (time.DurationPassed(0.05f))
                {
                    MoveCurrentPieceDown();
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

        private void MoveCurrentPieceDown()
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
            if (!Utility.CheckHorizontalMovementValidity(points, mapWidth, currentX)) return;

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

    }

}
