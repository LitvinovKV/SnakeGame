using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class GameField : PictureBox
    {
        public static int TableSize { get; } = 27;
        
        public GameField(int x, int y)
        {
            Table = new ElementBase[TableSize, TableSize];

            for (var i = 0; i < TableSize; i++)
            {
                for (var j = 0; j < TableSize; j++)
                {
                    Table[i, j] = new ElementBase(i, j);
                    Controls.Add(Table[i, j]);
                }
            }

            Width = TableSize * ElementBase.SIZE;
            Height = TableSize * ElementBase.SIZE;
            Location = new Point(x, y);
        }

        public void UpdateTableField(int i, int j, ElementType type)
        {
            if (i < 0 || i >= TableSize)
            {
                throw new ArgumentException($"i must be >= 0 and < { TableSize }");
            }

            if (j < 0 || j >= TableSize)
            {
                throw new ArgumentException($"j must be >= 0 and < { TableSize }");
            }

            Table[i, j].Type = type;
            Table[i, j].UpdateColor();
        }

        public ElementBase UpdateTableField(ElementBase tableField, int newI, int newJ)
        {
            UpdateTableField(newI, newJ, tableField.Type);
            //UpdateTableField(tableField.I, tableField.J, ElementType.BaseField);
            return Table[newI, newJ];
        }

        public ElementBase GetTableElement(int i, int j)
        {
            if (i < 0 || i >= TableSize)
            {
                throw new ArgumentException($"i must be >= 0 and < { TableSize }");
            }

            if (j < 0 || j >= TableSize)
            {
                throw new ArgumentException($"j must be >= 0 and < { TableSize }");
            }

            return Table[i, j];
        }

        private ElementBase[,] Table { get; set; }
    }
}
