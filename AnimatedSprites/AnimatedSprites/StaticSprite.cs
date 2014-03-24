using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;

namespace AnimatedSprites
{
    interface ICollidable { };

    abstract class StaticSprite : Sprite
    {
        public StaticSprite(SpriteSettings setting)
            : base(setting)
        {
        }

        public override Direction MoveDirection
        {
            get { return Direction.Up; }
        }
    }
}
