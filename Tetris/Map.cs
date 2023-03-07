using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Map
    {
        int mapWidth;
        int mapHeight;

        public Map(int mapWidth, int mapHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
        }

        public Map()
        {
            mapWidth = 20;
            mapHeight = 40;
        }

        public void CreateMap(Point point)
        {
            BottomBar(point);
            TopBar(point);
            LeftSideBar(point);
            RightSideBar(point);
        }
        private void TopBar(Point point)
        {
            point.UpdatePosition(mapWidth, mapHeight);
            point.SetCursorPosition(point);
            for (int i = 0; i < mapWidth; i++)
            {
                point.UpdatePosition(-1, 0, 'X');
            }
        }

        private void BottomBar(Point point)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                point.UpdatePosition(-1, 0, 'X');
            }
        }

        private void LeftSideBar(Point point)
        {
            for (int i = 0; i < mapHeight - 1; i++)
            {
                point.UpdatePosition(0, -1, 'X');
            }
        }

        private void RightSideBar(Point point)
        {
            point.UpdatePosition(mapWidth - 1, mapHeight - 1);
            for (int i = 0; i < mapHeight - 1; i++)
            {
                point.UpdatePosition(0, -1, 'X');
            }
        }

        public int GetMapWidth()
        {
            return mapWidth;
        }

        public int GetMapHeight()
        {
            return mapHeight;
        }

    }
}
