using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AnimatedSprites.GameSettings;

namespace AnimatedSprites
{
    class Bush : StaticSprite
    {
        public Bush(SpriteSettings setting)
            : base(setting)
        {
        }

        public override void GetCurrentFrame(ref Microsoft.Xna.Framework.Point currentFrame)
        {
            currentFrame.X = 272;
            currentFrame.Y = 32;
        }

        public override Rectangle collisionRect
        {
            get
            {
                return new Rectangle(0, 0, 0, 0);
            }
        }
    }
}
