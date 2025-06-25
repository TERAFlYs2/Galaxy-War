using System.Windows.Forms;

namespace Sokoban2
{
    partial class Game
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.timeGame = new System.Windows.Forms.Label();
            this.timeInterface = new System.Windows.Forms.Panel();
            this.healthInterface = new System.Windows.Forms.Panel();
            this.timeInterface.SuspendLayout();
            this.SuspendLayout();
            // 
            // timeGame
            // 
            this.timeGame.Font = new System.Drawing.Font("Times New Roman", 15F);
            this.timeGame.ForeColor = System.Drawing.Color.MediumPurple;
            this.timeGame.Location = new System.Drawing.Point(0, 0);
            this.timeGame.Name = "timeGame";
            this.timeGame.Size = new System.Drawing.Size(220, 23);
            this.timeGame.TabIndex = 0;
            this.timeGame.Text = "Time: ";
            // 
            // timeInterface
            // 
            this.timeInterface.BackColor = System.Drawing.Color.Black;
            this.timeInterface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeInterface.Controls.Add(this.timeGame);
            this.timeInterface.Location = new System.Drawing.Point(12, 12);
            this.timeInterface.Name = "timeInterface";
            this.timeInterface.Size = new System.Drawing.Size(220, 37);
            this.timeInterface.TabIndex = 1;
            // 
            // healthInterface
            // 
            this.healthInterface.BackColor = System.Drawing.Color.Black;
            this.healthInterface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.healthInterface.Location = new System.Drawing.Point(736, 12);
            this.healthInterface.Name = "healthInterface";
            this.healthInterface.Size = new System.Drawing.Size(161, 52);
            this.healthInterface.TabIndex = 2;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.BackgroundImage = global::Sokoban2.Properties.Resources.spaceBC;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(907, 807);
            this.Controls.Add(this.timeInterface);
            this.Controls.Add(this.healthInterface);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Galaxy War";
            this.timeInterface.ResumeLayout(false);
            this.ResumeLayout(false);

            this.Paint += Game_Paint;
            this.KeyDown += Game_KeyDown;
            this.KeyUp += Game_KeyUp;
        }

        










        #endregion

        private Panel timeInterface;
        private Panel healthInterface;
        private Label timeGame;
    }
}

