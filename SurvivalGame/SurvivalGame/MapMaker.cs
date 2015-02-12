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
        private Texture2D slopeMaps;
        private int seed;
        private List<int> groundTilesFirst = new List<int>() { 0, 0, 0, 0, 0, 1, 1, 1, 1, 6 };
        private List<int> groundTilesSecond = new List<int>() { 2, 2, 2, 3, 3, 3, 4, 5 };
        private int amountTiles;

        public List<MapRow> Rows = new List<MapRow>();
        public int MapWidth = 50;
        public int MapHeight = 50;

        Random rnd;

        public MapMaker(Texture2D mouseMap, Texture2D slopeMap)
        {
            this.mouseMap = mouseMap;
            this.slopeMaps = slopeMap;
            seed = (int)DateTime.Now.Ticks;
            rnd = new Random(seed);
            this.amountTiles = this.MapWidth * this.MapHeight;
            this.generateMap();
        }

        public MapMaker(Texture2D mouseMap, Texture2D slopeMap, int seed)
        {
            this.mouseMap = mouseMap;
            this.slopeMaps = slopeMap;
            this.seed = (int)DateTime.Now.Ticks;
            this.rnd = new Random(seed);
            this.generateMap();
            this.amountTiles = this.MapHeight * this.MapWidth;
        }

        private void generateMap()
        {
            this.generateGround();
        }

        private void generateGround()
        {
            byte[,] tilesPosition = this.generateHeightMap();
            // Convert the heightMap to position of the darker tiles
            this.convertHeightToGroundTiles(tilesPosition);
            smoothGround();
        }

        private byte[,] generateHeightMap()
        {
            byte[,] heightMap = new byte[this.MapWidth, this.MapHeight];

            double percentage = (double)rnd.Next(100, 400) / (double)2000;
            // Add random height to some cells
            for (int i = 0; i < (int)(this.amountTiles * percentage); i++)
            {
                heightMap[rnd.Next(this.MapWidth), rnd.Next(this.MapHeight)] = (byte)this.rnd.Next(255);
            }

            heightMap = smoothHeightMap(heightMap, rnd.Next(2, 5));

            return heightMap;
        }

        private byte[,] smoothHeightMap(byte[,] map, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                // Copy the map to fill in the smooth version
                byte[,] destinationMap = (byte[,])map.Clone();

                foreach (Point position in this.GetAllPositions(map))
                {
                    SmoothTile(map, destinationMap, position.X, position.Y);
                }

                // get reference to the smoothed map
                map = destinationMap;
            }
            return map;
        }

        private List<Point> GetAllPositions(byte[,] doubleArray)
        {
            int width = doubleArray.GetLength(0);
            int height = doubleArray.GetLength(1);

            List<Point> listOfPoints = new List<Point>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    listOfPoints.Add(new Point(x, y));
                }
            }
            return listOfPoints;
        }

        private void SmoothTile(byte[,] sourceMap, byte[,] destinationMap, int x, int y)
        {
            // for calculating the average value of neighbors of a tile:
            float tileSum = 0;              // the sum of the height of the neighbors
            float numberOfNeighbors = 0;    // the number of neighbors for this tile

            // get the neigbors and go through them
            foreach (var neighbor in this.GetNeighborContents(sourceMap, x, y))
            {
                numberOfNeighbors++;                        // increase the number of neighbors found
                tileSum += neighbor;                        // and store the new sum of heights
            }

            // calculate the average of all neighbors
            float averageForNeighbors = tileSum / numberOfNeighbors;

            // find out what the difference between this tile and the neighbor average is
            float difference = averageForNeighbors - sourceMap[x, y];
            // introduce a little randomness
            float randomPct = Math.Abs(difference * .1f) * (rnd.Next(6) - 2);
            // use a fifth of the difference to raise/lower + the randomness
            destinationMap[x, y] = (byte)MathHelper.Clamp((sourceMap[x, y] + difference * .2f + randomPct), 0, 255);
        }

        private List<Byte> GetNeighborContents(byte[,] doubleArray, int x, int y, bool onlyXYaxis = false)
        {
            List<Byte> neighborContent = new List<Byte>();
            foreach (var position in this.GetNeighborCoordinates(doubleArray, x, y, onlyXYaxis))
            {
                neighborContent.Add(doubleArray[position.X, position.Y]);
            }
            return neighborContent;
        }

        private List<Point> GetNeighborCoordinates(byte[,] doubleArray, int x, int y, bool onlyXYaxis = false)
        {
            List<Point> neighborCoordinates = new List<Point>();

            // for the column on the left to the column on the right of the tile
            for (int deltaX = -1; deltaX <= 1; deltaX++)
            {

                // for the row above to the row below the tile
                for (int deltaY = -1; deltaY <= 1; deltaY++)
                {
                    // if we are only looking at the cells directly above/below and beside the cell
                    // we skip diagonal neighbors
                    if (onlyXYaxis && deltaY * deltaX != 0) { continue; }

                    // the potential neighbor's coordinates
                    int neighborX = x + deltaX;
                    int neighborY = y + deltaY;

                    // if the coordinate is within the map
                    if (this.ContainsCoordinate(doubleArray, neighborX, neighborY))
                    {
                        // and we aren't looking at the tile itself
                        if (!(x == neighborX && y == neighborY))
                        {
                            neighborCoordinates.Add(new Point(neighborX, neighborY));
                        }
                    }
                }
            }
            return neighborCoordinates;
        }

        private bool ContainsCoordinate(byte[,] doubleArray, int x, int y)
        {
            return x >= 0 && x < doubleArray.GetLength(0) && y >= 0 && y < doubleArray.GetLength(1);
        }

        private void convertHeightToGroundTiles(byte[,] heightMap)
        {
            for (int i = 0; i < this.MapHeight; i++)
            {
                MapRow thisRow = new MapRow();
                for (int j = 0; j < this.MapWidth; j++)
                {
                    if (heightMap[j, i] >= (byte)10)
                    {
                        thisRow.Columns.Add(new MapCell(this.groundTilesSecond[rnd.Next() % this.groundTilesSecond.Count]));
                    }
                    else
                    {
                        thisRow.Columns.Add(new MapCell(this.groundTilesFirst[rnd.Next() % this.groundTilesFirst.Count]));
                    }
                }
                Rows.Add(thisRow);
            }
        }

        private void smoothGround()
        {
            for (int i = 0; i < this.MapHeight; i++)
            {
                for (int j = 0; j < this.MapWidth; j++)
                {
                    
                }
            }
        }

        public Point WorldToMapCell(Point worldPoint, out Point localPoint)
        {
            Point mapCell = new Point(
               (int)(worldPoint.X / mouseMap.Width),
               ((int)(worldPoint.Y / mouseMap.Height)) * 2);

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

        public Point WorldToMapCell(Vector2 worldPoint)
        {
            return WorldToMapCell(new Point((int)worldPoint.X, (int)worldPoint.Y));
        }

        public MapCell GetCellAtWorldPoint(Point worldPoint)
        {
            Point mapPoint = WorldToMapCell(worldPoint);
            return Rows[mapPoint.Y].Columns[mapPoint.X];
        }

        public MapCell GetCellAtWorldPoint(Vector2 worldPoint)
        {
            return this.GetCellAtWorldPoint(new Point((int)worldPoint.X, (int)worldPoint.Y));
        }

        public int GetSlopeMapHeight(Point localPixel, int slopeMap)
        {

            Point texturePoint = new Point(slopeMap * mouseMap.Width + localPixel.X, localPixel.Y);

            Color[] slopeColor = new Color[1];

            if (new Rectangle(0, 0, slopeMaps.Width, slopeMaps.Height).Contains(texturePoint.X, texturePoint.Y))
            {
                slopeMaps.GetData(0, new Rectangle(texturePoint.X, texturePoint.Y, 1, 1), slopeColor, 0, 1);

                int offset = (int)(((float)(255 - slopeColor[0].R) / 255f) * Tile.HeightTileOffset);

                return offset;
            }

            return 0;
        }

        public int GetSlopeHeightAtWorldPoint(Point worldPoint)
        {
            Point localPoint;
            Point mapPoint = WorldToMapCell(worldPoint, out localPoint);
            int slopeMap = Rows[mapPoint.Y].Columns[mapPoint.X].SlopeMap;

            return this.GetSlopeMapHeight(localPoint, slopeMap);
        }

        public int GetSlopeHeightAtWorldPoint(Vector2 worldPoint)
        {
            return this.GetSlopeHeightAtWorldPoint(new Point((int)worldPoint.X, (int)worldPoint.Y));
        }

        public int GetOverallHeight(Point worldPoint)
        {
            Point mapCellPoint = WorldToMapCell(worldPoint);
            int height = Rows[mapCellPoint.Y].Columns[mapCellPoint.X].HeightTiles.Count * Tile.HeightTileOffset;
            height += this.GetSlopeHeightAtWorldPoint(worldPoint);

            return height;
        }

        public int GetOverallHeight(Vector2 worldPoint)
        {
            return this.GetOverallHeight(new Point((int)worldPoint.X, (int)worldPoint.Y));
        }
    }

    public class MapRow
    {
        public List<MapCell> Columns = new List<MapCell>();
    }
}
