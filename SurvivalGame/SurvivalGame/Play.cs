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
            this.ks = new KeyboardState();
            this.ksprev = new KeyboardState();
        }

        public void LoadContent(ContentManager content)
        {
            this.mapReader.LoadContent(content);
            this.character.LoadContent(content);
            this.enemy.LoadContent(content);
            Camera.ViewWidth = Program.game.graphics.PreferredBackBufferWidth;
            Camera.ViewHeight = Program.game.graphics.PreferredBackBufferHeight;
            Camera.WorldWidth = ((this.mapReader.MyMap.MapWidth - 2) * Tile.TileStepX);
            Camera.WorldHeight = ((this.mapReader.MyMap.MapHeight - 2) * Tile.TileStepY);
            Camera.DisplayOffset = new Vector2(mapReader.BaseOffsetX, this.mapReader.BaseOffsetY);
        }

        public void Update(GameTime gameTime)
        {
            this.ks = Keyboard.GetState();
            if (!toggleOverlay)
            {
                this.character.Update(gameTime);
                Camera.Move(Camera.WorldToScreen(new Vector2(this.character.vlad.Position.X - (Camera.ViewWidth / 2), this.character.vlad.Position.Y - (Camera.ViewHeight / 2))));
                this.enemy.Move(gameTime);
            }

            if (this.CheckKeyboardReleased(Keys.Escape))
            {
                ToggleOverlay();
            }

            this.ksprev = this.ks;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.mapReader.Draw(spriteBatch);
            this.character.Draw(spriteBatch);
            this.enemy.Draw(spriteBatch);
            if (toggleOverlay)
            {
                mapReader.debugOverlay = false;
                overlay.Draw(spriteBatch);
            }
        }

        private void ToggleOverlay()
        {
            toggleOverlay = !toggleOverlay;
        }

        private bool CheckKeyboardPressed(Keys key)
        {
            return (this.ks.IsKeyDown(key) && !this.ksprev.IsKeyDown(key));
        }

        private bool CheckKeyboardReleased(Keys key)
        {
            return (this.ksprev.IsKeyDown(key) && !this.ks.IsKeyDown(key));
        }
    }
}
