using System;
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
        public bool canMove = true;
        private bool direction;
        Point currentSprite;

        public void Initialize()
        { 
            direction = true;
            currentSprite = new Point(0, 0);
        }
        public override void Update(GameTime gt)
        {
            if (canMove)
            {
                KeyboardState kstate = Keyboard.GetState();
                if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0)
                {
                    velocity.Y = -speed;
                }
                else if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
                {
                    velocity.Y = speed;
                }

                if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
                {
                    velocity.X = - speed;
                }
                else if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
                {
                    velocity.X = speed;
                }

                pos += velocity;
                velocity = Vector2.Zero;

            }
          
        }
    }
}