using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace UTAS
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D home;
        private Player asriel;
   
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
            
            _graphics.PreferredBackBufferHeight = 768; // 768
            _graphics.PreferredBackBufferWidth = 1280; // 1280
            _graphics.ApplyChanges();
            asriel = new Player("Sprites\\Characters\\Asriel\\spr_asrielkid_idle");
            base.Initialize();
            asriel.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

           home =  Content.Load<Texture2D>("Rooms\\New Home\\bg_HomeMainEnt");
            asriel.LoadContent(Content);

            try
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(Content.Load<Song>("Music/mus_home_remix"));
            } catch {}

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }


            asriel.Update(gameTime);

        //    InputHandler(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(home, new Rectangle(_graphics.PreferredBackBufferWidth / 4, _graphics.PreferredBackBufferHeight /  4, 
                home.Width * 2, home.Height * 2), Color.White); 
            
            asriel.draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}