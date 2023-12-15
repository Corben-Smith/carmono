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
    public class Car : GameObject
    {
        public static Vector2 startPosition = new Vector2(0, 0);


        public Car() : base()
        {
            position = new Vector2(0,0);
            speed = 5f;
            texture = null;
        }

        public Car(float speed) : base()
        {
            position = new Vector2(0, 0);
            this.speed = speed;
        }

        public Car(float posX, float posY) : base()
        {
            position = new Vector2(posX, posY);
            speed = 5f;
        }

        public Car(Car car) : base()
        {
            position = new Vector2(car.position.X, car.position.Y);
            speed = car.speed;
            texture = car.texture;
        }

        public void Move(string direction, int height, int width)
        {
            if (direction.ToLower() == "right")
            {
                if(position.X + speed < width - 20) 
                {
                    position.X += speed;
                }
            }
            else if (direction.ToLower() == "left")
            {
                if (position.X - speed > 0)
                {
                    position.X -= speed;
                }
            }
            else if (direction.ToLower() == "up")
            {
                if (position.Y + speed > 0)
                {
                    position.Y -= speed;
                }
            }
            else if (direction.ToLower() == "down")
            {
                if (position.Y - speed < height - 40)
                {
                    position.Y += speed;
                }
            }

        }

        public void Die()
        {
            position = new Vector2(50, 50);
        }
    }
}
