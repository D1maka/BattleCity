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
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //SpriteBatch for drawing
        SpriteBatch spriteBatch;
        //spawn stuff
        int enemySpawnMinMilliseconds = 1000;
        int enemySpawnMaxMilliseconds = 2000;
        //A sprite for the player and a list of automated sprites
        List<Sprite> spriteList = new List<Sprite>();
        Vector2 MiddleEnemyPosition { get; set; }
        Vector2 LeftEnemyPosition { get; set; }
        Vector2 RightEnemyPosition { get; set; }

        public SpriteManager(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            ResetSpawnTime();
            LeftEnemyPosition = new Vector2(AnimatedSprites.GameSettings.Default.WallSetting.FrameSize.X * SpriteSettings.Scale + 10, AnimatedSprites.GameSettings.Default.WallSetting.FrameSize.Y * SpriteSettings.Scale + 10);
            RightEnemyPosition = new Vector2(Game.Window.ClientBounds.Width - AnimatedSprites.GameSettings.Default.WallSetting.FrameSize.X * SpriteSettings.Scale - AnimatedSprites.GameSettings.Default.TankSetting.FrameSize.X * SpriteSettings.Scale - 10, AnimatedSprites.GameSettings.Default.WallSetting.FrameSize.Y * SpriteSettings.Scale + 10);
            MiddleEnemyPosition = new Vector2((Game.Window.ClientBounds.Width - AnimatedSprites.GameSettings.Default.TankSetting.FrameSize.X * SpriteSettings.Scale) / 2 + 10, AnimatedSprites.GameSettings.Default.WallSetting.FrameSize.Y * SpriteSettings.Scale + 10);
            RandomUtils.Game = Game;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            spriteList.Add(new UserControlledTank(Default.GetUserTankSetting(Game), Default.GetMissileSetting(Game)));
            for (int i = 0; i < Game.Window.ClientBounds.Width; i += (int)(AnimatedSprites.GameSettings.Default.WallSetting.FrameSize.X * SpriteSettings.Scale))
            {
                for (int j = 0; j < Game.Window.ClientBounds.Height; j += (int)(Game.Window.ClientBounds.Height - AnimatedSprites.GameSettings.Default.WallSetting.FrameSize.Y * SpriteSettings.Scale))
                    spriteList.Add(new IndestructibleWall(Default.GetWallSetting(Game, new Vector2(i, j))));
            }

            for (int i = 0; i < Game.Window.ClientBounds.Width; i += (int)(Game.Window.ClientBounds.Width - AnimatedSprites.GameSettings.Default.WallSetting.FrameSize.X * SpriteSettings.Scale))
            {
                for (int j = AnimatedSprites.GameSettings.Default.WallSetting.FrameSize.X; j < Game.Window.ClientBounds.Height; j += (int)(AnimatedSprites.GameSettings.Default.WallSetting.FrameSize.X * SpriteSettings.Scale))
                    spriteList.Add(new IndestructibleWall(Default.GetWallSetting(Game, new Vector2(i, j))));
            }

            spriteList.Add(new RandomMovedTank(Default.GetEnemyTankSetting(Game, LeftEnemyPosition), Default.GetMissileSetting(Game)));
            spriteList.Add(new RandomMovedTank(Default.GetEnemyTankSetting(Game, RightEnemyPosition), Default.GetMissileSetting(Game)));

            List<Vector2> walls = Default.GetWallPosition();
            foreach (Vector2 pos in walls)
                spriteList.Add(new Wall(Default.GetWallSetting(Game,pos)));

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
                ((Game1)Game).EndGame();

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
                            if (s.collisionRect.Intersects(spriteList[j].collisionRect))
                                Collisions.ReleaseCollision(s, spriteList[j]);
                        }
                    }

                    if (s is Tank)
                    {
                        Missile m = (s as Tank).CurrentMissle;
                        if (m != null && m.State == SpriteState.Alive && !spriteList.Contains(m) && !spawnedSprites.Contains(m))
                            spawnedSprites.Add(m);
                    }

                    if (s.IsOutOfBounds(Game.Window.ClientBounds))
                    {
                        s.State = SpriteState.Destroyed;
                    }
                    // Удаляем объект, если он вне поля
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
            //throw new NotImplementedException();
        }
    }
}
