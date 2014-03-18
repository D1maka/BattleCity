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
        SpriteFont scoreBoldFont;
        SpriteManager spriteManager;
        //Random staff
        public Random rnd { get; private set; }
        //XACT stuff
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue trackCue;
        //background
        Texture2D cursorTexture;
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //load font
            scoreFont = Content.Load<SpriteFont>(@"fonts\score");
            scoreBoldFont = Content.Load<SpriteFont>(@"fonts\scoreBold");
            // Load the XACT data
            audioEngine = new AudioEngine(@"Content\Audio\GameAudio.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Audio\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Audio\Sound Bank.xsb");
            cursorTexture = Content.Load<Texture2D>(@"Images\background");
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

        int selectedIndex = 0;
        const int GCD = 300;
        int CurrentGCD = 0;
        string[] texts = new string[] { "1 player", "2 players" };
        protected override void Update(GameTime gameTime)
        {
            switch (currentGameState)
            {

                case GameState.Start:
                    CurrentGCD -= gameTime.ElapsedGameTime.Milliseconds;
                    if (CurrentGCD < 0)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            CurrentGCD = GCD;
                            selectedIndex--;
                            if (selectedIndex < 0)
                                selectedIndex = texts.Count() - 1;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        {
                            CurrentGCD = GCD;
                            selectedIndex++;
                            if (selectedIndex > texts.Count() - 1)
                                selectedIndex = 0;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            spriteManager = new SpriteManager(this, selectedIndex == 1);
                            Components.Add(spriteManager);
                            currentGameState = GameState.InGame;
                        }
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

                    for (int i = 0; i < texts.Count(); i++)
                    {
                        spriteBatch.DrawString(selectedIndex == i ? scoreBoldFont : scoreFont, texts[i],
                               new Vector2((Window.ClientBounds.Width / 2) - (scoreFont.MeasureString(texts[i]).X / 2),
                               (Window.ClientBounds.Height / 2) - (scoreFont.MeasureString(texts[i]).Y / 2) + 30 * i),
                               Color.SaddleBrown);
                    }

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