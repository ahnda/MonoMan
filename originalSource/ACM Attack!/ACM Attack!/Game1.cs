using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Rectangle screen;
        SpriteBatch spriteBatch;
        SpriteFont test;
        Song bgm;
        SoundEffect buster, explode;
        Player megaman;
        Texture2D background;
        GamePadState currInput = GamePad.GetState(PlayerIndex.One), prevInput;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            megaman = new Player();
            megaman.position = new Vector2(100, 425);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            test = Content.Load<SpriteFont>("test");
            bgm = Content.Load<Song>("STS9 Collab");

            megaman.sprite = Content.Load<Texture2D>("Sprites/MegaMan");

            background = Content.Load<Texture2D>("Sprites/stage");

            buster = Content.Load<SoundEffect>("buster");
            explode = Content.Load<SoundEffect>("explode");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            prevInput = currInput;
            currInput = GamePad.GetState(PlayerIndex.One);

            // Allows the game to exit
            if (currInput.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (MediaPlayer.State != MediaState.Playing && currInput.Buttons.Y == ButtonState.Pressed)
                MediaPlayer.Play(bgm);

            if (currInput.Buttons.B == ButtonState.Pressed && prevInput.Buttons.B != ButtonState.Pressed)
                buster.Play();

            megaman.Update(gameTime);
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(-25, 0), null, Color.White, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 1.0f);

            if(megaman.direction == Direction.Right)
                spriteBatch.Draw(megaman.sprite, megaman.position, megaman.CurrentFrame, Color.White, 0.0f, Vector2.Zero, megaman.scale, SpriteEffects.None, 0.0f);
            else
                spriteBatch.Draw(megaman.sprite, megaman.position, megaman.CurrentFrame, Color.White, 0.0f, Vector2.Zero, megaman.scale, SpriteEffects.FlipHorizontally, 0.0f);
            spriteBatch.DrawString(test, megaman.state.ToString(), new Vector2(100, 100), Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
