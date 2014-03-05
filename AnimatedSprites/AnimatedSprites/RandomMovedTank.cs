using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;
using AnimatedSprites.Utils;

namespace AnimatedSprites
{
    class RandomMovedTank : AITank
    {
        public RandomMovedTank(SpriteSettings settings, SpriteSettings missileSetting)
            : base(settings, missileSetting)
        {
            _ChangeDirectionTimeMax = 10000;
            _ChangeDirectionTimeMin = 5000;
        }


        private Direction _MoveDirection = RandomUtils.GetRandomDirection();
        public override Direction MoveDirection
        {
            get
            {
                if (_ChangeDirectionTime < 0 || !Collisions.IsAllowedDirection(this, _MoveDirection, speedValue))
                {
                    _MoveDirection = RandomUtils.GetRandomDirection();
                    SetChangeDirectionTime();
                }
                return _MoveDirection;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {
            _ChangeDirectionTime -= gameTime.ElapsedGameTime.Milliseconds;
            Fire();
            base.Update(gameTime, clientBounds);
        }


        int _ChangeDirectionTimeMax { get; set; }
        int _ChangeDirectionTimeMin { get; set; }
        int _ChangeDirectionTime { get; set; }
        private void SetChangeDirectionTime()
        {
            _ChangeDirectionTime = RandomUtils.GetRandomInt(_ChangeDirectionTimeMin, _ChangeDirectionTimeMax);
        }
    }
}
