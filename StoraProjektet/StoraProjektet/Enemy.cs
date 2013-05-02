using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace StoraProjektet
{
    public class Enemy
    {
        //public ContentManager Content;
        public int xPos;
        public int yPos;
        public Texture2D enemyTexture;
        public bool alive;
        public int currentTextureX;
        public int currentTextureY;
        public int maxX;
        public int minX;
        public int maxAnimations;
        public int mobWidth;
        public int mobHeight;
        public int xMod;
        public int yMod;

        public Rectangle enemyRect;
        Rectangle sourceRect;
        Rectangle charBox;

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        public Direction currentDirection = Direction.Right;

        public Enemy(int posX, int posY, ContentManager Content, int maxX, int minX, int mobWidth, int mobHeight)
        {
            xPos = posX * Game1.gameSize;
            yPos = posY * Game1.gameSize;
            enemyTexture = Content.Load<Texture2D>("gaikotu");
            alive = true;
            this.maxX = maxX * Game1.gameSize;
            this.minX = minX * Game1.gameSize;
            this.mobWidth = mobWidth;
            this.mobHeight = mobHeight;

            enemyRect = new Rectangle(xPos, yPos, Game1.gameSize, Game1.gameSize);
            sourceRect = new Rectangle(0, 0, 32, 48);
        }

        public int XMod()
        {
            return (maxAnimations + 1) * mobWidth * xMod;
        }
        public float timer = 0;
        public float interval = 75;
        public void moveAnim(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            if (timer > interval)
            {
                timer = 0;
                currentTextureX += 1;
                if (currentTextureX > maxAnimations)
                    currentTextureX = 0;
            }
        }
        
        public virtual void Update(GameTime gameTime)
        {
            if (alive)
            {
                if (new Rectangle((int)Game1.charPlace.X, (int)Game1.charPlace.Y, Game1.charWidth, Game1.charHeight).Intersects(enemyRect) && !Game1.Invincible(gameTime))
                {
                    Game1.health -= 1;
                    Game1.timer = 0;
                }
                if (Attack.arrowHitbox.Intersects(enemyRect) && Attack.isShooting && Attack.attackType != Attack.typeOfAttack.None)
                {
                    alive = false;
                    Attack.isShooting = false;
                    Attack.attackType = Attack.typeOfAttack.None;
                }
            }

            switch (currentDirection)
            {
                case Direction.Right:
                    xPos += 1;
                    moveAnim(gameTime);
                    currentTextureY = 2;
                    if (xPos > maxX)
                        currentDirection = Direction.Left;
                    break;
                case Direction.Left:
                    xPos -= 1;
                    moveAnim(gameTime);
                    currentTextureY = 1;
                    if (xPos < minX)
                        currentDirection = Direction.Right;
                    break;
            }
            sourceRect = new Rectangle(currentTextureX * mobWidth + XMod(), currentTextureY * mobHeight /*+Modifier*/, mobWidth, mobHeight);
            enemyRect = new Rectangle(xPos, yPos, Game1.gameSize, Game1.gameSize);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(alive)
                spriteBatch.Draw(enemyTexture,enemyRect,sourceRect,Color.White);
        }
    }
}
