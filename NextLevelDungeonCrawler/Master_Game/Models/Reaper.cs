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
    public class Reaper : Sprite
    {
        public Texture2D defaultTexture;
        public Animation walkingAnimation;
        public Animation idleAnimation;
        public Animation attackingAnimation;
        public Animation walkingAttackingAnimation;
        public Animation jumpLoopAnimation;
      
        
        public Reaper(Texture2D defaultTexture) : base(defaultTexture)
        {
            this.animation = new Animation(defaultTexture, 18);
            this.handleMovement = true;
          
        }

        

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
        }

        public void LoadAnimations(Texture2D walkingTexture, Texture2D idleTexture, Texture2D attackingTexture, Texture2D walkingAttackingTexture, Texture2D jumpTexture)
        {
            this.walkingAnimation = new Animation(walkingTexture, 24);
            this.idleAnimation = new Animation(idleTexture, 18);
            this.attackingAnimation = new Animation(attackingTexture, 12);
            this.walkingAttackingAnimation = new Animation(walkingAttackingTexture, 12);
            this.walkingAttackingAnimation = new Animation(walkingAttackingTexture, 12);
            this.jumpLoopAnimation = new Animation(jumpTexture, 8);
        }

        public override void Update(GameTime gameTime)
        {
            

            if (isMoving && !isAttacking)
            {
                this.Play(walkingAnimation);

            }

            if (isAttacking && !isMoving)
            {
                this.Play(attackingAnimation);

            }

            if (isMoving && isAttacking)
            {
                this.Play(walkingAttackingAnimation);
            }

            if (!isMoving && !isAttacking)
            {
                this.Play(idleAnimation);
            }

            if (isJumping)
            {
                this.Play(jumpLoopAnimation);
            }





            base.Update(gameTime);
        }
    }
}
