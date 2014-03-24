using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimatedSprites.GameSettings;

namespace AnimatedSprites
{
    static class MapInfo
    {
        public static UserControlledTank user = null;
        private static Dictionary<Vector2, CellInformation> Map { get; set; }
        public static void Init(Dictionary<Vector2, CellInformation> map)
        {
            Map = map;
        }

        public static CellInformation Intersects(Rectangle rec)
        {
            foreach (var item in Map)
            {
                if (item.Value.Intersects(rec))
                    return item.Value;
            }

            return null;
        }

        public static void Draw(GameTime g, SpriteBatch s)
        {
            foreach (var item in Map)
                item.Value.Draw(g, s);
        }

        private static Vector2 UserDetectedPosition { get; set; }
        public static Vector2 GetUserPosition()
        {
            return UserDetectedPosition;
        }

        public static void SetUserPosition(Vector2 pos)
        {
            UserDetectedPosition = pos;
        }

        public static void LostUserPosition()
        {
            SetUserPosition(Vector2.Zero);
        }

        public static bool UserDetected { get { return UserDetectedPosition != Vector2.Zero; } }

        public static Direction GetOldestDirection(Vector2 position, List<Direction> allowedDirection)
        {
            Direction resultDirection = Direction.None;
            int minTimeToVisibility = int.MaxValue;
            if (allowedDirection.Contains(Direction.Left))
            {
                Vector2 leftPos = new Vector2(position.X - Default.WallSetting.FrameSize.X * SpriteSettings.Scale, position.Y);
                CellInformation currentCell = GetCell(leftPos);

                if (currentCell != null && minTimeToVisibility > currentCell.GetVisitTime())
                {
                    minTimeToVisibility = currentCell.GetVisitTime();
                    resultDirection = Direction.Left;
                }
            }

            if (allowedDirection.Contains(Direction.Right))
            {
                Vector2 leftPos = new Vector2(position.X + Default.TankSetting.FrameSize.X * SpriteSettings.Scale + Default.WallSetting.FrameSize.X * SpriteSettings.Scale, position.Y);
                CellInformation currentCell = GetCell(leftPos);

                if (currentCell != null && minTimeToVisibility > currentCell.GetVisitTime())
                {
                    minTimeToVisibility = currentCell.GetVisitTime();
                    resultDirection = Direction.Right;
                }
            }

            if (allowedDirection.Contains(Direction.Up))
            {
                Vector2 leftPos = new Vector2(position.X, position.Y - Default.WallSetting.FrameSize.Y * SpriteSettings.Scale);
                CellInformation currentCell = GetCell(leftPos);

                if (currentCell != null && minTimeToVisibility > currentCell.GetVisitTime())
                {
                    minTimeToVisibility = currentCell.GetVisitTime();
                    resultDirection = Direction.Up;
                }
            }

            if (allowedDirection.Contains(Direction.Down))
            {
                Vector2 leftPos = new Vector2(position.X, position.Y + Default.TankSetting.FrameSize.Y * SpriteSettings.Scale + Default.WallSetting.FrameSize.Y * SpriteSettings.Scale);
                CellInformation currentCell = GetCell(leftPos);

                if (currentCell != null && minTimeToVisibility > currentCell.GetVisitTime())
                {
                    minTimeToVisibility = currentCell.GetVisitTime();
                    resultDirection = Direction.Down;
                }
            }

            return resultDirection;
        }

        public static CellInformation GetCell(Vector2 position)
        {
            int x = (int)Math.Round(position.X / (Default.WallSetting.FrameSize.X * SpriteSettings.Scale));
            int y = (int)Math.Round(position.Y / (Default.WallSetting.FrameSize.Y * SpriteSettings.Scale));
            x *= (Default.WallSetting.FrameSize.X * (int)SpriteSettings.Scale);
            y *= (Default.WallSetting.FrameSize.Y * (int)SpriteSettings.Scale);
            Vector2 v = new Vector2(x, y);

            if (Map.ContainsKey(v))
                return Map[v];
            else return null;
        }

        public static void SetVisitTime(Vector2 pos)
        {
            CellInformation cel = GetCell(pos);
            if (cel != null)
                cel.SetVisitTime();
        }

        public static Vector2 GetUserVisiblePosition(Vector2 tankPosition)
        {
            int x = (int)(tankPosition.X / (Default.WallSetting.FrameSize.X * (int)SpriteSettings.Scale));
            int y = (int)(tankPosition.Y / (Default.WallSetting.FrameSize.Y * (int)SpriteSettings.Scale));
            x *= (Default.WallSetting.FrameSize.X * (int)SpriteSettings.Scale);
            y *= (Default.WallSetting.FrameSize.Y * (int)SpriteSettings.Scale);

            for (int i = 1; i < 20; i++)
            {
                Vector2 v = new Vector2(x + (i * Default.WallSetting.FrameSize.X * SpriteSettings.Scale), y);
                bool? result = Check(v);
                if (result == true)
                    return v;
                else if (result == false)
                    break;
            }

            for (int i = 1; i < 20; i++)
            {
                Vector2 v = new Vector2(x + (-i * Default.WallSetting.FrameSize.X * SpriteSettings.Scale), y);
                bool? result = Check(v);
                if (result == true)
                    return v;
                else if (result == false)
                    break;
            }

            for (int i = 1; i < 20; i++)
            {
                Vector2 v = new Vector2(x, y + (-i * Default.WallSetting.FrameSize.Y * SpriteSettings.Scale));
                bool? result = Check(v);
                if (result == true)
                    return v;
                else if (result == false)
                    break;
            }

            for (int i = 1; i < 20; i++)
            {
                Vector2 v = new Vector2(x, y + (i * Default.WallSetting.FrameSize.Y * SpriteSettings.Scale));
                bool? result = Check(v);
                if (result == true)
                    return v;
                else if (result == false)
                    break;
            }

            return Vector2.Zero;
        }

        public static CellInformation GetAdjacentCell(Vector2 position, List<Direction> allowedDirections)
        {

            throw new NotImplementedException();
        }

        private static bool? Check(Vector2 v)
        {
            if (!Map.ContainsKey(v))
                return false;

            CellInformation cell = Map[v];
            if (cell.GetOwnerType() == AnimatedSprites.GameSettings.Default.WallSetting.IndestructibleWall || cell.GetOwnerType() == AnimatedSprites.GameSettings.Default.WallSetting.Wall)
            {
                return false;
            }
            else
            {
                if (user.collisionRect.Intersects(new Rectangle((int)v.X, (int)v.Y, (int)(Default.TankSetting.FrameSize.X * SpriteSettings.Scale), (int)(Default.TankSetting.FrameSize.Y * SpriteSettings.Scale))))
                    return true;
            }

            return null;
        }
    }
}
