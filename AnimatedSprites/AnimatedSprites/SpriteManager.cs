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
        public bool isTwoPLayer;
        //SpriteBatch for drawing
        SpriteBatch spriteBatch;
        //spawn stuff
        int enemySpawnMinMilliseconds = 500;
        int enemySpawnMaxMilliseconds = 1000;
        //A sprite for the player and a list of automated sprites
        List<Sprite> tanks = new List<Sprite>();
        Vector2 MiddleEnemyPosition { get; set; }
        Vector2 LeftEnemyPosition { get; set; }
        Vector2 RightEnemyPosition { get; set; }
        Vector2 LeftUserPosition { get; set; }
        Vector2 RightUserPosition { get; set; }

        public SpriteManager(Game game, bool IsTwoPlayer)
            : base(game)
        {
            this.isTwoPLayer = IsTwoPlayer;
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
            SpriteUtils.GameWindow = Game.Window.ClientBounds;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            UserArrowControlled user = new UserArrowControlled(Default.GetUserTankSetting(Game, RightUserPosition), Default.GetMissileSetting(Game));
            MapInfo.user = user;
            tanks.Add(user);
            if (isTwoPLayer)
                tanks.Add(new UserWASDControlled(Default.GetUserTankSetting(Game, LeftUserPosition), Default.GetMissileSetting(Game)));

            MapInfo.Init(SpriteUtils.GetDynamicMap(Game));

            //Configure Utils
            Collisions.Tanks = tanks;

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (tanks.Find(item => item is UserControlledTank) == null)
                ((Game1)Game).EndGame();

            UpdateSprites(gameTime);

            nextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
            int aiCoiunt = tanks.Count(item => item is AITank);
            if (nextSpawnTime < 0 && aiCoiunt < 3)
            {
                SpawnEnemy();
                // Сбрасываем таймер
                ResetSpawnTime();
            }
            base.Update(gameTime);
        }

        void UpdateSprites(GameTime gameTime)
        {
            if (tanks.Count > 0)
            {
                List<Sprite> spawnedSprites = new List<Sprite>();
                for (int i = 0; i < tanks.Count; ++i)
                {
                    Sprite s = tanks[i];

                    s.Update(gameTime);

                    if (s is Missile)
                    {
                        for (int j = 0; j < tanks.Count; j++)
                        {
                            if (i == j)
                                continue;

                            if (tanks[j].State == SpriteState.Alive &&
                                s.State == SpriteState.Alive &&
                                s.collisionRect.Intersects(tanks[j].collisionRect))
                            {
                                Collisions.ReleaseCollision(s, tanks[j]);
                                break;
                            }
                        }

                        CellInformation cell = MapInfo.Intersects(s.collisionRect);
                        if (cell != null)
                        {
                            cell.ClearOwner();
                            s.Destroy();
                        }

                    }

                    if (s is Tank)
                    {
                        Missile m = (s as Tank).CurrentMissle;
                        if (m != null && m.State == SpriteState.Alive && !tanks.Contains(m) && !spawnedSprites.Contains(m))
                            spawnedSprites.Add(m);
                    }

                    // Удаляем объект, если он вне поля
                    if (Game.Window.ClientBounds.X > 0 && s.IsOutOfBounds(Game.Window.ClientBounds))
                        s.State = SpriteState.Destroyed;

                    if (s.State == SpriteState.Destroyed)
                    {
                        tanks.RemoveAt(i);
                        --i;
                    }
                }

                tanks.AddRange(spawnedSprites);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            MapInfo.Draw(gameTime, spriteBatch);
            foreach (Sprite s in tanks)
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
                tanks.Add(new SmartTank
                    (Default.GetEnemyTankSetting(Game, pos),
                    Default.GetMissileSetting(Game)));
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
            Rectangle rec = new Rectangle((int)position.X, (int)position.Y, AnimatedSprites.GameSettings.Default.TankSetting.FrameSize.X, AnimatedSprites.GameSettings.Default.TankSetting.FrameSize.Y);
            bool result = MapInfo.Intersects(rec) == null;
            if (result)
            {
                for (int i = 0; i < tanks.Count; i++)
                {
                    if (tanks[i].collisionRect.Intersects(rec))
                        return false;
                }
            }

            return result;
        }

    }

    class PlaceNotFoundException : Exception
    {
    }
}
