using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace StoraProjektet
{
    public class Enemy
    {
        //public ContentManager Content;
        public int xPos;
        public int yPos;
        public Texture2D enemyTexture;
        public bool alive;

        Rectangle enemyRect;

        public Enemy(int posX, int posY, ContentManager Content)
        {
            xPos = posX;
            yPos = posY;
            enemyTexture = Content.Load<Texture2D>("Textures/Char");
            alive = true;

            enemyRect = new Rectangle(xPos, yPos, Game1.gameSize, Game1.gameSize);
        }
        
        public virtual void Update()
        {
            if (alive)
            {
                if (Attack.arrowHitbox.Intersects(enemyRect))
                    alive = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(alive)
                spriteBatch.Draw(enemyTexture,enemyRect,Color.White);
        }
    }
}
