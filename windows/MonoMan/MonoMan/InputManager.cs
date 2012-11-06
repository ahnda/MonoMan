using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;

namespace MonoMan
{
    class InputManager
    {
        #region Member Variables
        private GamePadState mCurrentGamepadState, mPreviousGamepadState;
        private KeyboardState mCurrentKeyboardState, mPreviousKeyboardState;
        private Buttons mJumpButton, mShootButton;
        private Keys mJumpKey, mShootKey, mUpKey, mDownKey, mLeftKey, mRightKey;
        #endregion

        #region Singleton Functionality
        private static InputManager instance;
        private InputManager() 
        {
            mCurrentGamepadState  = mPreviousGamepadState  = GamePad.GetState(PlayerIndex.One);
            mCurrentKeyboardState = mPreviousKeyboardState = Keyboard.GetState();
            
            mJumpButton = Buttons.A;
            mShootButton = Buttons.B;

            mJumpKey = Keys.Space;
            mShootKey = Keys.Enter;
            mUpKey = Keys.W;
            mLeftKey = Keys.A;
            mDownKey = Keys.S;
            mRightKey = Keys.D;
        }

        public static InputManager Instance()
        {
            if (instance == null)
                instance = new InputManager();
            return instance;
        }
        #endregion

        #region Button Interface
        public bool IsJumpPressed()
        {
            return (mCurrentGamepadState.IsButtonDown(mJumpButton) && !mPreviousGamepadState.IsButtonDown(mJumpButton))
                || (mCurrentKeyboardState.IsKeyDown(mJumpKey) && !mPreviousKeyboardState.IsKeyDown(mJumpKey));
            
        }
        public bool IsShootPressed()
        {
            return (mCurrentGamepadState.IsButtonDown(mShootButton) && !mPreviousGamepadState.IsButtonDown(mShootButton))
                || (mCurrentKeyboardState.IsKeyDown(mShootKey) && !mPreviousKeyboardState.IsKeyDown(mShootKey));

        }
        public bool IsShootDown()
        {
            return (mCurrentGamepadState.IsButtonDown(mShootButton) || mCurrentKeyboardState.IsKeyDown(mShootKey));
        }
        public bool IsUpPressed()
        {
            return (mCurrentGamepadState.IsButtonDown(Buttons.DPadUp))
                || (mCurrentGamepadState.ThumbSticks.Left.Y > 0)
                || (mCurrentKeyboardState.IsKeyDown(mUpKey));
        }
        public bool IsLeftPressed()
        {
            return (mCurrentGamepadState.IsButtonDown(Buttons.DPadLeft))
                || (mCurrentGamepadState.ThumbSticks.Left.X < 0)
                || (mCurrentKeyboardState.IsKeyDown(mLeftKey));
        }
        public bool IsDownPressed()
        {
            return (mCurrentGamepadState.IsButtonDown(Buttons.DPadDown))
                || (mCurrentGamepadState.ThumbSticks.Left.Y < 0)
                || (mCurrentKeyboardState.IsKeyDown(mDownKey));
        }
        public bool IsRightPressed()
        {
            return (mCurrentGamepadState.IsButtonDown(Buttons.DPadRight))
                || (mCurrentGamepadState.ThumbSticks.Left.X > 0)
                || (mCurrentKeyboardState.IsKeyDown(mRightKey));
        }

        public void Update(GameTime aGameTime)
        {
            mPreviousGamepadState = mCurrentGamepadState;
            mPreviousKeyboardState = mCurrentKeyboardState;

            mCurrentGamepadState = GamePad.GetState(PlayerIndex.One);
            mCurrentKeyboardState = Keyboard.GetState();
        }
        #endregion
    }
}
