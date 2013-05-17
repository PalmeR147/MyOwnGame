using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StoraProjektet
{
    public class Arrow
    {
        public int xPos;
        public int yPos;
        public int range = 5;
        public int damage = 2;
        public string direction;
        public bool isShooting;

        public bool isHostile;

        public int startPosX;
        public int startPosY;

        public shootDir shootDirection = shootDir.none;

        KeyboardState key = Keyboard.GetState();

        public int spriteX;
        public int spriteY;
        public int spriteWidth;
        public int spriteHeight;
        public int spritePlaceModX = 0;
        public float spriteSizeModW = 1;
        public int spritePlaceModY = 0;
        public float spriteSizeModH = 1;

        public Rectangle hitBox = new Rectangle();

        public enum shootDir
        {
            arrowLeft,
            arrowRight,
            arrowUp,
            arrowDown,
            none//Obsolete after new arrow handling
        }
        
        public Arrow(string direction, int range)
        {
            xPos = (int)Game1.charPlace.X;
            yPos = (int)Game1.charPlace.Y;
            this.direction = direction;

            this.range = range;

            isShooting = true;

            startPosX = (int)Game1.charPlace.X;
            startPosY = (int)Game1.charPlace.Y;
            xPos = (int)Game1.charPlace.X;
            yPos = (int)Game1.charPlace.Y;
        }
        public Arrow(string direction, bool isHostile, int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.direction = direction;
            this.isHostile = isHostile;

            isShooting = true;

            startPosX = (int)Game1.charPlace.X;
            startPosY = (int)Game1.charPlace.Y;
            xPos = (int)Game1.charPlace.X;
            yPos = (int)Game1.charPlace.Y;
        }
        public void Update(GameTime gameTime)
        {
            hitBox = new Rectangle(xPos, yPos, Game1.gameSize, Game1.gameSize / 2);

            if (new Rectangle((int)Game1.charPlace.X, (int)Game1.charPlace.Y, Game1.charWidth, Game1.charHeight).Intersects(hitBox) && !Game1.Invincible(gameTime) && isHostile)
            {
                Game1.health -= 1;
                Game1.timer = 0;
                isShooting = false;
            }

            switch (direction)
            {
                case "arrowRight":
                    shootDirection = shootDir.arrowRight;
                    spriteX = 0;
                    spriteY = 150;
                    spriteWidth = 150;
                    spriteHeight = 30;
                    break;
                case "arrowLeft":
                    shootDirection = shootDir.arrowLeft;
                    spriteX = 30;
                    spriteY = 0;
                    spriteWidth = 150;
                    spriteHeight = 30;
                    break;
                case "arrowUp":
                    shootDirection = shootDir.arrowUp;
                    spriteX = 0;
                    spriteY = 0;
                    spriteWidth = 30;
                    spriteHeight = 150;
                    break;
                case "arrowDown":
                    shootDirection = shootDir.arrowDown;
                    spriteX = 150;
                    spriteY = 30;
                    spriteWidth = 30;
                    spriteHeight = 150;
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
                    xPos += 2;
                    if (xPos > Game1.maxX || Collision.isColliding("ArrowRight",xPos,yPos) || startPosX + range * Game1.gameSize <= xPos)
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
                    xPos -= 2;
                    if (xPos < Game1.minX || Collision.isColliding("ArrowLeft",xPos,yPos) || startPosX - range * Game1.gameSize >= xPos)
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
                    yPos -= 2;
                    if (yPos < Game1.minY || Collision.isColliding("ArrowUp",xPos,yPos) || startPosY - range * Game1.gameSize >= yPos)
                    {
                        shootDirection = shootDir.none;
                        isShooting = false;
                    }
                    break;
                case shootDir.arrowDown:
                    spritePlaceModX = 7;
                    spriteSizeModW = 0.5f;
                    spritePlaceModY = 0;
                    spriteSizeModH = 1f;
                    yPos += 2;
                    if (yPos < Game1.minY || Collision.isColliding("ArrowDown",xPos,yPos) || startPosY + range * Game1.gameSize <= yPos)
                    {
                        shootDirection = shootDir.none;
                        isShooting = false;
                    }
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isShooting) //ritar endast när pilen skjuts
                spriteBatch.Draw(Game1.arrowSpriteSheet, new Rectangle(xPos + spritePlaceModX, yPos + spritePlaceModY, (int)(Game1.gameSize * spriteSizeModW), (int)(Game1.gameSize * spriteSizeModH)), new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
        }
    }
}
