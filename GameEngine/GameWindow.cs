using GameEngine.Characters;
using GameEngine.Decorations;
using GameEngine.Extensions;
using GameEngine.Interfeces;
using GameEngine.Messages;
using GameEngine.Primitives.Enums;
using NAudio.Wave;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Security.Cryptography.X509Certificates;

namespace GameEngine
{
    public partial class GameWindow : Form
    {
        private readonly System.Timers.Timer _physicsUpdateTimer;
        private readonly BufferedGraphics _bufferedGraphics;
   
        private int _framesCount;
        private int _fps;
        public /*readonly*/ Counter _counter; 
        private readonly GameOver _gameover;
        private /*readonly*/ Player _player;
        private /*readonly*/ Land _land;
        private readonly List<ISprite> _sprites;
        private bool _showCollizionFrame = false;
        private DateTime lastShot = DateTime.MinValue;
        private readonly WaveOutEvent _outputDevice;


        // Construcor

        public GameWindow()
        {
            InitializeComponent();
            _bufferedGraphics = BufferedGraphicsManager.Current.Allocate(CreateGraphics(), DisplayRectangle);
            _physicsUpdateTimer = new System.Timers.Timer(10);
            _physicsUpdateTimer.Elapsed += _physicsUpdateTimer_Elapsed;
            _sprites = new List<ISprite>();

            CreateWorld();

            _outputDevice = new WaveOutEvent();


            //_player = new Player("Stiv");
            /*ISprite opponent = new Opponent(50, _counter, _player);
            _land = new Land();
            _counter = new Counter();

            _sprites.Add(_player);
            _sprites.Add(opponent);
            _sprites.Add(_land);
            _sprites.Add(_counter);*/

            //opponent.WriteDebugInfo(true);

            // Start timers

            UpdateScreenTimer.Start();
            FPSCounterTimer.Start();
            _physicsUpdateTimer.Start();

            // Subscriptions

            MessageBus.Instantce.Subscribe<OpponentDiedMessage>(OpponentDiedMessageHandler);
            MessageBus.Instantce.Subscribe<PlayerDiedMessage>(PlayerDiedMessageHandler);
            MessageBus.Instantce.Subscribe<OpponentShootsMessage>(OpponentShootsMessageHandler);
        }

        public void CreateWorld()
        {
            
            _sprites.Clear();
            
            var enterNameWindow = new EnterNameWindow();
            DialogResult result =  enterNameWindow.ShowDialog();

            string playerName = result == DialogResult.OK
                ? enterNameWindow.PlayerName
                : "Stiv";

            PlaySound("Resources/start2.mp3");
            _player = new Player(playerName);
            _counter = new Counter();
            _land = new Land();

            //_sprites.Add(player);
            _sprites.Add(_player);
            //_player._xp = 50;
            ISprite opponent = new Opponent(50);
            _sprites.Add(opponent);
            _sprites.Add(_land);
            _sprites.Add(_counter);

            PlaySound("Resources/fon.mp3");
        }

        // Methods

        public void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            Player player = _player;
                       
            if (e.KeyCode == Keys.Left)
            {
                _player.MoveLeft();
            }
            else if (e.KeyCode == Keys.Right)
            {
                _player.MoveRight();
            }
            else if (e.KeyCode == Keys.Up)
            {
                _player.MoveUp();

                PlaySound("Resources/jump1.mp3");
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (DateTime.Now - lastShot > TimeSpan.FromSeconds(2) && _player._xp > 0)
                {
                    Fire fire = new Fire(_player);
                    PlaySound("Resources/shot.mp3");
                    _sprites.Add(fire);
                    lastShot = DateTime.Now;
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (DateTime.Now - lastShot > TimeSpan.FromSeconds(1) && _player._xp > 0)
                {
                    Ice ice = new Ice(_player);
                    PlaySound("Resources/shot.mp3");
                    _sprites.Add(ice);
                    lastShot = DateTime.Now;
                }
            }
            else if (e.KeyCode == Keys.End)
            {
                _showCollizionFrame = !_showCollizionFrame;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                CreateWorld();
            }

        }

