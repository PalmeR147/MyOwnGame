using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StoraProjektet
{
    class Zombie : Enemy
    {
        public Zombie(int maxX, int minX, ContentManager Content, int posX, int posY, int mobWidth, int mobHeight)
            : base(posX, posY, Content, maxX, minX, mobWidth, mobHeight)
        {
            enemyTexture = Content.Load<Texture2D>("Textures/TileSets/MobsTileset");
            this.maxX = maxX * 16;
            this.minX = minX * 16;

            maxAnimations = 2;
            xMod = 1;
            yMod = 1;
        }
    }
}
