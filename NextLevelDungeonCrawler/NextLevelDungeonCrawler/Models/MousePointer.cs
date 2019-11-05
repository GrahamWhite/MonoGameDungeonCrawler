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
    class MousePointer
    {

        Animation animation;
        AnimationController animationController;

        public Vector2 position;

        public MousePointer(Animation animation)
        {
            this.animation = animation;
            animationController = new AnimationController(animation);
            
        }

        public void Update(GameTime gameTime)
        {
            this.animationController.position = Mouse.GetState().Position.ToVector2();
            this.position = animationController.position;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            this.animationController.Draw(spritebatch);
        }


    }
}
