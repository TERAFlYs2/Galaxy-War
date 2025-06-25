using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban2
{
    internal static class Physic
    {
        private static float currenSpeed = 0.0F;    
        private static float acceleration = 0.90F;
        private const float frictionCoefficient = 0.9F;
        private static int damageRceived = 0;
        public static void MovementPlayer(Player player, bool leftKeyPressed, bool rightKeyPressed, Game gameForm)
        {
            ColculationCollisionPlayer(player, gameForm);
            player.Transform.X += currenSpeed;
            Player.TransformCollisionX += currenSpeed;
            // Проверка клавиш и изменение скорости при необходимости
            if (leftKeyPressed)
            {
                // Ускорение влево
                currenSpeed -= acceleration;
            }
            else if (rightKeyPressed)
            {
                // Ускорение вправо
                currenSpeed += acceleration;
            }
            else
            {
                // Если нет нажатых клавиш, уменьшаем скорость (симуляция трения)
                currenSpeed *= frictionCoefficient;
            }

            // Ограничение скорости, чтобы персонаж не двигался слишком быстро
            currenSpeed = Math.Max(Math.Min(currenSpeed, Player.MaxSpeed), -Player.MaxSpeed);
        }

        private static void ColculationCollisionPlayer(Player player, Game gameForm)
        {
            if (player.Transform.X - 1 <= 0 ||
                player.Transform.X + 1 >= gameForm.Width - player.SizeF.Width - 20
                )
            {
                if (player.Transform.X - 3 <= 0 )
                {
                  currenSpeed *= -acceleration;  
                }
                if (player.Transform.X + 2 >= gameForm.Width - player.SizeF.Width - 20)           
                {
                    currenSpeed *= -acceleration;
                }

            }

        }
        private static bool IsCollisionBulletWithEnemy(PictureBox bullet, List <Enemy> enemys)
        {
           RectangleF bulletRect = new Rectangle(bullet.Location.X, bullet.Location.Y, bullet.Size.Width, bullet.Size.Height);
           
            for (byte j = 0; j < enemys.Count; j++)
            {     
                if (bulletRect.IntersectsWith(enemys[j].Collision))
                {
                    enemys.RemoveAt(j);
                    return true;
                }
            }
            return false;
        }
        public static void MovementBullets(Player player, List <Enemy> enemys)
        {
            for (int i = 0; i < player.Gun.Bullets.Count; i++)
            {
                PictureBox bullet = player.Gun.Bullets[i];
                bullet.Top -= (int)player.Gun.SpeedBullet;

                // Удаляем пулю, если она вышла за пределы экрана
                if (bullet.Bottom < 0 ||
                    IsCollisionBulletWithEnemy(player.Gun.Bullets[i], enemys)
                    )               
                {   
                    player.Gun.Bullets.RemoveAt(i);
                    bullet.Dispose(); // Освобождаем ресурсы
                }   
            }
        }

        public static void MovementNebula(List <Nebula> nebulas, Game gameForm)
        {
            foreach (var nebula in nebulas)
            {
                nebula.Transform.Y += nebula.Speed;
                if (nebula.Transform.Y > gameForm.Height)
                {
                    nebulas.Remove(nebula);
                    break;
                }
            }
        }
        private static bool IsCollisionPlayerWithEnemy(Enemy enemy)
        {   
            if (Player.Collision.IntersectsWith(enemy.Collision))            
                return true;

            return false;
        }
        public static void MovementEnemy(List<Enemy> enemys, Game gameForm, Player player)
        {
            for (byte i = 0; i < enemys.Count; i++)
            {
                enemys[i].Transform.Y += enemys[i].Speed;
                enemys[i].TransformCollisionY += enemys[i].Speed;
                if (enemys[i].Transform.Y + Enemy.Size.Height >= gameForm.Height ||
                    IsCollisionPlayerWithEnemy(enemys[i]))
                {
                    enemys.RemoveAt(i);
                    if (Bonus.IsBlockBonus)
                    {
                        continue;
                    }
                    if (damageRceived == player.Healths.Count - 1)
                    {
                        player.Healths[player.Healths.Count - 1 - damageRceived].Dispose();
                        gameForm.GameRestart();
                    }
                    else
                    {
                        player.Healths[player.Healths.Count - 1 - damageRceived].Dispose();
                        damageRceived++;
                    }
                }
            }

        }
        public static void MovementBonus(List<Bonus> bonuses, Game gameForm, Player player)
        {
            for (byte i = 0; i < bonuses.Count; i++)
            {
                bonuses[i].Transform.Y += Bonus.SpeedFall;
                bonuses[i].TransformCollisionY += Bonus.SpeedFall;
                if (bonuses[i].Transform.Y > gameForm.Height)
                {
                    bonuses.RemoveAt(i);
                    continue;
                }

                if (bonuses[i].Collision.IntersectsWith(Player.Collision))
                {
                    bonuses[i].DrawBonusInterface(bonuses[i].TypeBonus, player);
                    bonuses.RemoveAt(i);
                }
                
            } 
            
        }
        
    }
}