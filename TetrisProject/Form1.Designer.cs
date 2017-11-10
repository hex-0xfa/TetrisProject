namespace TetrisProject
{
    partial class TetrisFrom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelBoard = new System.Windows.Forms.Panel();
            this.tetrisMenu = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playPauseButton = new System.Windows.Forms.Label();
            this.nextBlockPanel = new System.Windows.Forms.Panel();
            this.nextBlockLabel = new System.Windows.Forms.Label();
            this.linesLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.socreNumber = new System.Windows.Forms.Label();
            this.levelNumber = new System.Windows.Forms.Label();
            this.linesNumber = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tetrisMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBoard
            // 
            this.panelBoard.BackColor = System.Drawing.Color.Blue;
            this.panelBoard.Location = new System.Drawing.Point(15, 48);
            this.panelBoard.Name = "panelBoard";
            this.panelBoard.Size = new System.Drawing.Size(350, 630);
            this.panelBoard.TabIndex = 1;
            this.panelBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBoard_Paint);
            // 
            // tetrisMenu
            // 
            this.tetrisMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tetrisMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.tetrisMenu.Location = new System.Drawing.Point(0, 0);
            this.tetrisMenu.Name = "tetrisMenu";
            this.tetrisMenu.Size = new System.Drawing.Size(682, 28);
            this.tetrisMenu.TabIndex = 2;
            this.tetrisMenu.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // playPauseButton
            // 
            this.playPauseButton.Font = new System.Drawing.Font("Comic Sans MS", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playPauseButton.ForeColor = System.Drawing.Color.Red;
            this.playPauseButton.Location = new System.Drawing.Point(390, 608);
            this.playPauseButton.Name = "playPauseButton";
            this.playPauseButton.Size = new System.Drawing.Size(250, 70);
            this.playPauseButton.TabIndex = 3;
            this.playPauseButton.Text = "Play";
            this.playPauseButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.playPauseButton.Click += new System.EventHandler(this.PlayPauseButton_Click);
            this.playPauseButton.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            this.playPauseButton.MouseHover += new System.EventHandler(this.label1_MouseHover);
            // 
            // nextBlockPanel
            // 
            this.nextBlockPanel.BackColor = System.Drawing.Color.Blue;
            this.nextBlockPanel.Location = new System.Drawing.Point(445, 98);
            this.nextBlockPanel.Name = "nextBlockPanel";
            this.nextBlockPanel.Size = new System.Drawing.Size(140, 140);
            this.nextBlockPanel.TabIndex = 4;
            this.nextBlockPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.nextBlockPanel_Paint);
            // 
            // nextBlockLabel
            // 
            this.nextBlockLabel.BackColor = System.Drawing.Color.Transparent;
            this.nextBlockLabel.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextBlockLabel.ForeColor = System.Drawing.Color.Purple;
            this.nextBlockLabel.Location = new System.Drawing.Point(389, 48);
            this.nextBlockLabel.Name = "nextBlockLabel";
            this.nextBlockLabel.Size = new System.Drawing.Size(250, 40);
            this.nextBlockLabel.TabIndex = 5;
            this.nextBlockLabel.Text = "Next Block";
            this.nextBlockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linesLabel
            // 
            this.linesLabel.BackColor = System.Drawing.Color.Transparent;
            this.linesLabel.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linesLabel.ForeColor = System.Drawing.Color.Purple;
            this.linesLabel.Location = new System.Drawing.Point(390, 368);
            this.linesLabel.Name = "linesLabel";
            this.linesLabel.Size = new System.Drawing.Size(250, 40);
            this.linesLabel.TabIndex = 6;
            this.linesLabel.Text = "Lines";
            this.linesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scoreLabel
            // 
            this.scoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.scoreLabel.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.ForeColor = System.Drawing.Color.Purple;
            this.scoreLabel.Location = new System.Drawing.Point(390, 258);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(250, 40);
            this.scoreLabel.TabIndex = 7;
            this.scoreLabel.Text = "Score";
            this.scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // levelLabel
            // 
            this.levelLabel.BackColor = System.Drawing.Color.Transparent;
            this.levelLabel.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelLabel.ForeColor = System.Drawing.Color.Purple;
            this.levelLabel.Location = new System.Drawing.Point(390, 478);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(250, 40);
            this.levelLabel.TabIndex = 8;
            this.levelLabel.Text = "Level";
            this.levelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // socreNumber
            // 
            this.socreNumber.BackColor = System.Drawing.Color.Transparent;
            this.socreNumber.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.socreNumber.ForeColor = System.Drawing.Color.Orange;
            this.socreNumber.Location = new System.Drawing.Point(390, 308);
            this.socreNumber.Name = "socreNumber";
            this.socreNumber.Size = new System.Drawing.Size(250, 40);
            this.socreNumber.TabIndex = 9;
            this.socreNumber.Text = "0";
            this.socreNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // levelNumber
            // 
            this.levelNumber.BackColor = System.Drawing.Color.Transparent;
            this.levelNumber.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelNumber.ForeColor = System.Drawing.Color.Orange;
            this.levelNumber.Location = new System.Drawing.Point(390, 528);
            this.levelNumber.Name = "levelNumber";
            this.levelNumber.Size = new System.Drawing.Size(250, 40);
            this.levelNumber.TabIndex = 10;
            this.levelNumber.Text = "0";
            this.levelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linesNumber
            // 
            this.linesNumber.BackColor = System.Drawing.Color.Transparent;
            this.linesNumber.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linesNumber.ForeColor = System.Drawing.Color.Orange;
            this.linesNumber.Location = new System.Drawing.Point(390, 418);
            this.linesNumber.Name = "linesNumber";
            this.linesNumber.Size = new System.Drawing.Size(250, 40);
            this.linesNumber.TabIndex = 11;
            this.linesNumber.Text = "0";
            this.linesNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TetrisFrom
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(682, 700);
            this.Controls.Add(this.linesNumber);
            this.Controls.Add(this.levelNumber);
            this.Controls.Add(this.socreNumber);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.linesLabel);
            this.Controls.Add(this.nextBlockLabel);
            this.Controls.Add(this.nextBlockPanel);
            this.Controls.Add(this.playPauseButton);
            this.Controls.Add(this.tetrisMenu);
            this.Controls.Add(this.panelBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.tetrisMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "TetrisFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.Load += new System.EventHandler(this.TetrisFrom_Load);
            this.tetrisMenu.ResumeLayout(false);
            this.tetrisMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelBoard;
        private System.Windows.Forms.MenuStrip tetrisMenu;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label playPauseButton;
        private System.Windows.Forms.Panel nextBlockPanel;
        private System.Windows.Forms.Label nextBlockLabel;
        private System.Windows.Forms.Label linesLabel;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.Label socreNumber;
        private System.Windows.Forms.Label levelNumber;
        private System.Windows.Forms.Label linesNumber;
        private System.Windows.Forms.Timer timer1;
    }
}

