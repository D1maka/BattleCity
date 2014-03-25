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
        private int movedDistance;
        public override Direction CalculateMoveDirection()
        {
            //Set current cell Visit
            MapInfo.SetVisitTime(position);

            //Get User position. if == current lost user
            Vector2 userTankPosition = MapInfo.GetUserPosition();
            if (userTankPosition == position)
                LostUserPosition();

            //Check visible user. If can see user "inform" all other tanks
            Vector2 userPos = MapInfo.GetUserVisiblePosition(position);
            if (userPos != Vector2.Zero)
            {
                MapInfo.SetUserPosition(userPos);
                UserVisibleDirection = AIUtils.GetMaxDirection(GetPosition - userPos);
            }
            else
                UserVisibleDirection = Direction.None;

            if (MapInfo.UserDetected)
            {
                Vector2 difference = this.GetPosition - userTankPosition;
                if (SpriteUtils.IsYAxisFromDirection(_MoveDirection) && SpriteUtils.CoordinateEqualToZero((int)difference.Y)
                    || !SpriteUtils.IsYAxisFromDirection(_MoveDirection) && SpriteUtils.CoordinateEqualToZero((int)difference.X))
                    _MoveDirection = Direction.None;

                if (!AllowedDirections.Contains(_MoveDirection))
                {
                    //Smart Algo Start
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
                //Smart Algo End
            }
            else
            {
                if (!AllowedDirections.Contains(_MoveDirection) || movedDistance > Default.CellSetting.CellSize.X)
                {
                    movedDistance = 0;
                    _MoveDirection = MapInfo.GetOldestDirection(position, AllowedDirections);
                }
                movedDistance += speedValue;
            }


            return _MoveDirection;
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

        private void LostUserPosition()
        {
            UserVisibleDirection = Direction.None;
            MapInfo.LostUserPosition();
        }
    }
}
