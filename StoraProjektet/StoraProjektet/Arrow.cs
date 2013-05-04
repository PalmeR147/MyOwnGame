using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StoraProjektet
{
    public class Arrow
    {
        public int xPos;
        public int yPos;
        public int range = 3;
        public int damage = 2;
        public string direction;
        public bool isShooting;

        public int startPosX;
        public int startPosY;

        shootDir shootDirection = shootDir.none;

        public int spriteX;
        public int spriteY;
        public int spriteWidth;
        public int spriteHeight;
        public int spritePlaceModX = 0;
        public float spriteSizeModW = 1;
        public int spritePlaceModY = 0;
        public float spriteSizeModH = 1;

        public enum shootDir
        {
            arrowLeft,
            arrowRight,
            arrowUp,
            arrowDown,
            none
        }
        
        public Arrow(string direction)
        {
            xPos = (int)Game1.charPlace.X;
            yPos = (int)Game1.charPlace.Y;
            this.direction = direction;


            startPosX = (int)Game1.charPlace.X;
            startPosY = (int)Game1.charPlace.Y;
            spriteX = 0;
            spriteY = 150;
            spriteWidth = 150;
            spriteHeight = 30;
            xPos = (int)Game1.charPlace.X;
            yPos = (int)Game1.charPlace.Y;
        }
        public void Update(GameTime gameTime)
        {
            switch (direction)
            {
                case "arrowRight":
                    spritePlaceModY = 7;
                    spriteSizeModH = 0.5f;
                    spritePlaceModX = 0;
                    spriteSizeModW = 1f;
                    isShooting = true;
                    xPos += 2;
                    break;
            }
            switch (shootDirection)
            {
                //Beroende på vilket håll man skjuter åt så bestämms massa modifiers (för att den skall vara längre än bred), och sedan skjuts pilen iväg tills den kolliderar med något.
                case shootDir.arrowRight:
                    spritePlaceModY = 7;
                    spriteSizeModH = 0.5f;
                    spritePlaceModX = 0;
                    spriteSizeModW = 1f;
                    isShooting = true;
                    xPos += 2;
                    if (xPos > Game1.maxX || Collision.isColliding("ArrowRight") || startPosX + range * Game1.gameSize <= xPos)
                    {
                        shootDirection = shootDir.none;
                        isShooting = false;
                    }
                    break;
                case shootDir.arrowLeft:
                    spritePlaceModY = 7;
                    spriteSizeModH = 0.5f;
                    spritePlaceModX = 0;
                    spriteSizeModW = 1f;
                    isShooting = true;
                    xPos -= 2;
                    if (xPos < Game1.minX || Collision.isColliding("ArrowLeft") || startPosX - range * Game1.gameSize >= xPos)
                    {
                        shootDirection = shootDir.none;
                        isShooting = false;
                    }
                    break;
                case shootDir.arrowUp:
                    spritePlaceModX = 7;
                    spriteSizeModW = 0.5f;
                    spritePlaceModY = 0;
                    spriteSizeModH = 1f;
                    isShooting = true;
                    yPos -= 2;
                    if (yPos < Game1.minY || Collision.isColliding("ArrowUp") || startPosY - range * Game1.gameSize >= yPos)
                    {
                        shootDirection = shootDir.none;
                        isShooting = false;
                    }
                    break;
                case shootDir.none:
                    spritePlaceModX = 7;
                    spriteSizeModW = 0.5f;
                    spritePlaceModY = 0;
                    spriteSizeModH = 1f;
                    isShooting = true;
                    yPos += 2;
                    if (yPos > Game1.maxY || Collision.isColliding("ArrowDown") || startPosY + range * Game1.gameSize <= yPos)
                    {
                        shootDirection = shootDir.none;
                        isShooting = false;
                    }
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isShooting) //fungerar om man tar bort isshooting, något gör att den inte sätts alive, kolla movement
                spriteBatch.Draw(Game1.arrowSpriteSheet, new Rectangle(xPos + spritePlaceModX, yPos + spritePlaceModY, (int)(Game1.gameSize * spriteSizeModW), (int)(Game1.gameSize * spriteSizeModH)), new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
        }
    }
}
