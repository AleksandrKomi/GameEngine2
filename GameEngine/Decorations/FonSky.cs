using GameEngine.DTO;
using GameEngine.Interfeces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Decorations
{
    public class FonSky : ISprite
    {
        // Resources

        private readonly Image _fonSkyImage;

        public  FonSky(string weather)   
        {
            _fonSkyImage = Image.FromFile($"Resources/{weather}fon.png");
        }

        public void Draw(Graphics g, Rectangle bounds)
        {
            // Draw here

            g.DrawImage(_fonSkyImage, 0, 100);

        }
    }
}
