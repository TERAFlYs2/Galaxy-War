using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban2
{
    internal class Transform
    {
        private float x;
        private float y;

        private float dx;
        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }
        public float Dx { get { return dx; } set { dx = value; } }

    }
}
