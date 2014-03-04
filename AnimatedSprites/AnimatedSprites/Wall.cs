using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;

namespace AnimatedSprites
{
    class Wall : Sprite
    {
        public Wall(SpriteSettings setting)
            : base(setting)
        {
        }
        public override Direction MoveDirection
        {
            get { return Direction.Up; }
        }

        public override void GetCurrentFrame(ref Microsoft.Xna.Framework.Point currentFrame)
        {
            currentFrame.X = 256;
            currentFrame.Y = 0;
        }
    }
}
