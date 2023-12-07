using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace carmono
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;

        public Car car;
        public List<Enemy> enemies;

        public int counter;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            car = new Car();
            enemies = new List<Enemy>() {new Enemy(), new Enemy(), new Enemy(), new Enemy(), new Enemy()};

            counter = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            car.texture = Content.Load<Texture2D>("car");
            font = Content.Load<SpriteFont>("font");

            foreach (Enemy enemy in enemies)
            {
                enemy.texture = Content.Load<Texture2D>("enemy");   
            }
        }

        protected override void Update(GameTime gameTime)
        {

            foreach (Enemy enemy in enemies)
            {
                enemy.bounds = new Rectangle((int)enemy.position.X, (int)enemy.position.Y, enemy.texture.Width, enemy.texture.Height);
            }
            car.bounds = new Rectangle((int)car.position.X, (int)car.position.Y, car.texture.Width, car.texture.Height);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                car.Move("Right");
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                car.Move("Left");
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                car.Move("up");
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                car.Move("Down");
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                car.Power();
            }

            foreach (var enemy in enemies)
            {

                if (enemy.position.X < -20)
                {
                    enemy.Reset();
                }
                else
                {
                    enemy.Move();
                }

                if (car.bounds.Intersects(enemy.bounds))
                {
                    car.position = new Vector2(0, 300);
                    counter++;
                }
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(car.texture, new Vector2(car.position.X, car.position.Y),  Color.White);

            foreach (var enemy in enemies)
            {
                _spriteBatch.Draw(enemy.texture, new Vector2(enemy.position.X, enemy.position.Y), Color.White);
            }

            _spriteBatch.DrawString(font, "Deaths: " + counter, new Vector2(10, 10), Color.Black);

            _spriteBatch.End();


            base.Draw(gameTime);

        }
    }
}