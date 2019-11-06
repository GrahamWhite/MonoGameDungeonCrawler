using GameTestFoler.Controllers;
using GameTestFoler.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextLevelDungeonCrawler.Models
{
    public class Button
    {
        AnimationController animationController;

        Animation regularAnimation;
        Animation hoverAnimation;
        Animation clickAnimation;

        public Vector2 position;
        public Vector2 origin;

        public string text;
        bool canAddHeart;
        public bool isHovering;

        public Button()
        {
            text = "";
            position = new Vector2(0, 0);

            regularAnimation = new Animation(Menu.buttonTexture, 1);
            hoverAnimation = new Animation(Menu.buttonHoverTexture, 1);
            clickAnimation = new Animation(Menu.buttonClickTexture, 1);
            animationController = new AnimationController(regularAnimation);
            animationController.Play(regularAnimation);
            isHovering = false;
        }

        public void Update(GameTime gameTime, MousePointer pointer, PlatformerSprite sprite)
        {
            animationController.Update(gameTime);
            animationController.position = this.position;
            origin = new Vector2(animationController.animation.frameWidth  * 1.25f, animationController.position.Y + 35);


            this.animationController.animation = regularAnimation;

            if (pointer.position.X > this.position.X && 
                pointer.position.X < this.position.X + this.regularAnimation.frameWidth &&
                pointer.position.Y > this.position.Y &&
                pointer.position.Y < this.position.Y + this.regularAnimation.frameHeight)
            {
                this.animationController.animation = hoverAnimation;
                isHovering = true;
            }
           

            if (isHovering && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                this.animationController.animation = clickAnimation;
                if (canAddHeart)
                {
                    this.animationController.animation = clickAnimation;
                    Animation heartAnimation = new Animation(Menu.heartTexture, 1);
                    AnimationController heart = new AnimationController(heartAnimation);
                    heart.position = new Vector2(300 + (30 * sprite.hearts.Count), 50);
                    sprite.hearts.Add(heart);

                    canAddHeart = false;
                }
               
            }

           

            if (Mouse.GetState().LeftButton == ButtonState.Released && isHovering)
            {
                canAddHeart = true;
            }

             //Health Manipulation
                //if (Keyboard.GetState().IsKeyDown(Keys.H) && canAddHeart)
                //{
                //    Animation heartAnimation = new Animation(Menu.heartTexture, 1);
                //    AnimationController heart = new AnimationController(heartAnimation);
                //    heart.position = new Vector2(300 + (30 * hearts.Count), 50);

                //    hearts.Add(heart);

                //    canAddHeart = false;
                //}
                //if (Keyboard.GetState().IsKeyUp(Keys.H))
                //{
                //    canAddHeart = true;
                //}


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animationController.Draw(spriteBatch);
            spriteBatch.DrawString(Menu.menuFont, "Add a Heart", this.origin, Color.Black);
        }
    }
}
