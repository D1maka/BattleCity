using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimatedSprites.GameSettings;
using Microsoft.Xna.Framework;

namespace AnimatedSprites.Utils
{
    static class SpriteUtils
    {
        public static Rectangle GameWindow { get; set; }
        public static StaticSprite GetWall(byte type, Vector2 position, Game game)
        {
            switch (type)
            {
                case Default.WallSetting.IndestructibleWall:
                    return new IndestructibleWall(Default.GetWallSetting(game, position));
                case Default.WallSetting.Wall:
                    return new Wall(Default.GetWallSetting(game, position));
                case Default.WallSetting.Bush:
                    return new Bush(Default.GetBushSetting(game, position));
                default:
                    return null;
            }
        }

        public static Dictionary<Vector2, byte> GetWallPositions()
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
                        walls.Add(new Vector2(j * Default.WallSetting.FrameSize.X * (int)SpriteSettings.Scale,
                            i * Default.WallSetting.FrameSize.Y * (int)SpriteSettings.Scale), map[i, j]);
                    }
                }
            }

            return walls;
        }

        public static Dictionary<Vector2, CellInformation> GetDynamicMap(Game game)
        {
            Dictionary<Vector2, CellInformation> walls = new Dictionary<Vector2, CellInformation>();
            int N = (int)(GameWindow.Height / Default.WallSetting.FrameSize.Y / SpriteSettings.Scale), M = (int)(GameWindow.Width / Default.WallSetting.FrameSize.X / SpriteSettings.Scale);

            byte[,] map = new byte[N, M];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    map[i, j] = RandomUtils.GetRandomStaticSprite();
                    Vector2 pos = new Vector2(j * Default.WallSetting.FrameSize.X * SpriteSettings.Scale,
                        i * Default.WallSetting.FrameSize.Y * SpriteSettings.Scale);
                    walls.Add(pos, new CellInformation(map[i, j], pos, game));
                }
            }

            return walls;
        }
    }
}
