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
        public static Rectangle GameWindow { get; set; }
        public static void ReleaseCollision(Sprite firstSprite, Sprite secondSprite)
        {
            if (firstSprite is Tank && secondSprite is Tank || secondSprite is StaticSprite && firstSprite is StaticSprite
                || firstSprite is Tank && secondSprite is StaticSprite || firstSprite is StaticSprite && secondSprite is Tank)
            {
            }
            else
            {
                firstSprite.Destroy();
                secondSprite.Destroy();
            }
        }

        public static bool IsOutOfBounds(Rectangle spritRectangle, Rectangle bounds)
        {
            return (spritRectangle.X < 0) ||
                (spritRectangle.Y < 0) ||
                (spritRectangle.X + spritRectangle.Width > bounds.Width) ||
                (spritRectangle.Y + spritRectangle.Height > bounds.Height);
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

            if (IsOutOfBounds(upRect, GameWindow))
                direcs.Remove(Direction.Up);
            if (IsOutOfBounds(rightRect, GameWindow))
                direcs.Remove(Direction.Right);
            if (IsOutOfBounds(leftRect, GameWindow))
                direcs.Remove(Direction.Left);
            if (IsOutOfBounds(downRect, GameWindow))
                direcs.Remove(Direction.Down);

            foreach (Sprite w in Walls)
            {
                if (direcs.Count == 0)
                    break;
                if (w is Missile || spr == w)
                    continue;

                if (direcs.Contains(Direction.Up) && upRect.Intersects(w.collisionRect))
                    direcs.Remove(Direction.Up);
                if (direcs.Contains(Direction.Right) && rightRect.Intersects(w.collisionRect))
                    direcs.Remove(Direction.Right);
                if (direcs.Contains(Direction.Left) && leftRect.Intersects(w.collisionRect))
                    direcs.Remove(Direction.Left);
                if (direcs.Contains(Direction.Down) && downRect.Intersects(w.collisionRect))
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

                    if ((allowedDirections.Contains(Direction.Left)) && tank.LeftRectangle.Intersects(item.collisionRect))
                        return Direction.Left;
                    else if ((allowedDirections.Contains(Direction.Right)) && tank.RightRectangle.Intersects(item.collisionRect))
                        return Direction.Right;
                    else if ((allowedDirections.Contains(Direction.Up)) && tank.UpRectangle.Intersects(item.collisionRect))
                        return Direction.Up;
                    else if ((allowedDirections.Contains(Direction.Down)) && tank.DownRectangle.Intersects(item.collisionRect))
                        return Direction.Down;
                }
            }

            return Direction.None;
        }

        public static Vector2 GetUserPosition(Vector2 aiPosition)
        {
            Vector2 closest = aiPosition;
            float minDistance = float.MaxValue;
            float distance = 0.0f;

            foreach (Sprite sprite in Walls)
            {
                if (sprite is UserControlledTank)
                {
                    Vector2 currentSpritePosition = sprite.GetPosition;
                    distance = (currentSpritePosition - aiPosition).Length();
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closest = currentSpritePosition;
                    }
                }
            }

            return closest;
        }
    }
}
