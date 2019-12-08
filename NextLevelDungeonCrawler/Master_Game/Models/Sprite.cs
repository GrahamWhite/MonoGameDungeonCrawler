using Master_Game.Controllers;
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

  
    public  abstract class Sprite : AnimationController
    {
        public double timer;
        public double speed;
        public double jumpHeight;
        public double jumpVelocity;
        public double jumpDelta;
        public double gravity;
        public int groundLevel;

        public bool applyGravity;

        public bool handleMovement;
        public bool isMoving;
        public bool isAttacking;
        public bool isJumping;
        public bool onGround;

        public DrawableGameComponent g;

        public Sprite(Texture2D defaultTexture)
        {
            this.animation = new Animation(defaultTexture, 1);
            this.position = new Microsoft.Xna.Framework.Vector2(100, 100);
            this.rotation = 0;
            this.scale = 1;
            this.timer = 0;
            this.speed = 3;

            this.jumpHeight = 500;
            this.jumpVelocity = 2;
            this.jumpDelta = 0;
            this.gravity = 3;
            this.groundLevel = 300;

            handleMovement = false;
            isMoving = false;
            isAttacking = false;
            isJumping = false;
            applyGravity = true;
        }

      

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (handleMovement)
            {
                isMoving = false; 

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    this.position.X -= (float)speed;
                    this.Flip(true);
                    isMoving = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    this.position.X += (float)speed;
                    this.Flip(false);
                    isMoving = true;
                }

                if (this.position.Y + animation.frameHeight >= groundLevel) onGround = true;
                else onGround = false;

                ////////////////////////////////
                //if (onGround && Keyboard.GetState().IsKeyDown(Keys.W))
                //{
                //    isJumping = true;
                //    jumpDelta = 0;
                //}
               

                //if (isJumping)
                //{
                //    jumpDelta += jumpVelocity;
                //    this.position.Y -= (float)jumpDelta;
                //}

                //if (jumpDelta > jumpHeight && isJumping)
                //{
                //    isJumping = false;
                //}
                
                






                if (applyGravity && position.Y < groundLevel)
                {
                    this.position.Y += (float)gravity;
                }

                //////////////////////////
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    isAttacking = true;
                }
                else
                {
                    isAttacking = false;
                }
            }




            

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
        }

        public override string ToString()
        {
            return $"Timer: {timer}\n" +
                $"Speed: {speed}\n" +
                $"Jump Height: {jumpHeight}\n" +
                $"Jump V: {jumpVelocity}\n" +
                $"Jump D: {jumpDelta}\n" +
                $"Position: {position}\n" +
                $"Is Jumping: {isJumping}\n"+
                $"On Ground: {onGround}\n";
        }



    }
}
