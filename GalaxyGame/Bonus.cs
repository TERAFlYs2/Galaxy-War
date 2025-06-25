using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban2
{
    internal class Bonus
    {
        private static Size size;
        private static float speedFall = 5.5F;
        private Transform transform;
        private Image image;
        private RectangleF bonusRect;
        private Bonuses typeBonus;
        private const byte timeAction = 8;
        private static Game _gameForm;
        private static bool isBlockBonus = false;
        static Bonus()
        {
            size = new Size(60, 60);
        }

        public Bonus(float posX, float posY, Bonuses typeBonus, Game gameForm)
        {
            _gameForm = gameForm;
            transform = new Transform();  
            transform.X = posX;
            transform.Y = posY;
            bonusRect = new RectangleF(transform.X, transform.Y, size.Width, size.Height);
            this.typeBonus = typeBonus;
            SelectBonus();  
        }

        private void SelectBonus()
        {
            switch (typeBonus)
            {
                case Bonuses.BONUS_SPEED:
                    image = Properties.Resources.bonusSpeed;
                    break;
                case Bonuses.BONUS_RELOAD:
                    image = Properties.Resources.bonusReload;
                    break;
                case Bonuses.BONUS_BLOCK:
                    image = Properties.Resources.bonusBlock;
                    break;
            }
        }
        private async void ChangeSpeed()
        {
            Player.MaxSpeed = 22F;
            await Task.Delay(timeAction * 1000);
            Player.ResetSpeed();
        }
        private async void ColculationBar(ProgressBar progressBonusAction)
        {
            while (progressBonusAction.Value > 0)
            {  
                await Task.Delay(63);
                progressBonusAction.Value -= 1;
            }
            
        }
        private async void ChangeReload(Player player)
        {
            player.Gun.ReloadTime = player.Gun.ReloadTime / 2;
            await Task.Delay(timeAction * 1000);
            player.Gun.ResetReload();
        }

        private async void ChangeBlock(Player player)
        {
            isBlockBonus = true;
            await Task.Delay(timeAction * 1000);
            isBlockBonus = false;
        }
        public void DrawBonus(Graphics g)
        {
            if (image != null)
            {
                g.DrawImage(image, transform.X, transform.Y, size.Width, size.Height);
            }
        }
        public async void DrawBonusInterface(Bonuses selectedBonus, Player player)
        {
            List<Panel> interfaceBonuses = new List<Panel>();
            interfaceBonuses.Add(new Panel());
            
            for (byte i = 0; i < interfaceBonuses.Count; i++) 
            {
                interfaceBonuses[i].BackColor = Color.Black;
                interfaceBonuses[i].BorderStyle = BorderStyle.FixedSingle;
                interfaceBonuses[i].Location = new Point(552, 60 + (30 * i));
                interfaceBonuses[i].Size = new Size(121, 40);
                _gameForm.Controls.Add(interfaceBonuses[i]);
            }
            ProgressBar progressBonusAction = new ProgressBar();
            progressBonusAction.Value = 100;
            progressBonusAction.BackColor = Color.Purple;
            progressBonusAction.ForeColor = Color.Purple;          
            progressBonusAction.Size = new Size(50, 20);

            PictureBox imageBonus = new PictureBox();
            imageBonus.Location = new Point(86, 3);
            imageBonus.Size = new Size(30, 30);
            imageBonus.SizeMode = PictureBoxSizeMode.StretchImage;
            switch (selectedBonus)
            {
                case Bonuses.BONUS_SPEED:
                    imageBonus.Image = Properties.Resources.bonusSpeed;
                    ChangeSpeed();
                    ColculationBar(progressBonusAction);
                    break;
                case Bonuses.BONUS_RELOAD:
                    imageBonus.Image = Properties.Resources.bonusReload;
                    ChangeReload(player);
                    ColculationBar(progressBonusAction);
                    break;
                case Bonuses.BONUS_BLOCK:
                    imageBonus.Image = Properties.Resources.bonusBlock;
                    ChangeBlock(player);
                    ColculationBar(progressBonusAction);
                   break;
            }  
            foreach (var interfaceBonus in interfaceBonuses)
            {
                interfaceBonus.Controls.Add(imageBonus);
                interfaceBonus.Controls.Add(progressBonusAction);
                await Task.Delay(timeAction * 1000);
                interfaceBonus.Dispose();
                progressBonusAction.Dispose();
            }
        }
        public static Size Size { get; }

        public Transform Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        public static float SpeedFall { get { return speedFall; }  }

        public RectangleF Collision
        {
            get { return bonusRect;}
        }

        public float TransformCollisionY
        {
            get { return bonusRect.Y; }
            set { bonusRect.Y = value;}
        }

        public static bool IsBlockBonus
        {
            get { return isBlockBonus; }
        }
        public Bonuses TypeBonus { get { return typeBonus; } }

    }
}
