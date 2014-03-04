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
        const string textureImageFile = @"Images\sprites";

        public static SpriteSettings GetCustomSetting(int defaultMillisecondsPerFrame, int originalSpeed, Texture2D textureImage
            , Point frameSize, Point firstFrame, Vector2 startPosition, int collisionOffset, string collisionCueName)
        {
            SpriteSettings spriteSettings = new SpriteSettings();
            spriteSettings.DefaultMillisecondsPerFrame = defaultMillisecondsPerFrame;
            spriteSettings.OriginalSpeed = originalSpeed;
            spriteSettings.TextureImage = textureImage;
            spriteSettings.FrameSize = frameSize;
            spriteSettings.FirstFrame = firstFrame;
            spriteSettings.StartPosition = startPosition;
            spriteSettings.CollisionOffset = collisionOffset;
            spriteSettings.CollisionCueName = collisionCueName;

            return spriteSettings;
        }

        public static SpriteSettings GetMissileSetting(Game game)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, 5, game.Content.Load<Texture2D>(textureImageFile), new Point(5, 5),
                new Point(1, 1), new Vector2(50, 50), 1, "CollisionMissle");
            return spriteSettings;
        }


        public static SpriteSettings GetUserTankSetting(Game game)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(16, 2, game.Content.Load<Texture2D>(textureImageFile), new Point(13, 13),
                new Point(1, 2), new Vector2(50, 50), 0, "CollisionMissle");
            return spriteSettings;
        }

        public static SpriteSettings GetEnemyTankSetting(Game game)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, 5, game.Content.Load<Texture2D>(textureImageFile), new Point(12, 12),
                new Point(11, 11), new Vector2(50, 50), 1, "CollisionMissle");
            return spriteSettings;
        }

        public static SpriteSettings GetWallSetting(Game game)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(16, 0, game.Content.Load<Texture2D>(textureImageFile), new Point(15, 15),
                new Point(256, 0), new Vector2(200, 200), 1, "CollisionMissle");
            return spriteSettings;
        }
    }
}
