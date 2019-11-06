using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTestFoler.Models
{
    /// <summary>
    /// A model for animating a spritesheet
    /// </summary>
    public class Animation
    {
        public int currentFrame { get; set; }
        public int frameCount { get; private set; }
        public Texture2D texture { get; set; }
        public int frameHeight { get { return texture.Height; } }
        public int frameWidth { get { return texture.Width / frameCount; } }
        public float frameSpeed { get; set; }
        public bool isLooping { get; set; }
        public bool horizontalFlip { get; set; }


        public Animation(Texture2D texture, int frameCount)
        {
            this.texture = texture;
            this.frameCount = frameCount;
            isLooping = true;
            horizontalFlip = false;

            frameSpeed = 0.05f;
        }


    }
}
