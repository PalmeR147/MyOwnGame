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
        public Zombie(int maxX, int maxY, int minX, int minY, ContentManager Content, int posX, int posY, int maxAnimations, int mobWidth, int mobHeight, int xMod, int yMod)
            : base(posX, posY, Content, maxX, maxY, minX, minY, maxAnimations, mobWidth, mobHeight, xMod, yMod)
        {
            enemyTexture = Content.Load<Texture2D>("Textures/TileSets/MobsTileset");
            this.maxX = maxX;
            this.maxY = maxY;
            this.minX = minX;
            this.minY = minY;
            this.maxAnimations = maxAnimations;
        }
    }
}
