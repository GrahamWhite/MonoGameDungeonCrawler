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
    class PlatformerSprite
    {
        Animation animation;
        Animation heroWalkingAnimation;       
        Animation heroIdleAnimation;
        Animation heroDeathAnimation;
        Animation heroJumpingAnimation;
        Animation heroJumpingStartAnimation;
        Animation heroAttackingAnimation;
       

        AnimationController animationController;
        
        public Vector2 position;

        Animation bulletAnimation;

        public List<Bullet> bullets;
        float bulletTimer;
        float bulletDelay;

        public float spriteSpeed;
        float gravity;
        public float jumpVelocity;
        double jumpTimer;
        bool isJumping = false;
        public Direction direction;
        public double jumpDuration;

        int health = 6;
        List<AnimationController> hearts;

        public bool canAddHeart;

        public float hitDelay;
        public float hitTimer;

        public PlatformerSprite(Animation animation)
        {
            this.animation = animation;
            animationController = new AnimationController(this.animation);
            heroWalkingAnimation = new Animation(Menu.playerWalking, 24);           
            heroIdleAnimation = new Animation(Menu.playerIdle, 18);
            heroDeathAnimation = new Animation(Menu.playerDead, 12);
            heroJumpingAnimation = new Animation(Menu.playerJumping, 6);
            heroJumpingStartAnimation = new Animation(Menu.playerJumpingStart, 6);
            heroAttackingAnimation = new Animation(Menu.playerAttackingTexture, 12);

            animationController.Play(animation);
            
            spriteSpeed = 3f;
            
            jumpVelocity = 10f;
            jumpTimer = 0;
            jumpDuration = 1200;
            gravity = 3f;
            animationController.scale = 2.2f;
            bulletTimer = 0;
            bulletDelay = 600;
            hitDelay = 150;
            direction = Direction.RIGHT;
            bullets = new List<Bullet>();
            animationController.animation = heroIdleAnimation;
            hearts = new List<AnimationController>();

            //Add 1 Heart
            Animation heartAnimation = new Animation(Menu.heartTexture, 1);
            AnimationController heart = new AnimationController(heartAnimation);
            heart.position = new Vector2(300 + (30 * hearts.Count), 50);

            hearts.Add(heart);

            canAddHeart = true;
        }

        public void SetBulletAnimation(Texture2D texture) { bulletAnimation = new Animation(texture, 1);}
        public Animation GetAnimation() => animationController.animation;
        public void SetAnimation(Animation animation) { this.animation = animation; }

        public void Update(GameTime gameTime)
        {
           


            

            if (hearts.Count == 0)
            {
                this.animationController.animation = heroDeathAnimation;
                this.animation.frameSpeed = 300;
                this.animationController.animation.isLooping = false;
            }
            else
            {

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    position = new Vector2(position.X - spriteSpeed, position.Y);
                    animationController.animation = heroWalkingAnimation;
                    animation.horizontalFlip = true;
                    animationController.Flip(true);
                    direction = Direction.LEFT;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    position = new Vector2(position.X + spriteSpeed, position.Y);
                    animationController.animation = heroWalkingAnimation;
                    animation.horizontalFlip = false;
                    animationController.Flip(false);
                    direction = Direction.RIGHT;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.K))
                {
                    if (hearts.Count > 0)
                    {
                        hearts.RemoveAt(hearts.Count - 1);
                    }
                }

                if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A))
                {
                   animationController.animation = heroIdleAnimation;
                }

           

                if (Keyboard.GetState().IsKeyDown(Keys.H) && canAddHeart)
                {
                    Animation heartAnimation = new Animation(Menu.heartTexture, 1);
                    AnimationController heart = new AnimationController(heartAnimation);
                    heart.position = new Vector2(300 + (30 * hearts.Count), 50);

                    hearts.Add(heart);

                    canAddHeart = false;
                }
                if (Keyboard.GetState().IsKeyUp(Keys.H))
                {
                    canAddHeart = true;
                }

               


                    bulletTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;


                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {

                    animationController.animation = heroAttackingAnimation;

                    if (bulletTimer > bulletDelay)
                    {
                        Bullet bullet = new Bullet(new Animation(Menu.bulletTexture, 1), this.position);
                        bullet.animationController.position = new Vector2(this.position.X + animation.frameWidth, position.Y + animation.frameHeight );
                        bullet.direction = this.direction;                    
                        bullets.Add(bullet);

                        bulletTimer = 0;
                    }
                }

           

                if (Keyboard.GetState().IsKeyDown(Keys.W) &&  Menu.groundLevel  - this.animation.texture.Height <= this.position.Y)
                {
               
                    jumpTimer = 0;
                    animationController.animation = heroJumpingStartAnimation;
                    
                }

                if (jumpTimer < jumpDuration && Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (animationController.animation.currentFrame < animationController.animation.frameCount)
                    {
                        
                        animationController.animation = heroJumpingStartAnimation;

                        if (animationController.animation.currentFrame == animation.frameCount)
                        {
                            animationController.animation = heroJumpingAnimation;
                        }
                    }
                    
                   
                    jumpTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                    this.position.Y -= jumpVelocity;

                }




            }





            //Apply gravity
            if (position.Y < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Menu.groundLevel && isJumping == false) 
            {
                position.Y += gravity;
               
            }
           

            foreach (var bullet in bullets)
            {

                bullet.Update(gameTime);
            }

            foreach (var bullet in bullets)
            {
                if (bullet.position.X < 0 || bullet.position.X > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                {
                    bullets.Remove(bullet);
                    break;
                }
            }

            foreach (var heart in hearts)
            {
                heart.Update(gameTime);
            }

            animationController.position = position;
            animationController.Update(gameTime);
            jumpTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

        }

        public float GetScale() => animationController.scale;

        public void RemoveHeart(GameTime gameTime)
        {
            hitTimer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (hearts.Count > 0 && hitTimer > hitDelay)
            {
                this.hearts.RemoveAt(hearts.Count() - 1);
                
                hitTimer = 0;
            }
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            animationController.Draw(spriteBatch);

            foreach (var bullet in bullets)
            {

                bullet.Draw(spriteBatch);
            }

            foreach (var heart in hearts)
            {
                heart.Draw(spriteBatch);
            }

        }
    }


}
