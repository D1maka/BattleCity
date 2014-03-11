using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;

namespace AnimatedSprites
{
    class IndestructibleWall : StaticSprite
    {
        public IndestructibleWall(SpriteSettings setting)
            : base(setting)
        {
        }

        public override void GetCurrentFrame(ref Microsoft.Xna.Framework.Point currentFrame)
        {
            currentFrame.X = 256;
            currentFrame.Y = 16;
        }

        public override void Destroy()
        {
        }
    }
}
