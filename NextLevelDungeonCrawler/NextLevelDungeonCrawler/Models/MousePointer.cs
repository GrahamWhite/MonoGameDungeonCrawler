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
    public class MousePointer
    {

        Animation animation;
        AnimationController animationController;

        public Vector2 position;
        public Vector2 origin;

        public MousePointer(Animation animation)
        {
            this.animation = animation;
            animationController = new AnimationController(animation);
            
        }

        public void Update(GameTime gameTime)
        {
            position = Mouse.GetState().Position.ToVector2();
            animationController.position = this.position;

            //origin = new Vector2(position.X + animationController.animation.frameWidth, position.Y + animationController.animation.frameHeight);
            
         
        }

        public void Draw(SpriteBatch spritebatch)
        {
            this.animationController.Draw(spritebatch);
        }


    }
}
