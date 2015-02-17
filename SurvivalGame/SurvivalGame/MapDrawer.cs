namespace SurvivalGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class MapDrawer
    {
        public MapMaker MyMap;
        public int SquaresAcross = 17;
        public int SquaresDown = 37;
        public int BaseOffsetX = -32;
        public int BaseOffsetY = -64;
        float heightRowDepthMod = 0.00001f;
        private Texture2D hilight;

        SpriteFont pericles6;

        public void LoadContent(ContentManager content)
        {
            Tile.TileSetTexture = content.Load<Texture2D>(@"Textures\TileSets\tileset");
            this.pericles6 = content.Load<SpriteFont>(@"Fonts\Pericles6");
            this.hilight = content.Load<Texture2D>(@"Textures\TileSets\hilight");
            this.MyMap = new MapMaker(content.Load<Texture2D>(@"Textures\TileSets\mousemap"),
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

            float maxdepth = ((this.MyMap.MapWidth + 1) + ((this.MyMap.MapHeight + 1) * Tile.TileWidth)) * 10;
            float depthOffset;

            Point vladMapPoint = this.MyMap.WorldToMapCell(new Point((int)Program.game.play.character.vlad.Position.X, (int)Program.game.play.character.vlad.Position.Y));
            Point vlad2MapPoint = this.MyMap.WorldToMapCell(new Point((int)Program.game.play.enemy.vlad2.Position.X, (int)Program.game.play.enemy.vlad2.Position.Y));

            for (int y = 0; y < this.SquaresDown; y++)
            {
                int rowOffset = 0;
                if ((firstY + y) % 2 == 1)
                {
                    rowOffset = Tile.OddRowXOffset;
                }

                for (int x = 0; x < this.SquaresAcross; x++)
                {
                    int mapx = (firstX + x);
                    int mapy = (firstY + y);
                    depthOffset = 0.7f - ((mapx + (mapy * Tile.TileWidth)) / maxdepth);

                    if ((mapx >= this.MyMap.MapWidth) || (mapy >= this.MyMap.MapHeight))
                    {
                        continue;
                    }

                    foreach (int tileID in this.MyMap.Rows[mapy].Columns[mapx].BaseTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2((mapx * Tile.TileStepX) + rowOffset, mapy * Tile.TileStepY)),
                            Tile.GetSourceRectangle(tileID),
                            Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
                    }

                    int heightRow = 0;

                    foreach (int tileID in this.MyMap.Rows[mapy].Columns[mapx].HeightTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            Camera.WorldToScreen(
                                new Vector2(
                                    (mapx * Tile.TileStepX) + rowOffset,
                                    (mapy * Tile.TileStepY) - (heightRow * Tile.HeightTileOffset))),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            depthOffset - ((float)heightRow * this.heightRowDepthMod));
                        heightRow++;
                    }

                    foreach (int tileID in this.MyMap.Rows[y + firstY].Columns[x + firstX].TopperTiles)
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
                            depthOffset - ((float)heightRow * this.heightRowDepthMod));
                    }

                    if ((mapx == vladMapPoint.X) && (mapy == vladMapPoint.Y))
                    {
                        Program.game.play.character.vlad.DrawDepth = depthOffset - ((float)(heightRow + 2) * this.heightRowDepthMod);
                    }

                    // Add by trying to fix draw position of enemy
                    if ((mapx == vlad2MapPoint.X) && (mapy == vlad2MapPoint.Y))
                    {
                        Program.game.play.enemy.vlad2.DrawDepth = depthOffset - ((float)(heightRow + 2) * this.heightRowDepthMod);
                    }

                    if (Options.debug)
                    {
                        spriteBatch.DrawString(this.pericles6, (x + firstX).ToString() + ", " + (y + firstY).ToString(),
                            new Vector2((x * Tile.TileStepX) - offsetX + rowOffset + this.BaseOffsetX + 24,
                                (y * Tile.TileStepY) - offsetY + this.BaseOffsetY + 48), Color.White, 0f, Vector2.Zero,
                                1.0f, SpriteEffects.None, 0.0f);
                    }
                }
            }

            Vector2 hilightLoc = Camera.ScreenToWorld(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            Point hilightPoint = this.MyMap.WorldToMapCell(new Point((int)hilightLoc.X, (int)hilightLoc.Y));

            int hilightrowOffset = 0;
            if ((hilightPoint.Y) % 2 == 1)
            {
                hilightrowOffset = Tile.OddRowXOffset;
            }

            spriteBatch.Draw(
                            this.hilight,
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
