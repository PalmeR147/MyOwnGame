using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StoraProjektet
{
    public class Attack
    {
        public enum typeOfAttack
        {
            BowRight,
            BowLeft,
            BowUp,
            BowDown,
            Sword,
            Magic,
            Spear,
            None
        }
        public static int arrowX = 0;
        public static int arrowY = 0;
        public static int arrowSpeed;
        public static int arrowDamage;
        public static float timer;
        public static float interval = 100;
        public static bool isShooting = false;
        public static Rectangle arrowHitbox;
        public ContentManager Content;
        public static GraphicsDeviceManager graphics;
        public static KeyboardState currentKey;
        public static typeOfAttack attackType = typeOfAttack.None;
        public static int spriteX;
        public static int spriteY;
        public static int spriteWidth;
        public static int spriteHeight;
        public static int spritePlaceModX = 0;
        public static float spriteSizeModW = 1;
        public static int spritePlaceModY = 0;
        public static float spriteSizeModH = 1;
        public static int startPosX;
        public static int startPosY;
        public static int range = 2;
        public static int damage = 1;

        public static Enemy t;

        public static void Update(GameTime gameTime)
        {
            //Kollar Knapptryck, och isåfall bestämmer vilken bild som skall visas från spriteSheeten och åt vilket håll man skjuter.
            currentKey = Keyboard.GetState();
            //TESTA HP LOSS
            ContentManager Content;
            
            if (currentKey.IsKeyDown(Keys.X))
                Game1.health -= 1;
            if (currentKey.IsKeyDown(Keys.NumPad6) && !isShooting)
            {
                startPosX = (int)Game1.charPlace.X;
                startPosY = (int)Game1.charPlace.Y;
                spriteX = 0;
                spriteY = 150;
                spriteWidth = 150;
                spriteHeight = 30;
                arrowX = (int)Game1.charPlace.X;
                arrowY = (int)Game1.charPlace.Y;
                attackType = typeOfAttack.BowRight;
            }
            if (currentKey.IsKeyDown(Keys.NumPad4) && !isShooting)
            {
                startPosX = (int)Game1.charPlace.X;
                startPosY = (int)Game1.charPlace.Y;
                spriteX = 30;
                spriteY = 0;
                spriteWidth = 150;
                spriteHeight = 30;
                arrowX = (int)Game1.charPlace.X;
                arrowY = (int)Game1.charPlace.Y;
                attackType = typeOfAttack.BowLeft;
            }
            if (currentKey.IsKeyDown(Keys.NumPad8) && !isShooting)
            {
                startPosX = (int)Game1.charPlace.X;
                startPosY = (int)Game1.charPlace.Y;
                spriteX = 0;
                spriteY = 0;
                spriteWidth = 30;
                spriteHeight = 150;
                arrowX = (int)Game1.charPlace.X;
                arrowY = (int)Game1.charPlace.Y;
                attackType = typeOfAttack.BowUp;
            }
            if (currentKey.IsKeyDown(Keys.NumPad2) && !isShooting)
            {
                startPosX = (int)Game1.charPlace.X;
                startPosY = (int)Game1.charPlace.Y;
                spriteX = 150;
                spriteY = 30;
                spriteWidth = 30;
                spriteHeight = 150;
                arrowX = (int)Game1.charPlace.X;
                arrowY = (int)Game1.charPlace.Y;
                attackType = typeOfAttack.BowDown;
            }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            arrowHitbox = new Rectangle(arrowX, arrowY, Game1.gameSize, Game1.gameSize / 2);
            switch (attackType)
            {
                //Beroende på vilket håll man skjuter åt så bestämms massa modifiers (för att den skall vara längre än bred), och sedan skjuts pilen iväg tills den kolliderar med något.
                case typeOfAttack.BowRight:
                    spritePlaceModY = 7;
                    spriteSizeModH = 0.5f;
                    spritePlaceModX = 0;
                    spriteSizeModW = 1f;
                    isShooting = true;
                    arrowX += 2;
                    if (arrowX > Game1.maxX || Collision.isColliding("ArrowRight") || startPosX + range * Game1.gameSize <= arrowX)
                    {
                        attackType = typeOfAttack.None;
                        isShooting = false;
                    }
                    break;
                case typeOfAttack.BowLeft:
                    spritePlaceModY = 7;
                    spriteSizeModH = 0.5f;
                    spritePlaceModX = 0;
                    spriteSizeModW = 1f;
                    isShooting = true;
                    arrowX -= 2;
                    if (arrowX < Game1.minX || Collision.isColliding("ArrowLeft") || startPosX - range * Game1.gameSize >= arrowX)
                    {
                        attackType = typeOfAttack.None;
                        isShooting = false;
                    }
                    break;
                case typeOfAttack.BowUp:
                    spritePlaceModX = 7;
                    spriteSizeModW = 0.5f;
                    spritePlaceModY = 0;
                    spriteSizeModH = 1f;
                    isShooting = true;
                    arrowY -= 2;
                    if (arrowY < Game1.minY || Collision.isColliding("ArrowUp") || startPosY - range * Game1.gameSize >= arrowY)
                    {
                        attackType = typeOfAttack.None;
                        isShooting = false;
                    }
                    break;
                case typeOfAttack.BowDown:
                    spritePlaceModX = 7;
                    spriteSizeModW = 0.5f;
                    spritePlaceModY = 0;
                    spriteSizeModH = 1f;
                    isShooting = true;
                    arrowY += 2;
                    if (arrowY > Game1.maxY || Collision.isColliding("ArrowDown") || startPosY + range*Game1.gameSize <= arrowY)
                    {
                        attackType = typeOfAttack.None;
                        isShooting = false;
                    }
                    break;
                case typeOfAttack.Magic:
                    //ShootBolt
                    break;
                case typeOfAttack.Spear:
                    //ThrowSpear
                    break;
                case typeOfAttack.Sword:
                    //SliceSword
                    break;
                case typeOfAttack.None:
                    break;
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (isShooting)
                spriteBatch.Draw(Game1.arrowSpriteSheet, new Rectangle(arrowX + spritePlaceModX, arrowY + spritePlaceModY, (int)(Game1.gameSize * spriteSizeModW), (int)(Game1.gameSize * spriteSizeModH)), new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
        }
        /*public static void PlayerAttack(typeOfAttack attackType)
        {
            switch (attackType)
            {
                case typeOfAttack.Bow:
                    //ShootBow
                    break;
                case typeOfAttack.Magic:
                    //ShootBolt
                    break;
                case typeOfAttack.Spear:
                    //ThrowSpear
                    break;
                case typeOfAttack.Sword:
                    //SliceSword
                    break;
            }
        }*/
    }
}
