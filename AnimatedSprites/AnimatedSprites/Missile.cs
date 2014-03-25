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

        public override Direction CalculateMoveDirection()
        {
            return fireDirection; 
        }

        public override void Update(GameTime gameTime)
        {
            MoveDirection = CalculateMoveDirection();
            position += speed;
            base.Update(gameTime);
        }

        public override void GetCurrentFrame(ref Microsoft.Xna.Framework.Point currentFrame)
        {
            if (DrawDirection == Direction.Up)
            {
                currentFrame.X = 322;
                currentFrame.Y = 101;
            }
            else if (DrawDirection == Direction.Left)
            {
                currentFrame.X = 329;
                currentFrame.Y = 101;
            }
            else if (DrawDirection == Direction.Down)
            {
                currentFrame.X = 338;
                currentFrame.Y = 101;
            }
            else if (DrawDirection == Direction.Right)
            {
                currentFrame.X = 345;
                currentFrame.Y = 101;
            }
        }
    }
}
