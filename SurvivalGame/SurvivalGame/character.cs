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
    public class Character
    {
        public SpriteAnimation vlad;
        KeyboardState ks;
        public void LoadContent(ContentManager content)
        {
            vlad = new SpriteAnimation(content.Load<Texture2D>(@"Textures\Characters\T_Vlad_Sword_Walking_48x48"));

            vlad.AddAnimation("WalkEast", 0, 48 * 0, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorth", 0, 48 * 1, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorthEast", 0, 48 * 2, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorthWest", 0, 48 * 3, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouth", 0, 48 * 4, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouthEast", 0, 48 * 5, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouthWest", 0, 48 * 6, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkWest", 0, 48 * 7, 48, 48, 8, 0.1f);

            vlad.AddAnimation("IdleEast", 0, 48 * 0, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorth", 0, 48 * 1, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorthEast", 0, 48 * 2, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorthWest", 0, 48 * 3, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouth", 0, 48 * 4, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouthEast", 0, 48 * 5, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouthWest", 0, 48 * 6, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleWest", 0, 48 * 7, 48, 48, 1, 0.2f);

            vlad.Position = new Vector2(100, 100);
            vlad.DrawOffset = new Vector2(-24, -38);
            vlad.CurrentAnimation = "WalkEast";
            vlad.IsAnimating = true;
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

            if (Program.game.play.mapReader.myMap.GetCellAtWorldPoint(this.vlad.Position + moveDir).Walkable == false)
            {
                moveDir = Vector2.Zero;
            }

            if (Math.Abs(Program.game.play.mapReader.myMap.GetOverallHeight(this.vlad.Position)
                - Program.game.play.mapReader.myMap.GetOverallHeight(this.vlad.Position + moveDir)) > 10)
            {
                moveDir = Vector2.Zero;
            }

            if (moveDir.Length() != 0)
            {
                this.vlad.MoveBy((int)moveDir.X, (int)moveDir.Y);
                if (this.vlad.CurrentAnimation != animation)
                    this.vlad.CurrentAnimation = animation;
            }
            else
            {
                this.vlad.CurrentAnimation = "Idle" + this.vlad.CurrentAnimation.Substring(4);
            }

            float vladX = MathHelper.Clamp(
                this.vlad.Position.X, 0 - this.vlad.DrawOffset.X - Program.game.play.mapReader.BaseOffsetX, Camera.WorldWidth);
            float vladY = MathHelper.Clamp(
                this.vlad.Position.Y, 0 - this.vlad.DrawOffset.Y - Program.game.play.mapReader.BaseOffsetY, Camera.WorldHeight);

            this.vlad.Position = new Vector2(vladX, vladY);

            this.vlad.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            vlad.Draw(spriteBatch, 0, -Program.game.play.mapReader.myMap.GetOverallHeight(vlad.Position));
        }
    }
}
