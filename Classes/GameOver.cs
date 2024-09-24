using AirShooter.Classes.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AirShooter;



namespace AirShooter.Classes
{
    internal class GameOver
    {
        public Label label;
        public Label lblMaxScore;

        public GameOver(int width, int height)
        {
            label = new Label("GAME OVER!!!", new Vector2(width / 2 - 45, height / 2), Color.White); 
            lblMaxScore = new Label("Record: ", new Vector2(width / 2 - 45, height / 2 + 60), Color.White);
        }
        public void AddScores(string playerScore, string totalScore)
        {
            if (playerScore is null)
            {
                throw new ArgumentNullException(nameof(playerScore));
            }

            lblMaxScore.Text += totalScore;
        }
        public void Restart()
        {
            lblMaxScore.Text = "Record: ";
        }
        public void LoadContent(ContentManager contentManager)
        {
            label.LoadContent(contentManager);
            lblMaxScore.LoadContent(contentManager);
        }
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Game1.gameMode = GameMode.Menu;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            label.Draw(spriteBatch);
            lblMaxScore.Draw(spriteBatch);
        }
    }
}
