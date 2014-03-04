using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;
using Microsoft.Xna.Framework.Input;
using AnimatedSprites.Utils;

namespace AnimatedSprites
{
    class UserControlledTank : Tank
    {
        public UserControlledTank(SpriteSettings tankSetting, SpriteSettings fireSetting)
            : base(tankSetting, fireSetting)
        {
        }

        public override Direction MoveDirection
        {
            get
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    DrawDirection = Direction.Left;
                    if (Collisions.IsAllowedDirection(this, Direction.Left, speedValue))
                        return Direction.Left;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    DrawDirection = Direction.Down;
                    if (Collisions.IsAllowedDirection(this, Direction.Down, speedValue))
                        return Direction.Down;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    DrawDirection = Direction.Up;
                    if (Collisions.IsAllowedDirection(this, Direction.Up, speedValue))
                        return Direction.Up;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    DrawDirection = Direction.Right;
                    if (Collisions.IsAllowedDirection(this, Direction.Right, speedValue))
                        return Direction.Right;
                }

                return Direction.None;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {
            position += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                Fire();

            base.Update(gameTime, clientBounds);
        }

        public override void GetCurrentFrame(ref Microsoft.Xna.Framework.Point currentFrame)
        {
            if (DrawDirection == Direction.Up)
            {
                currentFrame.X = 1;
                currentFrame.Y = 2;
            }
            else if (DrawDirection == Direction.Left)
            {
                currentFrame.X = 34;
                currentFrame.Y = 1;
            }
            else if (DrawDirection == Direction.Down)
            {
                currentFrame.X = 65;
                currentFrame.Y = 1;
            }
            else if (DrawDirection == Direction.Right)
            {
                currentFrame.X = 97;
                currentFrame.Y = 1;
            }
        }
    }
}
