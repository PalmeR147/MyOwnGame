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
        public Zombie(int maxX, int minX, ContentManager Content, int posX, int posY)
            : base(posX, posY, Content, maxX, minX)
        {
            enemyTexture = Content.Load<Texture2D>("Textures/TileSets/MobsTileset");
            this.maxX = maxX * Game1.gameSize;
            this.minX = minX * Game1.gameSize;
            mobWidth = 32;
            mobHeight = 32;

            maxAnimations = 2;
            xMod = 1;
            yMod = 1;
        }
    }
}
