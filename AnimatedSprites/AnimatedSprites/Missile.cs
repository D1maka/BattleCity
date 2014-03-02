using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AnimatedSprites
{
    class Missile : Sprite
    {

        Direction fireDirection { get; set; }
        public Missile(GameSettings.SpriteSettings setting, Direction direction)
            : base(setting)
        {
            fireDirection = direction;
        }

        public override Direction Direction
        {
            get { return fireDirection; }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += speed;
            base.Update(gameTime, clientBounds);
        }

        public override void GetCurrentFrame(Microsoft.Xna.Framework.Point currentFrame)
        {
            if (Direction == Direction.Up)
            {
                currentFrame.X = 323;
                currentFrame.Y = 102;
            }
            else if (Direction == Direction.Left)
            {
                currentFrame.X = 330;
                currentFrame.Y = 102;
            }
            else if (Direction == Direction.Down)
            {
                currentFrame.X = 339;
                currentFrame.Y = 102;
            }
            else if (Direction == Direction.Right)
            {
                currentFrame.X = 346;
                currentFrame.Y = 102;
            }
        }
    }
}
