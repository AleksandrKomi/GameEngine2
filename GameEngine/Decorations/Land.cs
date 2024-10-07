using GameEngine.Interfeces;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Decorations
{
    public class Land : ISprite
    {
        public const int BlockSize = 50;

        // Resources

        private readonly Image _landImage;

        public Land()
        {
            _landImage = Image.FromFile("Resources/land.png");
        }

        public void Draw(Graphics g, Rectangle bounds)
        {
            // Draw here

            for (int x = 0; x < bounds.Width; x += BlockSize)
            {
                g.DrawImage(_landImage, x, bounds.Bottom - BlockSize, BlockSize, BlockSize);
            }
        }
    }
}
