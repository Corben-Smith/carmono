﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace carmono
{
    public class Enemy : GameObject
    {           
        public Enemy() : base()
        {
            speed = random.Next(8, 15);
            position = new Vector2(800, random.Next(10, 500));
        }

        public void SpeedUpdate(float currentTime)
        {
            if (speed < 20)
            {
                speed += currentTime * random.NextSingle() * .005f;
            }
            else
            {
                speed -= currentTime * random.NextSingle() * .05f;
            }
        }
    }
}
