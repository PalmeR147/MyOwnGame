using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace StoraProjektet
{
    
    public class Player
    {
        public ContentManager Content;
        Texture2D charSheet;
        public static int posX;
        public static int posY;
        public static int charSize = Game1.gameSize;
        Rectangle currentTexture;

        private void LoadContent()
        {
            charSheet = Content.Load<Texture2D>("Textures/GubbeSheet");
            currentTexture = new Rectangle(0, 0, 32, 32);
        }
        public Player()
        {
            posX = Game1.gameSize *= 10;
            posY = Game1.gameSize *= 10;
        }
        public Player(int xPos, int yPos)
        {
            posX = xPos;
            posY = yPos;
            
        }
        public static void Update(GameTime gameTime)
        {
            
        }
        private void Draw(SpriteBatch SpriteBatch, GameTime gameTime)
        {
            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            SpriteBatch.Draw(charSheet, new Rectangle(posX + charSize, posY + charSize, Convert.ToInt32((0.75 * charSize)), Convert.ToInt32((0.75 * charSize))),currentTexture, Color.White);
            SpriteBatch.End();

            
            Draw(SpriteBatch, gameTime);
        }
    }
}
