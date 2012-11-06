using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace MonoMan
{
    class Camera
    {
        private Vector2 mPosition;
        public Vector2 Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }
        private int mWidth;
        public int Width
        {
            get { return mWidth; }
            set { mWidth = value; }
        }
        private int mHeight;
        public int Height
        {
            get { return mHeight; }
            set { mHeight = value; }
        }

        public Camera()
        {
            mPosition = Vector2.Zero;
        }

        public Vector2 Transform(Vector2 aObjectPosition)
        {
            return new Vector2((aObjectPosition.X + mPosition.X),
                (Height - aObjectPosition.Y + mPosition.Y));
        }
    }
}
