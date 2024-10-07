using GameEngine.Characters;
using GameEngine.Interfeces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Decorations
{
    public class Counter : ISprite
    {
        public int Count;


        public void Draw(Graphics g, Rectangle bounds)
        {
            Font countFont = new Font("Ariel", 16);

            g.DrawString($"Counter: {Count}", countFont, Brushes.Red,  200, 10);
        }

    }

}
