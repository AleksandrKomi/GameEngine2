using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Interfeces
{
    public interface ICollidable
    {
        int X { get; }
        int Y { get; }

        int Height { get; }
        int Width { get; }

        void OnCollision(ICollider collider);
    }
}
