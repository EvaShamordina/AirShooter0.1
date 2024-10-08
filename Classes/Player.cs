﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AirShooter.Classes
{
    internal class Player
    {
        private Vector2 position;
        private Texture2D texture;
        private float speed;

        private Rectangle collision;

        private List<Bullet> bulletList;

        private Shield shield;

        private int time;
        private int maxTime;

        private int health;
        private int score;

        public event Action TakeDamage;
        public event Action CollectHP;
        public event Action<int> ScoreUpdate;
        public event Action<int> ShieldUse;

        public int Health
        {
            get { return health; }
        }
        public Rectangle Collision
        {
            get { return collision; }
        }
        public List<Bullet> BulletList
        {
            get { return bulletList; }
        }
        public int Score
        {
            get { return score; }
        }
        public Player()
        {
            shield = new Shield();
            shield.Shielduse += UseShield;
            texture = null;
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("player");
            shield.LoadContent(manager);
            collision = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        public void Update(ContentManager manager)
        {
            time++;
            int width;
            int height;
            if (shield.IsActive)
            {
                width = shield.Texture.Width;
                height = shield.Texture.Height;
            }
            else
            {
                width = texture.Width;
                height = texture.Height;
            }
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += speed;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += speed;
            }

            if (time > maxTime)
            {
                Bullet b = new(5, 5, new Vector2(0, -1));
                b.LoadContent(manager);
                b.Position = new Vector2((int)position.X + width / 2 - b.Width / 2, (int)position.Y);
                bulletList.Add(b);
                time = 0;
            }
            

            for (int i = 0; i < bulletList.Count; i++)
            {
                if (bulletList[i].IsAlive == false)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }

            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
            }

            if (position.X + width > 800)
            {
                position.X = 800 - width;
            }

            if (position.Y + height > 600)
            {
                position.Y = 600 - height;
            }

            collision = new Rectangle((int)position.X, (int)position.Y, width, height);
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
            }
            shield.Update();
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            if (shield.IsActive == true)
            {
                _spriteBatch.Draw(shield.Texture, position, Color.White);
            }
            else
            {
                _spriteBatch.Draw(texture, position, Color.White);
            }


            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Draw(_spriteBatch);
            }
        }

        public void Reset(Vector2 startPos)
        {
            position = startPos;
            position.X -= texture.Width / 2;
            position.Y -= texture.Height / 2;
            speed = 10;
            bulletList = new List<Bullet>();
            time = 0;
            maxTime = 60;
            health = 10;
            score = 0;
        }

        public void Damage()
        {
            Damage(TakeDamage);
        }

        public void Damage(Action takeDamage)
        {
            if (shield.IsActive == true)
            {
                return;
            }
            health--;
            if (TakeDamage != null)
            {
                takeDamage();
            }
        }

        public Action GetCollectHP()
        {
            return CollectHP;
        }

        public void Heal(Action collectHP)
        {
            if (shield.IsActive == true || health >= 10)
            {
                return;
            }
            health += 2;
            if (CollectHP != null)
            {
                collectHP();
            }
        }

        public void AddScore()
        {
            AddScore(ScoreUpdate);
        }

        public void AddScore(Action<int> scoreUpdate)
        {
            score++;
            if (ScoreUpdate != null)
            {
                scoreUpdate(score);
            }
        }

        public void UseShield(int percent)
        {
            UseShield(percent, ShieldUse);
        }

        public void UseShield(int percent, Action<int> shieldUse)
        {
            if (ShieldUse != null)
            {
                shieldUse(percent);
            }
        }
    }
}
