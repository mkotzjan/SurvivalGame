using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurvivalGame
{
    public class MapCell
    {
        public List<int> BaseTiles = new List<int>();
        public List<int> HeightTiles = new List<int>();
        public List<int> TopperTiles = new List<int>();
        public bool Walkable { get; set; }
        public int SlopeMap { get; set; }

        public MapCell(int tileID)
        {
            TileID = tileID;
            Walkable = true;
            SlopeMap = -1;
        }

        public int TileID
        {
            get { return this.BaseTiles.Count > 0 ? this.BaseTiles[0] : 0; }
            set
            {
                if (this.BaseTiles.Count > 0)
                    this.BaseTiles[0] = value;
                else
                    this.AddBaseTile(value);
            }
        }

        public void AddBaseTile(int tileID)
        {
            this.BaseTiles.Add(tileID);
        }

        public void AddHeightTile(int tileID)
        {
            this.HeightTiles.Add(tileID);
        }

        public void AddTopperTile(int tileID)
        {
            TopperTiles.Add(tileID);
        }
    }
}
