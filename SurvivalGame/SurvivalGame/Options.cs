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

        int selected = 0;

        List<string> optionsList = new List<string>();

        /// <summary>
        /// Load content for options
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\Menu");
            optionsList.Add("Debugging");
            optionsList.Add("Testspinner");
            optionsList.Add("TestSlider");
        }

        /// <summary>
        /// Update options
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            this.keyboard = Keyboard.GetState();

            if (this.CheckKeyboard(Keys.Escape))
            {
                Game1.gameState = Game1.GameState.Menu;
            }
            else if (this.CheckKeyboard(Keys.Up))
            {
                if (this.selected > 0)
                {
                    selected--;
                }
            }
            else if (this.CheckKeyboard(Keys.Down))
            {
                if ((int)selected < this.optionsList.Count - 1)
                {
                    selected++;
                }
            }

            if (this.CheckKeyboard(Keys.Enter))
            {
                
            }

            this.prevKeyboard = this.keyboard;
        }

        /// <summary>
        /// Draw options
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Program.game.SetBackground(new Color(30, 30, 30));

            int x = 0;
            for (int i = 0; i < this.optionsList.Count; i++)
            {
                this.color = (i == selected) ? new Color(180, 180, 100) : new Color(58, 58, 58);
                if (i == selected)
                {
                    x = 5;
                }
                else
                {
                    x = 0;
                }
                spriteBatch.DrawString(spriteFont, this.optionsList[i], new Vector2((Game1.screen.Width / 4) -
                     +x, ((Game1.screen.Height / 2) -
                    (spriteFont.LineSpacing * this.optionsList.Count) / 2) +
                    (spriteFont.LineSpacing + this.linePadding * i)), this.color);
            }
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
