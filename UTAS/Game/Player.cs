using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
// https://www.dreamincode.net/forums/topic/194878-xna-animated-sprite/

namespace UTAS
{
    class Player : Sprite
    {
        private Vector2 velocity = new Vector2(0, 0);
        private float speed = 4;
        public Player(string texSource) : base(texSource){}


        public override void Update(GameTime gt)
        {
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up))
            {
                velocity.Y = -speed;
            }
            else if (kstate.IsKeyDown(Keys.Down))
            {
                velocity.Y = speed;
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                velocity.X = - speed;
            }
            else if (kstate.IsKeyDown(Keys.Right))
            {
                velocity.X = speed;
            }

            pos += velocity;
            velocity = Vector2.Zero;
        
        }
    }
}