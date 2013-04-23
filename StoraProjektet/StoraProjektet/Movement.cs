using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace StoraProjektet
{
    public class Movement
    {
        //KeyboardState oldState;

        public enum Direction
        {
            Right,
            Left,
            Up,
            Down,
            Still
        }
        public static Direction currentDirection = Direction.Still;
        public static void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();
            //KnappTryck / Rörelse: Kollar kollision och att man inte redan rör på sig. Sätter sedan HÅLLET man är "påväg" åt till höger/vänster/upp/ner
            if ((currentState.IsKeyDown(Keys.Right) || currentState.IsKeyDown(Keys.D)) && !Collision.isColliding("Right") && currentDirection == Direction.Still /*&& !oldState.IsKeyDown(Keys.Right)*/)
                //Move("Right");
                currentDirection = Direction.Right;
            if ((currentState.IsKeyDown(Keys.Left) || currentState.IsKeyDown(Keys.A)) && !Collision.isColliding("Left") && currentDirection == Direction.Still /*&& !oldState.IsKeyDown(Keys.Left)*/)
                //Move("Left");
                currentDirection = Direction.Left;
            if ((currentState.IsKeyDown(Keys.Down) || currentState.IsKeyDown(Keys.S)) && !Collision.isColliding("Down") && currentDirection == Direction.Still /*&& !oldState.IsKeyDown(Keys.Down)*/)
                //Move("Down");
                currentDirection = Direction.Down;
            if ((currentState.IsKeyDown(Keys.Up) || currentState.IsKeyDown(Keys.W)) && !Collision.isColliding("Up") && currentDirection == Direction.Still /*&& !oldState.IsKeyDown(Keys.Up)*/)
                //Move("Up");
                currentDirection = Direction.Up;
            //Kollar hela tiden vilket håll man är påväg åt, rör sig 1 pixel + animerar hela tiden TILLS man står i mitten av en ruta, då sätter den "hållet" till StillaStående
            switch (currentDirection)
            {
                case Direction.Right:
                        Game1.currentTextureY = 2;
                        Game1.charPlace.X += 5;
                        charAnim(gameTime);
                        if (Game1.charPlace.X % Game1.gameSize == 0)
                            currentDirection = Direction.Still;
                    break;
                case Direction.Left:
                        Game1.currentTextureY = 1;
                        Game1.charPlace.X -= 5;
                        charAnim(gameTime);
                        if (Game1.charPlace.X % Game1.gameSize == 0)
                            currentDirection = Direction.Still;
                    break;
                case Direction.Down:
                        Game1.currentTextureY = 0;
                        Game1.charPlace.Y += 1;
                        charAnim(gameTime);
                        if (Game1.charPlace.Y % Game1.gameSize == 0)
                            currentDirection = Direction.Still;
                    break;
                case Direction.Up:
                        Game1.currentTextureY = 3;
                        Game1.charPlace.Y -= 1;
                        charAnim(gameTime);
                        if (Game1.charPlace.Y % Game1.gameSize == 0)
                            currentDirection = Direction.Still;
                    break;
                    
            }
        }
        
        /*public static void Move(string direction)
        {
            switch (direction)
            {
                case "Right":
                    Game1.currentTextureY = 2;
                    if (!Collision.isColliding("Right"))
                    {
                        for (int i = (int)Game1.speed; i > 0; i--)
                        {
                            
                            Game1.charPlace.X += 1;
                        }
                    }
                    break;
                case "Left":
                    Game1.currentTextureY = 1;
                    if (!Collision.isColliding("Left"))
                        Game1.charPlace.X -= Game1.speed;
                    break;
                case "Up":
                    Game1.currentTextureY = 3;
                    if (!Collision.isColliding("Up"))
                        Game1.charPlace.Y -= Game1.speed;
                    break;
                case "Down":
                    Game1.currentTextureY = 0;
                    if (!Collision.isColliding("Down"))
                        Game1.charPlace.Y += Game1.speed;
                    break;
            }
        }*/
        //Animation, byter varje intervall till nästa bild i spritesheet'en, vilket får gubben att animera steg
        public static float timer;
        public static float interval = 75;
        public static void charAnim(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;

            if (timer > interval)
            {
                timer = 0;
                Game1.currentTextureX += 1;
                if (Game1.currentTextureX > 2)
                    Game1.currentTextureX = 0;
            }
        }
    }
}
