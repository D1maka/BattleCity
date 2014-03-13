using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimatedSprites.Utils
{
    static class Collisions
    {
        public static List<Sprite> Walls;

        public static void ReleaseCollision(Sprite firstSprite, Sprite secondSprite)
        {
            if (firstSprite is Tank && secondSprite is Missile)
                ReleaseCollision((Tank)firstSprite, (Missile)secondSprite);
            else if (secondSprite is Tank && firstSprite is Missile)
                ReleaseCollision((Tank)secondSprite, (Missile)firstSprite);
            else if (firstSprite is Tank && secondSprite is Tank || secondSprite is StaticSprite && firstSprite is StaticSprite
                || firstSprite is Tank && secondSprite is StaticSprite || firstSprite is StaticSprite && secondSprite is Tank)
            {
            }
            else
            {
                firstSprite.Destroy();
                secondSprite.Destroy();
            }
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

        public static List<Direction> GetAllowedDirections(Sprite spr, int speed)
        {
            Rectangle leftRect = spr.collisionRect;
            Rectangle upRect = spr.collisionRect;
            Rectangle downRect = spr.collisionRect;
            Rectangle rightRect = spr.collisionRect;
            upRect.Offset(0, -speed);
            downRect.Offset(0, speed);
            rightRect.Offset(speed, 0);
            leftRect.Offset(-speed, 0);

            List<Direction> direcs = new List<Direction>();
            direcs.Add(Direction.Down);
            direcs.Add(Direction.Left);
            direcs.Add(Direction.Right);
            direcs.Add(Direction.Up);
            foreach (Sprite w in Walls)
            {
                if (w is Missile)
                    continue;
                if (direcs.Contains(Direction.Up) && spr != w && upRect.Intersects(w.collisionRect))
                    direcs.Remove(Direction.Up);
                if (direcs.Contains(Direction.Right) && spr != w && rightRect.Intersects(w.collisionRect))
                    direcs.Remove(Direction.Right);
                if (direcs.Contains(Direction.Left) && spr != w && leftRect.Intersects(w.collisionRect))
                    direcs.Remove(Direction.Left);
                if (direcs.Contains(Direction.Down) && spr != w && downRect.Intersects(w.collisionRect))
                    direcs.Remove(Direction.Down);
            }

            return direcs;
        }

        public static Direction GetUserVisibleDirection(AITank tank, List<Direction> allowedDirections)
        {
            if (Walls != null)
            {
                foreach (var item in Walls)
                {
                    if (!(item is UserControlledTank))
                        continue;

                    if (allowedDirections.Contains(Direction.Left) && tank.LeftRectangle.Intersects(item.collisionRect))
                        return Direction.Left;
                    else if (allowedDirections.Contains(Direction.Right) && tank.RightRectangle.Intersects(item.collisionRect))
                        return Direction.Right;
                    else if (allowedDirections.Contains(Direction.Up) && tank.UpRectangle.Intersects(item.collisionRect))
                        return Direction.Up;
                    else if (allowedDirections.Contains(Direction.Down) && tank.DownRectangle.Intersects(item.collisionRect))
                        return Direction.Down;
                }
            }

            return Direction.None;
        }

        public static Vector2 GetUserPosition(Vector2 aiPosition)
        {
            //TODO: Get Closest UserTank position
            throw new NotImplementedException();
        }
    }
}
