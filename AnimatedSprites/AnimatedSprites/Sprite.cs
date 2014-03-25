using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimatedSprites.GameSettings;
using AnimatedSprites.Utils;

namespace AnimatedSprites
{
    public enum SpriteState
    {
        Alive,
        Destroyed
    }

    abstract class Sprite
    {
        //Original data
        protected SpriteSettings Settings { get; set; }
        public SpriteState State { get; set; }
        public int TeamNumber { get; protected set; }
        // Stuff needed to draw the sprite
        Point currentFrame;
        // Framerate stuff
        protected int timeSinceLastFrame = 0;
        protected int millisecondsPerFrame;
        // Movement data
        protected int speedValue;
        protected Vector2 speed
        {
            get
            {
                Vector2 result = new Vector2();

                if (MoveDirection == Direction.Down)
                    result.Y = speedValue;
                else if (MoveDirection == Direction.Left)
                    result.X = -speedValue;
                else if (MoveDirection == Direction.Up)
                    result.Y = -speedValue;
                else if (MoveDirection == Direction.Right)
                    result.X = speedValue;

                return result;
            }
        }
        protected Vector2 position;
        // Sound stuff
        public string collisionCueName { get; private set; }

        public Vector2 GetPosition
        {
            get { return position; }
        }

        public abstract Direction CalculateMoveDirection();

        // Abstract definition of direction property
        protected Direction MoveDirection { get; set; }

        // Abstract definition of direction property
        private Direction _DrawDirection;
        protected Direction DrawDirection
        {
            get
            {
                if (MoveDirection != Direction.None)
                    _DrawDirection = MoveDirection;

                return _DrawDirection;
            }
            set
            {
                _DrawDirection = value;
            }
        }

        public Sprite(SpriteSettings settings)
        {
            this.Settings = settings;
            this.currentFrame = settings.FirstFrame;
            this.speedValue = Settings.OriginalSpeed;
            this.collisionCueName = Settings.CollisionCueName;
            this.position = Settings.StartPosition;
            this.millisecondsPerFrame = Settings.DefaultMillisecondsPerFrame;
            this.TeamNumber = settings.TeamNumber;
        }

        public virtual void Update(GameTime gameTime)
        {

            // Update frame if time to do so based on framerate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                // Increment to next frame
                timeSinceLastFrame = 0;
                GetCurrentFrame(ref currentFrame);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw the sprite
            spriteBatch.Draw(Settings.TextureImage,
                position,
                new Rectangle(currentFrame.X,
                    currentFrame.Y,
                    Settings.FrameSize.X, Settings.FrameSize.Y),
                Color.White, 0, Vector2.Zero, SpriteSettings.Scale
                , SpriteEffects.None, Settings.DepthLayer);
        }

        public abstract void GetCurrentFrame(ref Point currentFrame);

        // Gets the collision rect based on position, framesize and collision offset
        public virtual Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                    (int)(position.X + (Settings.CollisionOffset * SpriteSettings.Scale)),
                    (int)(position.Y + (Settings.CollisionOffset * SpriteSettings.Scale)),
                    (int)((Settings.FrameSize.X - (Settings.CollisionOffset * 2)) * SpriteSettings.Scale),
                    (int)((Settings.FrameSize.Y - (Settings.CollisionOffset * 2)) * SpriteSettings.Scale));
            }
        }

        public bool IsOutOfBounds(Rectangle bounds)
        {
            return Collisions.IsOutOfBounds(collisionRect, bounds);
        }


        public virtual void Destroy()
        {
            State = SpriteState.Destroyed;
        }
    }
}
