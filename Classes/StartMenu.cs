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

        private int selected;

        private Color selectedColor;

        private KeyboardState keyboardState;
        private KeyboardState prevKeyBoardState;
        private Texture2D texture;
        private ContentManager manager;

        public StartMenu(int width, int height)
        {
            selected = 0;
            selectedColor = Color.Black;
            

        }

        public void LoadContent(ContentManager contentManager)
        {
            foreach (var button in buttonList)
            {
                button.LoadContent(contentManager);

            }
            texture = manager.Load<Texture2D>("mainMenu");
        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();

            if (prevKeyBoardState.IsKeyDown(Keys.S) && keyboardState.IsKeyUp(Keys.S))
            {
                if (selected < buttonList.Count - 1)
                {
                    selected = selected + 1;
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
