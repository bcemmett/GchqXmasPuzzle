using System;
using System.Collections.Generic;
using System.Linq;
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
            var gridData = new GridData();
            var gridSize = gridData.GetGridSize();

            var rowConstraints = gridData.GetRowConstraints();
            List<CellState[]>[] rowOptions = CalculateLineOptions(rowConstraints, gridSize);

            var columnConstraints = gridData.GetColumnConstraints();
            List<CellState[]>[] columnOptions = CalculateLineOptions(columnConstraints, gridSize);

            //Initialise the grid
            CellState[][] grid = new CellState[gridSize][];
            for (int i = 0; i < gridSize; i++)
            {
                grid[i] = new CellState[gridSize];
            }

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    bool blackRow = rowOptions[i].Any(x => x[j] == CellState.Black);
                    if (!blackRow)
                    {
                        grid[i][j] = CellState.White;
                    }
                    bool whiteRow = rowOptions[i].Any(x => x[j] == CellState.White);
                    if (!whiteRow)
                    {
                        grid[i][j] = CellState.Black;
                    }

                    bool blackColumn = columnOptions[i].Any(x => x[j] == CellState.Black);
                    if (!blackColumn)
                    {
                        grid[j][i] = CellState.White;
                    }
                    bool whiteColumn = columnOptions[i].Any(x => x[j] == CellState.White);
                    if (!whiteColumn)
                    {
                        grid[j][i] = CellState.Black;
                    }
                }
            }

            PrintGrid(grid);
        }

        private List<CellState[]>[] CalculateLineOptions(int[][] lineConstraints, int gridSize)
        {
            List<CellState[]>[] lineOptions = new List<CellState[]>[gridSize];
            for (int i = 0; i < gridSize; i++) //for each of the lines
            {
                lineOptions[i] = new List<CellState[]>();
                List<int[]> runStartingPositionSets = CalculatePossibleArrangements(lineConstraints[i], gridSize);
                foreach (var runStartingPositionSet in runStartingPositionSets) //for every option for that line
                {
                    CellState[] lineOption = new CellState[gridSize];
                    for (int run = 0; run < runStartingPositionSet.Length; run++) //for every run in the option
                    {
                        for (int position = runStartingPositionSet[run]; position < runStartingPositionSet[run] + lineConstraints[i][run]; position++)
                        {
                            lineOption[position] = CellState.Black;
                        }
                        for (int position = 0; position < gridSize; position++)
                        {
                            if (lineOption[position] != CellState.Black)
                            {
                                lineOption[position] = CellState.White;
                            }
                        }
                    }
                    lineOptions[i].Add(lineOption);
                }
            }
            return lineOptions;
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
    }
}