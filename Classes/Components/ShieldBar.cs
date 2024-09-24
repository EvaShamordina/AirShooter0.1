using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirShooter.Classes.Components
{
    class ShieldBar : Bar
    {
        private Texture2D backTexture;

        public int maxWidth;

        public ShieldBar(Vector2 position, int width, int height) : base(position, width, height, "mainMenu")
        {
            maxWidth = width;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            backTexture = contentManager.Load<Texture2D>("mainMenu");

            base.LoadContent(contentManager);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle backDestinationRectangle = new((int)position.X, (int)position.Y, maxWidth, height);
            spriteBatch.Draw(backTexture, backDestinationRectangle, Color.White);

            base.Draw(spriteBatch);
        }

        public static void A() { }
    }
}
