using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AnimatedSprites.Utils;
using AnimatedSprites.GameSettings;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites
{
    class CellInformation
    {
        byte OwnerType;
        public CellInformation(byte owner, Vector2 pos, Game g)
        {
            OwnerType = owner;
            Owner = SpriteUtils.GetWall(owner, pos, g);
        }

        public byte GetOwnerType()
        {
            return OwnerType;
        }

        private Sprite Owner { get; set; }

        public bool Intersects(Rectangle rec)
        {
            return Owner != null && Owner.collisionRect.Intersects(rec);
        }

        public void Draw(GameTime g, SpriteBatch s)
        {
            if (timeToVisit > 0)
                timeToVisit -= g.ElapsedGameTime.Milliseconds;

            if (Owner != null)
                Owner.Draw(g, s);
        }

        public void ClearOwner()
        {
            Owner = null;
            OwnerType = Default.WallSetting.EmptyPlace;
        }

        public void SetVisitTime()
        {
            timeToVisit = AnimatedSprites.GameSettings.Default.TankSetting.MemoryTime;
        }

        public int GetVisitTime()
        {
            if (Owner == null || !(Owner is ICollidable))
                return timeToVisit;
            else return int.MaxValue;
        }

        private int timeToVisit;
    }
}
