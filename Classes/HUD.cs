﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AirShooter.Classes.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AirShooter.Classes
{
    internal class HUD
    {
        private HealthBar healthBar;
        private Label labelScore;
        private readonly Label labelScoreText;
        private ShieldBar shieldBar;
        public void LoadContent(ContentManager contentManager)
        {
            healthBar = new HealthBar(new Vector2(680, 560), 100, 10);
            labelScore = new Label("Score: 0", new Vector2(5, 0), Color.Red);
            shieldBar = new ShieldBar(new Vector2(680, 580), 100, 10);
            healthBar.LoadContent(contentManager);
            labelScore.LoadContent(contentManager);
            shieldBar.LoadContent(contentManager);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            healthBar.Draw(spriteBatch);
            labelScore.Draw(spriteBatch);
            shieldBar.Draw(spriteBatch);
        }
        public void OnPlayerTakeDamage()
        {
            healthBar.Width -= 10;
        }
        public void OnPlayerHealed()
        {
            healthBar.Width += 20;
        }
        public void OnShieldUsed(int percent)
        {
            shieldBar.Width = percent;
        }
        public void OnPlayerScoreChanged(int score)
        {
            labelScore.Text = "Score: " + score.ToString();
        }
        public void Reset()
        {
            healthBar.Width = 85;
            shieldBar.Width = 85;
            labelScore.Text = "Score: 0";
        }
    }
}
