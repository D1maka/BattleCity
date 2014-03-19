using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites.GameSettings
{
    public class SpriteSettings
    {
        //Default milisseconds per frame
        public int DefaultMillisecondsPerFrame { get; set; }
        //Dafault Speed
        public int OriginalSpeed { get; set; }
        //SpriteSheet Image
        public Texture2D TextureImage { get; set; }
        //Sprite Image
        public Point FrameSize { get; set; }
        public Point FirstFrame { get; set; }
        public Vector2 StartPosition { get; set; }
        //Data for Collision Rectangle
        public int CollisionOffset { get; set; }
        //Data for Collision Audio
        public string CollisionCueName { get; set; }
        //Scale(don`t use)
        public int TeamNumber { get; set; }
        public const float Scale = 2;
        //Layer
        public int DepthLayer { get; set; }
    }
}
