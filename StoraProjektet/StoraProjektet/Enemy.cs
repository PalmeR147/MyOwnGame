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
        public int hP = 10;
        public Texture2D hpTex;

        public Rectangle enemyRect;
        Rectangle sourceRect;
        Rectangle charBox;
        Rectangle hpRec;

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        public Direction currentDirection = Direction.Right;

        public Enemy(int posX, int posY, ContentManager Content, int maxX, int minX)
        {
            xPos = posX * Game1.gameSize;
            yPos = posY * Game1.gameSize;
            enemyTexture = Content.Load<Texture2D>("gaikotu");
            hpTex = Content.Load<Texture2D>("Textures/Namnlös");
            alive = true;
            this.maxX = maxX * Game1.gameSize;
            this.minX = minX * Game1.gameSize;

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
            hpRec = new Rectangle(enemyRect.X, enemyRect.Y - enemyRect.Height / 2 + 5, hP * 2, 2);


            if (alive)
            {
                if (new Rectangle((int)Game1.charPlace.X, (int)Game1.charPlace.Y, Game1.charWidth, Game1.charHeight).Intersects(enemyRect) && !Game1.Invincible(gameTime))
                {
                    Game1.health -= 1;
                    Game1.timer = 0;
                }

                    foreach (Arrow a in Game1.arrows)
                    {
                        if (a.hitBox.Intersects(enemyRect) && a.isShooting && a.shootDirection != Arrow.shootDir.none)
                        {
                            hP -= a.damage;
                            a.isShooting = false;
                            a.shootDirection = Arrow.shootDir.none;
                        }
                    }
                
                if (hP <= 0)
                    alive = false;
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
            if (alive)
            {
                spriteBatch.Draw(hpTex, hpRec, Color.Red);
                spriteBatch.Draw(enemyTexture, enemyRect, sourceRect, Color.White);
            }
        }
    }
}
