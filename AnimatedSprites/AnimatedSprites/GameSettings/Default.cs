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
            , Point frameSize, Point firstFrame, Vector2 startPosition, int collisionOffset, string collisionCueName, int depthLayer, int teamNumber)
        {
            SpriteSettings spriteSettings = new SpriteSettings();
            spriteSettings.TeamNumber = teamNumber;
            spriteSettings.DefaultMillisecondsPerFrame = defaultMillisecondsPerFrame;
            spriteSettings.OriginalSpeed = originalSpeed;

            spriteSettings.TextureImage = textureImage;
            spriteSettings.FrameSize = frameSize;
            spriteSettings.FirstFrame = firstFrame;
            spriteSettings.StartPosition = startPosition;
            spriteSettings.CollisionOffset = collisionOffset;
            spriteSettings.CollisionCueName = collisionCueName;
            spriteSettings.DepthLayer = depthLayer;
            return spriteSettings;
        }

        public static SpriteSettings GetMissileSetting(Game game)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, 5, game.Content.Load<Texture2D>(textureImageFile), MissileSetting.FrameSize,
                new Point(1, 1), new Vector2(50, 50), 0, "CollisionMissle", 0, 0);
            return spriteSettings;
        }


        public static SpriteSettings GetUserTankSetting(Game game, Vector2 startPosition)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, TankSetting.SpeedValue, game.Content.Load<Texture2D>(textureImageFile), TankSetting.FrameSize,
                new Point(1, 2), startPosition, 0, "CollisionMissle", 0, Team.UserTeam);
            return spriteSettings;
        }

        public static SpriteSettings GetEnemyTankSetting(Game game, Vector2 startPosition)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, TankSetting.SpeedValue, game.Content.Load<Texture2D>(textureImageFile), TankSetting.FrameSize,
                new Point(11, 11), startPosition, 0, "CollisionMissle", 0, Team.EnemyTeam);
            return spriteSettings;
        }

        public static SpriteSettings GetWallSetting(Game game, Vector2 startPosition)
        {
            return GetStaticSetting(game, startPosition, new Point(256, 0));
        }

        public static SpriteSettings GetBushSetting(Game game, Vector2 startPosition)
        {
            return GetStaticSetting(game, startPosition, new Point(272, 32));
        }

        private static SpriteSettings GetStaticSetting(Game game, Vector2 startPosition, Point startFrame)
        {
            return Default.GetCustomSetting(1, 0, game.Content.Load<Texture2D>(textureImageFile), WallSetting.FrameSize,
                startFrame, startPosition, 0, "CollisionMissle", 1, Team.StaticTeam);
        }

        public class TankSetting
        {
            public static readonly Point FrameSize = new Point(13, 13);
            public const int FireGCD = 500;
            public const int MemoryTime = 20000;
            public const int SpeedValue = 1;
        }

        public class WallSetting
        {
            public const byte EmptyPlace = 0;
            public const byte Wall = 1;
            public const byte IndestructibleWall = 2;
            public const byte Bush = 3;
            public static readonly Point FrameSize = new Point(16, 16);
        }

        public class MissileSetting
        {
            public static readonly Point FrameSize = new Point(4, 4);
        }

        public class Team
        {
            public const int UserTeam = 1;
            public const int EnemyTeam = 2;
            public const int StaticTeam = 3;
        }

        public class CellSetting
        {
            public static readonly Point CellSize = new Point((int)(SpriteSettings.Scale * WallSetting.FrameSize.X), (int)(SpriteSettings.Scale * WallSetting.FrameSize.Y));
        }
    }
}
