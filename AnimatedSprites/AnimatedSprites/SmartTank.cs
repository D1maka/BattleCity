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
        private Direction _MoveDirection;

        public override Direction MoveDirection
        {
            get
            {
                UserVisibleDirection = Collisions.GetUserVisibleDirection(this, AllowedDirections);

                if (UserVisibleDirection == Direction.None)
                {
                    if (!AllowedDirections.Contains(_MoveDirection))
                    {
                        Vector2 userTankPosition = Collisions.GetUserPosition(this.GetPosition);

                        Direction maxD = AIUtils.GetMaxDirection(this.GetPosition - userTankPosition);
                        if (AllowedDirections.Contains(maxD))
                            _MoveDirection = maxD;
                        else
                        {

                            Direction minD = AIUtils.GetMinDirection(this.GetPosition - userTankPosition);
                            if (AllowedDirections.Contains(minD))
                                _MoveDirection = minD;
                            else
                            {

                                if (CurrentMissle == null || CurrentMissle.State == SpriteState.Destroyed)
                                    _MoveDirection = maxD;
                                else
                                {

                                    if (AllowedDirections.Count > 0)
                                        do
                                        {
                                            _MoveDirection = RandomUtils.GetRandomDirection();
                                        } while (!AllowedDirections.Contains(_MoveDirection));
                                    else
                                        _MoveDirection = Direction.None;
                                }
                            }
                        }
                    }
                }
                else
                    _MoveDirection = UserVisibleDirection;

                return _MoveDirection;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                // Increment to next frame
                timeSinceLastFrame = 0;
                base.Update(gameTime, clientBounds);
                if (UserVisibleDirection == DrawDirection || MoveDirection != Direction.None && !AllowedDirections.Contains(MoveDirection))
                    Fire();
            }
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}
