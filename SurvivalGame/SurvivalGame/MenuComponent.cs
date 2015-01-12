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
    class MenuComponent
    {
        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        MouseState mouse;
        MouseState prevMouse;

        SpriteFont spriteFont;
        Color color;
        int linePadding = 30;

        List<string> buttonList = new List<string>();

        Game1.GameState selected = Game1.GameState.Play;

        Spinner spinner;
        List<string> spinnerList = new List<string>();

        /// <summary>
        /// Constructor
        /// </summary>
        public MenuComponent()
        {
            buttonList.Add("Play");
            buttonList.Add("Options");
            buttonList.Add("Exit");

            spinnerList.Add("Test1");
            spinnerList.Add("Test2");
            spinnerList.Add("Test3");
            spinnerList.Add("Test4");
            spinnerList.Add("Test5");
            spinner = new Spinner(spinnerList);
        }

        /// <summary>
        /// Loads the menu content
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\Menu");
        }

        /// <summary>
        /// Update the Menu
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();

            if (CheckKeyboard(Keys.Up))
            {
                if (selected > 0)
                {
                    selected--;
                }
            }
            else if (CheckKeyboard(Keys.Down))
            {
                if ((int)selected < buttonList.Count - 1)
                {
                    selected++;
                }
            }

            if (CheckKeyboard(Keys.Enter))
            {
                if ((int)selected == 2)
                {
                    Program.game.Quit();
                }
                else Game1.gameState = selected;
                
            }

            prevKeyboard = keyboard;
            prevMouse = mouse;

            spinner.Update(gameTime);
        }

        /// <summary>
        /// Check for mouse click
        /// </summary>
        /// <returns>true or false</returns>
        public bool CheckMouse()
        {
            return (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released);
        }

        /// <summary>
        /// Check if given key is pressed
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true or false</returns>
        public bool CheckKeyboard(Keys key)
        {
            return (keyboard.IsKeyDown(key)  && !prevKeyboard.IsKeyDown(key));
        }

        /// <summary>
        /// This is called when the menu should draw itself
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            Program.game.SetBackground(new Color(30, 30, 30));
            normalPosition(spriteBatch);
        }


        /// <summary>
        /// The normal menu position
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void normalPosition(SpriteBatch spriteBatch)
        {
            int x = 0;
            for (int i = 0; i < buttonList.Count; i++)
            {
                color = (i == (int)selected) ? new Color(180, 180, 100) : new Color(58, 58, 58);
                if (i == (int)selected)
                {
                    x = 5;
                }
                else
                {
                    x = 0;
                }
                spriteBatch.DrawString(spriteFont, buttonList[i], new Vector2((Game1.screen.Width / 2) -
                     +x, ((Game1.screen.Height / 2) -
                    (spriteFont.LineSpacing * buttonList.Count) / 2) +
                    (spriteFont.LineSpacing + linePadding * i)), color);
                spinner.Draw(spriteBatch, spriteFont, new Vector2(20, 20), new Color(58, 58, 58), new Color(180, 180, 100));
            }
        }
    }
}
