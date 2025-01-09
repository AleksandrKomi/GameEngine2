using GameEngine.Decorations;
using GameEngine.Interfeces;
using GameEngine.Messages;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Characters
{
    public abstract class BaseOpponent : IPhysicalSprite, ICollidable, IPositionable
    {
        public BaseOpponent(int xp, string texturePath, string textureLeftPath)
        {
            // Load resources

            _xp = xp;
            //_counter = count;

            OpponentImage = Image.FromFile(texturePath);
            OpponentLeftImage = Image.FromFile(textureLeftPath);
            _currentDirectionOpponentImage = OpponentLeftImage;


        }

        public Image _currentDirectionOpponentImage;
        public Counter _counter;
        public int _xp;
        public abstract int OpponentSize { get; }
        private DateTime lastJump = DateTime.MinValue;
        private DateTime lastShoot = DateTime.MinValue;
        public int X { get; protected set; } = 500;

        public int Y { get; protected set; } = 600;

        public int Height => OpponentSize;

        public int Width => OpponentSize;

        protected Image OpponentImage { get; }
        protected Image OpponentLeftImage { get; }

        Direction IPositionable.Direction { get => CurrentDirection; }

        public Direction CurrentDirection = Direction.Left;

        public bool CanCollide(ICollider collider)
        {
            return collider is Fire || collider is Ice;
        }

       
        public void OnCollision(ICollider collider)
        {
            if (collider is Fire)
            {
                _xp -= 22;
            }
            else if (collider is Ice)
            {
                _xp -= 13;
            }

            if (_xp > 0)
            {
               Shoot();
            }

            if (_xp <= 0)
            {
                MessageBus.Instantce.Publish(new OpponentDiedMessage(this));
            }
        }

        protected void Shoot()
        {
            MessageBus.Instantce.Publish(new OpponentShootsMessage(this));
        }

       protected virtual void Jump()
       {

            if (CurrentDirection == Direction.Left)
            {
                Y += 100;
                X -= 50;
            }
            else if (CurrentDirection == Direction.Right)
            {
                Y += 100;
                X += 50;
            }
       }

        public virtual void ProcessPhysics()
        {
            // Вызывается 100 раз в сек.

            if (Y > Land.BlockSize)
            {
                Y -= 5;
            }

            if (Y < Land.BlockSize + 45 && _xp >= 0)
            {
                Travel();

                if (X < 300)
                {
                    CurrentDirection = Direction.Right;
                }
                else if (X > 750)
                {
                    CurrentDirection = Direction.Left;
                }

                Random rnd = new Random();
                int hop = rnd.Next(101);

                if (hop > 80 && DateTime.Now - lastJump > TimeSpan.FromSeconds(4))
                {
                    Jump();
                    lastJump = DateTime.Now;
                }

                if (DateTime.Now - lastShoot > TimeSpan.FromSeconds(3))
                {
                    Shoot();
                    lastShoot = DateTime.Now;
                }

            }
        }

        protected virtual void Travel()
        {
            if (CurrentDirection == Direction.Left)
            {
                Random rnd = new Random();
                int speed = rnd.Next(5);
                _currentDirectionOpponentImage = OpponentLeftImage;
                X -= speed;
            }
            else if (CurrentDirection == Direction.Right)
            {
                X += 2;
                _currentDirectionOpponentImage = OpponentImage;
            }
        }

        public void Draw(Graphics g, Rectangle bounds)
        {

            g.DrawImage(_currentDirectionOpponentImage, X, bounds.Bottom - Y - OpponentSize, OpponentSize, OpponentSize);
            Pen blackPen = new Pen(Color.Black, 1);

            if (_xp < 50)
            {
                g.DrawRectangle(blackPen, X + 15, bounds.Bottom - Y - Height - 15, 50, 10);
            }

            SolidBrush redBrush = new SolidBrush(Color.Red);
            int widthXP = _xp;

            if (_xp < 50)
            {
                widthXP -= 5;
            }

            g.FillRectangle(redBrush, X + 15, bounds.Bottom - Y - Height - 15, widthXP, 10);

        }
    }
}
