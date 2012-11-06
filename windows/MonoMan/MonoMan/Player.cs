using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMan
{
    enum PlayerState { Idle, Run, Jump, Injured, Attack, JumpAttack };


    class Player : GameObject
    {
        private PlayerState mState;
        public PlayerState State
        {
            get { return mState; }
            set { mState = value; }
        }
        private Dictionary<PlayerState, Animation> mAnimations;
        public Dictionary<PlayerState, Animation> Animations
        {
            get { return mAnimations; }
            set { mAnimations = value; }
        }

        private const int mFloor = 400;
        private const int MAX_BULLETS = 10;

        private GameObject[] mBullets;

        public Player()
        {
            this.Scale = 2.0f;
            this.ObjectDirection = Direction.Right;

            this.Animations = new Dictionary<PlayerState, Animation>();
            Animation temp = new Animation();
            temp.FrameCount = 1;
            temp.Frames = new Rectangle[1];
            temp.Frames[0] = new Rectangle(0, 0, 34, 30);
            this.Animations.Add(PlayerState.Idle, temp);

            temp = new Animation();
            temp.FrameCount = 3;
            temp.Frames = new Rectangle[3];
            temp.Frames[0] = new Rectangle(0, 31, 34, 30);
            temp.Frames[1] = new Rectangle(34, 31, 34, 30);
            temp.Frames[2] = new Rectangle(68, 31, 34, 30);
            this.Animations.Add(PlayerState.Run, temp);

            temp = new Animation();
            temp.FrameCount = 1;
            temp.Frames = new Rectangle[1];
            temp.Frames[0] = new Rectangle(34, 61, 33, 30);
            this.Animations.Add(PlayerState.Jump, temp);

            temp = new Animation();
            temp.FrameCount = 1;
            temp.Frames = new Rectangle[1];
            temp.Frames[0] = new Rectangle(68, 0, 34, 30);
            this.Animations.Add(PlayerState.Attack, temp);

            temp = new Animation();
            temp.FrameCount = 1;
            temp.Frames = new Rectangle[1];
            temp.Frames[0] = new Rectangle(0, 61, 33, 30);
            this.Animations.Add(PlayerState.JumpAttack, temp);

            mBullets = new GameObject[MAX_BULLETS];
            for (int i = 0; i < MAX_BULLETS; i++)
            {
                mBullets[i] = new GameObject();
            }
        }

        public void LoadContent(ContentManager aContent)
        {
            this.Sprite = aContent.Load<Texture2D>("Sprites/MegaMan");
            foreach (GameObject bullet in mBullets)
            {
                bullet.Sprite = aContent.Load<Texture2D>("Sprites/bullet");
            }
        }

        public void Update(GameTime gameTime)
        {
            InputManager input = InputManager.Instance();
            Vector2 pos = this.Position;
            Vector2 vel = this.Speed;

            if (!input.IsLeftPressed() && !input.IsRightPressed() && mState != PlayerState.Jump)
            {
                mState = (pos.Y + Sprite.Height < mFloor) ? mState = PlayerState.Jump : mState = PlayerState.Idle;
            }

            if (input.IsRightPressed() || input.IsLeftPressed())
            {
                if (mState != PlayerState.Jump && mState != PlayerState.JumpAttack)
                    mState = PlayerState.Run;

                if (input.IsRightPressed())
                {
                    ObjectDirection = Direction.Right;
                    pos.X += 5;
                }
                else if (input.IsLeftPressed())
                {
                    ObjectDirection = Direction.Left;
                    pos.X -= 5;
                }
            }

            if (input.IsJumpPressed() && mState != PlayerState.Jump)
            {
                mState = PlayerState.Jump;
                vel.Y = 10f;
            }

            if (input.IsShootDown())
            {
                if (input.IsShootPressed())
                    shoot();

                if ((pos.Y + Sprite.Height) < mFloor)
                    mState = PlayerState.JumpAttack;
                else
                    mState = PlayerState.Attack;
            }

            pos.Y -= vel.Y;
            if (pos.Y + Sprite.Height < mFloor)
                vel.Y -= 0.5f;

            if (pos.Y + Sprite.Height > mFloor)
            {
                vel.Y = 0;
                pos.Y = mFloor - Sprite.Height;
                mState = PlayerState.Idle;
            }
            this.Position = pos;
            this.Speed = vel;

            mAnimations[mState].Update(gameTime);
            updateBullets();
        }

        private void shoot()
        {
            foreach (GameObject bullet in mBullets)
            {
                if (!bullet.Visible)
                {
                    bullet.Visible = true;
                    Vector2 startingPos = this.Position;
                    startingPos.X += (this.ObjectDirection == Direction.Right) ? 25.0f : 0f;
                    startingPos.Y += this.Sprite.Height / 4;
                    bullet.Position = startingPos;
                    bullet.Speed = new Vector2(0f, 0f);
                    bullet.Speed = (this.ObjectDirection == Direction.Right) ? new Vector2(10.0f, 0f) : new Vector2(-10.0f, 0f);
                    bullet.Scale = 2.0f;
                    return;
                }
            }
        }

        private void updateBullets()
        {
            foreach (GameObject bullet in mBullets)
            {
                if (bullet.Visible)
                {
                    bullet.Position += bullet.Speed;
                    if ((bullet.Position.X > (this.Position.X + 1000)) || (bullet.Position.X < this.Position.X - 1000))
                    {
                        bullet.Visible = false;
                    }
                }
            }
        }

        public Rectangle CurrentFrame
        {
            get { return mAnimations[mState].CurrentFrame; }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            SpriteEffects direction = (this.ObjectDirection == Direction.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            aSpriteBatch.Draw(this.Sprite, this.Position, this.CurrentFrame, Color.White, 0.0f, Vector2.Zero, this.Scale, direction, 0.2f);

            foreach (GameObject bullet in mBullets)
            {
                if (bullet.Visible)
                    aSpriteBatch.Draw(bullet.Sprite, bullet.Position, null, Color.White, 0.0f, Vector2.Zero, bullet.Scale, SpriteEffects.None, 0.1f);
            }
        }
    }
}
