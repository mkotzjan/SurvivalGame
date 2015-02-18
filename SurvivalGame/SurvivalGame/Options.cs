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
        /// <summary>
        /// Load content for options
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {

        }

        /// <summary>
        /// Update options
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

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
