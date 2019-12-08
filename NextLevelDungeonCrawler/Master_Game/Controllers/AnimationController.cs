using Master_Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Master_Game.Controllers
{
    public abstract class AnimationController
    {
        public Animation animation;
        public Vector2 position;
        public float scale;
        public float rotation;
        private float timer;

        SpriteEffects flip;

        //public virtuaAnimationController(Animation animation)
        //{
        //    this.animation = animation;
        //    scale = 1;
        //    flip = SpriteEffects.None;
        //    rotation = 0;
        //}

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(animation.texture,
                new Rectangle((int)position.X, (int)position.Y, animation.frameWidth * (int)scale, animation.frameHeight * (int)scale),
                new Rectangle(animation.currentFrame * animation.frameWidth, 0, animation.frameWidth, animation.frameHeight),
                Color.White,
                rotation,
                new Vector2(scale, scale),
                flip,
                0
                );
        }

        public virtual void Play(Animation animation)
        {
            if (this.animation == animation)
                return;

            this.animation = animation;

            this.animation.currentFrame = 0;

            this.timer = 0;

        }

        public virtual void Stop()
        {
            timer = 0f;
            animation.currentFrame = 0;

        }

        public virtual void Rotate(float rot)
        {
            rotation = rot;
        }

        public virtual void Flip(bool flip)
        {
            if (flip)
                this.flip = SpriteEffects.FlipHorizontally;
            else
                this.flip = SpriteEffects.None;
        }

        public virtual void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= animation.frameSpeed)
            {
                timer = 0f;
                animation.currentFrame++;
            }

            if (animation.currentFrame >= animation.frameCount && animation.isLooping)
            {
                animation.currentFrame = 0;
            }

            if (!animation.isLooping && animation.currentFrame == animation.frameCount)
            {
                animation.currentFrame = animation.frameCount - 1;

            }






        }
    }
}
