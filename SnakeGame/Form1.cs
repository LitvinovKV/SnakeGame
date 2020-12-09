using SnakeGame.RadnomService;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
