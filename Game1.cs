using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace carmono
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteBatch _endSpriteBatch;
        private SpriteFont font;

        public bool end = false;

        public int windowHeight;
        public int windowWidth;

        public Texture2D roadTexture;
        public Car car;
        public List<Enemy> enemies;
        public List<Coin> coins;


        public int counter;
        public int pointCounter;
        public float currentTime;

        Vector2 roadPosition = Vector2.Zero;
        Vector2 road2Position;

        float deathTimer = 10;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            car = new Car(8f);
            enemies = new List<Enemy>() {new Enemy(), new Enemy(), new Enemy(), new Enemy(), new Enemy()};
            coins = new List<Coin>() { new Coin(), new Coin(), new Coin()};


            windowHeight = _graphics.PreferredBackBufferHeight; // Get window height
            windowWidth = _graphics.PreferredBackBufferWidth; // Get window width

            counter = 0;
            pointCounter = 0;

            road2Position = new Vector2(-windowWidth, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _endSpriteBatch = new SpriteBatch(GraphicsDevice);
            car.texture = Content.Load<Texture2D>("car1");
            font = Content.Load<SpriteFont>("font");

            roadTexture = Content.Load<Texture2D>("road");

            foreach (Enemy enemy in enemies)
            {
                enemy.texture = Content.Load<Texture2D>("enemy1");   
            }

            foreach (Coin coin in coins)
            {
                coin.texture = Content.Load<Texture2D>("coin");
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (!end)
            {
                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                foreach (Enemy enemy in enemies)
                {
                    enemy.bounds = new Rectangle((int)enemy.position.X, (int)enemy.position.Y, enemy.texture.Width, enemy.texture.Height);
                }

                foreach (Coin coin in coins)
                {
                    coin.bounds = new Rectangle((int)coin.position.X, (int)coin.position.Y, coin.texture.Width, coin.texture.Height);
                }

                car.bounds = new Rectangle((int)car.position.X, (int)car.position.Y, car.texture.Width, car.texture.Height);

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    car.Move("Right", windowHeight, windowWidth);
                }
                else if (keyboardState.IsKeyDown(Keys.Left))
                {
                    car.Move("Left", windowHeight, windowWidth);
                }
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    car.Move("up", windowHeight, windowWidth);
                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    car.Move("Down", windowHeight, windowWidth);
                }


                foreach (var enemy in enemies)
                {

                    if (enemy.position.X < -20)
                    {
                        enemy.Reset();

                    }
                    else
                    {
                        enemy.SpeedUpdate(currentTime);
                        enemy.Move();
                    }

                    if (car.bounds.Intersects(enemy.bounds) && deathTimer > 1)
                    {
                        deathTimer = 0;
                        car.Die();
                        counter++;
                        if (counter >= 3)
                        {
                            end = true;
                        }
                    }
                }

                foreach (var coin in coins)
                {

                    if (coin.position.X < -20)
                    {
                        coin.Reset();
                    }
                    else
                    {
                        coin.Move();
                    }

                    if (car.bounds.Intersects(coin.bounds) && deathTimer > 1)
                    {
                        coin.Reset();
                        pointCounter += 500;
                    }
                }

                roadPosition.X += 10;
                road2Position.X += 10;


                if (roadPosition.X >= windowWidth)
                {
                    roadPosition.X = -windowWidth;
                }
                if (road2Position.X >= windowWidth)
                {
                    road2Position.X = -windowWidth;
                }

                pointCounter += (int)gameTime.ElapsedGameTime.Seconds * 10;


            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (end) { 
                _endSpriteBatch.Begin();
                GraphicsDevice.Clear(Color.IndianRed);

                Vector2 textSize = font.MeasureString("GAMEOVER");
                Vector2 position = new Vector2(windowWidth / 2, windowHeight / 2) - new Vector2(textSize.X / 2, textSize.Y / 2);
                _endSpriteBatch.DrawString(font, "GAMEOVER", position, Color.Black);

                textSize = font.MeasureString("Points: " + pointCounter);
                position = new Vector2(windowWidth / 2, (windowHeight/2) + 30) - new Vector2(textSize.X / 2, textSize.Y / 2);
                _endSpriteBatch.DrawString(font, "Points: " + pointCounter, position, Color.Black);

                _endSpriteBatch.End();
            }
            else
            {
                _spriteBatch.Begin();

                if (deathTimer < 1)
                {
                    _spriteBatch.Draw(car.texture, new Vector2(car.position.X, car.position.Y), Color.HotPink);
                }
                else
                {
                    _spriteBatch.Draw(car.texture, new Vector2(car.position.X, car.position.Y), Color.White);
                }

                foreach (var enemy in enemies)
                {
                    _spriteBatch.Draw(enemy.texture, new Vector2(enemy.position.X, enemy.position.Y), Color.White);
                }

                foreach (var coin in coins)
                {
                    _spriteBatch.Draw(coin.texture, new Rectangle((int)coin.position.X, (int)coin.position.Y, 30, 30), Color.White);
                }


                _spriteBatch.DrawString(font, "Deaths: " + counter, new Vector2(10, 10), Color.Black);

                _spriteBatch.DrawString(font, "Points: " + pointCounter, new Vector2(200, 10), Color.Black);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}