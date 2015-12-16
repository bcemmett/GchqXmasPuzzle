using System.Drawing;
using System.Windows.Forms;

namespace GchqXmasPuzzle
{
    public partial class ResultWindow : Form
    {
        public ResultWindow()
        {
            InitializeComponent();
            var processor = new GridProcessor();
            var grid = processor.Populate();
            PrintGrid(grid);
        }

        private void PrintGrid(Grid grid)
        {
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowCount = grid.Size;
            tableLayoutPanel1.ColumnCount = grid.Size;
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            for (int i = 0; i < grid.Size; i++)
            {
                for (int j = 0; j < grid.Size; j++)
                {
                    Panel cell = GetCell(grid.Cells[i][j]);
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

        private void Form1_Load(object sender, System.EventArgs e)
        {

        }
    }
}