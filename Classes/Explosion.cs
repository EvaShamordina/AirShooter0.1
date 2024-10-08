﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShooter.Classes;

    internal class Explosion
    {
        private Texture2D texture;

        private Rectangle sourceRectangle;

        public int frameWidth;
        public int frameHeight;
        private int frameNumber; 

        private Vector2 position;
        public double duration; 
        private double totalTime;

        private bool isAlive;

        public bool isLoop = true;

        public int speed;

        public bool IsAlive
        {
            get { return isAlive; }
        }

        public Explosion(Vector2 position)
        {
            this.position = position;
            texture = null;
            duration = 0.05;
            frameNumber = 0;
            frameWidth = 117;
            frameHeight = 117;
            speed = 2;
            isAlive = true;
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("explosion");
        }
        public void Update(GameTime gameTime)
        {
            position.Y += speed;
            totalTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (totalTime > duration)
            {
                frameNumber++;

                totalTime = 0;
            }

            if (frameNumber == 17)
            {
                if (isLoop == true)
                {
                    frameNumber = 0;
                }
                isAlive = false;
            }

            sourceRectangle = new Rectangle(frameNumber * frameWidth, 0, frameWidth, frameHeight);

            Debug.WriteLine($"Time: {totalTime}                          ElapsedGameTime: {gameTime.ElapsedGameTime.TotalSeconds} ");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X - frameWidth / 2 + 22, position.Y), sourceRectangle, Color.White);

        }
    }

