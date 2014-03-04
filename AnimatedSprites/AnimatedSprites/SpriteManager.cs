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
        UserControlledTank player;
        List<Sprite> spriteList = new List<Sprite>();


        public SpriteManager(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            ResetSpawnTime();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            player = new UserControlledTank(Default.GetUserTankSetting(Game), Default.GetMissileSetting(Game));
            spriteList.Add(new Wall(Default.GetWallSetting(Game)));
            //TODO: Load the player sprite
            //throw new NotImplementedException();
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Update player
            player.Update(gameTime, Game.Window.ClientBounds);
            //Spawning
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
            List<Sprite> spawnedSprites = new List<Sprite>();
            if (spriteList.Count > 0)
            {
                for (int i = 0; i < spriteList.Count; ++i)
                {
                    Sprite s = spriteList[i];

                    s.Update(gameTime, Game.Window.ClientBounds);

                    if (spriteList.Count > i + 1)
                    {
                        for (int j = i + 1; i < spriteList.Count; j++)
                        {
                            if (s.collisionRect.Intersects(spriteList[j].collisionRect))
                                Collisions.ReleaseCollision(s, spriteList[j]);
                        }
                    }


                    // Удаляем объект, если он вне поля
                    if (s.IsOutOfBounds(Game.Window.ClientBounds))
                    {
                        spriteList.RemoveAt(i);
                        --i;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            // Draw the player
            player.Draw(gameTime, spriteBatch);

            // Draw all sprites
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

        public Vector2 GetPlayerPosition()
        {
            return player.GetPosition;
        }
    }
}
