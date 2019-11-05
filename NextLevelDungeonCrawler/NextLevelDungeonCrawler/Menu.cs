using GameTestFoler.Models;
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

        
        public static float groundLevel = 600;

        SpriteFont menuFont;
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
           
            pointerTexture = Content.Load<Texture2D>("Img/pointer");
          
            blobTexture = Content.Load<Texture2D>("Img/spr_blob");
            bigBlobTexture = Content.Load<Texture2D>("Img/spr_blob_big");

            //buttonTexture = Content.Load<Texture2D>("Img/button");
            //buttonHoverTexture = Content.Load<Texture2D>("Img/buttonHover");

            player = new PlatformerSprite(new Animation(playerWalking, 7));
            player.position = new Vector2(200, 200);
            


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
                Random random = new Random((int)gameTime.TotalGameTime.Ticks);


                Enemy enemy = new Enemy(new Animation(blobTexture, 5));
                enemy.position = pointer.position;
                enemies.Add(enemy);
                enemyCanSpawn = false;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                enemyCanSpawn = true;
            }
            // TODO: Add your update logic here          
           
            
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime, player);

                
            }

            player.Update(gameTime);
            pointer.Update(gameTime);
            
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

            
            pointer.Draw(this.spriteBatch);
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
           
            // TODO: Add your drawing code here

            spriteBatch.DrawString(menuFont, "Game Editor", new Vector2(20, 20), Color.White);
            spriteBatch.DrawString(menuFont, "Press WSAD to control the player", new Vector2(20, 40), Color.White);
            spriteBatch.DrawString(menuFont, "Press SPACE to fire", new Vector2(20, 60), Color.White);
            spriteBatch.DrawString(menuFont, "Press LEFT_MOUSE_BUTTON to spawn an enemy", new Vector2(20, 80), Color.White);
            spriteBatch.DrawString(menuFont, "Press H to add a heart", new Vector2(20, 100), Color.White);
            spriteBatch.DrawString(menuFont, "Press K to kill the player", new Vector2(20, 120), Color.White);
            spriteBatch.DrawString(menuFont, "* Dev Note: Jumpstart & Jump are still beta", new Vector2(20, 160), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
