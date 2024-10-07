using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Interfeces
{
    public interface ICollider 
    {
        int X { get; }
        int Y { get; }
                
        bool IsDisappearsOnCollision { get; }
                        
    }
}
