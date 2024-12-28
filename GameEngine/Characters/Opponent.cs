using GameEngine.Decorations;
using GameEngine.Interfeces;
using GameEngine.Messages;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Characters
{
    public class Opponent : /*IPhysicalSprite, ICollidable, IPositionable,*/ BaseOpponent
    {
        //public int OpponentSize = 75;
        // private int _xp;

        /*public int X { get; private set; } = 500;*/
        /* public int Y { get; private set; } = 600;*/

        /* public string Text
         {
             get => text;
             private set
             {
                 text = value;
             }
         }*/

        /*public int Height => OpponentSize;*/
        /*public int Width => OpponentSize;*/

        //public Direction _currentDirection = Direction.Left;
        /* private Image _opponent;
         private Image _opponentLeft;
         private Image _opponentRight;*/
        /*private DateTime lastJump = DateTime.MinValue;
        private DateTime lastShoot = DateTime.MinValue;*/
        // private string text;
        //public Counter _counter;
        // private IPositionable _player;
        // private readonly Bullet _bullet;

        private Opponent opponent;

        public Opponent(int xp)
            : base(xp, "Resources/opponent.png", "Resources/opponentLeft.png")
        {
            // Load resources
                                             
        }

        public override int OpponentSize => 75;

        /*public void Draw(Graphics g, Rectangle bounds)
        {

            g.DrawImage(_opponent, X, bounds.Bottom - Y - OpponentSize, OpponentSize, OpponentSize);
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

        }*/

        /* public void ProcessPhysics()
         {
             // Вызывается 100 раз в сек.

             _opponent = _opponentLeft;

             if (Y > Land.BlockSize)
             {
                 Y -= 5;
             }

             if (Y < Land.BlockSize + 45 && _xp >= 0)
             {
                 Travel();

                 if (X < 300)
                 {
                     _currentDirection = Direction.Right;
                 }
                 else if (X > 750)
                 {
                     _currentDirection = Direction.Left;
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
         }*/
        /* public void Jump()
         {

             if (_currentDirection == Direction.Left)
             {
                 Y += 100;
                 X -= 50;
             }
             else if (_currentDirection == Direction.Right)
             {
                 Y += 100;
                 X += 50;
             }
         }*/

        /*public void Travel()
        {

            if (_currentDirection == Direction.Left)
            {
                Random rnd = new Random();
                int speed = rnd.Next(5);
                _opponent = _opponentLeft;
                X -= speed;
            }
            else if (_currentDirection == Direction.Right)
            {
                _opponent = _opponentRight;
                X += 2;
            }
        }*/

        /*public void OnCollision(ICollider collider)
        {

            if (collider is Fire)
            {
                _xp -= 12;
            }
            else if (collider is Ice)
            {
                _xp -= 20; //7;  
            }

            if (_xp > 0)
            {
                Shoot();
            }

            if (_xp <= 0)
            {
                MessageBus.Instantce.Publish(new OpponentDiedMessage(this));
            }
        }*/

        /* public bool CanCollide(ICollider collider)
         {
             return collider is Fire || collider is Ice;
         }*/

        /*private void Shoot()
        {
            MessageBus.Instantce.Publish(new OpponentShootsMessage(this));

        }*/

        /*Direction IPositionable.Direction { get => _currentDirection; }*/

    }
}
