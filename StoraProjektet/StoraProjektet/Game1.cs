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
            
            for (int y = 0; y < Maps.map1.GetLength(0); y++)
            {
                for (int x = 0; x < Maps.map1.GetLength(1); x++)
                {
                    if (Maps.map1[y, x] == 0 || Maps.map1[y,x] == 2 || Maps.map1[y,x] == 6)
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        #region declarations
        public static int gameSize = 32;
        //Vektorer:
        public static Vector2 charPlace = new Vector2(0, 0);
        Vector2 enemyPlace = new Vector2(150, 150);
        Vector2 playButton;

        //Texturer & rektanglar:
        public static Texture2D character;
        Texture2D enemy;
        Texture2D healthTexture;
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

        GameState currentState = new GameState();
        #endregion
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            enemies1.Add(new Skelett(32, 32, this.Content, 32, 32, 0, 0, 3, 32, 48,0,0));
            enemies1.Add(new Zombie(128, 128, 64, 64, this.Content, 96, 96, 2, 32, 32,0,0));
            enemies1.Add(new Zombie(64, 64, 32, 32, this.Content, 50, 50, 2, 32, 32, 1, 1));
            enemies1.Add(new Zombie(64, 64, 32, 32, this.Content, 32, 64, 2, 32, 32, 2, 2));
            enemies2.Add(new Skelett(256, 256, this.Content, 512, 512, 128, 128, 3, 32, 48,0,0));
            enemies3.Add(new Skelett(64, 64, this.Content, 128, 128, 32, 32, 3, 32, 48,0,0));
            enemies4.Add(new Skelett(100, 100, this.Content, 150, 150, 50, 50, 3, 32, 48,0,0));
            
            currentTextureY = 0;
            currentTextureX = 0;
            // TODO: use this.Content to load your game content here
            character = Content.Load<Texture2D>("Textures/Char");
            enemy = Content.Load<Texture2D>("ENAMI");
            healthTexture = Content.Load<Texture2D>("Textures/Namnlös");
            play = Content.Load<Texture2D>("Textures/play");
            charSheet = Content.Load<Texture2D>("Textures/GubbeSheet");
            arrowSprite = Content.Load<Texture2D>("Textures/arrow");
            arrowSpriteSheet = Content.Load<Texture2D>("Textures/ArrowSheet");
            fontDefault = Content.Load<SpriteFont>("DefaultFont");
            enemyTexture = Content.Load<Texture2D>("Textures/Char");

            tileSet = Content.Load<Texture2D>("Textures/TileSets/tileset");

            tiles.Add(Content.Load<Texture2D>("grass_tile"));
            tiles.Add(Content.Load<Texture2D>("imgres"));
            tiles.Add(Content.Load<Texture2D>("Textures/water"));

            Player p1 = new Player(150, 150);

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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
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
            
            switch (level)
            {
                case 1:
                    if (charPlace.X > GraphicsDevice.Viewport.Width - character.Width)
                    {
                        currentState = GameState.Level2;
                        charPlace.X = 0;
                        collisionTiles.Clear();
                        for (int y = 0; y < Maps.map2.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.map2.GetLength(1); x++)
                            {
                                if (Maps.map2[y, x] == 0 || Maps.map2[y, x] == 2 || Maps.map2[y, x] == 6)
                                {
                                    collisionTiles.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                                }
                            }
                        }
                    }
                    foreach (Enemy e1 in enemies1)
                    {
                        e1.Update(gameTime);
                    }
                    break;
                case 2:
                    if (charPlace.X == 0)
                    {
                        currentState = GameState.Level1;
                        charPlace.X = GraphicsDevice.Viewport.Width - character.Width;
                        collisionTiles.Clear();
                        for (int y = 0; y < Maps.map1.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.map1.GetLength(1); x++)
                            {
                                if (Maps.map1[y, x] == 0 || Maps.map1[y, x] == 2 || Maps.map1[y, x] == 6)
                                {
                                    collisionTiles.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                                }
                            }
                        }
                    }
                    if (charPlace.X > GraphicsDevice.Viewport.Width - character.Width)
                    {
                        currentState = GameState.Level3;
                        charPlace.X = 0;
                        collisionTiles.Clear();
                        for (int y = 0; y < Maps.map3.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.map3.GetLength(1); x++)
                            {
                                if (Maps.map3[y, x] == 0 || Maps.map3[y, x] == 2 || Maps.map3[y, x] == 6)
                                {
                                    collisionTiles.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                                }
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
                        for (int y = 0; y < Maps.map2.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.map2.GetLength(1); x++)
                            {
                                if (Maps.map2[y, x] == 0 || Maps.map2[y, x] == 2 || Maps.map2[y, x] == 6)
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
                        for (int y = 0; y < Maps.map4.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.map4.GetLength(1); x++)
                            {
                                if (Maps.map4[y, x] == 0 || Maps.map4[y, x] == 2 || Maps.map4[y, x] == 6)
                                {
                                    collisionTiles.Add(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
                                }
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
                        for (int y = 0; y < Maps.map3.GetLength(0); y++)
                        {
                            for (int x = 0; x < Maps.map3.GetLength(1); x++)
                            {
                                if (Maps.map3[y, x] == 0 || Maps.map3[y, x] == 2 || Maps.map3[y, x] == 6)
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
            Invincible(gameTime);
        }
        public void GameOverUpdate()
        {
            if (mus.LeftButton == ButtonState.Pressed)
                if (musRec.Intersects(playBut))
                {
                    health = 10;
                    charPlace = new Vector2(0, 0);
                    currentState = GameState.Level1;
                }
        }
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
                        spriteBatch.Draw(tileSet, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(Maps.map1[y, x] * 48, 0, 48, 48), Color.White);
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
                        spriteBatch.Draw(tileSet, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(Maps.map2[y, x] * 48, 0, 48, 48), Color.White);
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
                        spriteBatch.Draw(tileSet, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(Maps.map3[y, x] * 48, 0, 48, 48), Color.White);
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
                        spriteBatch.Draw(tileSet, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), new Rectangle(Maps.map4[y, x] * 48, 0, 48, 48), Color.White);
                    }
                }
            }


            //^Map^

            if (currentState == GameState.Menu)
            {
                spriteBatch.Draw(play, new Vector2(graphics.PreferredBackBufferWidth / 2 - play.Width / 2, graphics.PreferredBackBufferHeight / 2 - play.Height / 2), Color.White);
            }
            if (currentState == GameState.GameOver)
            {
                spriteBatch.Draw(play, new Vector2(graphics.PreferredBackBufferWidth / 2 - play.Width / 2, graphics.PreferredBackBufferHeight / 2 - play.Height / 2), Color.White);
                spriteBatch.DrawString(fontDefault, gameOver, new Vector2(GraphicsDevice.Viewport.Width / 2 - (fontDefault.MeasureString(gameOver).X / 2), GraphicsDevice.Viewport.Height / 2 - (fontDefault.MeasureString(gameOver).Y) - 50), Color.Red);
            }
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            if (currentState == GameState.Playing || currentState == GameState.Level1 || currentState == GameState.Level2 || currentState == GameState.Level3 || currentState == GameState.Level4)
            {
                spriteBatch.Draw(charSheet, new Rectangle((int)(charPlace.X) + charWidth, (int)(charPlace.Y) + charHeight, (int)(0.75 * gameSize), (int)(0.75 * gameSize)), charTex, Color.White);
                Attack.Draw(spriteBatch);
                spriteBatch.Draw(healthTexture, new Rectangle(15, 15, health * 10, 10), Color.White);
            }
            if (currentState == GameState.Level1)
            {
                foreach (Enemy e1 in enemies1)
                {
                    e1.Draw(spriteBatch);
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
            
            spriteBatch.End();
            base.Draw(gameTime);
            
        }
    }
}
