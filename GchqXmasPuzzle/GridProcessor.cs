using System.Collections.Generic;
using System.Linq;

namespace GchqXmasPuzzle
{
    class GridProcessor
    {
        private Grid m_Grid;

        public Grid Populate()
        {
            var gridData = new GridData();
            m_Grid = new Grid(gridData.GetGridSize());

            var blackCells = gridData.GetBlackCells();
            foreach (var blackCell in blackCells)
            {
                m_Grid.Cells[blackCell[0]][blackCell[1]] = CellState.Black;
            }

            var rowConstraints = gridData.GetRowConstraints();
            List<CellState[]>[] rowOptions = CalculateLineOptions(rowConstraints);

            var columnConstraints = gridData.GetColumnConstraints();
            List<CellState[]>[] columnOptions = CalculateLineOptions(columnConstraints);

            bool moreSweepsNeeded = true;
            while (moreSweepsNeeded)
            {
                moreSweepsNeeded = false;
                
                //First strip out any options which are not compatible with the grid
                for (int i = 0; i < m_Grid.Size; i++)
                {
                    for (int j = 0; j < m_Grid.Size; j++)
                    {
                        if (m_Grid.Cells[i][j] != CellState.Unknown)
                        {
                            rowOptions[i] = rowOptions[i].Where(option => m_Grid.Cells[i][j] == option[j]).ToList();
                        }
                        if (m_Grid.Cells[j][i] != CellState.Unknown)
                        {
                            columnOptions[i] = columnOptions[i].Where(option => m_Grid.Cells[j][i] == option[j]).ToList();
                        }
                    }
                }

                //Then look for cells which can only have one value
                for (int i = 0; i < m_Grid.Size; i++)
                {
                    for (int j = 0; j < m_Grid.Size; j++)
                    {
                        if (m_Grid.Cells[i][j] == CellState.Unknown)
                        {
                            bool blackRow = rowOptions[i].Any(x => x[j] == CellState.Black);
                            if (!blackRow)
                            {
                                m_Grid.Cells[i][j] = CellState.White;
                                moreSweepsNeeded = true;
                            }
                            bool whiteRow = rowOptions[i].Any(x => x[j] == CellState.White);
                            if (!whiteRow)
                            {
                                m_Grid.Cells[i][j] = CellState.Black;
                                moreSweepsNeeded = true;
                            }
                        }
                        if (m_Grid.Cells[j][i] == CellState.Unknown)
                        {
                            bool blackColumn = columnOptions[i].Any(x => x[j] == CellState.Black);
                            if (!blackColumn)
                            {
                                m_Grid.Cells[j][i] = CellState.White;
                                moreSweepsNeeded = true;
                            }
                            bool whiteColumn = columnOptions[i].Any(x => x[j] == CellState.White);
                            if (!whiteColumn)
                            {
                                m_Grid.Cells[j][i] = CellState.Black;
                                moreSweepsNeeded = true;
                            }
                        }
                    }
                }
            }

            return m_Grid;
        }

        private List<CellState[]>[] CalculateLineOptions(int[][] lineConstraints)
        {
            List<CellState[]>[] lineOptions = new List<CellState[]>[m_Grid.Size];
            for (int i = 0; i < m_Grid.Size; i++) //For each of the lines in the grid
            {
                lineOptions[i] = new List<CellState[]>();
                List<int[]> runStartingPositionSets = CalculatePossibleRunStartingPositions(lineConstraints[i]);
                foreach (var runStartingPositionSet in runStartingPositionSets) //for every option for that line
                {
                    var lineOption = GetPopulatedLineFromRunStartingPositions(runStartingPositionSet, lineConstraints[i]);
                    lineOptions[i].Add(lineOption);
                }
            }
            return lineOptions;
        }

        private CellState[] GetPopulatedLineFromRunStartingPositions(int[] runStartingPositionSet, int[] lineConstraint)
        {
            CellState[] lineOption = new CellState[m_Grid.Size];
            for (int position = 0; position < m_Grid.Size; position++)
            {
                lineOption[position] = CellState.White;
            }

            for (int run = 0; run < runStartingPositionSet.Length; run++) //For every run in the set
            {
                for (int position = runStartingPositionSet[run]; position < runStartingPositionSet[run] + lineConstraint[run]; position++)
                {
                    lineOption[position] = CellState.Black;
                }
                
            }
            return lineOption;
        }

        private List<int[]> CalculatePossibleRunStartingPositions(int[] constraint)
        {
            List<int[]> acceptableArrangements = new List<int[]>();
            int[] arrangement = new int[constraint.Length];
            int[] furthestAccpetablePositions = new int[constraint.Length];

            //Find right-most acceptable positions
            furthestAccpetablePositions[constraint.Length - 1] = m_Grid.Size - constraint[constraint.Length - 1];
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
                    //The left-most run can't move further to the right, so we're done here
                    moreArrangements = false;
                }
            }

            return acceptableArrangements;
        }
    }
}
