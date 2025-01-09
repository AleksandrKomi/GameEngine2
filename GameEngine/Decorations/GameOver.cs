using GameEngine.Characters;
using GameEngine.Interfeces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Decorations
{
    public class GameOver : ISprite
    {
        int result;


        public GameOver(Counter counter)
        {
            result = counter.Count;

        }

        public void Draw(Graphics g, Rectangle bounds)
        {
             Font gameoverFont = new Font("Ariel", 25);
             //g.DrawString($"Game Over\nВаш результат: {result}\nHажмите Enter для продолжения", gameoverFont, Brushes.Black, 200, 350);
             g.DrawString("Game Over", gameoverFont, Brushes.DarkRed, 270, 250);
             g.DrawString($"Вы убили врагов: {result}", gameoverFont, Brushes.DarkGreen, 230, 300);
             g.DrawString("Hажмите Enter, чтобы начать снова.", gameoverFont, Brushes.Black, 130, 350);
            
        }

        
    }
}    
  
