using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivalGame
{
    public class MapMaker
    {
        private Texture2D mouseMap;
        public List<MapRow> Rows = new List<MapRow>();
        public int MapWidth = 50;
        public int MapHeight = 50;

        public MapMaker(Texture2D mouseMap)
        {
            this.mouseMap = mouseMap;
            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    thisRow.Colums.Add(new MapCell(0));
                }
                Rows.Add(thisRow);
            }
            // Create Sample Map Data
            Rows[0].Colums[3].TileID = 3;
            Rows[0].Colums[4].TileID = 3;
            Rows[0].Colums[5].TileID = 1;
            Rows[0].Colums[6].TileID = 1;
            Rows[0].Colums[7].TileID = 1;

            Rows[1].Colums[3].TileID = 3;
            Rows[1].Colums[4].TileID = 1;
            Rows[1].Colums[5].TileID = 1;
            Rows[1].Colums[6].TileID = 1;
            Rows[1].Colums[7].TileID = 1;

            Rows[2].Colums[2].TileID = 3;
            Rows[2].Colums[3].TileID = 1;
            Rows[2].Colums[4].TileID = 1;
            Rows[2].Colums[5].TileID = 1;
            Rows[2].Colums[6].TileID = 1;
            Rows[2].Colums[7].TileID = 1;

            Rows[3].Colums[2].TileID = 3;
            Rows[3].Colums[3].TileID = 1;
            Rows[3].Colums[4].TileID = 1;
            Rows[3].Colums[5].TileID = 2;
            Rows[3].Colums[6].TileID = 2;
            Rows[3].Colums[7].TileID = 2;

            Rows[4].Colums[2].TileID = 3;
            Rows[4].Colums[3].TileID = 1;
            Rows[4].Colums[4].TileID = 1;
            Rows[4].Colums[5].TileID = 2;
            Rows[4].Colums[6].TileID = 2;
            Rows[4].Colums[7].TileID = 2;

            Rows[5].Colums[2].TileID = 3;
            Rows[5].Colums[3].TileID = 1;
            Rows[5].Colums[4].TileID = 1;
            Rows[5].Colums[5].TileID = 2;
            Rows[5].Colums[6].TileID = 2;
            Rows[5].Colums[7].TileID = 2;
            Rows[16].Colums[4].AddHeightTile(54);

            Rows[17].Colums[3].AddHeightTile(54);

            Rows[15].Colums[3].AddHeightTile(54);
            Rows[16].Colums[3].AddHeightTile(53);

            Rows[15].Colums[4].AddHeightTile(54);
            Rows[15].Colums[4].AddHeightTile(54);
            Rows[15].Colums[4].AddHeightTile(51);

            Rows[18].Colums[3].AddHeightTile(51);
            Rows[19].Colums[3].AddHeightTile(50);
            Rows[18].Colums[4].AddHeightTile(55);

            Rows[14].Colums[4].AddHeightTile(54);

            Rows[14].Colums[5].AddHeightTile(62);
            Rows[14].Colums[5].AddHeightTile(61);
            Rows[14].Colums[5].AddHeightTile(63);

            Rows[17].Colums[4].AddTopperTile(114);
            Rows[16].Colums[5].AddTopperTile(115);
            Rows[14].Colums[4].AddTopperTile(125);
            Rows[15].Colums[5].AddTopperTile(91);
            Rows[16].Colums[6].AddTopperTile(94);



            // End Create Sample Map Data
        }

        public Point WorldToMapCell(Point worldPoint, out Point localPoint)
        {
            Point mapCell = new Point(
               (int)(worldPoint.X / mouseMap.Width),
               ((int)(worldPoint.Y / mouseMap.Height)) * 2
               );

            int localPointX = worldPoint.X % mouseMap.Width;
            int localPointY = worldPoint.Y % mouseMap.Height;

            int dx = 0;
            int dy = 0;

            uint[] myUint = new uint[1];

            if (new Rectangle(0, 0, mouseMap.Width, mouseMap.Height).Contains(localPointX, localPointY))
            {
                mouseMap.GetData(0, new Rectangle(localPointX, localPointY, 1, 1), myUint, 0, 1);

                if (myUint[0] == 0xFF0000FF) // Red
                {
                    dx = -1;
                    dy = -1;
                    localPointX = localPointX + (mouseMap.Width / 2);
                    localPointY = localPointY + (mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFF00FF00) // Green
                {
                    dx = -1;
                    localPointX = localPointX + (mouseMap.Width / 2);
                    dy = 1;
                    localPointY = localPointY - (mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFF00FFFF) // Yellow
                {
                    dy = -1;
                    localPointX = localPointX - (mouseMap.Width / 2);
                    localPointY = localPointY + (mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFFFF0000) // Blue
                {
                    dy = +1;
                    localPointX = localPointX - (mouseMap.Width / 2);
                    localPointY = localPointY - (mouseMap.Height / 2);
                }
            }

            mapCell.X += dx;
            mapCell.Y += dy - 2;

            localPoint = new Point(localPointX, localPointY);

            return mapCell;
        }

        public Point WorldToMapCell(Point worldPoint)
        {
            Point dummy;
            return WorldToMapCell(worldPoint, out dummy);
        }
    }

    public class MapRow
    {
        public List<MapCell> Colums = new List<MapCell>();
    }
}
