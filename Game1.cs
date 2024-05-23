using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Topic_5___Enumerations
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        enum Screen
        {
            Intro,
            Animation,
            Outro
        }
        Screen screen;
        MouseState mouseState, prevMouseState;
        Song atmosphere;
        Rectangle screens;
        Rectangle car;
        Texture2D outroTexture;
        SpriteFont text;
        Texture2D coolCar;
        Texture2D dvd;
        Rectangle dvdRect;
        Vector2 dvdSpeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            screens = new Rectangle(0, 0, 900, 500);
            car = new Rectangle(100, 100, 500, 350);
            dvdRect = new Rectangle(100, 100, 100, 100);
            dvdSpeed = new Vector2(3, 1);
            // TODO: Add your initialization logic here
            screen = Screen.Intro;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            atmosphere = Content.Load<Song>("They All Get Mad At You");
            outroTexture = Content.Load<Texture2D>("Outro");
            text = Content.Load<SpriteFont>("Text");
            coolCar = Content.Load<Texture2D>("Cool Car");
            dvd = Content.Load<Texture2D>("DVD");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    screen = Screen.Animation;
            }

            else if (screen == Screen.Animation)
            {
                MediaPlayer.Play(atmosphere);
                dvdRect.X += (int)dvdSpeed.X;
                dvdRect.Y += (int)dvdSpeed.Y;

                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    screen = Screen.Outro;

                if (dvdRect.Right >= 900 || dvdRect.Left <= 0)
                {
                    dvdSpeed.X *= -1;
                }
                if (dvdRect.Bottom >= 500 || dvdRect.Top <= 0)
                {
                    dvdSpeed.Y *= -1;
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);
            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(coolCar, car, Color.White);
                _spriteBatch.DrawString(text, ("Left click to continue (Welcome)"), new Vector2(150, 40), Color.White);
            }

            else if (screen == Screen.Animation)
            {
                _spriteBatch.Draw(dvd, dvdRect, Color.White);
                _spriteBatch.DrawString(text,("Left click to continue"), new Vector2(0, 400), Color.White);
            }

            else if (screen == Screen.Outro)
            {
                _spriteBatch.Draw(outroTexture, screens, Color.White);
            }

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}