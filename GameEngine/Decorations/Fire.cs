using GameEngine.Characters;
using GameEngine.Interfeces;
using GameEngine.Primitives.Enums;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Decorations
{
    public class Fire : BaseFire
    {
        public Fire(IPositionable shooter) : base(shooter, "Resources/fire.png")
        {
            // Nothing here
        }
                
        protected override int FireSize => 30;
                
    }
}
