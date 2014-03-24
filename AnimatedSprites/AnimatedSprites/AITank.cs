using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;
using AnimatedSprites.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites
{
    abstract class AITank : Tank
    {
        const int VissionRectangleLength = 1000;
        public AITank(SpriteSettings settings, SpriteSettings missileSetting)
            : base(settings, missileSetting)
        {
        }

        public Rectangle LeftRectangle { get { return new Rectangle((int)position.X - VissionRectangleLength, (int)position.Y + Default.TankSetting.FrameSize.Y * (int)SpriteSettings.Scale / 2 - Default.MissileSetting.FrameSize.Y * (int)SpriteSettings.Scale / 2, VissionRectangleLength, Default.MissileSetting.FrameSize.Y * (int)SpriteSettings.Scale); } }
        public Rectangle RightRectangle { get { return new Rectangle((int)(position.X + Settings.FrameSize.X * SpriteSettings.Scale), (int)(int)position.Y + Default.TankSetting.FrameSize.Y * (int)SpriteSettings.Scale / 2 - Default.MissileSetting.FrameSize.Y * (int)SpriteSettings.Scale / 2, VissionRectangleLength, Default.MissileSetting.FrameSize.Y * (int)SpriteSettings.Scale); } }
        public Rectangle DownRectangle { get { return new Rectangle((int)position.X + Default.TankSetting.FrameSize.X * (int)SpriteSettings.Scale / 2 - Default.MissileSetting.FrameSize.X * (int)SpriteSettings.Scale / 2, (int)position.Y + Settings.FrameSize.Y * (int)SpriteSettings.Scale, Default.MissileSetting.FrameSize.X * (int)SpriteSettings.Scale, VissionRectangleLength); } }
        public Rectangle UpRectangle { get { return new Rectangle((int)position.X + Default.TankSetting.FrameSize.X * (int)SpriteSettings.Scale / 2 - Default.MissileSetting.FrameSize.X * (int)SpriteSettings.Scale / 2, (int)position.Y - VissionRectangleLength, Default.MissileSetting.FrameSize.X * (int)SpriteSettings.Scale, VissionRectangleLength); } }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Settings.TextureImage, LeftRectangle, Color.White);
            //spriteBatch.Draw(Settings.TextureImage, RightRectangle, Color.White);
            //spriteBatch.Draw(Settings.TextureImage, UpRectangle, Color.White);
            //spriteBatch.Draw(Settings.TextureImage, DownRectangle, Color.White);
            base.Draw(gameTime, spriteBatch);
        }

        public override void GetCurrentFrame(ref Microsoft.Xna.Framework.Point currentFrame)
        {
            switch (DrawDirection)
            {
                case Direction.Up:
                    currentFrame.X = 129;
                    currentFrame.Y = 2;
                    break;
                case Direction.Left:
                    currentFrame.X = 162;
                    currentFrame.Y = 1;
                    break;
                case Direction.Right:
                    currentFrame.X = 225;
                    currentFrame.Y = 1;
                    break;
                case Direction.Down:
                    currentFrame.X = 193;
                    currentFrame.Y = 1;
                    break;
                default:
                    break;
            }
        }
    }
}
