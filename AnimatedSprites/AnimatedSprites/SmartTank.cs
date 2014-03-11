using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;
using AnimatedSprites.Utils;

namespace AnimatedSprites
{
    class SmartTank : AITank
    {
        public SmartTank(SpriteSettings settings, SpriteSettings missileSetting)
            : base(settings, missileSetting)
        {
        }

        private Direction UserVisibleDirection { get; set; }

        public override Direction MoveDirection
        {
            get
            {
                Direction d = Collisions.GetUserVisibleDirection(this, AllowedDirections);
                UserVisibleDirection = d;
                if (d == Direction.None)
                {
                    
                }

                return d;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {

            if (UserVisibleDirection == DrawDirection)
                Fire();
            base.Update(gameTime, clientBounds);
        }
    }
}
