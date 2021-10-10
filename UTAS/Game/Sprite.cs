using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace UTAS
{
    public class Sprite
    {
        protected Texture2D tex;

        protected string texSource;
        protected Vector2 pos = new Vector2(0, 0);

        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }


        public Sprite(string texsource)
        {
            this.texSource = texsource;
        }


        public virtual void LoadContent(ContentManager content)
        {
            tex = content.Load<Texture2D>(texSource);
        }


        public virtual void Update(GameTime gt)
        {
            
        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(tex, new Rectangle((int) pos.X, (int)pos.Y, tex.Width * 2, tex.Height * 2), Color.White);
        }
    }
}