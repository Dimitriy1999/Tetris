using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal interface IPoint
    {
        void SetCursorPosition(Point point);
        void UpdatePosition(int xAmount, int yAmount, char character);
        void UpdatePosition(int xAmount, int yAmount);
        void SetPositionToStartingPoint();
        void SetPosition(int x, int y);
        int X { get; set; }
        int Y { get; set; }

    }
}
