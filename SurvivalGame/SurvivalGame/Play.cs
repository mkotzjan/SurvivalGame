using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivalGame
{
    class Play
    {
        MapReader mapReader = new MapReader();
        public Play()
        {
        }

        public void LoadContent(ContentManager content)
        {
            mapReader.LoadContent(content);
            Camera.ViewWidth = Program.game.graphics.PreferredBackBufferWidth;
            Camera.ViewHeight = Program.game.graphics.PreferredBackBufferHeight;
            Camera.WorldWidth = ((mapReader.myMap.MapWidth - 2) * Tile.TileStepX);
            Camera.WorldHeight = ((mapReader.myMap.MapHeight - 2) * Tile.TileStepY);
            Camera.DisplayOffset = new Vector2(mapReader.baseOffsetX, mapReader.baseOffsetY);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                Camera.Move(new Vector2(-2, 0));
            }

            if (ks.IsKeyDown(Keys.Right))
            {
                Camera.Move(new Vector2(2, 0));
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                Camera.Move(new Vector2(0, -2));
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                Camera.Move(new Vector2(0, 2));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mapReader.Draw(spriteBatch);
        }
    }
}
