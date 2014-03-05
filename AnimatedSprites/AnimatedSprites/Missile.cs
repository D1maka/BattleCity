using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AnimatedSprites
{
    class Missile : Sprite
    {

        public Direction fireDirection { get; set; }
        public Missile(GameSettings.SpriteSettings setting, Direction direction)
            : base(setting)
        {
            fireDirection = direction;
        }

        public override Direction MoveDirection
        {
            get { return fireDirection; }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += speed;
            base.Update(gameTime, clientBounds);
        }

        public override void GetCurrentFrame(ref Microsoft.Xna.Framework.Point currentFrame)
        {
            if (DrawDirection == Direction.Up)
            {
                currentFrame.X = 323 - 1;
                currentFrame.Y = 102 - 1;
            }
            else if (DrawDirection == Direction.Left)
            {
                currentFrame.X = 330 - 1;
                currentFrame.Y = 102 - 1;
            }
            else if (DrawDirection == Direction.Down)
            {
                currentFrame.X = 339 - 1;
                currentFrame.Y = 102 - 1;
            }
            else if (DrawDirection == Direction.Right)
            {
                currentFrame.X = 346 - 1;
                currentFrame.Y = 102 - 1;
            }
        }
    }
}
