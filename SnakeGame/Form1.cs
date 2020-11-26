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
        private Game game { get; }

        public Form1()
        {
            InitializeComponent();
            game = new Game(pictureBox1);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    game.ArrowButtonClick(e.KeyCode);
                    break;
                case Keys.Space:
                    game.SpaceButtonClick();
                    break;
            }
        }
    }
}
