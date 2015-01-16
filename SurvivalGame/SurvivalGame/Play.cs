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
    public class Play
    {
        public MapDrawer mapReader = new MapDrawer();
        public Character character = new Character();
        public Enemy enemy = new Enemy();
        Overlay overlay = new Overlay();
        bool toggleOverlay = false;
        KeyboardState ks;
        KeyboardState ksprev;

        public Play()
        {
            ks = new KeyboardState();
            ksprev = new KeyboardState();
        }

        public void LoadContent(ContentManager content)
        {
            mapReader.LoadContent(content);
            character.LoadContent(content);
            enemy.LoadContent(content);
            Camera.ViewWidth = Program.game.graphics.PreferredBackBufferWidth;
            Camera.ViewHeight = Program.game.graphics.PreferredBackBufferHeight;
            Camera.WorldWidth = ((mapReader.myMap.MapWidth - 2) * Tile.TileStepX);
            Camera.WorldHeight = ((mapReader.myMap.MapHeight - 2) * Tile.TileStepY);
            Camera.DisplayOffset = new Vector2(mapReader.baseOffsetX, mapReader.baseOffsetY);
        }

        public void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            if (!toggleOverlay)
            {
                character.Update(gameTime);
                Camera.Move(Camera.WorldToScreen(new Vector2(character.vlad.Position.X - (Camera.ViewWidth / 2), character.vlad.Position.Y - (Camera.ViewHeight / 2))));
                enemy.Move();
            }

            if (CheckKeyboardReleased(Keys.Escape))
            {
                ToggleOverlay();
            }

            ksprev = ks;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mapReader.Draw(spriteBatch);
            character.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            if (toggleOverlay)
            {
                overlay.Draw(spriteBatch);
            }
        }

        private void ToggleOverlay()
        {
            toggleOverlay = !toggleOverlay;
        }

        private bool CheckKeyboardPressed(Keys key)
        {
            return (ks.IsKeyDown(key) && !ksprev.IsKeyDown(key));
        }

        private bool CheckKeyboardReleased(Keys key)
        {
            return (ksprev.IsKeyDown(key) && !ks.IsKeyDown(key));
        }
    }
}
