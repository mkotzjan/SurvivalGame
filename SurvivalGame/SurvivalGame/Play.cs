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
        public MapReader mapReader = new MapReader();
        Character character = new Character();

        public Play()
        {
        }

        public void LoadContent(ContentManager content)
        {
            mapReader.LoadContent(content);
            character.LoadContent(content);
            Camera.ViewWidth = Program.game.graphics.PreferredBackBufferWidth;
            Camera.ViewHeight = Program.game.graphics.PreferredBackBufferHeight;
            Camera.WorldWidth = ((mapReader.myMap.MapWidth - 2) * Tile.TileStepX);
            Camera.WorldHeight = ((mapReader.myMap.MapHeight - 2) * Tile.TileStepY);
            Camera.DisplayOffset = new Vector2(mapReader.baseOffsetX, mapReader.baseOffsetY);
        }

        public void Update(GameTime gameTime)
        {
            Vector2 moveVector = Vector2.Zero;
            Vector2 moveDir = Vector2.Zero;
            string animation = "";

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Left) && ks.IsKeyDown(Keys.Up))
            {
                moveDir = new Vector2(-2, -1);
                animation = "WalkNorthWest";
                moveVector += new Vector2(-2, -1);
            }

            else if (ks.IsKeyDown(Keys.Right) && ks.IsKeyDown(Keys.Up))
            {
                moveDir = new Vector2(2, -1);
                animation = "WalkNorthEast";
                moveVector += new Vector2(2, -1);
            }

            else if (ks.IsKeyDown(Keys.Left) && ks.IsKeyDown(Keys.Down))
            {
                moveDir = new Vector2(-2, 1);
                animation = "WalkSouthWest";
                moveVector += new Vector2(-2, 1);
            }

            else if (ks.IsKeyDown(Keys.Right) && ks.IsKeyDown(Keys.Down))
            {
                moveDir = new Vector2(2, 1);
                animation = "WalkSouthEast";
                moveVector += new Vector2(2, 1);
            }

            else if (ks.IsKeyDown(Keys.Up))
            {
                moveDir = new Vector2(0, -1);
                animation = "WalkNorth";
                moveVector += new Vector2(0, -1);
            }

            else if (ks.IsKeyDown(Keys.Left))
            {
                moveDir = new Vector2(-2, 0);
                animation = "WalkWest";
                moveVector += new Vector2(-2, 0);
            }

            else if (ks.IsKeyDown(Keys.Right))
            {
                moveDir = new Vector2(2, 0);
                animation = "WalkEast";
                moveVector += new Vector2(2, 0);
            }

            else if (ks.IsKeyDown(Keys.Down))
            {
                moveDir = new Vector2(0, 1);
                animation = "WalkSouth";
                moveVector += new Vector2(0, 1);
            }

            if (moveDir.Length() != 0)
            {
                character.vlad.MoveBy((int)moveDir.X, (int)moveDir.Y);
                if (character.vlad.CurrentAnimation != animation)
                    character.vlad.CurrentAnimation = animation;
            }
            else
            {
                character.vlad.CurrentAnimation = "Idle" + character.vlad.CurrentAnimation.Substring(4);
            }

            float vladX = MathHelper.Clamp(
                character.vlad.Position.X, 0 - character.vlad.DrawOffset.X - mapReader.baseOffsetX, Camera.WorldWidth);
            float vladY = MathHelper.Clamp(
                character.vlad.Position.Y, 0 - character.vlad.DrawOffset.Y - mapReader.baseOffsetY, Camera.WorldHeight);

            character.vlad.Position = new Vector2(vladX, vladY);

            Vector2 testPosition = Camera.WorldToScreen(character.vlad.Position);

            if (testPosition.X < 100)
            {
                Camera.Move(new Vector2(testPosition.X - 100, 0));
            }

            if (testPosition.X > (Camera.ViewWidth - 100))
            {
                Camera.Move(new Vector2(testPosition.X - (Camera.ViewWidth - 100), 0));
            }

            if (testPosition.Y < 100)
            {
                Camera.Move(new Vector2(0, testPosition.Y - 100));
            }

            if (testPosition.Y > (Camera.ViewHeight - 100))
            {
                Camera.Move(new Vector2(0, testPosition.Y - (Camera.ViewHeight - 100)));
            }

            character.vlad.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mapReader.Draw(spriteBatch);
            character.Draw(spriteBatch);
        }
    }
}
