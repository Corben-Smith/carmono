using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace carmono
{
    public class Car
    {
        public Vector2 position;
        private float speed;
        public bool on;
        public Rectangle bounds;

        public Texture2D texture;

        public Car()
        {
            position = new Vector2(0,0);
            speed = 5f;
            on = true;
            texture = null;
        }

        public Car(float speed)
        {
            position = new Vector2(0, 0);
            this.speed = speed;
            on = true;
        }

        public Car(float posX, float posY)
        {
            position = new Vector2(posX, posY);
            speed = 5f;
            on = true;
        }

        public Car(Car car)
        {
            position = new Vector2(car.position.X, car.position.Y);
            speed = car.speed;
            on = car.on;
            texture = car.texture;
        }

        public void Move(string direction)
        {
            switch (direction.ToLower())
            {
                case "right":
                    position.X += speed;
                    break;
                case "left":
                    position.X -= speed;
                    break;
                case "up":
                    position.Y -= speed;
                    break;
                case "down":
                    position.Y += speed;
                    break;
                default:
                    throw new Exception("Invalid Direction");
            }
        }

        public void Power()
        {
            on = !on;
        }
    }
}
