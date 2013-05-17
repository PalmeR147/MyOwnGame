using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace StoraProjektet
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        
        public Game1()
        {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            /*graphics.PreferredBackBufferWidth = 400;
            graphics.PreferredBackBufferHeight = 240;
            graphics.ApplyChanges();*/
            
            for (int y = 0; y < Maps.colmap1.GetLength(0); y++)
            {
                for (int x = 0; x < Maps.colmap1.GetLength(1); x++)
                {
                    if (Maps.colmap1[y, x] != 0)
                    {
                        collisionTiles.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                    }
                }
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            oldState = Keyboard.GetState();
            base.Initialize();
            graphics.PreferredBackBufferHeight = 480;

            graphics.PreferredBackBufferWidth = 800;

            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        #region declarations
        public static int gameSize = 32;
        //Vektorer:
        public static Vector2 charPlace = new Vector2(32, 32);
        Vector2 enemyPlace = new Vector2(150, 150);
        Vector2 playButton;

        //Texturer & rektanglar:
        public static Texture2D character;
        Texture2D enemy;
        Texture2D healthTexture;
        Texture2D healthOutline;
        Texture2D tileSet;
        Texture2D play;
        Texture2D charSheet;
        public static Texture2D arrowSpriteSheet;
        public static Texture2D arrowSprite;
        List<Texture2D> tiles = new List<Texture2D>();
        public static List<Rectangle> collisionTiles = new List<Rectangle>();
        Rectangle musRec;
        Rectangle playBut;
        Rectangle charTex;
        public static int currentTextureY;
        public static int currentTextureX;
        SpriteFont fontDefault;
        SpriteFont xpLVL;
        public static Texture2D enemyTexture;
        public static Rectangle charBox;

        //Variabler
        public static float speed = gameSize;
        public static int charWidth = Convert.ToInt32(0.125 * gameSize);
        public static int charHeight = Convert.ToInt32(0.125 * gameSize);
        int tileWidth = gameSize;
        int tileHeight = gameSize;
        public static int maxY;
        public static int minY;
        public static int maxX;
        public static int minX;
        public static int health = 10;
        public static int exp = 0;
        public static int expToLevel = 50;
        public static int lvl = 1;
        public static string gameOver = "Game Over";
        public static float timer = 0;
        public static float interval = 1000;

        //Övrigt / Objekt
        KeyboardState oldState;
        MouseState mus;
        List<Enemy> enemies1 = new List<Enemy>();
        List<Enemy> enemies2 = new List<Enemy>();
        List<Enemy> enemies3 = new List<Enemy>();
        List<Enemy> enemies4 = new List<Enemy>();

        List<Item> items1 = new List<Item>();

        public static List<Arrow> arrows = new List<Arrow>();

        enum GameState 
        {
            Menu,
            Playing,
            Level1,
            Level2,
            Level3,
            Level4,
            GameOver,
            Paused
        }

        enum shootDir
        {
            arrowRight,
            arrowLeft,
            arrowUp,
            arrowDown,
            none
        }
        GameState currentState = new GameState();
        #endregion
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            enemies1.Add(new Skelett(14, 9, this.Content, 9, 2));
            enemies1.Add(new Zombie(5, 2, this.Content, 1, 2));
            enemies1.Add(new Zombie(5, 2, this.Content, 5, 6));

            items1.Add(new Bow(20, 5, this.Content));

            enemies2.Add(new Skelett(256, 256, this.Content, 512, 128));
            enemies3.Add(new Skelett(64, 64, this.Content, 128, 32));
            enemies4.Add(new Skelett(100, 100, this.Content, 150, 50));
            
            currentTextureY = 0;
            currentTextureX = 0;
            // TODO: use this.Content to load your game content here
            character = Content.Load<Texture2D>("Textures/Char");
            enemy = Content.Load<Texture2D>("ENAMI");
            healthTexture = Content.Load<Texture2D>("Textures/Namnlös");
            healthOutline = Content.Load<Texture2D>("Textures/HPBOutline");
            play = Content.Load<Texture2D>("Textures/play");
            charSheet = Content.Load<Texture2D>("Textures/GubbeSheet");
            arrowSprite = Content.Load<Texture2D>("Textures/arrow");
            arrowSpriteSheet = Content.Load<Texture2D>("Textures/ArrowSheet");
            fontDefault = Content.Load<SpriteFont>("DefaultFont");
            xpLVL = Content.Load<SpriteFont>("expandlevel");
            enemyTexture = Content.Load<Texture2D>("Textures/Char");

            tileSet = Content.Load<Texture2D>("Textures/TileSets/FIXEDZELDA");
            tileSet = Content.Load<Texture2D>("Textures/TileSets/NewFixedzElda");
            


            tiles.Add(Content.Load<Texture2D>("grass_tile"));
            tiles.Add(Content.Load<Texture2D>("imgres"));
            tiles.Add(Content.Load<Texture2D>("Textures/water"));

            currentState = GameState.Menu;
            playButton = new Vector2(graphics.PreferredBackBufferWidth / 2 - play.Width / 2, graphics.PreferredBackBufferHeight / 2 - play.Height / 2);
            playBut = new Rectangle((int)playButton.X, (int)playButton.Y, (int)play.Width, (int)play.Height);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // TODO: Add your update logic here
            //Rektanglar / Hitboxes
            //Rectangle walkBox = new Rectangle(Convert.ToInt32(charPlace.X), Convert.ToInt32(charPlace.Y), character.Width, character.Height);
            charBox = new Rectangle(Convert.ToInt32(charPlace.X), Convert.ToInt32(charPlace.Y), character.Width, character.Height);
            Rectangle enemyBox = new Rectangle(Convert.ToInt32(enemyPlace.X), Convert.ToInt32(enemyPlace.Y), enemy.Width, enemy.Height);

            if (health <= 0)
                currentState = GameState.GameOver;
            
            charTex = new Rectangle(currentTextureX * 32, currentTextureY * 32, 32, 32);
            maxX = graphics.GraphicsDevice.Viewport.Width - charWidth;
            maxY = graphics.GraphicsDevice.Viewport.Height - charHeight;
            minX = 0;
            minY = 0;
            mus = Mouse.GetState();
            musRec = new Rectangle(mus.X, mus.Y, 1, 1);

            //För att man ej ska kunna gå ut ur kanten på skärmen
            if (charPlace.X > maxX)
            {
                charPlace.X = maxX;
            }
            if (charPlace.X < minX)
            {
                charPlace.X = minX;
            }
            if (charPlace.Y > maxY)
            {
                charPlace.Y = maxY;
            }
            if (charPlace.Y < minY)
            {
                charPlace.Y = minY;
            }

            //Gamestates, för meny + paus osv
            switch (currentState)
            {
                case GameState.Paused:
                    PausedUpdate();
                    break;
                case GameState.Menu:
                    MenuUpdate();
                    break;
                case GameState.Playing:
                    PlayingUpdate(gameTime,1);
                    break;
                case GameState.Level1:
                    PlayingUpdate(gameTime, 1);
                    break;
                case GameState.Level2:
                    PlayingUpdate(gameTime, 2);
                    break;
                case GameState.Level3:
                    PlayingUpdate(gameTime, 3);
                    break;
                case GameState.Level4:
                    PlayingUpdate(gameTime, 4);
                    break;
                case GameState.GameOver:
                    GameOverUpdate();
                    break;
            
            }
            base.Update(gameTime);
        }

        public void PausedUpdate()
        {
        }
        public void MenuUpdate()
        {

            if (mus.LeftButton == ButtonState.Pressed)
                if (musRec.Intersects(playBut))
                {
                    currentState = GameState.Level1;
                }
        }
        public void PlayingUpdate(GameTime gameTime,int level)
        {
            Movement.Update(gameTime);
            
            Attack.Update(gameTime);

            Player.Update(gameTime);

            if (exp >= expToLevel)
            {
                //Player.LevelUp();//fixalevelup
                lvl += 1;
                exp = 0;
                expToLevel += expToLevel;
                
            }

            foreach (Arrow a in arrows)
            {
                a.Update(gameTime);
            }
            for (int i = 0; i < arrows.Count(); i++)
            {
                if (!arrows[i].isShooting)
                    arrows.RemoveAt(i);
            }

            //Om siffran i map'en är 1, läggs en kollisionsrektangel till, annars ej.
            #region ColMap and Change Level
            switch (level)
            {
                case 1:
                    if (charPlace.X > GraphicsDevice.Viewport.Width - character.Width)
                    {
                        currentState = GameState.Level2;
                        charPlace.X = 0;
                        collisionTiles.Clear();
                        for (int y = 0; y < Maps.colmap2.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.colmap2.GetLength(1); x++)
                            {
                                if (Maps.colmap2[y, x] != 0)
                                {
                                    collisionTiles.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                                }
                            }
                        }
                        foreach (Enemy e in enemies2)
                        {
                            if (e.hP == 0)
                            {
                                e.hP = 10;
                                e.alive = true;
                            }
                        }
                    }
                    foreach (Enemy e1 in enemies1)
                    {
                        e1.Update(gameTime);
                    }
                    foreach (Item i in items1)
                    {
                        i.Update(gameTime);
                    }
                    break;
                case 2:
                    if (charPlace.X == 0)
                    {
                        currentState = GameState.Level1;
                        charPlace.X = GraphicsDevice.Viewport.Width - character.Width;
                        collisionTiles.Clear();
                        for (int y = 0; y < Maps.colmap1.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.colmap1.GetLength(1); x++)
                            {
                                if (Maps.colmap1[y, x] != 0)
                                {
                                    collisionTiles.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                                }
                            }
                        }
                        foreach (Enemy e in enemies1)
                        {
                            if (e.hP == 0)
                            {
                                e.hP = 10;
                                e.alive = true;
                            }
                        }
                    }
                    if (charPlace.X > GraphicsDevice.Viewport.Width - character.Width)
                    {
                        currentState = GameState.Level3;
                        charPlace.X = 0;
                        collisionTiles.Clear();
                        for (int y = 0; y < Maps.colmap3.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.colmap3.GetLength(1); x++)
                            {
                                if (Maps.colmap3[y, x] != 0)
                                {
                                    collisionTiles.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                                }
                            }
                        }
                        foreach (Enemy e in enemies3)
                        {
                            if (e.hP == 0)
                            {
                                e.hP = 10;
                                e.alive = true;
                            }
                        }
                    }
                    foreach (Enemy e2 in enemies2)
                    {
                        e2.Update(gameTime);
                    }
                    break;
                case 3:
                    if (charPlace.X == 0)
                    {
                        currentState = GameState.Level2;
                        charPlace.X = GraphicsDevice.Viewport.Width - character.Width;
                        collisionTiles.Clear();
                        for (int y = 0; y < Maps.colmap2.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.colmap2.GetLength(1); x++)
                            {
                                if (Maps.colmap2[y, x] != 0)
                                {
                                    collisionTiles.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                                }
                            }
                        }
                    }
                    if (charPlace.X > GraphicsDevice.Viewport.Width - character.Width)
                    {
                        currentState = GameState.Level4;
                        charPlace.X = 0;
                        collisionTiles.Clear();
                        for (int y = 0; y < Maps.colmap4.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.colmap4.GetLength(1); x++)
                            {
                                if (Maps.colmap4[y, x] != 0)
                                {
                                    collisionTiles.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                                }
                            }
                        }
                        foreach (Enemy e in enemies4)
                        {
                            if (e.hP == 0)
                            {
                                e.hP = 10;
                                e.alive = true;
                            }
                        }
                    }
                    foreach (Enemy e3 in enemies3)
                    {
                        e3.Update(gameTime);
                    }
                    break;
                case 4:
                    if (charPlace.X <= 0)
                    {
                        currentState = GameState.Level3;
                        charPlace.X = GraphicsDevice.Viewport.Width - character.Width;
                        collisionTiles.Clear();
                        for (int y = 0; y < Maps.colmap3.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.colmap3.GetLength(1); x++)
                            {
                                if (Maps.colmap3[y, x] != 0)
                                {
                                    collisionTiles.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                                }
                            }
                        }
                    }
                    foreach (Enemy e4 in enemies4)
                    {
                        e4.Update(gameTime);
                    }
                    break;
            }
            #endregion
            Invincible(gameTime);
        }
        public void GameOverUpdate()
        {
            if (mus.LeftButton == ButtonState.Pressed)
                if (musRec.Intersects(playBut))
                {
                    health = 10;
                    charPlace.X = 1 * gameSize;
                    charPlace.Y = 1 * gameSize;
                    currentState = GameState.Level1;
                }
        }

        //För att man inte ska kunna ta skada "snabbt", man blir odödlig i cirka 750ms
        public static bool Invincible(GameTime gameTime)
        {
            
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer < interval)
                return true;
            else
                return false;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //spriteBatch.Draw(enemy, enemyPlace, Color.White);
            //Ritar bakgrunden, eller det understa, alltså mapen
            #region MAP/Bottom
            if (currentState == GameState.Playing)
            {
                
            }
            if (currentState == GameState.Level1)
            {
                for (int y = 0; y < Maps.map1.GetLength(0); y++)
                {
                    for (int x = 0; x < Maps.map1.GetLength(1); x++)
                    {
                        //spriteBatch.Draw(tiles[Maps.map1[y,x]], new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(5,5,25,25), Color.White);
                        
                        int currentTileY = (int)Maps.map1[y,x] / 39;
                        int currentTileX;
                        if (Maps.map1[y, x] > 39)
                            currentTileX = Maps.map1[y, x] % 39 - 1;
                        else
                            currentTileX = Maps.map1[y, x] - 1;
                        spriteBatch.Draw(tileSet, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(currentTileX * 16, currentTileY * 16, 16, 16), Color.White);
                    }
                }
            }
            if (currentState == GameState.Level2)
            {
                for (int y = 0; y < Maps.map2.GetLength(0); y++)
                {
                    for (int x = 0; x < Maps.map2.GetLength(1); x++)
                    {
                        //spriteBatch.Draw(tiles[Maps.map1[y,x]], new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(5,5,25,25), Color.White);
                        
                        int currentTileY = (int)Maps.map2[y, x] / 39;
                        int currentTileX;
                        if (Maps.map2[y, x] > 39)
                            currentTileX = Maps.map2[y, x] % 39 - 1;
                        else
                            currentTileX = Maps.map2[y, x] - 1;
                        spriteBatch.Draw(tileSet, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(currentTileX * 16, currentTileY * 16, 16, 16), Color.White);  
                    }
                }
            }
            if (currentState == GameState.Level3)
            {
                for (int y = 0; y < Maps.map3.GetLength(0); y++)
                {
                    for (int x = 0; x < Maps.map3.GetLength(1); x++)
                    {
                        //spriteBatch.Draw(tiles[Maps.map1[y,x]], new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(5,5,25,25), Color.White);
                        
                        int currentTileY = (int)Maps.map3[y, x] / 39;
                        int currentTileX;
                        if (Maps.map3[y, x] > 39)
                            currentTileX = Maps.map3[y, x] % 39 - 1;
                        else
                            currentTileX = Maps.map3[y, x] - 1;
                        spriteBatch.Draw(tileSet, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(currentTileX * 16, currentTileY * 16, 16, 16), Color.White);
                    }
                }
            }
            if (currentState == GameState.Level4)
            {
                for (int y = 0; y < Maps.map4.GetLength(0); y++)
                {
                    for (int x = 0; x < Maps.map4.GetLength(1); x++)
                    {
                        //spriteBatch.Draw(tiles[Maps.map1[y,x]], new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(5,5,25,25), Color.White);
                        
                        int currentTileY = (int)Maps.map4[y, x] / 39;
                        int currentTileX;
                        if (Maps.map4[y, x] > 39)
                            currentTileX = Maps.map4[y, x] % 39 - 1;
                        else
                            currentTileX = Maps.map4[y, x] - 1;
                        spriteBatch.Draw(tileSet, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(currentTileX * 16, currentTileY * 16, 16, 16), Color.White);
                    }
                }
            }
