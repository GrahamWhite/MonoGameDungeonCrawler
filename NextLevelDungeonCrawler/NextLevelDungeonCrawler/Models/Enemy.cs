using GameTestFoler.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NextLevelDungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTestFoler.Models
{
    class Enemy
    {
        Animation animation;
        Animation heroWalkingAnimation;
        Animation heroJumpingAnimation;

        AnimationController animationController;
        
        public Vector2 position;

        Animation bulletAnimation;


        public float spriteSpeed;
        float gravity;
        public float jumpVelocity;
        double jumpTimer;
        bool isJumping = false;
        public Direction direction;
        public double jumpDuration;
        public bool isDead;

        public double timeAlive;
        

        public Enemy(Animation animation)
        {
            this.animation = animation;
            animationController = new AnimationController(this.animation);
            heroWalkingAnimation = new Animation(Menu.blobTexture, 6);
            heroJumpingAnimation = new Animation(Menu.blobTexture, 6);
            animationController.Play(animation);
            
            spriteSpeed = 4f;
            direction = Direction.RIGHT;
            jumpVelocity = 21f;
            jumpTimer = 0;
            jumpDuration = 145;
            gravity = 6f;
            animationController.scale = 2.2f;

            timeAlive = 0;
         
            isDead = false;

        }

        public void SetBulletAnimation(Texture2D texture)
        {
            bulletAnimation = new Animation(texture, 1);            
        }
        public Animation GetAnimation() => animationController.animation;
        public void SetAnimation(Animation animation) { this.animation = animation; }
        
        public void Update(GameTime gameTime, PlatformerSprite sprite)
        {

            timeAlive += gameTime.ElapsedGameTime.TotalSeconds;

           
           

            foreach (var bullet in sprite.bullets)
            {
                if (bullet.position.X > this.position.X && 
                    bullet.position.X < this.position.X + this.animation.frameWidth && 
                    bullet.position.Y > this.position.Y && 
                    bullet.position.Y < this.position.Y + this.animation.frameHeight)
                {
                    
                        isDead = true;
                    
                   
                }
            }

           

            
            if (this.position.X < sprite.position.X)
            {
                position.X += spriteSpeed;
            }

            if (this.position.X > sprite.position.X)
            {
                position.X -= spriteSpeed;
            }


            if (this.position.Y >= Menu.groundLevel)
            {
                if (this.position.X < sprite.position.X)
                {
                    this.position.X += spriteSpeed;
                    this.animationController.Flip(false);
                }

                if (this.position.X > sprite.position.X)
                {
                    this.position.X -= spriteSpeed;
                    this.animationController.Flip(true);
                }
            }
            

            if (position.Y < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Menu.groundLevel + animation.frameHeight) 
            {
                position.Y += gravity;
                animationController.animation = heroJumpingAnimation;
            }



            if (isJumping && jumpTimer < jumpDuration)
            {
                position.Y -= jumpVelocity;
                animationController.animation = heroJumpingAnimation;

            }
            else if (timeAlive > 5)
            {
                animationController.animation = new Animation(Menu.bigBlobTexture, 6);
            }
            else
            {
                animationController.animation = heroWalkingAnimation;
                isJumping = false;
                jumpTimer = 0;
            }

            if (position.Y < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Menu.groundLevel)
            {
                position.Y += gravity;
                animationController.animation = heroJumpingAnimation;
            }

          

            animationController.position = position;
            animationController.Update(gameTime);
            jumpTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            

        }

        public float GetScale() => animationController.scale;

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isDead)
            {
                animationController.Draw(spriteBatch);
            }
        
        }
    }


}
