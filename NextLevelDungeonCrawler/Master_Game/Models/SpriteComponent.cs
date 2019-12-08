using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_Game.Models
{
    public class SpriteComponent : DrawableGameComponent
    {
        private Game1 parent;
        private Texture2D tex;
        private int width;
        private int height;
        private int currentX;
        private int currentY;
        private Vector2 position;
        public bool canLoop;

        SpriteEffects effects;
        float speed;
        float rotation;
        float scale;
        bool isMoving;
        bool applyGravity;
        int groundLevel;
      
        float gravity;

        public SpriteComponent(Game game, int rows, int cols, string image) : base(game)
        {
            parent = (Game1)game;
          
            tex = parent.Content.Load<Texture2D>(image);

            this.position = Vector2.Zero;

            this.width = tex.Width / cols;
            this.height = tex.Height / rows;

            currentX = 0;
            currentY = 0;

            canLoop = true;
            speed = 6;
            isMoving = false;
            effects = SpriteEffects.None;
            rotation = 0;
            scale = 1;
            gravity = 3;
            applyGravity = true;
            groundLevel = 300;
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle sourceRectangle = new Rectangle(currentX, currentY, width, height);
            
            parent.Sprite.Begin();
           
            parent.Sprite.Draw(tex,
                new Rectangle((int)position.X, (int)position.Y, width, height),
                sourceRectangle,
                Color.White,
                rotation,
                new Vector2(scale, scale),
                effects,
                0
                );
            parent.Sprite.End();
            base.Draw(gameTime);
        }

     
        public override void Update(GameTime gameTime)
        {
            currentX += width;
            if (currentX >= tex.Width)
            {
                currentX = 0;
                currentY += height;

                if (canLoop)
                {
                    StartAnimation(this.position);
                }
          
                
            }


            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.position.X -= (float)speed;
                effects = SpriteEffects.FlipHorizontally;
                isMoving = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.position.X += (float)speed;
               
                effects = SpriteEffects.None;
                isMoving = true;
            }

            if (applyGravity && position.Y < groundLevel)
            {
                this.position.Y += (float)gravity;
            }

            base.Update(gameTime);
        }

       
        public void StartAnimation(Vector2 position)
        {
            this.position = position;
            currentX = 0;
            currentY = 0;
            this.Enabled = true;
            this.Visible = true;
        }
    }
}
