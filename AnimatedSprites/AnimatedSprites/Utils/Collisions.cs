using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimatedSprites.Utils
{
    class Collisions
    {
        public static List<Sprite> Walls;

        public static void ReleaseCollision(Sprite firstSprite, Sprite secondSprite)
        {
            if (firstSprite is Tank && secondSprite is Missile)
                ReleaseCollision((Tank)firstSprite, (Missile)secondSprite);
            if (secondSprite is Tank && firstSprite is Missile)
                ReleaseCollision((Tank)secondSprite, (Missile)firstSprite);
        }

        private static void ReleaseCollision(Tank tank, Missile missle)
        {
            if (tank.CurrentMissle != missle)
            {
                tank.Destroy();
                missle.Destroy();
            }
        }

        public static bool IsAllowedDirection(Sprite spr, Direction direction, int speed)
        {
            Rectangle rect = spr.collisionRect;
            switch (direction)
            {
                case Direction.Up: rect.Offset(0, -speed);
                    break;
                case Direction.Down: rect.Offset(0, speed);
                    break;
                case Direction.Right: rect.Offset(speed, 0);
                    break;
                case Direction.Left: rect.Offset(-speed, 0);
                    break;
            }

            foreach (Sprite w in Walls)
            {
                if (spr != w && rect.Intersects(w.collisionRect))
                    return false;
            }

            return true;
        }

    }
}
