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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace AnimatedSprites
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont scoreFont;
        SpriteManager spriteManager;
        //Random staff
        public Random rnd { get; private set;}
        //XACT stuff
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue trackCue;
        //background
        Texture2D backgroundTexture;
        //Game status staff
        public void EndGame()
        {
            currentGameState = GameState.GameOver;
        }

        enum GameState { Start, InGame, GameOver };
        GameState _currentGameState = GameState.Start;
        GameState currentGameState
        {
            get { return _currentGameState; }
            set
            {
                if (value == GameState.GameOver)
                {
                    spriteManager.Enabled = false;
                    spriteManager.Visible = false;
                }
                _currentGameState = value;
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            rnd = new Random();
            graphics.IsFullScreen = false;
        }

        protected override void Initialize()
        {
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);
            spriteManager.Visible = false;
            spriteManager.Enabled = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //load font
            scoreFont = Content.Load<SpriteFont>(@"fonts\score");
            // Load the XACT data
            audioEngine = new AudioEngine(@"Content\Audio\GameAudio.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Audio\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Audio\Sound Bank.xsb");
            backgroundTexture = Content.Load<Texture2D>(@"Images\background");
            // Start the soundtrack audio
            trackCue = soundBank.GetCue("track");
            trackCue.Play();

            // Play the start sound
            soundBank.PlayCue("start");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.Start:
                    if (Keyboard.GetState().GetPressedKeys().Length > 0)
                    {
                        currentGameState = GameState.InGame;
                        spriteManager.Enabled = true;
                        spriteManager.Visible = true;
                    }
                    break;
                case GameState.InGame:
                    break;
                case GameState.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        this.Exit();
                    break;
            }
            // Update audio engine 
            audioEngine.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.Start:
                        GraphicsDevice.Clear(Color.AliceBlue);

                        // Вывод в заставке текста
                        spriteBatch.Begin();
                        string text = "Kill tanks!";
                        spriteBatch.DrawString(scoreFont, text,
                            new Vector2((Window.ClientBounds.Width / 2)
                            - (scoreFont.MeasureString(text).X / 2),
                            (Window.ClientBounds.Height / 2)
                            - (scoreFont.MeasureString(text).Y / 2)),
                            Color.SaddleBrown);

                        text = "(Press any key to start game)";
                        spriteBatch.DrawString(scoreFont, text,
                            new Vector2((Window.ClientBounds.Width / 2)
                            - (scoreFont.MeasureString(text).X / 2),
                            (Window.ClientBounds.Height / 2)
                            - (scoreFont.MeasureString(text).Y / 2) + 30),
                            Color.SaddleBrown);

                        spriteBatch.End();
                    break;
                case GameState.InGame: 
                    GraphicsDevice.Clear(Color.Black);
                    base.Draw(gameTime);
                    break;
                case GameState.GameOver:
                    GraphicsDevice.Clear(Color.AliceBlue);
                    spriteBatch.Begin();
                    string textEnd = "Game Over! AI Win!\nPress ENTER to exit";
                    spriteBatch.DrawString(scoreFont, textEnd,
                        new Vector2((Window.ClientBounds.Width / 2)
                        - (scoreFont.MeasureString(textEnd).X / 2),
                        (Window.ClientBounds.Height / 2)
                        - (scoreFont.MeasureString(textEnd).Y / 2)),
                        Color.SaddleBrown);
                    spriteBatch.End();
                    break;
            }

        }


        public void PlayCue(string cueName)
        {
            soundBank.PlayCue(cueName);
        }
    }
}