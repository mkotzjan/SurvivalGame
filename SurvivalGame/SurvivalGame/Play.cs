using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivalGame
{
    class Play
    {
        MapReader mapReader;
        public Play()
        {
            mapReader = new MapReader();
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X - 2, 0, (mapReader.myMap.MapWidth - mapReader.squaresAcross) * 32);
            }

            if (ks.IsKeyDown(Keys.Right))
            {
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X + 2, 0, (mapReader.myMap.MapWidth - mapReader.squaresAcross) * 32);
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - 2, 0, (mapReader.myMap.MapHeight - mapReader.squaresDown) * 32);
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + 2, 0, (mapReader.myMap.MapHeight - mapReader.squaresDown) * 32);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mapReader.Draw(spriteBatch);
        }
    }
}
