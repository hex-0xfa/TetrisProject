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
        private int toggle;
        public TetrisFrom()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            toggle = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (toggle == 0)
            {
                Block myBlock = new Block(Color.Red, board, 1, 1);
                myBlock.DisplayBlock();
                toggle = 1;
            }
            else
            {
                Block myBlock = new Block(Color.Red, board, 1, 1);
                myBlock.DisappearBlock();
                toggle = 0;
            }

        }

        private void TetrisFrom_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Green;
        }
    }
}
