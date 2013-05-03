using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace StoraProjektet
{
    public class Item
    {
        public int xPos;
        public int yPos;
        public string item;
        public bool onGround;
        public Texture2D itemTex;

        public Item(int xPos, int yPos, string item, Texture2D itemTex)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.item = item;
            this.itemTex = itemTex;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (onGround)
                spriteBatch.Draw(itemTex, new Rectangle(xPos, yPos, Game1.gameSize, Game1.gameSize), Color.White);
        }
    }
}
