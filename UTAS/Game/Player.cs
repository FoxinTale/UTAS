using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// https://www.dreamincode.net/forums/topic/194878-xna-animated-sprite/

namespace UTAS
{
    class Player : Sprite
    {
        private Vector2 velocity = new Vector2(0, 0);
        public Vector2 playerPosition;
        private float speed = 4;
        public float health = 20;
        public Texture2D playerSpritesheet;
        public Rectangle[,] playerSpriteCuts;
        public Player(string texSource) : base(texSource){}
        public bool canMove = true;
        private bool notIdle = true;
        public bool direction;
        public Point currentSprite;
        int priorX;
        double lastAnimate;

        public void Initialize(GraphicsDeviceManager _graphics)
        { 
            direction = true;
            currentSprite = new Point(0, 0);
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
        public override void Update(GameTime gt)
        {
            KeyboardState kstate = Keyboard.GetState();

            if (canMove)
            {
                if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0)
                {
                   // velocity.Y = -speed;
                   currentSprite.X = 0;
                  // cameraPosition.Y += 2.5f;
                   notIdle = true;
                }
                else if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
                {
                   // velocity.Y = speed;
                   currentSprite.X = 0;
               //    cameraPosition.Y -= 2.5f;
                   notIdle = true;
                }

                if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
                {
                  //  velocity.X = - speed;
                  currentSprite.X = 0;
                  // cameraPosition.X += 2.5f;
                  direction = true;
                  notIdle = true;
                }
                else if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
                {
                   // velocity.X = speed;
                   currentSprite.X = 0;
                 //  cameraPosition.X -= 2.5f;
                   direction = false;
                   notIdle = true;
                }
                if (!notIdle)
                {
                    currentSprite.X = 4;
                }
                if (priorX != currentSprite.X)
                {
                    currentSprite.Y = 0;
                    lastAnimate = gt.TotalGameTime.TotalMilliseconds;
                }
                else if (gt.TotalGameTime.TotalMilliseconds - lastAnimate > 60)
                {
                    if (currentSprite.Y < 9 && playerSpriteCuts[currentSprite.X, currentSprite.Y].X != -1)
                        currentSprite.Y += 1;
                    else
                        currentSprite.Y = 0;
                    if (currentSprite.X == 2 && currentSprite.Y == 4)
                    {

                        if (Keyboard.GetState().IsKeyDown(Keys.E) || GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
                        {
                            currentSprite.Y = 0;
                            currentSprite.X = 3;
                        }
                        else
                        {
                            currentSprite.Y = 0;
                            currentSprite.X = 4;
                            canMove = false;
                        }
                    }
                    if (currentSprite.X == 3 && currentSprite.Y == 5)
                    {
                        currentSprite.Y = 0;
                        currentSprite.X = 4;
                        canMove = false;
                    }
                    lastAnimate = gt.TotalGameTime.TotalMilliseconds;
                }

                priorX = currentSprite.X;

               pos += velocity;
               // velocity = Vector2.Zero;

            }

            if (kstate.IsKeyDown(Keys.L))
            {
                health = 0;
            }
        }
        
    }
}