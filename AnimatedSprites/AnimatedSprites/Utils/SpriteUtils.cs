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
        public static StaticSprite GetWall(byte type, Vector2 position, Game game)
        {
            switch (type)
            {
                case Default.WallSetting.IndestructibleWall:
                    return new IndestructibleWall(Default.GetWallSetting(game, position));
                case Default.WallSetting.Wall:
                    return new Wall(Default.GetWallSetting(game, position));
                case Default.WallSetting.Bush:
                    return new Bush(Default.GetWallSetting(game, position));
                default:
                    return null;
            }
        }
    }
}
