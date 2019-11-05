using GameTestFoler.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTestFoler.Models
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    class Bullet
    {
        public Animation animation;
        public AnimationController animationController;

        public float bulletVelocity;
        public float rateOfFire;
        public Vector2 position;
        public Direction direction;

        public Bullet(Animation animation, Vector2 origin)
        {
            this.animation = animation;
            animationController = new AnimationController(this.animation);
            animationController.Play(animation);
            position = origin;
            bulletVelocity = 12.0f;
            rateOfFire = 100f;
            direction = Direction.RIGHT;
            
           
        }

        public void Update(GameTime gameTime)
        {
            
                if (direction == Direction.RIGHT)
                {
                    position = new Vector2(position.X + bulletVelocity, animationController.position.Y);
                }

                if (direction == Direction.LEFT)
                {
                    position = new Vector2(position.X - bulletVelocity, animationController.position.Y);
                }

        }


        public void Draw(SpriteBatch spriteBatch, float scale = 1f)
        {
            var s = new Vector2(scale, scale);

            spriteBatch.Draw(animation.texture, 
                new Rectangle((int)position.X, (int)position.Y, animation.frameWidth * (int)scale, animation.frameHeight * (int)scale), 
                new Rectangle(0, 0, animation.frameWidth, animation.frameHeight), 
                Color.White, 
                0, 
                s, 
                SpriteEffects.None, 
                1);
        }
    }
}
