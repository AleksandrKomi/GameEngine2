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
    public abstract class BaseFire : IPhysicalSprite, IDisappearableSprite, ICollider
    {               
        
        public BaseFire(IPositionable shooter, string texturePath)
        {
            // Load resources

            FireImage = Image.FromFile(texturePath);

            X = shooter.X;
            Y = shooter.Y;
            CurrentDirection = shooter.Direction;
        }

        protected abstract int FireSize { get; }
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public bool IsDisappearsOnCollision => true;
        protected Image FireImage { get; }
        protected Direction CurrentDirection { get; set; } = Direction.Right;
                
        public virtual void Draw(Graphics g, Rectangle bounds)
        {
            g.DrawImage(FireImage, X - FireSize / 2, bounds.Bottom - Y - FireSize - FireSize / 2, FireSize, FireSize);
        }

        public virtual void ProcessPhysics()
        {
            MoveDown();
        }

        public void MoveDown()
        {
            if (CurrentDirection == Direction.Right)
            {
                X += 8;
            }
            else if (CurrentDirection == Direction.Left)
            {
                X -= 8;
            }
            
        }

    }
}
