using GameEngine.Characters;
using GameEngine.Decorations;
using GameEngine.Extensions;
using GameEngine.Interfeces;
using GameEngine.Messages;
using GameEngine.Primitives.Enums;
//using NAudio.Wave;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
//using NAudio;
using System.Threading;
using System.Security.Policy;
using System.Drawing.Text;


namespace GameEngine
{
    public partial class GameWindow : Form
    {
        private readonly System.Timers.Timer _physicsUpdateTimer;
        private readonly BufferedGraphics _bufferedGraphics;
   
        private int _framesCount;
        private int _fps;
        public /*readonly*/ Counter _counter; 
        private /*readonly*/ GameOver _gameover;
        private /*readonly*/ Player _player;
        private /*readonly*/ Land _land;
        private FonSky _fonSky;
        private readonly List<ISprite> _sprites;
        private bool _showCollizionFrame = false;
        private DateTime lastShot = DateTime.MinValue;
        //private readonly WaveOutEvent _outputDevice;
        private readonly Dictionary<string, MemoryStream> _cachedAudio;
        private readonly AudioPlaybackEngine _audioPlaybackEngine;
        private readonly CachedSound _shootCachedSound;
        private readonly CachedSound _jumpCachedSound;
        private readonly CachedSound _startCachedSound;
        private readonly CachedSound _hitCachedSound;
        private readonly CachedSound _bulletCachedSound;
        private readonly CachedSound _laserCachedSound;
        private readonly CachedSound _fragCachedSound;
        private readonly CachedSound _xpCachedSound;
        private readonly CachedSound _dinoCachedSound;
        private readonly CachedSound _gameoverCachedSound;
        //private readonly CachedSound _fonCachedSound;
        private LoopStream _fonLoopStream;

        // Construcor

        public GameWindow()
        {
            InitializeComponent();
            _bufferedGraphics = BufferedGraphicsManager.Current.Allocate(CreateGraphics(), DisplayRectangle);
            _physicsUpdateTimer = new System.Timers.Timer(10);
            _physicsUpdateTimer.Elapsed += _physicsUpdateTimer_Elapsed;
            _sprites = new List<ISprite>();

            _cachedAudio = new Dictionary<string, MemoryStream>();
            _audioPlaybackEngine = new AudioPlaybackEngine();
            
            _shootCachedSound = new CachedSound("Resources/shot.mp3");
            _jumpCachedSound = new CachedSound("Resources/jump1.mp3");
            _startCachedSound = new CachedSound("Resources/start.mp3");
            _hitCachedSound = new CachedSound("Resources/hit.mp3");
            _bulletCachedSound = new CachedSound("Resources/bullet.mp3");
            _laserCachedSound = new CachedSound("Resources/laser.mp3");
            _fragCachedSound = new CachedSound("Resources/frag.mp3");
            _xpCachedSound = new CachedSound("Resources/xp.mp3");
            _dinoCachedSound = new CachedSound("Resources/dino.mp3");
            _gameoverCachedSound = new CachedSound("Resources/gameover.mp3");

            CreateWorld();

           // _fonLoopStream = new LoopStream("Resources/fon.mp3");
           // _audioPlaybackEngine.PlayLoopSound(_fonLoopStream);

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

            _fonSky = new FonSky();
            _sprites.Add(_fonSky);
            _audioPlaybackEngine.PlaySound(_startCachedSound);
            _player = new Player(playerName);
            _counter = new Counter();
            _land = new Land();
            _sprites.Add(_player);
            ISprite opponent = new Opponent(50);
            _sprites.Add(opponent);
            _sprites.Add(_land);
            _sprites.Add(_counter);
            _isOver = false;
        }

        // Methods

        public void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            Player player = _player;
            bool _music = true;

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

                _audioPlaybackEngine.PlaySound(_jumpCachedSound);
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (DateTime.Now - lastShot > TimeSpan.FromSeconds(2) && _player._xp > 0)
                {
                    Fire fire = new Fire(_player);
                    _audioPlaybackEngine.PlaySound(_shootCachedSound);
                    _sprites.Add(fire);
                    lastShot = DateTime.Now;
                }
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (DateTime.Now - lastShot > TimeSpan.FromSeconds(1) && _player._xp > 0)
                {
                    Ice ice = new Ice(_player);
                    _audioPlaybackEngine.PlaySound(_shootCachedSound);
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
                if (_isOver == true)
                {
                    CreateWorld();
                    _fonLoopStream = new LoopStream("Resources/fon.mp3");
                    _audioPlaybackEngine.PlayLoopSound(_fonLoopStream);
                }
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                _fonLoopStream.Stop();
                 _music = false;
            }
            else if (e.KeyCode == Keys.PageUp && _music == false)
            {
                _fonLoopStream = new LoopStream("Resources/fon.mp3");
                _audioPlaybackEngine.PlayLoopSound(_fonLoopStream);
                
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

                                _audioPlaybackEngine.PlaySound(_hitCachedSound);
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
                    _audioPlaybackEngine.PlaySound(_bulletCachedSound);
                    _sprites.Add(bomb);
                }
                else if (message.Opponent is RobotOpponent)
                {
                    if (shoot < 7)
                    {
                        _audioPlaybackEngine.PlaySound(_laserCachedSound);
                        Laser laser = new Laser(message.Opponent, _player);
                        _sprites.Add(laser);
                    }
                }
                else if (message.Opponent is Opponent)
                {
                    Bullet bullet = new Bullet(message.Opponent, _player);
                    _audioPlaybackEngine.PlaySound(_bulletCachedSound);
                    _sprites.Add(bullet);
                }
            }         
        }

        private void OpponentDiedMessageHandler(OpponentDiedMessage message)
        {
            // TODO: добавить обработку смерти оппонента

            _audioPlaybackEngine.PlaySound(_fragCachedSound);
            _sprites.Remove(message.Opponent);

            _counter.Count++;

            ISprite opponent;

            if (_counter.Count % 5 == 0 && _counter.Count > 0)
            {
                opponent = new Boss(100);
                _player._xp = _player._xp + 30;
                _audioPlaybackEngine.PlaySound(_xpCachedSound);
            }
            else if (_counter.Count % 3 == 0 && _counter.Count > 0)
            {
                opponent = new RobotOpponent(75);
            }
            else if (_player._xp >= 80)
            {
                opponent = new Dino(50);
                _audioPlaybackEngine.PlaySound(_dinoCachedSound);
            }
            else
            {
                opponent = new Opponent(50);
            }
            
            _sprites.Add(opponent);
        }


        bool _isOver;

        private async void PlayerDiedMessageHandler(PlayerDiedMessage message)
        {
          
            _audioPlaybackEngine.PlaySound(_gameoverCachedSound);

           // _fonLoopStream.Stop();

            _sprites.Remove(message.Player);
            System.Diagnostics.Debug.WriteLine($"Game Over, Ваш результат: {_counter.Count}");

                    
            _sprites.Clear();
            
            GameOver gameover = new GameOver(_counter);
            _sprites.Add(gameover);

            _isOver = true;
        
            // new EnterNameWindow(_counter.Count).ShowDialog();
            await LeaderAPI.PostLeader(_player.Nickname, _counter.Count);
            new LeaderBoard().ShowDialog();
              
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