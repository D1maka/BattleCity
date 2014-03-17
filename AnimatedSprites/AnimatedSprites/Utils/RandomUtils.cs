using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AnimatedSprites.Utils
{
    static class RandomUtils
    {
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
            int place = ((Game1)Game).rnd.Next(0, 2);
            return (SpawnPlace)place;
        }
    }
}
