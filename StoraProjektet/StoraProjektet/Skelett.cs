using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace StoraProjektet
{
    public class Skelett : Enemy
    {

        public Skelett(int maxX, int minX, ContentManager Content,int xPos, int yPos)
            : base(xPos, yPos, Content, maxX, minX)
        {
            enemyTexture = Content.Load<Texture2D>("gaikotu");
            this.maxX = maxX * Game1.gameSize;
            this.minX = minX * Game1.gameSize;
            mobWidth = 32;
            mobHeight = 48;
            maxAnimations = 3;
        }
        public void Skjuta()
        {

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        
    }
}
