using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StoraProjektet
{
    class Bow : Item
    {
        public Bow(int xPos, int yPos, ContentManager Content)
            : base(xPos, yPos)
        {
            itemTex = Content.Load<Texture2D>("Textures/ArrowPUP");
            onGround = true;
            this.xPos = xPos;
            this.yPos = yPos;
        }
    }
}
