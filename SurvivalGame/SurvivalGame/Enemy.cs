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
    public class Enemy
    {
        public bool isDead { get; set; }
        private int strength;
        private int health;

        public SpriteAnimation vlad2;

        public void LoadContent(ContentManager content)
        {
            vlad2 = new SpriteAnimation(content.Load<Texture2D>(@"Textures\Characters\T_Vlad_Sword_Walking_48x48"));

            vlad2.AddAnimation("WalkEast", 0, 48 * 0, 48, 48, 8, 0.1f);
            vlad2.AddAnimation("WalkNorth", 0, 48 * 1, 48, 48, 8, 0.1f);
            vlad2.AddAnimation("WalkNorthEast", 0, 48 * 2, 48, 48, 8, 0.1f);
            vlad2.AddAnimation("WalkNorthWest", 0, 48 * 3, 48, 48, 8, 0.1f);
            vlad2.AddAnimation("WalkSouth", 0, 48 * 4, 48, 48, 8, 0.1f);
            vlad2.AddAnimation("WalkSouthEast", 0, 48 * 5, 48, 48, 8, 0.1f);
            vlad2.AddAnimation("WalkSouthWest", 0, 48 * 6, 48, 48, 8, 0.1f);
            vlad2.AddAnimation("WalkWest", 0, 48 * 7, 48, 48, 8, 0.1f);

            vlad2.AddAnimation("IdleEast", 0, 48 * 0, 48, 48, 1, 0.2f);
            vlad2.AddAnimation("IdleNorth", 0, 48 * 1, 48, 48, 1, 0.2f);
            vlad2.AddAnimation("IdleNorthEast", 0, 48 * 2, 48, 48, 1, 0.2f);
            vlad2.AddAnimation("IdleNorthWest", 0, 48 * 3, 48, 48, 1, 0.2f);
            vlad2.AddAnimation("IdleSouth", 0, 48 * 4, 48, 48, 1, 0.2f);
            vlad2.AddAnimation("IdleSouthEast", 0, 48 * 5, 48, 48, 1, 0.2f);
            vlad2.AddAnimation("IdleSouthWest", 0, 48 * 6, 48, 48, 1, 0.2f);
            vlad2.AddAnimation("IdleWest", 0, 48 * 7, 48, 48, 1, 0.2f);

            vlad2.Position = new Vector2(500, 500);
            vlad2.DrawOffset = new Vector2(-24, -38);
            vlad2.CurrentAnimation = "WalkEast";
            vlad2.IsAnimating = true;
        }

        public void Move(GameTime gameTime)
        {
            Vector2 moveVector = Vector2.Zero;
            Vector2 moveDir = Vector2.Zero;
            string animation = "";

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.A) && ks.IsKeyDown(Keys.W))
            {
                moveDir = new Vector2(-2, -1);
                animation = "WalkNorthWest";
                moveVector += new Vector2(-2, -1);
            }

            else if (ks.IsKeyDown(Keys.D) && ks.IsKeyDown(Keys.W))
            {
                moveDir = new Vector2(2, -1);
                animation = "WalkNorthEast";
                moveVector += new Vector2(2, -1);
            }

            else if (ks.IsKeyDown(Keys.A) && ks.IsKeyDown(Keys.S))
            {
                moveDir = new Vector2(-2, 1);
                animation = "WalkSouthWest";
                moveVector += new Vector2(-2, 1);
            }

            else if (ks.IsKeyDown(Keys.D) && ks.IsKeyDown(Keys.S))
            {
                moveDir = new Vector2(2, 1);
                animation = "WalkSouthEast";
                moveVector += new Vector2(2, 1);
            }

            else if (ks.IsKeyDown(Keys.W))
            {
                moveDir = new Vector2(0, -1);
                animation = "WalkNorth";
                moveVector += new Vector2(0, -1);
            }

            else if (ks.IsKeyDown(Keys.A))
            {
                moveDir = new Vector2(-2, 0);
                animation = "WalkWest";
                moveVector += new Vector2(-2, 0);
            }

            else if (ks.IsKeyDown(Keys.D))
            {
                moveDir = new Vector2(2, 0);
                animation = "WalkEast";
                moveVector += new Vector2(2, 0);
            }

            else if (ks.IsKeyDown(Keys.S))
            {
                moveDir = new Vector2(0, 1);
                animation = "WalkSouth";
                moveVector += new Vector2(0, 1);
            }

            if (Program.game.play.mapReader.MyMap.GetCellAtWorldPoint(this.vlad2.Position + moveDir).Walkable == false)
            {
                moveDir = Vector2.Zero;
            }

            if (Math.Abs(Program.game.play.mapReader.MyMap.GetOverallHeight(this.vlad2.Position)
                - Program.game.play.mapReader.MyMap.GetOverallHeight(this.vlad2.Position + moveDir)) > 10)
            {
                moveDir = Vector2.Zero;
            }

            if (moveDir.Length() != 0)
            {
                this.vlad2.MoveBy((int)moveDir.X, (int)moveDir.Y);
                if (this.vlad2.CurrentAnimation != animation)
                    this.vlad2.CurrentAnimation = animation;
            }
            else
            {
                this.vlad2.CurrentAnimation = "Idle" + this.vlad2.CurrentAnimation.Substring(4);
            }

            float vladX = MathHelper.Clamp(
                this.vlad2.Position.X, 0 - this.vlad2.DrawOffset.X - Program.game.play.mapReader.BaseOffsetX, Camera.WorldWidth);
            float vladY = MathHelper.Clamp(
                this.vlad2.Position.Y, 0 - this.vlad2.DrawOffset.Y - Program.game.play.mapReader.BaseOffsetY, Camera.WorldHeight);

            this.vlad2.Position = new Vector2(vladX, vladY);

            this.vlad2.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            vlad2.Draw(spriteBatch, 0, -Program.game.play.mapReader.MyMap.GetOverallHeight(vlad2.Position));
        }
    }
}
