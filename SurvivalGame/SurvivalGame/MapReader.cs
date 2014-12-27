using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivalGame
{
    public class MapReader
    {
        public MapMaker myMap = new MapMaker();
        public int squaresAcross = 5;
        public int squaresDown = 5;
        SpriteBatch spriteBatch;

        public void LoadContent(ContentManager content)
        {
            Tile.TileSetTexture = content.Load<Texture2D>(@"Textures\TileSets\part1_tileset");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            Vector2 firstSquare = new Vector2(Camera.Location.X / 32, Camera.Location.Y / 32);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % 32, Camera.Location.Y % 32);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;

            for (int y = 0; y < squaresDown; y++)
            {
                for (int x = 0; x < squaresAcross; x++)
                {
                    spriteBatch.Draw(
                        Tile.TileSetTexture,
                        new Rectangle((x * 32) - offsetX, (y * 32) - offsetY, 32, 32),
                        Tile.GetSourceRectangle(myMap.Rows[y + firstY].Colums[x + firstX].TileID),
                        Color.White);
                }
            }
            spriteBatch.End();
        }
    }
}
