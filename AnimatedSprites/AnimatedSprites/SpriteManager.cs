using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AnimatedSprites.GameSettings;
using AnimatedSprites.Utils;


namespace AnimatedSprites
{
    public enum SpawnPlace
    {
        TopLeft, TopMiddle, TopRight, BottomLeft, BottomRight
    }
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //SpriteBatch for drawing
        SpriteBatch spriteBatch;
        //spawn stuff
        int enemySpawnMinMilliseconds = 1000;
        int enemySpawnMaxMilliseconds = 6000;
        //A sprite for the player and a list of automated sprites
        List<Sprite> spriteList = new List<Sprite>();
        Vector2 MiddleEnemyPosition { get; set; }
        Vector2 LeftEnemyPosition { get; set; }
        Vector2 RightEnemyPosition { get; set; }
        Vector2 LeftUserPosition { get; set; }
        Vector2 RightUserPosition { get; set; }

        public SpriteManager(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            ResetSpawnTime();
            LeftEnemyPosition = new Vector2(0, 0);
            RightEnemyPosition = new Vector2(Game.Window.ClientBounds.Width - AnimatedSprites.GameSettings.Default.TankSetting.FrameSize.X * SpriteSettings.Scale, 0);
            MiddleEnemyPosition = new Vector2((Game.Window.ClientBounds.Width - AnimatedSprites.GameSettings.Default.TankSetting.FrameSize.X * SpriteSettings.Scale) / 2, 0);
            LeftUserPosition = new Vector2(0, Game.Window.ClientBounds.Height - AnimatedSprites.GameSettings.Default.TankSetting.FrameSize.Y * SpriteSettings.Scale);
            RightUserPosition = new Vector2(Game.Window.ClientBounds.Width - AnimatedSprites.GameSettings.Default.TankSetting.FrameSize.X * SpriteSettings.Scale, Game.Window.ClientBounds.Height - AnimatedSprites.GameSettings.Default.TankSetting.FrameSize.Y * SpriteSettings.Scale);
            RandomUtils.Game = Game;
            Collisions.GameWindow = Game.Window.ClientBounds;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            spriteList.Add(new UserControlledTank(Default.GetUserTankSetting(Game), Default.GetMissileSetting(Game)));

            Dictionary<Vector2, byte> walls = Default.GetWallPosition();
            foreach (KeyValuePair<Vector2, byte> pos in walls)
                spriteList.Add(SpriteUtils.GetWall(pos.Value, pos.Key, Game));

            //Configure Utils
            Collisions.Walls = spriteList;

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (spriteList.Find(item => item is UserControlledTank) == null)
            {
                ((Game1)Game).EndGame();
            }

            nextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
            if (nextSpawnTime < 0)
            {
                SpawnEnemy();
                // Сбрасываем таймер
                ResetSpawnTime();
            }
            UpdateSprites(gameTime);
            base.Update(gameTime);
        }

        void UpdateSprites(GameTime gameTime)
        {
            if (spriteList.Count > 0)
            {
                List<Sprite> spawnedSprites = new List<Sprite>();
                for (int i = 0; i < spriteList.Count; ++i)
                {
                    Sprite s = spriteList[i];

                    s.Update(gameTime, Game.Window.ClientBounds);

                    if (spriteList.Count > i + 1)
                    {
                        for (int j = i + 1; j < spriteList.Count; j++)
                        {
                            if (spriteList[j].State == SpriteState.Alive &&
                                s.State == SpriteState.Alive &&
                                s.collisionRect.Intersects(spriteList[j].collisionRect))
                                Collisions.ReleaseCollision(s, spriteList[j]);
                        }
                    }

                    if (s is Tank)
                    {
                        Missile m = (s as Tank).CurrentMissle;
                        if (m != null && m.State == SpriteState.Alive && !spriteList.Contains(m) && !spawnedSprites.Contains(m))
                            spawnedSprites.Add(m);
                    }

                    // Удаляем объект, если он вне поля
                    if (Game.Window.ClientBounds.X > 0 && s.IsOutOfBounds(Game.Window.ClientBounds))
                        s.State = SpriteState.Destroyed;

                    if (s.State == SpriteState.Destroyed)
                    {
                        spriteList.RemoveAt(i);
                        --i;
                    }
                }

                spriteList.AddRange(spawnedSprites);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            foreach (Sprite s in spriteList)
                s.Draw(gameTime, spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        int nextSpawnTime = 0;
        private void ResetSpawnTime()
        {
            nextSpawnTime = ((Game1)Game).rnd.Next(
                enemySpawnMinMilliseconds,
                enemySpawnMaxMilliseconds);
        }

        //TODO:Realese Spawning
        private void SpawnEnemy()
        {
            SpawnPlace place = RandomUtils.GetRandomEnemySpawnPlace();
            Vector2 pos = GetSpawnPosition(place);
            if (IsAllowedSpawnPosition(pos))
            {
                spriteList.Add(new SmartTank(Default.GetEnemyTankSetting(Game, pos), Default.GetMissileSetting(Game)));
            }
        }

        public Vector2 GetSpawnPosition(SpawnPlace place)
        {
            switch (place)
            {
                case SpawnPlace.TopLeft:
                    return LeftEnemyPosition;
                case SpawnPlace.TopMiddle:
                    return MiddleEnemyPosition;
                case SpawnPlace.TopRight:
                    return RightEnemyPosition;
                case SpawnPlace.BottomLeft:
                    return LeftUserPosition;
                case SpawnPlace.BottomRight:
                    return RightUserPosition;
                default:
                    throw new PlaceNotFoundException();
            }

        }

        public bool IsAllowedSpawnPosition(Vector2 position)
        {
            Rectangle rect = new Rectangle((int)position.X, (int)position.Y, AnimatedSprites.GameSettings.Default.TankSetting.FrameSize.X, AnimatedSprites.GameSettings.Default.TankSetting.FrameSize.Y);
            foreach (var item in spriteList)
            {
                if (item.collisionRect.Intersects(rect))
                {
                    return false;
                }
            }
            return true;
        }

    }

    class PlaceNotFoundException : Exception
    {
    }
}
