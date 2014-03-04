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
            return new Missile(MissileSetting, MoveDirection);
        }

        public Vector2 GetMissileStartPosition(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
                case Direction.Down:
                    break;
                default:
                    break;
            }
        }
    }
}
