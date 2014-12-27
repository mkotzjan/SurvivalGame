﻿using System;
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
    class MenuComponent
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        MouseState mouse;
        MouseState prevMouse;

        SpriteFont spriteFont;
        Color color;
        int linePadding = 30;

        List<string> buttonList = new List<string>();

        Game1.GameState selected = Game1.GameState.Play;

        public MenuComponent()
        {
            buttonList.Add("Play");
            buttonList.Add("Options");
            buttonList.Add("Exit");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager content)
        {
            // spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = content.Load<SpriteFont>("Menu");
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();

            if (CheckKeyboard(Keys.Up))
            {
                if (selected > 0) selected--;
            }
            else if (CheckKeyboard(Keys.Down))
            {
                if ((int)selected < buttonList.Count - 1) selected++;
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
        }

        public bool CheckMouse()
        {
            return (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released);
        }

        public bool CheckKeyboard(Keys key)
        {
            return (keyboard.IsKeyDown(key)  && !prevKeyboard.IsKeyDown(key));
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Program.game.SetBackground(new Color(30, 30, 30));
            float x = 0;
            spriteBatch.Begin();
            for (int i = 0; i < buttonList.Count; i++)
            {
                color = (i == (int)selected) ? new Color(180, 180, 100) : new Color(58, 58, 58);
                if (i == (int)selected)
                {
                    x += 10;
                }
                else
                {
                    x = 0;
                }
                spriteBatch.DrawString(spriteFont, buttonList[i], new Vector2((Game1.screen.Width / 2) - 
                     + x, ((Game1.screen.Height / 2) -
                    (spriteFont.LineSpacing * buttonList.Count) / 2) +
                    (spriteFont.LineSpacing + linePadding * i)), color);
            }
            spriteBatch.End();
        }
    }
}