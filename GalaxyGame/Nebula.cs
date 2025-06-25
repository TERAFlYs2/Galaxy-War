using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;

namespace Sokoban2
{
    internal class Nebula
    {
        private Size size;
        private Image image;
        private Transform transform;
        private const float speed = 1.2F;

        public Nebula(float beginPosX, float beginPosY, Size size, byte number)
        {
            transform = new Transform();
            transform.X = beginPosX;
            transform.Y = beginPosY;
            this.size = size;
            SelectionNebula(number);
        }

        private void SelectionNebula(byte number)
        {
            switch (number)
            {
                case 1:  
                    image = Properties.Resources.spaceNebula_1;
                    break;
                case 2:
                    image = Properties.Resources.spaceNebula_2;
                    break;
                case 3:
                    image = Properties.Resources.spaceNebula_3;
                    break;
            }
        }

       
        public Nebula(float beginPosX, float beginPosY)
        {
            transform = new Transform();
            transform.X = beginPosX;
            transform.Y = beginPosY;         
        }
        public void DrawNebula(Graphics g)
        { 
            g.DrawImage(image, transform.X, transform.Y, size.Width, size.Height);  
        }
        public Transform Transform
        {
            get { return transform; }
            set { transform = value; }
        }
        public float Speed
        {
            get { return speed; }
        }

    }
}
