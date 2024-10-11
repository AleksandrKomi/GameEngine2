using GameEngine.Interfeces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Extensions
{
    public static class SpriteExtensions
    {
        public static void WriteDebugInfo(this ISprite sprite, bool extraInfo) 
        {
            Debug.WriteLine($"{sprite}");
        }
    }
}
