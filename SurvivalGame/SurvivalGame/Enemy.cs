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

        SpriteAnimation vlad2;

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

            vlad2.Position = new Vector2(100, 100);
            vlad2.DrawOffset = new Vector2(-24, -38);
            vlad2.CurrentAnimation = "WalkEast";
            vlad2.IsAnimating = true;

            vlad2.Position = new Vector2(600, 600);
            vlad2.DrawOffset = new Vector2(-24, -38);
            vlad2.CurrentAnimation = "WalkEast";
            vlad2.IsAnimating = true;
        }

        public void Move()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            vlad2.Draw(spriteBatch, 0, -Program.game.play.mapReader.myMap.GetOverallHeight(vlad2.Position));
        }
    }
}
