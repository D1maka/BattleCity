using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;

namespace AnimatedSprites
{
    class AITank : Tank
    {
        public AITank(SpriteSettings settings, SpriteSettings missileSetting)
            : base(settings, missileSetting)
        {
        }

        public override Direction MoveDirection
        {
            get { return Direction.None; }
        }

        public override void GetCurrentFrame(ref Microsoft.Xna.Framework.Point currentFrame)
        {
            switch (DrawDirection)
            {
                case Direction.Up:
                    currentFrame.X = 129;
                    currentFrame.Y = 2;
                    break;
                case Direction.Left:
                    currentFrame.X = 162;
                    currentFrame.Y = 1;
                    break;
                case Direction.Right:
                    currentFrame.X = 193;
                    currentFrame.Y = 1;
                    break;
                case Direction.Down:
                    currentFrame.X = 225;
                    currentFrame.Y = 1;
                    break;
                case Direction.None:
                    currentFrame.X = 225;//TODO:DELETE
                    currentFrame.Y = 1;
                    break;
                default:
                    break;
            }
        }
    }
}
