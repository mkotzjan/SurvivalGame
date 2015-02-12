using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SurvivalGame
{
    public class FrameAnimation : ICloneable
    {
        // The first frame of the Animation.  We will calculate other
        // frames on the fly based on this frame.
        private Rectangle rectInitialFrame;


        // Number of frames in the Animation
        private int iFrameCount = 1;


        // The frame currently being displayed. 
        // This value ranges from 0 to iFrameCount-1
        private int iCurrentFrame = 0;


        // Amount of time (in seconds) to display each frame
        private float fFrameLength = 0.2f;


        // Amount of time that has passed since we last animated
        private float fFrameTimer = 0.0f;


        // The number of times this animation has been played
        private int iPlayCount = 0;


        // The animation that should be played after this animation
        private string sNextAnimation = null;

        /// 
        /// The number of frames the animation contains
        /// 
        public int FrameCount
        {
            get { return this.iFrameCount; }
            set { this.iFrameCount = value; }
        }

        /// 
        /// The time (in seconds) to display each frame
        /// 
        public float FrameLength
        {
            get { return this.fFrameLength; }
            set { this.fFrameLength = value; }
        }

        /// 
        /// The frame number currently being displayed
        /// 
        public int CurrentFrame
        {
            get { return this.iCurrentFrame; }
            set { this.iCurrentFrame = (int)MathHelper.Clamp(value, 0, this.iFrameCount - 1); }
        }

        public int FrameWidth
        {
            get { return rectInitialFrame.Width; }
        }

        public int FrameHeight
        {
            get { return rectInitialFrame.Height; }
        }

        /// 
        /// The rectangle associated with the current
        /// animation frame.
        /// 
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(
                    this.rectInitialFrame.X + (this.rectInitialFrame.Width * this.iCurrentFrame),
                    this.rectInitialFrame.Y, this.rectInitialFrame.Width, this.rectInitialFrame.Height);
            }
        }

        public int PlayCount
        {
            get { return this.iPlayCount; }
            set { this.iPlayCount = value; }
        }

        public string NextAnimation
        {
            get { return sNextAnimation; }
            set { sNextAnimation = value; }
        }

        public FrameAnimation(Rectangle FirstFrame, int Frames)
        {
            this.rectInitialFrame = FirstFrame;
            this.iFrameCount = Frames;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int Frames)
        {
            rectInitialFrame = new Rectangle(X, Y, Width, Height);
            this.iFrameCount = Frames;
        }

        public FrameAnimation(int X, int Y, int Width, int Height, int Frames, float FrameLength)
        {
            this.rectInitialFrame = new Rectangle(X, Y, Width, Height);
            this.iFrameCount = Frames;
            this.fFrameLength = FrameLength;
        }

        public FrameAnimation(int X, int Y,
            int Width, int Height, int Frames,
            float FrameLength, string strNextAnimation)
        {
            this.rectInitialFrame = new Rectangle(X, Y, Width, Height);
            this.iFrameCount = Frames;
            this.fFrameLength = FrameLength;
            this.sNextAnimation = strNextAnimation;
        }

        public void Update(GameTime gameTime)
        {
            this.fFrameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.fFrameTimer > this.fFrameLength)
            {
                this.fFrameTimer = 0.0f;
                this.iCurrentFrame = (this.iCurrentFrame + 1) % this.iFrameCount;
                if (this.iCurrentFrame == 0)
                    this.iPlayCount = (int)MathHelper.Min(this.iPlayCount + 1, int.MaxValue);
            }
        }

        object ICloneable.Clone()
        {
            return new FrameAnimation(this.rectInitialFrame.X, this.rectInitialFrame.Y,
                                      this.rectInitialFrame.Width, this.rectInitialFrame.Height,
                                      this.iFrameCount, this.fFrameLength, sNextAnimation);
        }
    }
}