        private void ProcessPhysics()
        {
            // Вызывается 100 раз в сек.

            ISprite[] sprites = _sprites.ToArray();

            foreach (ISprite sprite in sprites)
            {
                if (sprite is IPhysicalSprite physicalSprite)
                {
                    physicalSprite.ProcessPhysics();
                }

                if (sprite is IDisappearableSprite disappearableSprite)
                {

                    if (disappearableSprite.X > 800 || disappearableSprite.X < 0)
                    {
                        _sprites.Remove(sprite);
                    }

                }
                if (sprite is ICollider colliderSprite)
                {
                    foreach (ISprite s in sprites)
                    {
                        if (s is ICollidable collidableSprite)
                        {
                            if (collidableSprite.CanCollide(colliderSprite) == false)
                                continue;

                            if (colliderSprite.X >= collidableSprite.X && colliderSprite.X <= collidableSprite.X + collidableSprite.Width &&
                                colliderSprite.Y >= collidableSprite.Y && colliderSprite.Y <= collidableSprite.Y + collidableSprite.Height)
                            {
                                if (colliderSprite.IsDisappearsOnCollision)
                                    _sprites.Remove(sprite);

                                collidableSprite.OnCollision(colliderSprite);

                                PlaySound("Resources/hit.mp3");

                                System.Diagnostics.Debug.WriteLine("hit");

                            }

                        }
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Prepare

            Graphics graphics = _bufferedGraphics.Graphics;
            graphics.Clear(Color.Azure);

            Font fpsFont = new Font("Ariel", 16);

            ISprite[] sprites = _sprites.ToArray();

            foreach (ISprite sprite in sprites)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                sprite.Draw(graphics, DisplayRectangle);
                stopwatch.Stop();
                //System.Diagnostics.Debug.WriteLine($"sprite {sprite} draw time:  {stopwatch.Elapsed.TotalMilliseconds}" );


                // добавить логику отрисовки коллайдеров 

                if (sprite is ICollidable collidableSprite)
                {
                    Pen blackPen = new Pen(Color.Black, 1);

                    if (_showCollizionFrame == true)
                    {
                        graphics.DrawRectangle(blackPen, collidableSprite.X, DisplayRectangle.Bottom - collidableSprite.Y - collidableSprite.Height,
                        collidableSprite.Width, collidableSprite.Height);
                    }
                }
            }

            // Draw FPS

            graphics.DrawString($"FPS: {_fps}", fpsFont, Brushes.Red, DisplayRectangle.Right - 100, 10);

            // Render

            _bufferedGraphics.Render();

            _framesCount++;

        }

        private void PlaySound(string fileName)
        {
            var outputDevice = new WaveOutEvent();
            var audioFile = new AudioFileReader(fileName);

            outputDevice.Init(audioFile);
            outputDevice.Play();
            
        }

        // Handlers

        private void OpponentShootsMessageHandler(OpponentShootsMessage message)
        {
            Random rnd = new Random();
            int shoot = rnd.Next(10);

            if (shoot < 4)
            {
                if (message.Opponent is Boss)
                {
                    Bomb bomb = new Bomb(message.Opponent, _player);
                    _sprites.Add(bomb);
                }
                else if (message.Opponent is RobotOpponent)
                {
                    if (shoot < 7)
                    {
                        PlaySound("Resources/laser.mp3");

                        Laser laser = new Laser(message.Opponent, _player);
                        _sprites.Add(laser);
                    }
                }
                else if (message.Opponent is Opponent)
                {
                    Bullet bullet = new Bullet(message.Opponent, _player);

                    PlaySound("Resources/bullet.mp3");

                    _sprites.Add(bullet);
                }
            }
               
                        
        }

        private void OpponentDiedMessageHandler(OpponentDiedMessage message)
        {
            // TODO: добавить обработку смерти оппонента

            PlaySound("Resources/frag.mp3");

            _sprites.Remove(message.Opponent);

            _counter.Count++;

            ISprite opponent;

            if (_counter.Count % 5 == 0 && _counter.Count > 0)
            {
                opponent = new Boss(100);
                _player._xp = _player._xp + 30;

                PlaySound("Resources/xp.mp3");
            }
            else if (_counter.Count % 3 == 0 && _counter.Count > 0)
            {
                opponent = new RobotOpponent(75);
            }
            else if (_player._xp >= 80)
            {
                opponent = new Dino(50);
            }
            else
            {
                opponent = new Opponent(50);
            }
            
            _sprites.Add(opponent);
        }

        private async void PlayerDiedMessageHandler(PlayerDiedMessage message)
        {
            PlaySound("Resources/game-over.mp3");
            _sprites.Remove(message.Player);
            System.Diagnostics.Debug.WriteLine($"Game Over, Ваш результат: {_counter.Count}");
                       
            _sprites.Clear();
            
            GameOver gameover = new GameOver(_counter);
            _sprites.Add(gameover);
        
            // new EnterNameWindow(_counter.Count).ShowDialog();
            await LeaderAPI.PostLeader(_player.Nickname, _counter.Count);
            new LeaderBoard().ShowDialog();
                
            // _sprites.Remove(_counter);
        }
        
        #region Timers

        private void UpdateScreenTimer_Tick(object sender, EventArgs e)
        {
            InvokePaint(this, null);
        }

        private void FPSCounterTimer_Tick(object sender, EventArgs e)
        {
            _fps = _framesCount;
            _framesCount = 0;
        }

        private void _physicsUpdateTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            ProcessPhysics();
        }

        #endregion
    }
}