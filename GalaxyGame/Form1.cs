using System;
using System.Windows.Forms;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Sokoban2
{
    public enum Bonuses
    {
        BONUS_SPEED,
        BONUS_RELOAD,
        BONUS_BLOCK
    }
    public partial class Game : Form
    {
        private const float WIDTH_PLAYER = 80;
        private const float HEIGHT_PLAYER = 70;
        private const int INTERVAL = 20;
        private const int INTERVAL_NEBULA = 5000;
        private const int INTERVAL_ENEMY = 6000;
        private const int MAX_INTERVAL_BONUS = 50000;
        private const int MIN_INTERVAL_BONUS = 25000;

        private float positionYFirstBC = 0F;
        private float positionYSecondBC = -655;
        private float speedBC = 0.5F;

        private bool isGameOver = false;
        private bool isLeftPressed = false;
        private bool isRightPressed = false;
        private bool isReloaded = true;

        private Random r;
        private Timer timer;
        private Player player;
        private List<Enemy> enemys;       
        private List<Nebula> nebulas;
        private List<Bonus> bonuses;
        public Game()
        {
            InitializeComponent();      
            InitializeGame();
        }
        private void InitializeGame() // основная функция загрузки игры 
        {
            r = new Random();
            InitializeObjects(); // инициализация объектов 
            InitializeTimer();
            RegenRandomNebula();
            RegenRandomEnemy();
            RegenRandomBonus();
           
            UpdateGameTime();
        }
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = INTERVAL;
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private async void Timer_Tick(object sender, EventArgs e)
        {
            positionYFirstBC += speedBC;
            positionYSecondBC += speedBC;
            await Task.Run(() =>
            {
                Physic.MovementNebula(nebulas, this);
                Physic.MovementPlayer(player, isLeftPressed, isRightPressed, this);
               
            });
            Physic.MovementBonus(bonuses, this, player);
            Physic.MovementBullets(player, enemys);
            Physic.MovementEnemy(enemys, this, player);
            Invalidate();
        }
        private void InitializeObjects()
        {
            nebulas = new List<Nebula>();
            player = new Player(this.Width / 2 - WIDTH_PLAYER, this.Height - 50 - HEIGHT_PLAYER, WIDTH_PLAYER, HEIGHT_PLAYER);
            player.DrawInterface(healthInterface);
            enemys = new List<Enemy>();
            bonuses = new List<Bonus>();
            Enemy.Size = new SizeF(WIDTH_PLAYER, HEIGHT_PLAYER);
            
        }

        private void ParallaxRendering(Graphics g)
        {
            g.DrawImage(Properties.Resources.spaceBC, 0, positionYFirstBC, ClientSize.Width, ClientSize.Height);
            g.DrawImage(Properties.Resources.spaceBC, 0, positionYSecondBC, ClientSize.Width, ClientSize.Height);
            if (positionYFirstBC >= this.ClientSize.Height)
            {
                positionYFirstBC = -650;
            }
            if (positionYSecondBC >= this.ClientSize.Height)
            {
                positionYSecondBC = -650;
            }
        }
        private void Game_Paint(object sender, PaintEventArgs e)
        {
            ParallaxRendering(e.Graphics);
            player.DrawGun(player, e.Graphics);
            player.DrawPlayer(e.Graphics);

            foreach (var bonus in bonuses)
                bonus.DrawBonus(e.Graphics);

            foreach (var enemy in enemys)
                enemy.DrawEnemy(e.Graphics);

            foreach (var nebula in nebulas)
                nebula.DrawNebula(e.Graphics);
        }
        private async void RegenRandomNebula()
        {
            while (true)
            {
                byte number = (byte)r.Next(1,10);
                ushort size = (ushort)r.Next(150, 300);
                switch (number)
                {
                    case 1:
                    case 2:
                    case 3:
                        nebulas.Add(new Nebula(r.Next(0, this.Width - size), -size * 2, new Size(size, size), number));       
                        break;
                }
                await Task.Delay(INTERVAL_NEBULA);
            }
        }
        private async void RegenRandomEnemy()
        {
            float initialDelay = INTERVAL_ENEMY; // Начальная задержка между спавнами (в миллисекундах)
            float growthRate = 0.985f; // Коэффициент роста задержки (настраиваемый под ваше желание)
            short MIN_DELAY = 2200;
            ushort minIntervalRandomSpawn = 0;

            while (true)
            {
                await Task.Delay((int)initialDelay);
                byte defaultSpawnCount = 1;
                short rangeY = 0;
                for (byte i = 0; i < defaultSpawnCount; i++)
                {
                    float x = r.Next(0, this.Width - (int)Enemy.Size.Width);
                    enemys.Add(new Enemy(x, 0 - Enemy.Size.Height - rangeY));
                    if (r.Next(MIN_DELAY, INTERVAL_ENEMY) > initialDelay + minIntervalRandomSpawn)
                    {
                        defaultSpawnCount = (byte)r.Next(2, 3);
                        rangeY = (short)(r.Next(15, 100) * -1);
                    }
                }
                // Уменьшайте задержку между спавнами экспоненциально
                initialDelay *= growthRate;

                // Дождитесь указанной задержки перед следующим спавном
                // Для предотвращения переполнения задержки установите минимальное значение
                if (initialDelay < MIN_DELAY)
                {
                    initialDelay = MIN_DELAY;
                    minIntervalRandomSpawn = 350;
                } 
            }
        }

        private async void RegenRandomBonus()
        {
            while (true)
            {
                bonuses.Add(new Bonus(r.Next(0, this.ClientSize.Width - Bonus.Size.Width - 5), 0 - Bonus.Size.Height, (Bonuses)r.Next(0, Enum.GetValues(typeof(Bonuses)).Length), this));
                await Task.Delay(r.Next(MIN_INTERVAL_BONUS, MAX_INTERVAL_BONUS));
            }
        }
        private async void UpdateGameTime()
        {
            byte seconds, minutes, hours;
            seconds = minutes = hours = 0;

            while (!isGameOver)
            {
                if (seconds >= 60)
                {
                    seconds = 0;
                    minutes++;
                }
                if (minutes >= 60)
                {
                    minutes = 0;
                    hours++;
                }

                string time = $"Time: {hours}h : {minutes}m : {seconds}s";
                if (minutes == 10 && seconds == 10)
                {
                    timeGame.Size = new Size(timeGame.Size.Width + 20, timeGame.Size.Height);
                    timeInterface.Size = new Size(timeInterface.Size.Width + 20, timeInterface.Size.Height);
                }
                
                timeGame.Text = time;
                await Task.Delay(1000);
                seconds++;
            }
        }
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                isLeftPressed = true;
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                isRightPressed = true;
            }

            else if (e.KeyCode == Keys.Space && isReloaded)
            {
                player.DrawBullet(this);
                isReloaded = false;
                Reload();
            }

            else if (e.KeyCode == Keys.Escape)
            {
                GameRestart();
            }
        }
        private async void Reload()
        {
            await Task.Delay(player.Gun.ReloadTime);
            isReloaded = true;
        }
        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                isLeftPressed = false;
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                isRightPressed = false;
            }
        }

        public void GameRestart()
        {
            timer.Stop();
            isGameOver = true;

            for (byte i = 0; i < bonuses.Count; i++)   
                bonuses?.RemoveAt(i);

            for (byte i = 0; i < enemys.Count; i++)
                enemys?.RemoveAt(i);

            for (byte i = 0; i < nebulas.Count; i++)
                nebulas?.RemoveAt(i);

            for (byte i = 0; i < player.Gun.Bullets.Count; i++)
                player.Gun.Bullets?.RemoveAt(i);


        }
    }
   
}
