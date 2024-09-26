using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using AirShooter.Classes;
using AirShooter.Classes.Components;
using System;
using SharpDX.XAudio2;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using SharpDX.Direct3D9;
using System.Text;


namespace AirShooter
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Space space;
        private List<Mine> mine;
        private List<Explosion> explosions;
        private int screenWidth;
        private int screenHeight;
        private int asteroidAmount;
        private StartMenu startMenu;
        private GameOver gameOver;
        private HUD hud;
        public static GameMode gameMode;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            screenWidth = 800;
            screenHeight = 600;
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            asteroidAmount = 10;
        }

        protected override void Initialize()
        {
            player = new Player();                             
            space = new Space();                              
            mine = new List<Mine>();     
            explosions = new List<Explosion>();             
                                                              
            startMenu = new StartMenu(screenWidth, screenHeight);
            gameOver = new GameOver(screenWidth, screenHeight);
            hud = new HUD();                                  
            player.TakeDamage += hud.OnPlayerTakeDamage;
            player.CollectHP += hud.OnPlayerHealed;
            player.ScoreUpdate += hud.OnPlayerScoreChanged;
            player.ShieldUse += hud.OnShieldUsed;
            base.Initialize();                      
        }

        private void Restart()
        {
            player.Reset(new Vector2(screenWidth / 2, screenHeight / 2));
            space.Reset();
            mine = new List<Mine>();
            explosions = new List<Explosion>();
            hud.Reset();
            gameOver.Restart();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            player.LoadContent(Content);
            space.LoadContent(Content);


            startMenu.LoadContent(Content);
            gameOver.LoadContent(Content);
            hud.LoadContent(Content);

            Restart();
        }

        protected override void Update(GameTime gameTime)
        {

            switch (gameMode)
            {
                case GameMode.Menu:
                    startMenu.Update();
                    break;
                
                case GameMode.PlaingPrapare:
                    Restart();
                    gameMode = GameMode.Playing;
                    break;
                case GameMode.Playing:
                    player.Update(Content);
                    space.Update();
                    UpdateAsteroid();
                    CheckCollision();
                    UpdateExplosion(gameTime);
                    UpdatehealBoosters();
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameMode = GameMode.Pause;
                    }
                    break;
                case GameMode.GameOver:
                    gameOver.Update();

                    break;
                case GameMode.Exit:
                    Exit();
                    break;
                default:
                    break;
            }
            base.Update(gameTime);
        }

        private void CheckCollision()
        {
            throw new NotImplementedException();
        }

        private void UpdateAsteroid()
        {
            throw new NotImplementedException();
        }

        private void UpdatehealBoosters()
        {
            throw new NotImplementedException();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            switch (gameMode)
            {
                case GameMode.Menu:
                    startMenu.Draw(_spriteBatch);
                    break;
                case GameMode.Pause:
                    space.Draw(_spriteBatch);

                    player.Draw(_spriteBatch);

                    foreach (Mine a in mine)
                    {
                        a.Draw(_spriteBatch);
                    }
                    foreach (Explosion exp in explosions)
                    {
                        exp.Draw(_spriteBatch);
                    }




                    break;
                case GameMode.Playing:
                    space.Draw(_spriteBatch);

                    player.Draw(_spriteBatch);

                    foreach (Mine a in mine)
                    {
                        a.Draw(_spriteBatch);
                    }
                    foreach (Explosion exp in explosions)
                    {
                        exp.Draw(_spriteBatch);
                    }
                    

                    hud.Draw(_spriteBatch);

                    break;
                case GameMode.GameOver:
                    gameOver.Draw(_spriteBatch);
                    break;
                case GameMode.Exit:
                    break;
                default:
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void LoadExplotion(Vector2 pos)
        {
            Explosion exp = new Explosion(pos);
            exp.LoadContent(Content);
            explosions.Add(exp);
        }

        private void LoadAsteroid()
        {
          Mine mine1 = new Mine();

            mine1.LoadContent(Content);

            bool isSpawnNormal = false;

            do
            {
                Random random = new Random();

                int x = random.Next(0, screenWidth - mine1.Width);
                int y = random.Next(0 - screenHeight - mine1.Height, 0);

                mine1.Position = new Vector2(x, y);
                mine1.Collision = new Rectangle(x, y, mine1.Width, mine1.Height);

                bool flag = false;

                foreach (Mine a in mine)
                {
                    if (mine1.Collision.Intersects(a.Collision))
                    {
                 
                        flag = true;
                    }
                }

                if (flag == false)
                {
                    isSpawnNormal = true;
                }

            } while (isSpawnNormal == false);


            mine.Add(mine1);
        }
        private void CheckCollision(List<SaveData> totalScore)
        {
            foreach (Mine a in mine)
            {
                if (a.Collision.Intersects(player.Collision))
                {
                    a.IsAlive = false;
                    player.Damage();
                    LoadExplotion(a.Position);
                    if (player.Health <= 0)
                    {
                        gameMode = GameMode.GameOver;
                        string plaerScore = player.Score.ToString();
                        if (totalScore != null)
                        {
                            gameOver.AddScores(plaerScore, totalScore[totalScore.Count - 1].Score.ToString());
                            if (int.Parse(plaerScore) > totalScore[totalScore.Count - 1].Score)
                            {
                                SaveData sd = new SaveData
                                {
                                    RecordSetTime = DateTime.Now,
                                    Score = player.Score
                                };
                                totalScore.Add(sd);
                                
                            }
                        }
                        else
                        {
                            SaveData sd = new SaveData
                            {
                                RecordSetTime = DateTime.Now,
                                Score = player.Score
                            };
                            totalScore.Add(sd);
                           
                        }
                        break;
                    }
                }
                foreach (Bullet b in player.BulletList)
                {
                    if (a.Collision.Intersects(b.Collision))
                    {
                        a.IsAlive = false;
                        
                        b.IsAlive = false;
                        LoadExplotion(a.Position);
                        player.AddScore();
                    }
                }
            }
           
        }
        private void UpdateExplosion(GameTime gametime)
        {
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Update(gametime);
                if (explosions[i].IsAlive == false)
                {
                    explosions.Remove(explosions[i]);
                    i--;
                }
            }
        }
        
        private void UpdaMine()
        {
            for (int i = 0; i < mine.Count; i++)
            {
                Mine a = mine[i];

                a.Update();

               
                if (a.Position.Y > screenHeight + 50 || a.IsAlive == false)
                {
                    mine.RemoveAt(i);
                    i--;
                }
            }
            
            if (mine.Count < asteroidAmount)
            {
                LoadAsteroid();
            }
        }

        internal class AirShooter
        {
            public AirShooter()
            {
            }
        }
    }
}