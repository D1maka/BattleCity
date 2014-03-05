using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;


namespace AnimatedSprites
{
    public enum Direction { Up, Left, Right, Down, None }

    abstract class Tank : Sprite
    {
        GameSettings.SpriteSettings MissileSetting { get; set; }
        public Missile CurrentMissle { get; set; }

        public Tank(GameSettings.SpriteSettings tankSettings, GameSettings.SpriteSettings missileSetting)
            : base(tankSettings)
        {
            MissileSetting = missileSetting;
        }

        public Missile Fire()
        {
            if (CurrentMissle != null && CurrentMissle.State == SpriteState.Alive)
                return null;

            MissileSetting.StartPosition = GetMissileStartPosition();
            CurrentMissle = new Missile(MissileSetting, DrawDirection);

            return CurrentMissle;
        }

        public Vector2 GetMissileStartPosition()
        {
            Direction dir = DrawDirection;
            Vector2 missileStartPosition = new Vector2();

            switch (dir)
            {
                case Direction.Up:
                    missileStartPosition.X = position.X + Settings.FrameSize.X * SpriteSettings.Scale / 2 - MissileSetting.FrameSize.X * SpriteSettings.Scale / 2;
                    missileStartPosition.Y = position.Y - MissileSetting.FrameSize.Y * SpriteSettings.Scale;
                    break;
                case Direction.Left:
                    missileStartPosition.X = position.X - MissileSetting.FrameSize.X * SpriteSettings.Scale;
                    missileStartPosition.Y = position.Y + Settings.FrameSize.Y * SpriteSettings.Scale / 2 - MissileSetting.FrameSize.Y * SpriteSettings.Scale / 2;
                    break;
                case Direction.Right:
                    missileStartPosition.X = position.X + Settings.FrameSize.X * SpriteSettings.Scale;
                    missileStartPosition.Y = position.Y + Settings.FrameSize.Y * SpriteSettings.Scale / 2 - MissileSetting.FrameSize.Y * SpriteSettings.Scale / 2;
                    break;
                case Direction.Down:
                    missileStartPosition.X = position.X + Settings.FrameSize.X * SpriteSettings.Scale / 2 - MissileSetting.FrameSize.X * SpriteSettings.Scale / 2;
                    missileStartPosition.Y = position.Y + Settings.FrameSize.Y * SpriteSettings.Scale;
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
