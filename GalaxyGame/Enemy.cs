using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban2
{
    internal class Enemy
    {
        private Transform transform;
        private static Image image;
        private RectangleF enemyRect;
        private static Bitmap imageSettings;
        private static SizeF size;
        private float speed = 3F;

        static Enemy()
        {
            imageSettings = new Bitmap(Properties.Resources.player);
            imageSettings.RotateFlip(RotateFlipType.Rotate180FlipNone);
            image = imageSettings;
        }
        public Enemy(float posX, float posY)
        {
            transform = new Transform();  
            transform.X = posX;
            transform.Y = posY;
            enemyRect = new RectangleF(transform.X, transform.Y, size.Width, size.Height);
        }


        public void DrawEnemy(Graphics g)
        {
            g.DrawImage(image, transform.X, transform.Y, size.Width, size.Width);
        }

        public static SizeF Size
        {
            get { return size; }
            set { size = value; }
        }

        public Transform Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public RectangleF Collision
        {
            get { return enemyRect; }
        }
        public float TransformCollisionY
        {
            get { return enemyRect.Y; }
            set { enemyRect.Y = value; }
        }
    }
}
