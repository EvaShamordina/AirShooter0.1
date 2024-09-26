using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AirShooter.Classes.Components;
using SharpDX.Direct3D9;

namespace AirShooter.Classes
{
    internal class StartMenu
    {
        
        private List<Label> buttonList = new List<Label>();
        public void ButtonList(List<Label> buttonList)
        {
            buttonList.Add(endMenu);
            buttonList.Add(explosion);
            buttonList.Add(bgLayer1);
            buttonList.Add(bgLayer2);
            buttonList.Add(mainMenu);
            buttonList.Add(healthbar);
            buttonList.Add(laser);
            buttonList.Add(mainbackground);
            buttonList.Add(mine);
            buttonList.Add(mineAnimation);
            buttonList.Add(player);
            buttonList.Add(shipAnimation);
        }
        public int selected;

        private Color selectedColor;

        private KeyboardState keyboardState;
        private KeyboardState prevKeyBoardState;
        public Texture2D texture;
        public ContentManager manager;
        private Label endMenu;
        private Label explosion;
        private Label bgLayer1;
        private Label bgLayer2;
        private Label mainMenu;
        private Label healthbar;
        private Label laser;
        private Label mainbackground;
        private Label mine;
        private Label mineAnimation;
        private Label player;
        private Label shipAnimation;
        public int Width { get; }
        public int Height { get; }

        public StartMenu(int width, int height)
        {
            selected = 0;
            selectedColor = Color.Black;
            Width = width;
            Height = height;
        }

        public void LoadContent(ContentManager contentManager)
        {
            foreach (var button in buttonList)
            {
                button.LoadContent(contentManager);

            }
            texture = contentManager.Load<Texture2D>("mainMenu");
        }

        public void Update()
        {
            Update(selected);
        }

        public void Update(int selected)
        {
            keyboardState = Keyboard.GetState();

            if (prevKeyBoardState.IsKeyDown(Keys.S) && keyboardState.IsKeyUp(Keys.S))
            {
                if (selected < buttonList.Count - 1)
                {
                    selected += selected + 1;
                }

            }

            if (prevKeyBoardState.IsKeyDown(Keys.W) && keyboardState.IsKeyUp(Keys.W))
            {
                if (selected > 0)
                {
                    selected = selected - 1;
                }
            }




            prevKeyBoardState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < buttonList.Count; i++)
            {
                if (i == selected)
                {
                    buttonList[i].Color = selectedColor;
                }
                else
                {
                    buttonList[i].Color = Color.White;
                }

                buttonList[i].Draw(spriteBatch);
            }
        }
    }
}
