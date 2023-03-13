using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Map
    {
        public static int Width;
        public static int Height;

        private const char mapChar = 'X';

        public Map(int mapWidth, int mapHeight)
        {
            Map.Width = mapWidth;
            Map.Height = mapHeight;
        }

        public Map()                            
        {
            Map.Width = 20;
            Map.Height = 40;
        }

        public void CreateMap()
        {
            TopBar(Point.StartingPosition);
            BottomBar(Point.StartingPosition);
            LeftSideBar(Point.StartingPosition);
            RightSideBar(Point.StartingPosition);
        }

        private void TopBar(Point point)
        {
            for (int i = 0; i < Width; i++)
            {
                Utility.SetCursorPosition(point.X - i, point.Y, mapChar);
            }
        }

        private void BottomBar(Point point)
        {
            for (int i = 0; i < Width; i++)
            {
                Utility.SetCursorPosition(point.X - i, point.Y + Height, mapChar);
            }
        }


        private void LeftSideBar(Point point)
        {
            for (int i = 0; i <= Height; i++)
            {
                Utility.SetCursorPosition(point.X - Width, point.Y + i, mapChar);
            }
        }

        private void RightSideBar(Point point)
        {
            for (int i = 0; i <= Height; i++)
            {
                Utility.SetCursorPosition(point.X, point.Y + i, mapChar);
            }
        }
    }
}
