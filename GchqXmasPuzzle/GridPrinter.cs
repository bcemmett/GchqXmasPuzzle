using System.Drawing;
using System.Windows.Forms;

namespace GchqXmasPuzzle
{
    public partial class Form1
    {
        private void PrintGrid(CellState[][] grid)
        {
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel1.SuspendLayout();
            int gridSize = grid.Length;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowCount = gridSize;
            tableLayoutPanel1.ColumnCount = gridSize;
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Panel cell = GetCell(grid[i][j]);
                    tableLayoutPanel1.Controls.Add(cell, j, i);
                }
            }
            tableLayoutPanel1.ResumeLayout();
            tableLayoutPanel1.Visible = true;
        }

        private Panel GetCell(CellState state)
        {
            Panel cell = new Panel
            {
                Margin = new Padding(0),
                Size = new Size(15, 15),
                BackColor = Color.BurlyWood
            };
            switch (state)
            {
                case CellState.Black:
                    cell.BackColor = Color.Black;
                    break;
                case CellState.White:
                    cell.BackColor = Color.White;
                    break;
            }
            return cell;
        }
    }
}
