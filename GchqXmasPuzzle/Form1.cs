using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GchqXmasPuzzle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Go_Click(object sender, EventArgs e)
        {
            int gridSize = 25;

            List<int[]> arrangements = CalculatePossibleArrangements(new int[] { 7,3,1,1,7 }, gridSize);

            CellState[][] grid = new CellState[gridSize][];
            for (int i = 0; i < gridSize; i++)
            {
                grid[i] = new CellState[gridSize];
            }
            PrintGrid(grid);
        }

        private List<int[]> CalculatePossibleArrangements(int[] constraint, int gridSize)
        {
            List<int[]> acceptableArrangements = new List<int[]>();
            int[] arrangement = new int[constraint.Length];
            int[] furthestAccpetablePositions = new int[constraint.Length];

            //Find right-most acceptable positions
            furthestAccpetablePositions[constraint.Length - 1] = gridSize - constraint[constraint.Length - 1];
            for (int i = constraint.Length - 2; i >= 0; i--)
            {
                furthestAccpetablePositions[i] = furthestAccpetablePositions[i + 1] - constraint[i] - 1;
            }
            
            //Find a first arrangement, as far to the left as possible
            arrangement[0] = 0;
            for (int i = 1; i < constraint.Length; i++)
            {
                arrangement[i] = arrangement[i - 1] + constraint[i - 1] + 1;
            }
            acceptableArrangements.Add(arrangement);

            bool moreArrangements = true;

            //Starting from the right hand side, try to move runs to the right
            int j = constraint.Length - 1;
            while (moreArrangements)
            {
                arrangement = (int[])arrangement.Clone();
                if (arrangement[j] < furthestAccpetablePositions[j])
                {
                    arrangement[j]++;
                    //Everything to the right of the moved run should move back to its new left-most position
                    for (int k = j + 1; k < constraint.Length; k++)
                    {
                        arrangement[k] = arrangement[k - 1] + constraint[k - 1] + 1;
                    }
                    acceptableArrangements.Add(arrangement);
                    j = constraint.Length - 1;
                }
                else
                {
                    j--;
                }
                if (j == 0 && arrangement[j] == furthestAccpetablePositions[j])
                {
                    moreArrangements = false;
                }
            }

            return acceptableArrangements;
        }

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