#endregion
            if (currentState == GameState.Menu)
            {
                //Ritar menyn
                spriteBatch.Draw(play, new Vector2(graphics.PreferredBackBufferWidth / 2 - play.Width / 2, graphics.PreferredBackBufferHeight / 2 - play.Height / 2), Color.White);
            }
            if (currentState == GameState.GameOver)
            {
                //Ritar gameover skärmen
                spriteBatch.Draw(play, new Vector2(graphics.PreferredBackBufferWidth / 2 - play.Width / 2, graphics.PreferredBackBufferHeight / 2 - play.Height / 2), Color.White);
                spriteBatch.DrawString(fontDefault, gameOver, new Vector2(GraphicsDevice.Viewport.Width / 2 - (fontDefault.MeasureString(gameOver).X / 2), GraphicsDevice.Viewport.Height / 2 - (fontDefault.MeasureString(gameOver).Y) - 50), Color.Red);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //ritar saker som är högst upp, fiender, player, pilar
            #region onTopDrawing
            if (currentState == GameState.Playing || currentState == GameState.Level1 || currentState == GameState.Level2 || currentState == GameState.Level3 || currentState == GameState.Level4)
            {
                spriteBatch.Draw(charSheet, new Rectangle((int)(charPlace.X) + charWidth, (int)(charPlace.Y) + charHeight, (int)(0.75 * gameSize), (int)(0.75 * gameSize)), charTex, Color.White);
               
                foreach (Arrow a in arrows)
                    a.Draw(spriteBatch);

                spriteBatch.Draw(healthTexture, new Rectangle(gameSize, gameSize/2 - healthOutline.Height/2, health * 10, 10), Color.White);
                spriteBatch.Draw(healthOutline, new Rectangle(gameSize, gameSize/2 - healthOutline.Height/2, 100, 10), Color.White);
                spriteBatch.DrawString(xpLVL, "Level: " + lvl.ToString(), new Vector2(GraphicsDevice.Viewport.Width - xpLVL.MeasureString("Level: x").X - 35, Game1.gameSize / 2 + (xpLVL.MeasureString("Level: x").Y) / 2), Color.Black);
                spriteBatch.DrawString(xpLVL, "Exp: " + exp.ToString() + "/" +expToLevel.ToString(), new Vector2(GraphicsDevice.Viewport.Width - xpLVL.MeasureString("Level: x").X - 35, Game1.gameSize / 2 + (xpLVL.MeasureString("Level: x").Y) + 5), Color.Black);
            }
            if (currentState == GameState.Level1)
            {
                foreach (Enemy e1 in enemies1)
                {
                    e1.Draw(spriteBatch);
                }
                foreach (Item i in items1)
                {
                    i.Draw(spriteBatch);
                }
            }
            if (currentState == GameState.Level2)
            {
                foreach (Enemy e2 in enemies2)
                {
                    e2.Draw(spriteBatch);
                }
            }
            if (currentState == GameState.Level3)
            {
                foreach (Enemy e3 in enemies3)
                {
                    e3.Draw(spriteBatch);
                }
            }
            if (currentState == GameState.Level4)
            {
                foreach (Enemy e4 in enemies4)
                {
                    e4.Draw(spriteBatch);
                }
            }
            #endregion
            spriteBatch.End();

            //Full override drawing, allting som skall vara HELT på toppen. Överlappa ALLT. T.ex specscreen
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            Player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
            
        }
    }
}
