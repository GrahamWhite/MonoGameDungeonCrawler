﻿using GameTestFoler.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NextLevelDungeonCrawler.Models;
using System;
using System.Collections.Generic;

namespace NextLevelDungeonCrawler
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Menu : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Texture2D playerWalking;
        public static Texture2D bulletTexture;
        public static Texture2D playerJumping;
        public static Texture2D playerJumpingStart;
        public static Texture2D playerIdle;
        public static Texture2D playerDead;
        public static Texture2D blobTexture;
        public static Texture2D heartTexture;
        public static Texture2D bigBlobTexture;
        public static Texture2D background;
        public static Texture2D pointerTexture;
        public static Texture2D playerAttackingTexture;

        public static Texture2D buttonTexture;
        public static Texture2D buttonHoverTexture;
        public static Texture2D buttonClickTexture;

        public static Texture2D blueButtonTexture;
        public static Texture2D blueButtonHoverTexture;
        public static Texture2D blueButtonClickTexture;

        public static float groundLevel = 600;

        public Button button;

        bool showStats;

        public static SpriteFont menuFont;
        List<Enemy> enemies;
        PlatformerSprite player;
        MousePointer pointer;

        bool enemyCanSpawn;
      
        public Menu()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.PreferredBackBufferWidth = 1280;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 720;   // set this value to the desired height of your window
            graphics.ApplyChanges();
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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("Img/background");
            menuFont = Content.Load<SpriteFont>("Font/menuFont");
            heartTexture = Content.Load<Texture2D>("Img/heart");
            bulletTexture = Content.Load<Texture2D>("Img/bullet");

            playerWalking = Content.Load<Texture2D>("Img/hero");
            playerJumping = Content.Load<Texture2D>("Img/heroJumping");
            playerJumpingStart = Content.Load<Texture2D>("Img/heroJumpingStart");
            playerAttackingTexture = Content.Load<Texture2D>("Img/heroAttacking");
            playerIdle = Content.Load<Texture2D>("Img/heroIdle");
            playerDead = Content.Load<Texture2D>("Img/heroDeath");

            buttonTexture = Content.Load<Texture2D>("Img/button");
            buttonHoverTexture = Content.Load<Texture2D>("Img/buttonHighlight");
            buttonClickTexture = Content.Load<Texture2D>("Img/buttonClick");

            blueButtonTexture = Content.Load<Texture2D>("Img/blueButton");
            blueButtonHoverTexture = Content.Load<Texture2D>("Img/blueButtonHighlight");
            blueButtonClickTexture = Content.Load<Texture2D>("Img/blueButtonClick");

            pointerTexture = Content.Load<Texture2D>("Img/pointer");
          
            blobTexture = Content.Load<Texture2D>("Img/spr_blob");
            bigBlobTexture = Content.Load<Texture2D>("Img/spr_blob_big");

            //buttonTexture = Content.Load<Texture2D>("Img/button");
            //buttonHoverTexture = Content.Load<Texture2D>("Img/buttonHover");

            player = new PlatformerSprite(new Animation(playerWalking, 7));
            player.position = new Vector2(200, 200);

            button = new Button();
            button.position = new Vector2(50, 50);

       


            enemies = new List<Enemy>();
            enemyCanSpawn = true;

            pointer = new MousePointer(new Animation(pointerTexture, 1));

           
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

            if (Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                showStats = true;
            }
            else
            {
                showStats = false;
            }


            foreach (var enemy in enemies)
            {
                if (enemy.position.X < player.position.X && enemy.position.X + enemy.GetAnimation().frameWidth > player.position.X)
                {
                    player.RemoveHeart(gameTime);
                }

               
            }

            foreach (var enemy in enemies)
            {
                if (enemy.isDead)
                {
                    enemies.Remove(enemy);
                    break;
                }

            }


            //Spawn Enemy
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && enemyCanSpawn)
            {
                //Random random = new Random((int)gameTime.TotalGameTime.Ticks);


                //Enemy enemy = new Enemy(new Animation(blobTexture, 5));
                //enemy.position = pointer.position;
                //enemies.Add(enemy);
                //enemyCanSpawn = false;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                //enemyCanSpawn = true;
            }
            // TODO: Add your update logic here          
           
            
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime, player);

                
            }

            player.Update(gameTime);
            pointer.Update(gameTime);
            button.Update(gameTime, pointer, player);
           
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);
            button.Draw(spriteBatch);
         

          
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            pointer.Draw(this.spriteBatch);

            if (showStats)
            {
                spriteBatch.DrawString(menuFont, $"Mouse Position: {pointer.position}", new Vector2(20, 80), Color.White);
                spriteBatch.DrawString(menuFont, $"Player Position: {player.position}", new Vector2(20, 100), Color.White);
                spriteBatch.DrawString(menuFont, $"Heart Button Position: {button.position}", new Vector2(20, 120), Color.White);
            }
           


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
