using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;
using AnimatedSprites.Utils;
using Microsoft.Xna.Framework;

namespace AnimatedSprites
{
    class SmartTank : AITank
    {
        public SmartTank(SpriteSettings settings, SpriteSettings missileSetting)
            : base(settings, missileSetting)
        {
        }

        private Direction UserVisibleDirection { get; set; }

        public override Direction MoveDirection
        {
            get
            {
                Direction d = Collisions.GetUserVisibleDirection(this, AllowedDirections);
                UserVisibleDirection = d;
                if (d == Direction.None)
                {
                    Vector2 userTankPosition = Collisions.GetUserPosition(this.GetPosition);

                    d = AIUtils.GetMaxDirection(this.GetPosition - userTankPosition);
                    if (AllowedDirections.Contains(d) || CurrentMissle == null || CurrentMissle.State == SpriteState.Destroyed)
                        return d;

                    d = AIUtils.GetMinDirection(this.GetPosition - userTankPosition);
                    if (AllowedDirections.Contains(d) || CurrentMissle == null || CurrentMissle.State == SpriteState.Destroyed)
                        return d;

                    if (AllowedDirections.Count > 0)
                        do
                        {
                            d = RandomUtils.GetRandomDirection();
                        } while (!AllowedDirections.Contains(d));
                    else
                        d = Direction.None;

                    return d;
                }

                return d;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {
            base.Update(gameTime, clientBounds);
            if (UserVisibleDirection == DrawDirection || MoveDirection != Direction.None && !AllowedDirections.Contains(MoveDirection))
                Fire();
        }
    }
}
