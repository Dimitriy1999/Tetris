using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Point : IPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static int StartingX
        {
            get { return Console.WindowWidth / 2; }
            private set { }
        }
        public static int StartingY
        {
            get { return Console.WindowHeight / 2 - 10; }
            private set { }
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetCursorPosition(Point point)
        {
            Console.SetCursorPosition(point.X, point.Y);
        }
        public void UpdatePosition(int xAmount, int yAmount, char character)
        {
            X += xAmount;
            Y += yAmount;
            SetCursorPosition(this);
            Console.WriteLine(character);
        }
        public void UpdatePosition(int xAmount, int yAmount)
        {
            X += xAmount;
            Y += yAmount;
            SetCursorPosition(this);
        }
        public void SetPositionToStartingPoint()
        {
            X = StartingX;
            Y = StartingY;
        }
        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
            SetCursorPosition(this);
        }

    }
}
