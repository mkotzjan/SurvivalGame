using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SurvivalGame
{
    public class MapReader
    {
        public MapMaker myMap;
        public int squaresAcross = 17;
        public int squaresDown = 37;
        public int baseOffsetX = -32;
        public int baseOffsetY = -64;
        float heightRowDepthMod = 0.00001f;
        private Texture2D hilight;

        SpriteFont pericles6;
        private bool DebugOverlay = false;

        public void LoadContent(ContentManager content)
        {
            Tile.TileSetTexture = content.Load<Texture2D>(@"Textures\TileSets\part4_tileset");
            pericles6 = content.Load<SpriteFont>(@"Fonts\Pericles6");
            hilight = content.Load<Texture2D>(@"Textures\TileSets\hilight");
            myMap = new MapMaker(content.Load<Texture2D>(@"Textures\TileSets\mousemap"),
                content.Load<Texture2D>(@"Textures\TileSets\slopemaps"));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Program.game.SetBackground(Color.Black);
            Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileStepX, Camera.Location.Y % Tile.TileStepY);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;

            float maxdepth = ((myMap.MapWidth + 1) + ((myMap.MapHeight + 1) * Tile.TileWidth)) * 10;
            float depthOffset;

            Point vladMapPoint = myMap.WorldToMapCell(new Point((int)Program.game.play.character.vlad.Position.X, (int)Program.game.play.character.vlad.Position.Y));

            for (int y = 0; y < squaresDown; y++)
            {
                int rowOffset = 0;
                if ((firstY + y) % 2 == 1)
                    rowOffset = Tile.OddRowXOffset;

                for (int x = 0; x < squaresAcross; x++)
                {
                    int mapx = (firstX + x);
                    int mapy = (firstY + y);
                    depthOffset = 0.7f - ((mapx + (mapy * Tile.TileWidth)) / maxdepth);

                    if ((mapx >= myMap.MapWidth) || (mapy >= myMap.MapHeight))
                        continue;

                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].BaseTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2((mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            1.0f);
                    }
                    int heightRow = 0;

                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].HeightTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2(
                                    (mapx * Tile.TileStepX) + rowOffset,
                                    mapy * Tile.TileStepY - (heightRow * Tile.HeightTileOffset))),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            depthOffset - ((float)heightRow * heightRowDepthMod));
                        heightRow++;
                    }

                    foreach (int tileID in myMap.Rows[y + firstY].Columns[x + firstX].TopperTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2((mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            depthOffset - ((float)heightRow * heightRowDepthMod));
                    }

                    if ((mapx == vladMapPoint.X) && (mapy == vladMapPoint.Y))
                    {
                        Program.game.play.character.vlad.DrawDepth = depthOffset - (float)(heightRow + 2) * heightRowDepthMod;
                    }

                    if (DebugOverlay)
                    {
                        spriteBatch.DrawString(pericles6, (x + firstX).ToString() + ", " + (y + firstY).ToString(),
                            new Vector2((x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX + 24,
                                (y * Tile.TileStepY) - offsetY + baseOffsetY + 48), Color.White, 0f, Vector2.Zero,
                                1.0f, SpriteEffects.None, 0.0f);
                    }
                }
            }

            Vector2 hilightLoc = Camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            Point hilightPoint = myMap.WorldToMapCell(new Point((int)hilightLoc.X, (int)hilightLoc.Y));

            int hilightrowOffset = 0;
            if ((hilightPoint.Y) % 2 == 1)
                hilightrowOffset = Tile.OddRowXOffset;

            spriteBatch.Draw(
                            hilight,
                            Camera.WorldToScreen(

                                new Vector2(

                                    (hilightPoint.X * Tile.TileStepX) + hilightrowOffset,

                                    (hilightPoint.Y + 2) * Tile.TileStepY)),
                            new Rectangle(0, 0, 64, 32),
                            Color.White * 0.3f,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            0.0f);
        }
    }
}
