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

namespace SurvivalGame.GUI
{
    class Spinner
    {
        // Variables
        List<string> contentList;

        int quantity;
        int selected;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        /// <summary>
        /// Constructor for Lists
        /// </summary>
        /// <param name="list"></param>
        public Spinner(List<string> list)
        {
            contentList = list;
            quantity = list.Count();
            selected = 0;
        }

        /// <summary>
        /// Update the spinner
        /// </summary>
        /// <param name="gametime"></param>
        public void Update(GameTime gametime)
        {
            keyboard = Keyboard.GetState();

            if (CheckKeyboard(Keys.Up))
            {
                if (selected > 0)
                {
                    selected--;
                }
            }
            else if (CheckKeyboard(Keys.Down))
            {
                if ((int)selected < quantity - 1)
                {
                    selected++;
                }
            }

            prevKeyboard = keyboard;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 pos, Color normalColor, Color selectedColor)
        {
            int selectedFrozen = 0 - selected;
            for (int i = selectedFrozen; i < selectedFrozen + quantity; i++)
            {
                Color color = (i + selected == (int)selected) ? selectedColor : normalColor;
                if (contentList.Any())
                {
                    spriteBatch.DrawString(spriteFont, contentList[i + selected], new Vector2(pos.X, pos.Y + (20 * i)), color);
                }
            }
        }

        /// <summary>
        /// Check if given key is pressed
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true or false</returns>
        public bool CheckKeyboard(Keys key)
        {
            return (keyboard.IsKeyDown(key) && !prevKeyboard.IsKeyDown(key));
        }
    }
}
