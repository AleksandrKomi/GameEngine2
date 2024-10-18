using GameEngine.Characters;
using GameEngine.Interfeces;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Decorations
{
    public class Bullet : IPhysicalSprite, IDisappearableSprite, ICollider
    {
        public bool IsDisappearsOnCollision => true;
        private Image _bulletImage;
        private readonly Image _bulletLeft;
        private readonly Image _bulletRight;
        public const int BulletSize = 30;
        public Direction CurrentDirection = Direction.Right;
        private Opponent _opponent;
        private Player _player;
        
        public int X { get; private set; }

        public int Y { get; private set; }
        
        public Bullet(Opponent opponent, Player player)
        {
            /*this.*/_opponent = opponent;
            _player = player;
            _bulletImage = Image.FromFile("Resources/bullet.png");
            _bulletLeft = Image.FromFile("Resources/bulletLeft.png");
            _bulletRight = _bulletImage;
            X = opponent.X;
            Y = opponent.Y;
           

            /*int currentLocationPlayer = player.X;
            int currentLocationOpponent = opponent.X;*/

            if (player.X  < opponent.X)
            {
                CurrentDirection = Direction.Left;
                _bulletImage = _bulletLeft;
            }
            else if (player.X > opponent.X)
            {
                CurrentDirection = Direction.Right;
                _bulletImage = _bulletRight;
            }

        }
                            
        public void Draw(Graphics g, Rectangle bounds)
        {
            g.DrawImage(_bulletImage, X - BulletSize / 2, bounds.Bottom - Y - BulletSize - BulletSize / 2, BulletSize, BulletSize);
        }

        public void ProcessPhysics()
        {
            PhysicsBullet();
        }

        public void PhysicsBullet()
        {
            
            if (CurrentDirection == Direction.Left)
            {
                _bulletImage = _bulletLeft;
                X -= 5;

                if (_opponent.Y < _player.Y)
                {
                    Y += 1;
                }
            }
            else
            {
                _bulletImage = _bulletRight;
                X += 5;

                if (_opponent.Y > _player.Y)
                {
                    Y -= 1;
                }
            }
                          
        }
                
    }
}
