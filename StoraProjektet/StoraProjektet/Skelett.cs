using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StoraProjektet
{
    class Skelett : Enemy
    {
        public Skelett(int xPos, int yPos, ContentManager Content)
            : base(xPos, yPos, Content)
        {
            enemyTexture = Content.Load<Texture2D>("ENAMI");
        }
        public void Skjuta()
        {

        }
        public override void Update()
        {

            base.Update();
        }
    }
}
