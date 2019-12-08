using Master_Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Master_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;

        public SpriteBatch Sprite;
        //SpriteFont font;

        //Texture2D defaultTexture;

        //Texture2D reaperIdleTexture;
        //Texture2D reaperWalkingTexture;
        //Texture2D reaperThrowingTexture;
        //Texture2D reaperWalkingAndThrowingTexture;
        //Texture2D jumpingTexture;
        public SpriteComponent sprite;
        //Reaper reaper;



        //bool showstats;

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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Sprite = new SpriteBatch(GraphicsDevice);
            //defaultTexture = Content.Load<Texture2D>("button");
            //reaperIdleTexture = Content.Load<Texture2D>("heroIdle");
            //reaperWalkingTexture = Content.Load<Texture2D>("heroWalking");
            //reaperThrowingTexture = Content.Load<Texture2D>("heroAttacking");
            //reaperWalkingAndThrowingTexture = Content.Load<Texture2D>("heroAttackingWalking");
            //jumpingTexture = Content.Load<Texture2D>("heroJumping");

            //font = Content.Load<SpriteFont>("defaultFont");

            //reaper = new Reaper(reaperIdleTexture);
            //reaper.LoadAnimations(reaperWalkingTexture, reaperIdleTexture, reaperThrowingTexture, reaperWalkingAndThrowingTexture, jumpingTexture);
            //// TODO: use this.Content to load your game content here


            sprite = new SpriteComponent(this, 1, 18, "heroIdle");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            //{
            //    showstats = true;
            //}
            //else showstats = false;
            //TODO: Add your update logic here
            //reaper.Update(gameTime);
            sprite.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Sprite.Begin();
            //reaper.Draw(spriteBatch);
            sprite.Draw(gameTime);
            //if (showstats)
            //{
            //    spriteBatch.DrawString(font, reaper.ToString(), new Vector2(200, 200), Color.Black);
            //}

            // TODO: Add your drawing code here
             //Sprite.End();
            base.Draw(gameTime);
        }
    }
}
