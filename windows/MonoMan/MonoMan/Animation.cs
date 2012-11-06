using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoMan
{
    class Animation
    {
        private int mFrameCount;
        public int FrameCount 
        {
            get { return mFrameCount; }
            set { mFrameCount = value; }
        }

        private int mCurrentFrameNumber;
        public Rectangle CurrentFrame 
        {
            get { return mFrames[mCurrentFrameNumber]; }
        }

        private Rectangle[] mFrames;
        public Rectangle[] Frames 
        {
            get { return mFrames; }
            set { mFrames = value; }
        }

        private float mFrameLength;
        public float FrameLength
        {
            get { return mFrameLength; }
            set { mFrameLength = value; }
        }

        private float mTimer;
        public float Timer
        {
            get { return mTimer; }
            set { mTimer = value; }
        }

        public Animation()
        {
            mFrameCount = 0;
            mFrames = null;
            mCurrentFrameNumber = 0;
            mFrameLength = 1.0f / 6.0f;
            mTimer = 0.0f;
        }

        public void Update(GameTime aGameTime)
        {
            mTimer += (float)aGameTime.ElapsedGameTime.TotalSeconds;

            if (mTimer >= mFrameLength)
            {
                mTimer = 0.0f;
                mCurrentFrameNumber = (mCurrentFrameNumber + 1) % mFrameCount;
            }
        }
    }
}
