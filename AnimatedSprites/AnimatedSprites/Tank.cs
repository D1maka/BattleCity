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

        public Tank(GameSettings.SpriteSettings tankSettings, GameSettings.SpriteSettings missileSetting)
            : base(tankSettings)
        {
            MissileSetting = missileSetting;
        }

        public Missile Fire()
        {
            return new Missile(MissileSetting, MoveDirection);
        }
    }
}
