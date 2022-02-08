using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace UTAS
{
    public class MainGame : Game
    {
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Song homeTheme;
        private Song deathTheme;

        Texture2D home;

        // private Player asriel;
        private Vector2 velocity = new Vector2(0, 0);
        private Vector2 pos;
        public Vector2 playerPosition;
        private float speed = 4;
        public float health = 20;
        public Texture2D playerSpritesheet;
        public Rectangle[,] playerSpriteCuts;
        public bool canMove = true;
        private bool notIdle = false;
        public bool direction;
        public Point currentSprite;
        int priorX;
        double lastAnimate;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // https://en.wikipedia.org/wiki/List_of_common_resolutions
        protected override void Initialize()
        {
            Window.Title = "Undertale: Asriel's Story";
            this.Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferHeight = 768;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.ApplyChanges();
            //    asriel = new Player("Sprites\\Characters\\Asriel\\spr_asrielkid_idle");
            base.Initialize();
            //      asriel.Initialize(_graphics);
            direction = true;
            currentSprite = new Point(0, 0);
            pos = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 9, _graphics.PreferredBackBufferHeight / 2 - 14);
            playerPosition = new Vector2(_graphics.PreferredBackBufferWidth/2 - 9, _graphics.PreferredBackBufferHeight/2 - 14);
            playerSpriteCuts = new Rectangle[,] { 
                {
                    new Rectangle(0, 0, 18, 28), 
                    new Rectangle(18, 0, 18, 28), 
                    new Rectangle( 36, 0, 18, 28), 
                    new Rectangle( 54, 0, 18, 28) 
                }, {
                    new Rectangle(0, 28, 18, 28), 
                    new Rectangle(18, 28, 18, 28),
                    new Rectangle(-1, -1, 0, 0),
                    new Rectangle(-1, -1, 0, 0)
                }, {
                    new Rectangle(0, 56, 18, 28), 
                    new Rectangle(18, 56, 18, 28),
                    new Rectangle(-1, -1, 0, 0),
                    new Rectangle(-1, -1, 0, 0)
                },{
                    new Rectangle(0, 84, 18, 28), 
                    new Rectangle(18,  84, 18, 28), 
                    new Rectangle( 36, 84, 18, 28), 
                    new Rectangle( 54, 84,  18, 28)
                },
            };

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            home = Content.Load<Texture2D>("Rooms\\New Home\\bg_HomeMainEnt");
            // asriel.LoadContent(Content);
            playerSpritesheet = Content.Load<Texture2D>("Sprites\\Characters\\Asriel\\spr_asrielkid");
            homeTheme = Content.Load<Song>("Music/mus_home_remix");
            deathTheme = Content.Load<Song>("Music/mus_death");
            try
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(homeTheme);
            }
            catch
            {
                // empty because for whatever reason it is required.
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }


            KeyboardState kstate = Keyboard.GetState();

           // if (canMove)
         //   {
         notIdle = false;
         if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W) ||
             GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0)
                {
                    velocity.Y = -speed;
                    currentSprite.Y = 0;
                    notIdle = true;
                }
                else if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S) ||
                         GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
                {
                    velocity.Y = speed;
                    currentSprite.Y = 0;
                    notIdle = true;
                }

                if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A) ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
                {
                    velocity.X = - speed;
                    currentSprite.X = 0;
                    direction = true;
                    notIdle = true;
                }
                else if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D) ||
                         GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
                {
                    velocity.X = speed;
                    currentSprite.X = 0;
                    direction = false;
                    notIdle = true;
                }

                if (!notIdle)
                {
                    currentSprite.X = 1;
                }

                if (priorX != currentSprite.X)
                {
                    currentSprite.Y = 0;
                    lastAnimate = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (gameTime.TotalGameTime.TotalMilliseconds - lastAnimate > 60)
                {
                    if (currentSprite.Y < 3 && playerSpriteCuts[currentSprite.X, currentSprite.Y].X != -1)
                        currentSprite.Y += 1;
                    else
                        currentSprite.Y = 0;
                    if (currentSprite.X == 2 && currentSprite.Y == 3)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.E) ||
                            GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
                        {
                            currentSprite.Y = 0;
                            currentSprite.X = 3;
                        }
                        else
                        {
                            currentSprite.Y = 0;
                            currentSprite.X = 3;
                         //   canMove = false;
                        }
                    }

                    if (currentSprite.X == 3 && currentSprite.Y == 3)
                    {
                        currentSprite.Y = 0;
                        currentSprite.X = 3;
                      //  canMove = false;
                    }
                    lastAnimate = gameTime.TotalGameTime.TotalMilliseconds;
                }

                priorX = currentSprite.X;
                pos += velocity;
                velocity = Vector2.Zero;
          //  }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(home, new Rectangle(_graphics.PreferredBackBufferWidth / 4,
                _graphics.PreferredBackBufferHeight / 4,
                home.Width * 2, home.Height * 2), Color.White);

            //   asriel.draw(_spriteBatch);
           // _spriteBatch.Draw(playerSpritesheet, );
            _spriteBatch.Draw(playerSpritesheet, pos, playerSpriteCuts[currentSprite.X, currentSprite.Y], Color.White, 
                0f, new Vector2(0), 2, SpriteEffects.None, 1);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}