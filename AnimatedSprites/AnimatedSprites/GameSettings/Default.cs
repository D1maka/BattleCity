using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace AnimatedSprites.GameSettings
{
    static class Default
    {
        private static Game game = new Game();
        public static SpriteSettings GetCustomSetting(int defaultMillisecondsPerFrame, int originalSpeed, Texture2D textureImage,
            Point sheetSize, Point frameSize, Point firstFrame, Vector2 startPosition, int collisionOffset, string collisionCueName)
        {
            SpriteSettings spriteSettings = new SpriteSettings();
            spriteSettings.DefaultMillisecondsPerFrame = defaultMillisecondsPerFrame;
            spriteSettings.OriginalSpeed = originalSpeed;
            spriteSettings.TextureImage = textureImage;
            spriteSettings.SheetSize = sheetSize;
            spriteSettings.FrameSize = frameSize;
            spriteSettings.FirstFrame = firstFrame;
            spriteSettings.StartPosition = startPosition;
            spriteSettings.CollisionOffset = collisionOffset;
            spriteSettings.CollisionCueName = collisionCueName;

            return spriteSettings;
        }

        public static SpriteSettings GetMissleSetting()
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, 5, game.Content.Load<Texture2D>(@"Images\NES - Battle City - General Sprites.png"), new Point(100, 100), new Point(5, 5),
                new Point(1, 1), new Vector2(50, 50), 1, "CollisionMissle");
            return spriteSettings;
        }


        public static SpriteSettings GetTankSetting()
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, 5, game.Content.Load<Texture2D>(@"Images\NES - Battle City - General Sprites.png"), new Point(100, 100), new Point(5, 5),
                new Point(11, 11), new Vector2(50, 50), 1, "CollisionMissle");
            return spriteSettings;
        }
    }
}
