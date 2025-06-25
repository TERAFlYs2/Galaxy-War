using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sokoban2
{
    internal class Player
    {
        private Transform transform;
        private List<PictureBox> healths;
        private static SizeF sizePlayer;
        private static Image imagePlayer;
        private static RectangleF playerRect;
        private static Image imageHealth;
        private const byte INTERVAL_HEALTHS = 40;
        private const byte COUNT_HEALTHS = 3;
        private static float maxSpeed = 15.0F;
        private Gun gun;

        static Player()
        {
            imagePlayer = Properties.Resources.player;
            imageHealth = Properties.Resources.health;
        }
        public Player(float beginPosX, float beginPosY, float width, float height)
        {
            sizePlayer = new SizeF(width, height);
            transform = new Transform();
            gun = new LaserGun(new Size(10, 25));
            transform.X = beginPosX;
            transform.Y = beginPosY;
            playerRect = new RectangleF(transform.X, transform.Y, sizePlayer.Width, sizePlayer.Height);
        }

        public void DrawPlayer(Graphics g)
        {
            g.DrawImage(imagePlayer, transform.X, transform.Y, sizePlayer.Width, sizePlayer.Height);
            if (Bonus.IsBlockBonus)
            {
                g.DrawImage(Properties.Resources.barrier, transform.X - 10, transform.Y - 25, 100, 100);
            }
        }

        public void DrawGun(Player player, Graphics g)
        {
            gun.DrawGun(player, g);
        }
        public void DrawBullet(Game gameForm)
        {
            gun.DrawBullet(gameForm);
        }
        public void DrawInterface(Panel healthInterface)
        {
            healths = new List<PictureBox>();
            for (int i = 0; i < COUNT_HEALTHS; i++)
            {
                PictureBox healthPictureBox = new PictureBox();
                healthPictureBox.Image = imageHealth;
                healthPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                healthPictureBox.Size = new Size(40, 40);
                healthPictureBox.Location = new Point(i * INTERVAL_HEALTHS, 0); // Пример расположения
                healthInterface.Controls.Add(healthPictureBox); // Добавляем PictureBox на форму или панель
                healths.Add(healthPictureBox); // Добавляем PictureBox в список healths
            }
        }

        public static void ResetSpeed()
        {
            maxSpeed = 15.0F;
        }
        public Transform Transform
        {
            get { return transform; }
            set { transform = value; }
        }
        
        public SizeF SizeF
        {
            get { return sizePlayer; }
        }

        public List <PictureBox> Healths
        {
            get { return healths; }
            set { healths = value; }
        }
        public Gun Gun { get { return gun; } }

        public static float MaxSpeed {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        public static float TransformCollisionX
        {
            get { return playerRect.X; }
            set { playerRect.X = value; }
        }

        public static RectangleF Collision
        {
            get { return playerRect; }
        }
    }
}
