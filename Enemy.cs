using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace carmono
{
    public class Enemy
    {
        private Random random = new Random();
        
        public Vector2 position;
        private float speed;

        public Rectangle bounds;
        public Texture2D texture;
        
        public Enemy()
        {
            speed = random.Next(8, 15);
            position = new Vector2(800, random.Next(10, 500));
        }

        public void Move()
        {
            position.X -= speed;
        }

        public void Reset() { 
            position = new Vector2(800,random.Next(10, 500));
        }
    }
}
