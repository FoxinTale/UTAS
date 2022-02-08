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
        
        private Player player;

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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content.Load<Texture2D>("Sprites\\Characters\\Asriel\\spr_asrielkid"), 1, 18, 28);
            
            home = Content.Load<Texture2D>("Rooms\\New Home\\bg_HomeMainEnt");
            homeTheme = Content.Load<Song>("Music/mus_home_remix");
            deathTheme = Content.Load<Song>("Music/mus_death");
            
            player.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

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
            player.HandleSpriteMovement(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

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
            
            _spriteBatch.Draw(player.Texture, player.Position, player.SourceRect, Color.White, 0f, player.Origin, 2.0f, SpriteEffects.None, 0);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}