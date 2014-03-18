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
            , Point frameSize, Point firstFrame, Vector2 startPosition, int collisionOffset, string collisionCueName, int depthLayer)
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
            spriteSettings.DepthLayer = depthLayer;
            return spriteSettings;
        }

        public static SpriteSettings GetMissileSetting(Game game)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, 5, game.Content.Load<Texture2D>(textureImageFile), MissileSetting.FrameSize,
                new Point(1, 1), new Vector2(50, 50), 0, "CollisionMissle", 0);
            return spriteSettings;
        }


        public static SpriteSettings GetUserTankSetting(Game game, Vector2 startPosition)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, 1, game.Content.Load<Texture2D>(textureImageFile), TankSetting.FrameSize,
                new Point(1, 2), startPosition, 0, "CollisionMissle", 0);
            return spriteSettings;
        }

        public static SpriteSettings GetEnemyTankSetting(Game game, Vector2 startPosition)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, 1, game.Content.Load<Texture2D>(textureImageFile), TankSetting.FrameSize,
                new Point(11, 11), startPosition, 0, "CollisionMissle", 0);
            return spriteSettings;
        }

        public static SpriteSettings GetWallSetting(Game game, Vector2 startPosition)
        {
            return Default.GetCustomSetting(1, 0, game.Content.Load<Texture2D>(textureImageFile), WallSetting.FrameSize,
                new Point(256, 0), startPosition, 0, "CollisionMissle", 1);
        }

        public static Dictionary<Vector2, byte> GetWallPosition()
        {
            Dictionary<Vector2, byte> walls = new Dictionary<Vector2, byte>();
            const int N = 14, M = 23;

            byte[,] map = new byte[N, M]   {{0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                          {0,0,1,0,0,0,1,3,3,3,3,3,0,0,1,0,0,0,0,0,0,0,0},
                                          {0,0,0,0,1,1,1,1,1,1,0,0,0,3,1,0,0,0,0,0,1,1,1},
                                          {0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0},
                                          {0,0,1,1,1,0,1,1,0,1,1,1,1,1,1,0,0,1,0,0,1,1,1},
                                          {1,0,0,0,1,0,1,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0},
                                          {1,1,1,0,0,0,1,0,0,1,0,0,1,1,1,1,1,1,0,1,0,0,0},
                                          {1,0,0,0,0,0,1,0,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0},
                                          {0,0,0,1,1,1,1,0,0,1,1,1,1,0,0,1,0,0,0,1,0,0,0},
                                          {0,0,0,1,0,1,1,0,0,1,1,1,1,0,0,1,0,1,1,1,0,0,0},
                                          {0,0,0,1,0,1,1,0,0,1,0,1,1,0,0,1,0,0,0,1,0,0,0},
                                          {0,0,0,1,0,1,1,0,0,1,0,1,1,0,0,1,0,1,1,1,0,0,0},
                                          {0,1,1,1,0,0,0,0,0,0,3,0,0,0,0,0,0,1,0,0,0,0,0},
                                          {0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,3,0}};
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (map[i, j] > 0)
                    {
                        walls.Add(new Vector2(j * WallSetting.FrameSize.X * SpriteSettings.Scale,
                            i * WallSetting.FrameSize.Y * SpriteSettings.Scale), map[i, j]);
                    }
                }
            }

            return walls;
        }

        public class TankSetting
        {
            public static readonly Point FrameSize = new Point(13, 13);
            public const int FireGCD = 500;
        }

        public class WallSetting
        {
            public const byte Wall = 1;
            public const byte IndestructibleWall = 2;
            public const byte Bush = 3;
            public static readonly Point FrameSize = new Point(16, 16);
        }

        public class MissileSetting
        {
            public static readonly Point FrameSize = new Point(4, 4);
        }
    }
}
