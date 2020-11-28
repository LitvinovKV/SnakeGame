using SnakeGame.RadnomService;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private Game game { get; }

        public Form1()
        {
            InitializeComponent();

            var randomService = new RandomService();

            game = new Game(12, 12, randomService);
            Controls.Add(game.GameField);
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
