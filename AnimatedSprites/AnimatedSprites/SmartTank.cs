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
                    //TODO Get UserPosition  and determine the max difference axis
                    //between AITank(this.position) and UserTank

                    //if current missile == nul and CurrentMissile.State = alive
                    //return Direction to minimize max difference(Direction from AIUtils)

                    //else return direction another direction

                    Vector2 userTankPosition = Collisions.GetUserPosition(this.GetPosition);

                    Direction d1 = AIUtils.GetMaxDirection(this.GetPosition - userTankPosition);
                    if (AllowedDirections.Contains(d1))
                        return d1;

                    Direction d2 = AIUtils.GetMinDirection(this.GetPosition - userTankPosition);
                    //if (AllowedDirections.Contains(d2))
                    return d2;

                    //Direction d3;
                    //do
                    //{
                    //    d3 = RandomUtils.GetRandomDirection();
                    //} while (!AllowedDirections.Contains(d3));
                    //return d3;
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
