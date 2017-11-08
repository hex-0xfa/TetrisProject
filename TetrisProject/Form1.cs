using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisProject
{
    public partial class TetrisFrom : Form
    {
        private int i = 1;

        private enum PlayStatus         //The Status of the current game
        {
            NotPlay,                    //The game has not been played.
            PlayNotPause,               //The 
            PlayPause
        }

        private PlayStatus currentGameStatus;

        public TetrisFrom()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            currentGameStatus = PlayStatus.NotPlay;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            i = 2;

        }

        private void TetrisFrom_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           if(currentGameStatus == PlayStatus.NotPlay)
            {
                //generate event / call methods from timer class for timer for starting the game
                currentGameStatus = PlayStatus.PlayNotPause;
                label1.Text = "Pause";
 
            }
           else if (currentGameStatus == PlayStatus.PlayNotPause)
            {
                //generate event / call methods from timer class for timer to pause the game
                currentGameStatus = PlayStatus.PlayPause;
                label1.Text = "Resume";
            }
           else if(currentGameStatus == PlayStatus.PlayPause)
            {
                //generate event / call methods from timer class for timer to resume the game
                currentGameStatus = PlayStatus.PlayNotPause;
                label1.Text = "Pause";
            }
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Green;

        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void board_Paint(object sender, PaintEventArgs e)
        {
            //display the content of the board.
            //display the piece if any.
            Block myBlock = new Block(Color.Red, board, i, 1);
            myBlock.DisplayBlock();
        }
    }
}
