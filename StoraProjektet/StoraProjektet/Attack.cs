using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace StoraProjektet
{
    public class Attack
    {
        public static int arrowX = 0;
        public static int arrowY = 0;
        public ContentManager Content;
        public static GraphicsDeviceManager graphics;
        public static KeyboardState currentKey;
        public static Stopwatch s = new Stopwatch();
        public static Stopwatch r = new Stopwatch();
        public static int range = 5;

        public static bool b = true;
        
        public static Enemy t;

        public static void Update(GameTime gameTime)
        {
            //Kollar Knapptryck, och isåfall bestämmer vilken bild som skall visas från spriteSheeten och åt vilket håll man skjuter.
            currentKey = Keyboard.GetState();
            //TESTA HP LOSS
            ContentManager Content;
            //Init till Stopwatch
            if (b)
            {
                s.Start();
                r.Start();
                b = false;
            }

            #region ArrowKeyPress
            if (currentKey.IsKeyDown(Keys.NumPad6) && s.ElapsedMilliseconds > 750)
            {
                arrowX = (int)Game1.charPlace.X;
                arrowY = (int)Game1.charPlace.Y;
                Game1.arrows.Add(new Arrow("arrowRight", range));
                s.Restart();
                s.Start();
            }
            if (currentKey.IsKeyDown(Keys.NumPad4) && s.ElapsedMilliseconds > 750)
            {
                arrowX = (int)Game1.charPlace.X;
                arrowY = (int)Game1.charPlace.Y;
                Game1.arrows.Add(new Arrow("arrowLeft", range));
                s.Restart();
                s.Start();
            }
            if (currentKey.IsKeyDown(Keys.NumPad8) && s.ElapsedMilliseconds > 750)
            {
                arrowX = (int)Game1.charPlace.X;
                arrowY = (int)Game1.charPlace.Y;
                Game1.arrows.Add(new Arrow("arrowUp", range));
                s.Restart();
                s.Start();
            }
            if (currentKey.IsKeyDown(Keys.NumPad2) && s.ElapsedMilliseconds > 750)
            {
                arrowX = (int)Game1.charPlace.X;
                arrowY = (int)Game1.charPlace.Y;
                Game1.arrows.Add(new Arrow("arrowDown", range));
                s.Restart();
                s.Start();
            }
            if (currentKey.IsKeyDown(Keys.NumPad5) && r.ElapsedMilliseconds > 10000)
            {
                arrowX = (int)Game1.charPlace.X;
                arrowY = (int)Game1.charPlace.Y;
                Game1.arrows.Add(new Arrow("arrowDown", range));
                Game1.arrows.Add(new Arrow("arrowUp", range));
                Game1.arrows.Add(new Arrow("arrowLeft", range));
                Game1.arrows.Add(new Arrow("arrowRight", range));
                r.Restart();
                r.Start();
            }
            #endregion

            if (currentKey.IsKeyDown(Keys.X))
                Game1.health -= 1;
        }
    }
}
