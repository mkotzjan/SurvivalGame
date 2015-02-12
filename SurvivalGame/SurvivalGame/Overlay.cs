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
using SurvivalGame.GUI;

namespace SurvivalGame
{
    class Overlay
    {
        Texture2D overlay;
        int width = Program.game.graphics.PreferredBackBufferWidth;
        int height = Program.game.graphics.PreferredBackBufferHeight;

        public Overlay()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.overlay = new Texture2D(Program.game.graphics.GraphicsDevice, this.width, this.height);
            Color[] data = new Color[this.width * this.height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            this.overlay.SetData(data);
            spriteBatch.Draw(overlay, new Vector2(0, 0), Color.Black * 0.6f);
        }
    }
}
