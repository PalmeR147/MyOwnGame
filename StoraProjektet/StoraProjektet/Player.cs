using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace StoraProjektet
{
    
    public class Player
    {
        public static int skillPoints = 0;
        public static bool levelUpScreen = false;
        public static Rectangle attackSpeed;
        public static Rectangle attackRange;
        public static Rectangle attackDamage;
        public static Texture2D levelUp;

        public void LoadContent(ContentManager content)
        {
            levelUp = content.Load<Texture2D>("Textures/Namnlös");
        }

        public static void Update(GameTime gameTime)
        {
            attackSpeed = new Rectangle(25, 25, 200, 70);
            attackRange = new Rectangle(225, 95, 200, 70);
            attackDamage = new Rectangle(425, 165, 200, 70);

            //Om mus intersectar + klick, set arrow.range += 1; etc etc etc
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (levelUpScreen)
                spriteBatch.Draw(levelUp, new Rectangle(25, 25, 600, 200), Color.White);
        }

        public static void LevelUp()
        {
            Game1.lvl += 1;
            skillPoints += 5;
            levelUpScreen = true;
        }
    }
}
