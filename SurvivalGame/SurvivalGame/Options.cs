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
    public static class Options
    {
        public static bool debug {get; set; }

        /// <summary>
        /// Load content for options
        /// </summary>
        /// <param name="content"></param>
        public static void LoadContent(ContentManager content)
        {
            debug = false;
        }

        /// <summary>
        /// Update options
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draw options
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
