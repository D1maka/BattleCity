using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AnimatedSprites
{
    public enum Direction { Up, Left, Right, Down, None }

    abstract class Tank : Sprite
    {
        GameSettings.SpriteSettings MissileSetting { get; set; }
        Missile currentMissle { get; set; }

        public Tank(GameSettings.SpriteSettings tankSettings, GameSettings.SpriteSettings missileSetting)
            : base(tankSettings)
        {
            MissileSetting = missileSetting;
        }

        public Missile Fire()
        {
            if (currentMissle != null && currentMissle.State == SpriteState.Alive)
            {
                MissileSetting.StartPosition = GetMissileStartPosition();
                currentMissle = new Missile(MissileSetting, MoveDirection);
            }

            return currentMissle;
        }

        public Vector2 GetMissileStartPosition()
        {
            Direction dir = DrawDirection;
            Vector2 missileStartPosition = new Vector2();

            switch (dir)
            {
                case Direction.Up:
                    missileStartPosition.X = position.X + Settings.FrameSize.X / 2 - MissileSetting.FrameSize.X / 2;
                    missileStartPosition.Y = position.Y - MissileSetting.FrameSize.Y;
                    break;
                case Direction.Left:
                    missileStartPosition.X = position.X - MissileSetting.FrameSize.X;
                    missileStartPosition.Y = position.Y + Settings.FrameSize.Y / 2 - MissileSetting.FrameSize.Y / 2;
                    break;
                case Direction.Right:
                    missileStartPosition.X = position.X + Settings.FrameSize.X;
                    missileStartPosition.Y = position.Y + Settings.FrameSize.Y / 2 - MissileSetting.FrameSize.Y / 2;
                    break;
                case Direction.Down:
                    missileStartPosition.X = position.X + Settings.FrameSize.X / 2 - MissileSetting.FrameSize.X / 2;
                    missileStartPosition.Y = position.Y + Settings.FrameSize.Y;
                    break;
                case Direction.None:
                    break;
                default:
                    break;
            }

            return missileStartPosition;
        }
    }
}
