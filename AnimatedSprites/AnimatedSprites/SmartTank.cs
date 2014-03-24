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
            UserVisibleDirection = Direction.None;
        }

        private Direction UserVisibleDirection { get; set; }
        private Direction _MoveDirection;

        public override Direction MoveDirection
        {
            get
            {
                Vector2 userPos = MapInfo.GetUserVisiblePosition(position);
                MapInfo.SetVisitTime(position);
                if (userPos == Vector2.Zero)
                {
                    if (!AllowedDirections.Contains(_MoveDirection))
                    {
                        Vector2 userTankPosition = MapInfo.GetUserPosition();
                        if (userTankPosition == position)
                            MapInfo.LostUserPosition();
                        if (MapInfo.UserDetected)
                        {
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
                        else
                        {
                            _MoveDirection = MapInfo.GetOldestDirection(position, AllowedDirections);
                            return _MoveDirection;
                        }
                    }
                }
                else
                {
                    UserVisibleDirection = AIUtils.GetMaxDirection(this.GetPosition - userPos);
                    _MoveDirection = UserVisibleDirection;
                    MapInfo.SetUserPosition(userPos);
                }

                return _MoveDirection;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                // Increment to next frame
                timeSinceLastFrame = 0;
                base.Update(gameTime);
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
