using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivalGame
{
    static class Tile
    {
        static public Texture2D TileSetTexture;
        static public int TileWidth = 33;
        static public int TileHeight = 27;
        static public int TileStepX = 52;
        static public int TileStepY = 14;
        static public int OddRowXOffset = 26;

        static public Vector2 originPoint = new Vector2(19, 39);

        static public Rectangle GetSourceRectangle(int tileIndex)
        {
            int tileY = tileIndex / (TileSetTexture.Width / TileWidth);
            int tileX = tileIndex % (TileSetTexture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }
    }
}
