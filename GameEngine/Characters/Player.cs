using GameEngine.Decorations;
using GameEngine.Interfeces;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Characters
{
    public class Player : IPhysicalSprite
    {
        public const int PlayerSize = 64;
        private readonly string _nickname;

        public int X { get; private set; }
        public int Y { get; private set; } = 600;
        public Direction CurrentDirection = Direction.Right;

        // Resources

        private Image _playerImage;
        private readonly Image _playerLeft;
        private readonly Image _playerRight;
                
        public Player(string nickname)
        {
            _nickname = nickname;

            // Load resources

            _playerImage = Image.FromFile("Resources/player.png");
            _playerLeft = Image.FromFile("Resources/playerLeft.png");
            _playerRight = _playerImage;

        }

        public void Draw(Graphics g, Rectangle bounds)
        {
            // Draw here
            
            g.DrawImage(_playerImage, X, bounds.Bottom - Y - PlayerSize, PlayerSize, PlayerSize);
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
                while (Y < Land.BlockSize + 50)
                {
                    Y += 1;
                    X += 1;
                }
            }
            else if (CurrentDirection == Direction.Left)
            {
                while (Y < Land.BlockSize + 50)
                {
                    Y += 1;
                    X -= 1;
                }
            }
               
        }                 
    }
}
