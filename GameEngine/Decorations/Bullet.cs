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
        public int X { get; private set; }

        public int Y { get; private set; }

        public Bullet(Opponent opponent)
        {
            _bulletImage = Image.FromFile("Resources/bullet.png");
            _bulletLeft = Image.FromFile("Resources/bulletLeft.png");
            _bulletRight = _bulletImage;
            CurrentDirection = Direction.Left;
            X = opponent.X;
            Y = opponent.Y;
          
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
            X -= 10;
            //Y = player.Y;
        }
    }

}
