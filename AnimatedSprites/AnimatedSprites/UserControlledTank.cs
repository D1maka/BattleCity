using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;

namespace AnimatedSprites
{
    class UserControlledTank : Tank
    {
        public UserControlledTank(SpriteSettings tankSetting, SpriteSettings fireSetting)
            : base(tankSetting, fireSetting)
        {
        }

        public override Direction Direction
        {
            get { throw new NotImplementedException(); }
        }

        public override void GetCurrentFrame(Microsoft.Xna.Framework.Point currentFrame)
        {
            throw new NotImplementedException();
        }
    }
}
