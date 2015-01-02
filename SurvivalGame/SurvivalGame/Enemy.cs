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
    class Enemy
    {
        public bool isDead { get; set; }
        private int strength;
        private int health;

        public void LoadContent(ContentManager content)
        {
            //vlad = new SpriteAnimation(content.Load<Texture2D>(@"Textures\Characters\T_Vlad_Sword_Walking_48x48"));

            //vlad.AddAnimation("WalkEast", 0, 48 * 0, 48, 48, 8, 0.1f);
            //vlad.AddAnimation("WalkNorth", 0, 48 * 1, 48, 48, 8, 0.1f);
            //vlad.AddAnimation("WalkNorthEast", 0, 48 * 2, 48, 48, 8, 0.1f);
            //vlad.AddAnimation("WalkNorthWest", 0, 48 * 3, 48, 48, 8, 0.1f);
            //vlad.AddAnimation("WalkSouth", 0, 48 * 4, 48, 48, 8, 0.1f);
            //vlad.AddAnimation("WalkSouthEast", 0, 48 * 5, 48, 48, 8, 0.1f);
            //vlad.AddAnimation("WalkSouthWest", 0, 48 * 6, 48, 48, 8, 0.1f);
            //vlad.AddAnimation("WalkWest", 0, 48 * 7, 48, 48, 8, 0.1f);

            //vlad.AddAnimation("IdleEast", 0, 48 * 0, 48, 48, 1, 0.2f);
            //vlad.AddAnimation("IdleNorth", 0, 48 * 1, 48, 48, 1, 0.2f);
            //vlad.AddAnimation("IdleNorthEast", 0, 48 * 2, 48, 48, 1, 0.2f);
            //vlad.AddAnimation("IdleNorthWest", 0, 48 * 3, 48, 48, 1, 0.2f);
            //vlad.AddAnimation("IdleSouth", 0, 48 * 4, 48, 48, 1, 0.2f);
            //vlad.AddAnimation("IdleSouthEast", 0, 48 * 5, 48, 48, 1, 0.2f);
            //vlad.AddAnimation("IdleSouthWest", 0, 48 * 6, 48, 48, 1, 0.2f);
            //vlad.AddAnimation("IdleWest", 0, 48 * 7, 48, 48, 1, 0.2f);

            //vlad.Position = new Vector2(100, 100);
            //vlad.DrawOffset = new Vector2(-24, -38);
            //vlad.CurrentAnimation = "WalkEast";
            //vlad.IsAnimating = true;
        }

        public void move()
        {

        }

        public void draw(SpriteBatch spriteBatch)
        {

        }
    }
}
