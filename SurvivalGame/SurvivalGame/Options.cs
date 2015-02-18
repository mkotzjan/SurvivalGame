using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SurvivalGame
{
    public class Options
    {
        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        SpriteFont spriteFont;
        Color color;
        int linePadding = 30;

        List<string> buttonList = new List<string>();

        /// <summary>
        /// Load content for options
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\Menu");
        }

        /// <summary>
        /// Update options
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            this.keyboard = Keyboard.GetState();

            if (CheckKeyboard(Keys.Escape))
            {
                Game1.gameState = Game1.GameState.Menu;
            }

            this.prevKeyboard = this.keyboard;
        }

        /// <summary>
        /// Draw options
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {

        }

        /// <summary>
        /// Check if given key is pressed
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true or false</returns>
        private bool CheckKeyboard(Keys key)
        {
            return (this.keyboard.IsKeyDown(key) && !this.prevKeyboard.IsKeyDown(key));
        }
    }
}
