using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;
using Microsoft.Xna.Framework.Input;
using AnimatedSprites.Utils;

namespace AnimatedSprites
{
    abstract class UserControlledTank : Tank
    {
        public UserControlledTank(SpriteSettings tankSetting, SpriteSettings fireSetting)
            : base(tankSetting, fireSetting)
        {
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

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}
