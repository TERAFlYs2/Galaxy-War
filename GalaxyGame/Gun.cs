using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban2
{
    interface Gun
    {
         void DrawGun(Player player, Graphics g);
         void DrawBullet(Game gameForm);
         void ResetReload();
         short DamageGun { get; }
         float SpeedBullet { get; }
         List <PictureBox> Bullets { get; set; }
         Transform TransformBullet { get; set; }
         int ReloadTime { get; set; }
    }

    internal class LaserGun : Gun
    {
        private static int reloadTime = 500;
        private static short damage = 1;
        private const float speedBullet = 10;
        private Transform transformBullet;
        private List<PictureBox> bullets;

        private static Image imageLaserGun;
        private static Size sizeLaserGun;      
        private Transform transformGun;    

        static LaserGun()
        {
            imageLaserGun = Properties.Resources.laserGun; 
        }
        public LaserGun(Size sizeGun)
        {
            sizeLaserGun = sizeGun;
            transformGun = new Transform();
            transformBullet = new Transform();
            bullets = new List <PictureBox>();
        }
        private void InitBullet(Game gameForm)
        {
            PictureBox bullet = new PictureBox();
            transformBullet.X = transformGun.X + sizeLaserGun.Width / 2 - 2;
            transformBullet.Y = transformGun.Y + sizeLaserGun.Height / 2 - 25;
            bullet.Image = Properties.Resources.laserBullet;
            bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            bullet.Size = new Size(5, 15);
            bullet.Location = new Point((int)transformBullet.X, (int)transformBullet.Y);
            bullets.Add(bullet);
            gameForm.Controls.Add(bullet);  
        }
        public void DrawGun(Player player, Graphics g)
        {
            transformGun.X = player.Transform.X + player.SizeF.Width / 2 - 5;
            transformGun.Y = player.Transform.Y + player.SizeF.Height / 2 - 55;
            g.DrawImage(imageLaserGun, transformGun.X, transformGun.Y, sizeLaserGun.Width, sizeLaserGun.Height);
         }
        public void DrawBullet(Game gameForm)
        {
            InitBullet(gameForm);
        }

        public void ResetReload()
        {
            reloadTime = 500;
        }
        public List <PictureBox> Bullets
        {
            get { return bullets; }
            set { bullets = value; }
        }
        public Transform TransformBullet
        {
            get { return transformBullet; }
            set { transformBullet = value; }
        }
        public short DamageGun
        {
            get { return damage; }
        }
        public float SpeedBullet
        {
            get { return speedBullet; }
        }
        public int ReloadTime
        {
            get { return reloadTime; }
            set { reloadTime = value; }
        }
    }
}
