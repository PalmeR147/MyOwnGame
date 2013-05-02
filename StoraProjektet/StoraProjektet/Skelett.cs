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

        public Skelett(int xPos, int yPos, ContentManager Content, int maxX, int minX, int mobWidth, int mobHeight)
            : base(xPos, yPos, Content, maxX, minX, mobWidth, mobHeight)
        {
            enemyTexture = Content.Load<Texture2D>("gaikotu");
            this.maxX = maxX * 16;
            this.minX = minX * 16;
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
