using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace ACM_Attack_
{
    enum PlayerState { Idle, Run, Jump, Injured, Attack, JumpAttack };
    enum Direction { Left, Right };

    class Animation
    {
        public int frameCount;
        protected int currentFrame = 0;
        public Rectangle[] frames;
        public float frameLength = 1f / 6f;
        public float timer = 0.0f;

        public Animation()
        {}

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= frameLength)
            {
                timer = 0f;
                currentFrame = (currentFrame + 1) % frameCount;
            }
        }

        public Rectangle CurrentFrame
        {
            get { return frames[currentFrame]; }
        }
    }

    class Player : GameObject
    {
        public PlayerState state = PlayerState.Idle;
        public GamePadState currInput = GamePad.GetState(PlayerIndex.One), prevInput;
        public Dictionary<PlayerState, Animation> animations;
        public Direction direction = Direction.Right;

        public Player()
        {
            this.scale = 3.0f;

            this.animations = new Dictionary<PlayerState, Animation>();
            Animation temp = new Animation();
            temp.frameCount = 1;
            temp.frames = new Rectangle[1];
            temp.frames[0] = new Rectangle(0, 0, 34, 30);
            animations.Add(PlayerState.Idle, temp);

            temp = new Animation();
            temp.frameCount = 3;
            temp.frames = new Rectangle[3];
            temp.frames[0] = new Rectangle(0, 31, 34, 30);
            temp.frames[1] = new Rectangle(34, 31, 34, 30);
            temp.frames[2] = new Rectangle(68, 31, 34, 30);
            animations.Add(PlayerState.Run, temp);

            temp = new Animation();
            temp.frameCount = 1;
            temp.frames = new Rectangle[1];
            temp.frames[0] = new Rectangle(34, 60, 33, 30);
            animations.Add(PlayerState.Jump, temp);
        }

        public void Update(GameTime gameTime)
        {
            prevInput = currInput;
            currInput = GamePad.GetState(PlayerIndex.One);

            if (currInput.ThumbSticks.Left.X == 0 && state != PlayerState.Jump)
                state = PlayerState.Idle;

            if (currInput.ThumbSticks.Left.X != 0)
            {
                if (state != PlayerState.Jump)
                    state = PlayerState.Run;
                if (currInput.ThumbSticks.Left.X > 0)
                {
                    direction = Direction.Right;
                    position.X += 5;
                }
                else if (currInput.ThumbSticks.Left.X < 0)
                {
                    direction = Direction.Left;
                    position.X -= 5;
                }
            }

            
            if (currInput.Buttons.A == ButtonState.Pressed && prevInput.Buttons.A != ButtonState.Pressed && state != PlayerState.Jump)
            {
                state = PlayerState.Jump;
                ySpeed = 10f;
            }

            position.Y -= ySpeed;
            if(position.Y < 425)
                ySpeed -= 0.5f;
            
            if (position.Y > 425)
            {
                ySpeed = 0;
                position.Y = 425;
                state = PlayerState.Idle;
            }

            animations[state].Update(gameTime);
        }

        public Rectangle CurrentFrame
        {
            get { return animations[state].CurrentFrame; }
        }
    }
}
