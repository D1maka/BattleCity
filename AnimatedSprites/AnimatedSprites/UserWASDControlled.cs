using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using AnimatedSprites.GameSettings;

namespace AnimatedSprites
{
    class UserWASDControlled : UserControlledTank
    {
        public UserWASDControlled(SpriteSettings tankSetting, SpriteSettings fireSetting)
            : base(tankSetting, fireSetting)
        {
        }

        public override Direction CalculateMoveDirection()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                DrawDirection = Direction.Left;
                if (AllowedDirections.Contains(Direction.Left))
                    return Direction.Left;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                DrawDirection = Direction.Down;
                if (AllowedDirections.Contains(Direction.Down))
                    return Direction.Down;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                DrawDirection = Direction.Up;
                if (AllowedDirections.Contains(Direction.Up))
                    return Direction.Up;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                DrawDirection = Direction.Right;
                if (AllowedDirections.Contains(Direction.Right))
                    return Direction.Right;
            }

            return Direction.None;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                Fire();
        }
    }
}
