using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMan
{
    enum Direction { Left, Right };

    class GameObject
    {
        public GameObject()
        {
            mPosition = Vector2.Zero;
            mSprite = null;
            mScale = 1.0f;
            mSpeed = Vector2.Zero;
            mVisible = false;
            mDirection = Direction.Right;
        }

        private Vector2 mPosition;
        public Vector2 Position 
        {
            get { return mPosition; }
            set { mPosition = value; }
        }

        private Texture2D mSprite;
        public Texture2D Sprite 
        {
            get { return mSprite; }
            set { mSprite = value; }
        }

        private float mScale;
        public float Scale 
        {
            get { return mScale; }
            set { mScale = value; }
        }

        private Vector2 mSpeed;
        public Vector2 Speed 
        {
            get { return mSpeed; }
            set { mSpeed = value; }
        }

        private bool mVisible;
        public bool Visible
        {
            get { return mVisible; }
            set { mVisible = value; }
        }

        private Direction mDirection;
        public Direction ObjectDirection
        {
            get { return mDirection; }
            set { mDirection = value; }
        }
    }
}
