using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private Game Game { get; }

        public Form1()
        {
            InitializeComponent();
            Game = new Game(pictureBox1.Location, pictureBox1.Size.Width, pictureBox1.Height);
        }
    }
}
