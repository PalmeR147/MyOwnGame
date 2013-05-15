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

        public bool onGround;
        public Texture2D itemTex;
        public Rectangle destinationRec;

        public Item(int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;

            destinationRec = new Rectangle(xPos * Game1.gameSize, yPos * Game1.gameSize, Game1.gameSize, Game1.gameSize);
        }
        public void Update(GameTime gameTime)
        {
            if (destinationRec.Intersects(Game1.charBox))
            {
                PickupItem("bow");
            }
        }
        public void PickupItem(string item)
        {
            if (item == "bow")
            {
                
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (onGround)
                spriteBatch.Draw(itemTex, destinationRec, Color.White);
        }
    }
}
