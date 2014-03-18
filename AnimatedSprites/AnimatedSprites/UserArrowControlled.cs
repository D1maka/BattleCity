using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;
using Microsoft.Xna.Framework.Input;

namespace AnimatedSprites
{
    class UserArrowControlled : UserControlledTank
    {
        public UserArrowControlled(SpriteSettings tankSetting, SpriteSettings fireSetting)
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
                    if (AllowedDirections.Contains(Direction.Left))
                        return Direction.Left;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    DrawDirection = Direction.Down;
                    if (AllowedDirections.Contains(Direction.Down))
                        return Direction.Down;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    DrawDirection = Direction.Up;
                    if (AllowedDirections.Contains(Direction.Up))
                        return Direction.Up;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    DrawDirection = Direction.Right;
                    if (AllowedDirections.Contains(Direction.Right))
                        return Direction.Right;
                }

                return Direction.None;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {
            base.Update(gameTime, clientBounds);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                Fire();
        }
    }
}
