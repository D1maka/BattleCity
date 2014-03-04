using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AnimatedSprites.GameSettings;

namespace AnimatedSprites
{
    abstract class Sprite
    {
        //Original data
        SpriteSettings Settings { get; set; }

        // Stuff needed to draw the sprite
        Point currentFrame;
        // Framerate stuff
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        // Movement data
        int speedValue;
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

        // Abstract definition of direction property
        public abstract Direction MoveDirection
        {
            get;
        }

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
        }

        public Sprite(SpriteSettings settings)
        {
            this.Settings = settings;
            this.currentFrame = Settings.FirstFrame;
            this.speedValue = Settings.OriginalSpeed;
            this.collisionCueName = Settings.CollisionCueName;
            this.millisecondsPerFrame = Settings.DefaultMillisecondsPerFrame;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
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
                , SpriteEffects.None, 0);
        }

        public abstract void GetCurrentFrame(ref Point currentFrame);

        // Gets the collision rect based on position, framesize and collision offset
        public Rectangle collisionRect
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
            return (position.X < (-Settings.FrameSize.X) ||
                position.Y < (-Settings.FrameSize.Y) ||
                position.X > bounds.Width ||
                position.Y > bounds.Height);
        }
    }
}
