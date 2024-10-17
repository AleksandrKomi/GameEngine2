using GameEngine.Decorations;
using GameEngine.Interfeces;
using GameEngine.Messages;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Characters
{
    public class Player : IPhysicalSprite, ICollidable
    {
        public const int PlayerSize = 64;
        private readonly string _nickname;
        private int _xp;

        public int X { get; private set; }
        public int Y { get; private set; } = 600;
        public Direction CurrentDirection = Direction.Right;
        public int Height => PlayerSize;
        public int Width => PlayerSize;

        // Resources

        private Image _playerImage;
        private readonly Image _playerLeft;
        private readonly Image _playerRight;

        public Player(string nickname)
        {
            _nickname = nickname;
            _xp = 50;

            // Load resources

            _playerImage = Image.FromFile("Resources/player.png");
            _playerLeft = Image.FromFile("Resources/playerLeft.png");
            _playerRight = _playerImage;

        }

        public void Draw(Graphics g, Rectangle bounds)
        {
            // Draw here

            g.DrawImage(_playerImage, X, bounds.Bottom - Y - PlayerSize, PlayerSize, PlayerSize);

            Pen blackPen = new Pen(Color.Black, 1);
            if (_xp > 0)
            {
                g.DrawRectangle(blackPen, X + 15, bounds.Bottom - Y - Height - 15, 50, 10);
            }

            SolidBrush redBrush = new SolidBrush(Color.Green);
            int widthXP = _xp;

            if (_xp < 50)
            {
                widthXP -= 5;
            }

            g.FillRectangle(redBrush, X + 15, bounds.Bottom - Y - Height - 15, widthXP, 10);

        }

        public void ProcessPhysics()
        {
            if (Y > Land.BlockSize)
                Y -= 5;
        }

        public void MoveLeft()
        {
            CurrentDirection = Direction.Left;
            _playerImage = _playerLeft;
            X -= 10;
        }

        public void MoveRight()
        {
            CurrentDirection = Direction.Right;
            _playerImage = _playerRight;
            X += 10;
        }

        public void MoveUp()
        {
            if (CurrentDirection == Direction.Right)
            {
                while (Y < Land.BlockSize + 80)
                {
                    Y += 1;
                    X += 1;
                }
            }
            else if (CurrentDirection == Direction.Left)
            {
                while (Y < Land.BlockSize + 80)
                {
                    Y += 1;
                    X -= 1;
                }
            }
        }

        public void OnCollision(ICollider collider)
        {
            if (collider is Bullet)
            {
                _xp -= 8;
            }
        }

        public bool CanCollide(ICollider collider)
        {
            return collider is Bullet;
        }
    }
    
}
