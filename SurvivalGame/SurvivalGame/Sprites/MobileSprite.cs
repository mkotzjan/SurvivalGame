using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SurvivalGame
{
    public class MobileSprite
    {
        // The SpriteAnimation object that holds the graphical and animation data for this object
        SpriteAnimation asSprite;

        // A queue of pathing vectors to allow the sprite to move along a path
        Queue<Vector2> queuePath = new Queue<Vector2>();

        // The location the sprite is currently moving towards
        Vector2 v2Target;

        // The speed at which the sprite will close with it's target
        float fSpeed = 1f;

        // These two integers represent a clipping range for determining bounding-box style
        // collisions.  They return the bounding box of the sprite trimmed by a horizonal and
        // verticle offset to get a collision cushion
        int iCollisionBufferX = 0;
        int iCollisionBufferY = 0;

        // Determine the status of the sprite.  An inactive sprite will not be updated but will be drawn.
        bool bActive = true;

        // Determines if the sprite should track towards a v2Target.  If set to false, the sprite
        // will not move on it's own towards v2Target, and will not process pathing information
        bool bMovingTowardsTarget = true;

        // Determines if the sprite will follow the path in it's Path queue.  If true, when the sprite
        // has reached v2Target the next path node will be pulled from the queue and set as
        // the new v2Target.
        bool bPathing = true;

        // If true, any pathing node popped from the Queue will be placed back onto the end of the queue
        bool bLoopPath = true;

        // If true, the sprite can collide with other objects.  Note that this is only provided as a flag
        // for testing with outside code.
        bool bCollidable = true;

        // If true, the sprite will be drawn to the screen
        bool bVisible = true;

        // If true, the sprite will be deactivated when the Pathing Queue is empty.
        bool bDeactivateAtEndOfPath = false;

        // If true, bVisible will be set to false when the Pathing Queue is empty.
        bool bHideAtEndOfPath = false;

        // If set, when the Pathing Queue is empty, the named animation will be set as the
        // current animation on the sprite.
        string sEndPathAnimation = null;

        public SpriteAnimation Sprite
        {
            get { return this.asSprite; }
        }

        public Vector2 Position
        {
            get { return this.asSprite.Position; }
            set { this.asSprite.Position = value; }
        }

        public Vector2 Target
        {
            get { return v2Target; }
            set { v2Target = value; }
        }

        public int HorizontalCollisionBuffer
        {
            get { return this.iCollisionBufferX; }
            set { this.iCollisionBufferX = value; }
        }

        public int VerticalCollisionBuffer
        {
            get { return this.iCollisionBufferY; }
            set { this.iCollisionBufferY = value; }
        }

        public bool IsPathing
        {
            get { return this.bPathing; }
            set { this.bPathing = value; }
        }

        public bool DeactivateAfterPathing
        {
            get { return this.bDeactivateAtEndOfPath; }
            set { this.bDeactivateAtEndOfPath = value; }
        }

        public bool LoopPath
        {
            get { return this.bLoopPath; }
            set { this.bLoopPath = value; }
        }

        public string EndPathAnimation
        {
            get { return this.sEndPathAnimation; }
            set { this.sEndPathAnimation = value; }
        }

        public bool HideAtEndOfPath
        {
            get { return this.bHideAtEndOfPath; }
            set { this.bHideAtEndOfPath = value; }
        }

        public bool IsVisible
        {
            get { return this.bVisible; }
            set { this.bVisible = value; }
        }

        public float Speed
        {
            get { return this.fSpeed; }
            set { this.fSpeed = value; }
        }

        public bool IsActive
        {
            get { return this.bActive; }
            set { this.bActive = value; }
        }

        public bool IsMoving
        {
            get { return this.bMovingTowardsTarget; }
            set { this.bMovingTowardsTarget = value; }
        }

        public bool IsCollidable
        {
            get { return this.bCollidable; }
            set { this.bCollidable = value; }
        }

        public Rectangle BoundingBox
        {
            get { return this.asSprite.BoundingBox; }
        }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    this.asSprite.BoundingBox.X + this.iCollisionBufferX,
                    this.asSprite.BoundingBox.Y + this.iCollisionBufferY,
                    this.asSprite.Width - (2 * this.iCollisionBufferX),
                    this.asSprite.Height - (2 * this.iCollisionBufferY));
            }
        }

        public MobileSprite(Texture2D texture)
        {
            this.asSprite = new SpriteAnimation(texture);
        }

        public void AddPathNode(Vector2 node)
        {
            queuePath.Enqueue(node);
        }

        public void AddPathNode(int X, int Y)
        {
            queuePath.Enqueue(new Vector2(X, Y));
        }

        public void ClearPathNodes()
        {
            queuePath.Clear();
        }

        public void Update(GameTime gameTime)
        {
            if (this.bActive && this.bMovingTowardsTarget)
            {
                if (!(v2Target == null))
                {
                    // Get a vector pointing from the current location of the sprite
                    // to the destination.
                    Vector2 Delta = new Vector2(v2Target.X - this.asSprite.X, v2Target.Y - this.asSprite.Y);

                    if (Delta.Length() > Speed)
                    {
                        Delta.Normalize();
                        Delta *= Speed;
                        Position += Delta;
                    }
                    else
                    {
                        if (v2Target == this.asSprite.Position)
                        {
                            if (this.bPathing)
                            {
                                if (queuePath.Count > 0)
                                {
                                    v2Target = queuePath.Dequeue();
                                    if (this.bLoopPath)
                                    {
                                        queuePath.Enqueue(v2Target);
                                    }
                                }
                                else
                                {
                                    if (!(this.sEndPathAnimation == null))
                                    {
                                        if (!(Sprite.CurrentAnimation == sEndPathAnimation))
                                        {
                                            Sprite.CurrentAnimation = sEndPathAnimation;
                                        }
                                    }

                                    if (this.bDeactivateAtEndOfPath)
                                    {
                                        this.IsActive = false;
                                    }

                                    if (this.bHideAtEndOfPath)
                                    {
                                        this.IsVisible = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.asSprite.Position = v2Target;
                        }
                    }
                }
            }
            if (this.bActive)
                this.asSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.bVisible)
            {
                this.asSprite.Draw(spriteBatch, 0, 0);
            }
        }
    }
}
