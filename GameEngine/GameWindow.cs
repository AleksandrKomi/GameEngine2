using GameEngine.Characters;
using GameEngine.Decorations;
using GameEngine.Interfeces;
using GameEngine.Messages;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace GameEngine
{
    public partial class GameWindow : Form
    {
        private readonly System.Timers.Timer _physicsUpdateTimer;
        private readonly BufferedGraphics _bufferedGraphics;

        private int _framesCount;
        private int _fps;
        private readonly Counter _counter;
        
        private readonly Player _player;
        private readonly Land _land;
        private readonly List<ISprite> _sprites;
        bool frame = false;

        // Construcor

        public GameWindow()
        {
            InitializeComponent();
            _bufferedGraphics = BufferedGraphicsManager.Current.Allocate(CreateGraphics(), DisplayRectangle);
            _physicsUpdateTimer = new System.Timers.Timer(10);
            _physicsUpdateTimer.Elapsed += _physicsUpdateTimer_Elapsed;
            
            _player = new Player("Stiv");
            ISprite opponent  = new Opponent(50);
            _land = new Land();
            _sprites = new List<ISprite>();
            _counter = new Counter();
            _sprites.Add(_player);
            _sprites.Add(opponent);
            _sprites.Add(_land);
            _sprites.Add(_counter);

            // Start timers

            UpdateScreenTimer.Start();
            FPSCounterTimer.Start();
            _physicsUpdateTimer.Start();

            // Subscriptions

            MessageBus.Instantce.Subscribe<OpponentDiedMessage>(OpponentDiedMessageHandler);
        }

        // Methods

        public void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {

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
            }
            else if (e.KeyCode == Keys.Down)
            {
                Fire fire = new Fire(_player);
                _sprites.Add(fire);
            }
            else if (e.KeyCode == Keys.Space)
            {
                Ice ice = new Ice(_player);
                _sprites.Add(ice);
            }
            else if (e.KeyCode == Keys.End)
            {
                frame = !frame;
            }
        }

        private void ProcessPhysics()
        {     
            // ���������� 100 ��� � ���.

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
                            if (colliderSprite.X >= collidableSprite.X && colliderSprite.X <= collidableSprite.X + collidableSprite.Width && colliderSprite.Y 
                                <= collidableSprite.Y + collidableSprite.Height)
                            {
                                if (colliderSprite.IsDisappearsOnCollision)
                                   _sprites.Remove(sprite);

                                collidableSprite.OnCollision(colliderSprite);
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
                sprite.Draw(graphics, DisplayRectangle);

                // �������� ������ ��������� ����������� 

                if (sprite is ICollidable collidableSprite)
                {
                    Pen blackPen = new Pen(Color.Black, 1);

                    if (frame == true)
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

        private void OpponentDiedMessageHandler(OpponentDiedMessage message) 
        {
            // TODO: �������� ��������� ������ ���������

            _sprites.Remove(message.Opponent);
            
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