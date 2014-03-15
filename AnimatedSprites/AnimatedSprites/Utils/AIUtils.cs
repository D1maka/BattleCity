using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AnimatedSprites.Utils
{
    public static class AIUtils
    {
        public static Direction GetMinDirection(Vector2 difference)
        {
            Direction direction;
            if (Math.Abs(difference.X) < Math.Abs(difference.Y))
            {
                direction = (difference.X > 0) ? Direction.Left : Direction.Right;
            }
            else
            {
                direction = (difference.Y > 0) ? Direction.Up : Direction.Down;
            }

            return direction;
        }

        public static Direction GetMaxDirection(Vector2 difference)
        {
            Direction direction;
            if (Math.Abs(difference.X) > Math.Abs(difference.Y))
            {
                direction = (difference.X > 0) ? Direction.Left : Direction.Right;
            }
            else
            {
                direction = (difference.Y > 0) ? Direction.Up : Direction.Down;
            }

            return direction;
        }
    }
}
