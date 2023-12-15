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
    public class GameObject
    {
        public Random random = new Random();

        public Vector2 position;
        public float speed;
        public Rectangle bounds;
        public Texture2D texture;

        public GameObject()
        {
            position = new Vector2(0,0);
            speed = 5f;
            texture = null;
        }

        public GameObject(float speed)
        {
            position = new Vector2(0, 0);
            this.speed = speed;
        }

        public GameObject(float posX, float posY)
        {
            position = new Vector2(posX, posY);
            speed = 5f;
        }

        public GameObject(GameObject car)
        {
            position = new Vector2(car.position.X, car.position.Y);
            speed = car.speed;
            texture = car.texture;
        }

        public virtual void Move()
        {
            position.X -= speed;
        }

        public void Reset()
        {
            position = new Vector2(800, random.Next(10, 500));
        }
    }
}
