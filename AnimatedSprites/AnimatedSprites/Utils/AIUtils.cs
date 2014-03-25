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
                direction = GetXDirection(difference.X);
            else
                direction = GetYDirection(difference.Y);

            return direction;
        }

        public static Direction GetMaxDirection(Vector2 difference)
        {
            Direction direction;
            if (Math.Abs(difference.X) > Math.Abs(difference.Y))
                direction = GetXDirection(difference.X);
            else
                direction = GetYDirection(difference.Y);

            return direction;
        }

        private static Direction GetXDirection(float x)
        {
            if (x > 0)
                return Direction.Left;
            else if (x < 0)
                return Direction.Right;

            return Direction.None;
        }

        private static Direction GetYDirection(float y)
        {
            if (y > 0)
                return Direction.Up;
            else if (y < 0)
                return Direction.Down;

            return Direction.None;
        }
    }
}
