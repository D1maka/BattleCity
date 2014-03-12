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
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, 5, game.Content.Load<Texture2D>(textureImageFile), new Point(4, 4),
                new Point(1, 1), new Vector2(50, 50), 1, "CollisionMissle");
            return spriteSettings;
        }


        public static SpriteSettings GetUserTankSetting(Game game)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, 1, game.Content.Load<Texture2D>(textureImageFile), TankSetting.FrameSize,
                new Point(1, 2), new Vector2(400, 400), -1, "CollisionMissle");
            return spriteSettings;
        }

        public static SpriteSettings GetEnemyTankSetting(Game game, Vector2 startPosition)
        {
            SpriteSettings spriteSettings = Default.GetCustomSetting(1, 1, game.Content.Load<Texture2D>(textureImageFile), TankSetting.FrameSize,
                new Point(11, 11), startPosition, -1, "CollisionMissle");
            return spriteSettings;
        }

        public static SpriteSettings GetWallSetting(Game game, Vector2 startPosition)
        {
            return Default.GetCustomSetting(1, 0, game.Content.Load<Texture2D>(textureImageFile), WallSetting.FrameSize,
                new Point(256, 0), startPosition, 0, "CollisionMissle");
        }

        public static List<Vector2> GetWallPosition()
        {
            List<Vector2> walls = new List<Vector2>();
            const int N = 13, M = 23;

            int[,] map = new int[N, M]   {{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                          {0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0},
                                          {0,0,0,0,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,1,1,1},
                                          {0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0},
                                          {0,0,1,1,1,0,1,1,0,1,1,1,1,1,1,0,0,1,0,0,1,1,1},
                                          {0,0,0,0,1,0,1,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0},
                                          {0,0,0,0,0,0,1,0,0,1,0,0,1,1,1,1,1,1,0,1,0,0,0},
                                          {0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0},
                                          {0,0,0,1,1,1,1,0,0,1,1,1,1,0,0,1,0,0,0,1,0,0,0},
                                          {0,0,0,1,0,1,1,0,0,1,1,1,1,0,0,1,0,1,1,1,0,0,0},
                                          {0,0,0,1,0,1,1,0,0,1,0,1,1,0,0,1,0,0,0,1,0,0,0},
                                          {0,0,0,1,0,1,1,0,0,1,0,1,1,0,0,1,0,0,0,1,0,0,0},
                                          {0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (map[i, j] == 1)
                    {
                        walls.Add(new Vector2(WallSetting.FrameSize.X * SpriteSettings.Scale + j * WallSetting.FrameSize.X * 
                        SpriteSettings.Scale, WallSetting.FrameSize.Y * SpriteSettings.Scale + i * WallSetting.FrameSize.Y * SpriteSettings.Scale));
                    }
                }
            }

            return walls;
        }

        public class TankSetting
        {
            public static readonly Point FrameSize = new Point(13, 13);
        }
        public class WallSetting
        {
            public static readonly Point FrameSize = new Point(15, 15);
        }
    }
}
