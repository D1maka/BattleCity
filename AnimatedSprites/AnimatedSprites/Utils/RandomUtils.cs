using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AnimatedSprites.Utils
{
    static class RandomUtils
    {
        const int EmptyPlacePossibility = 60;
        const int WallPossibility = 30;
        const int BushPossibility = 10;

        public static Game Game { get; set; }

        public static Direction GetRandomDirection()
        {
            int direction = ((Game1)Game).rnd.Next(0, 4);
            return (Direction)direction;
        }

        public static int GetRandomInt(int start, int end)
        {
            return ((Game1)Game).rnd.Next(start, end);
        }

        public static SpawnPlace GetRandomEnemySpawnPlace()
        {
            int place = ((Game1)Game).rnd.Next(0, 3);
            return (SpawnPlace)place;
        }

        public static byte GetRandomStaticSprite()
        {
            int r = ((Game1)Game).rnd.Next(0, 100);
            if (r < EmptyPlacePossibility)
                return AnimatedSprites.GameSettings.Default.WallSetting.EmptyPlace;
            if (r < EmptyPlacePossibility + WallPossibility)
                return AnimatedSprites.GameSettings.Default.WallSetting.Wall;
            if (r < EmptyPlacePossibility + WallPossibility + BushPossibility)
                return AnimatedSprites.GameSettings.Default.WallSetting.Bush;

            return AnimatedSprites.GameSettings.Default.WallSetting.EmptyPlace;
        }
    }
}
