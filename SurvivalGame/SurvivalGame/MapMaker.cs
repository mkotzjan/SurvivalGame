using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurvivalGame
{
    public class MapMaker
    {
        public List<MapRow> Rows = new List<MapRow>();
        public int MapWidth = 50;
        public int MapHeight = 50;

        public MapMaker()
        {
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

            // End Create Sample Map Data
        }
    }

    public class MapRow
    {
        public List<MapCell> Colums = new List<MapCell>();
    }
}
