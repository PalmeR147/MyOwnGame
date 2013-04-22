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

        public Skelett(int xPos, int yPos, ContentManager Content, int maxX, int maxY, int minX, int minY)
            : base(xPos, yPos, Content, maxX, maxY, minX, minY)
        {
            enemyTexture = Content.Load<Texture2D>("gaikotu");
            this.maxX = maxX;
            this.maxY = maxY;
            this.minX = minX;
            this.minY = minY;
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
