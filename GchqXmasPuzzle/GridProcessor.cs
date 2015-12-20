using System.Collections.Generic;
using System.Linq;

namespace GchqXmasPuzzle
{
    class GridProcessor
    {
        private Grid m_Grid;
        private List<CellState[]>[] m_RowOptions;
        private List<CellState[]>[] m_ColumnOptions;

        public Grid Populate()
        {
            var gridData = new GridData();
            m_Grid = new Grid(gridData.GetGridRowCount(), gridData.GetGridColumnCount());

            var blackCells = gridData.GetBlackCells();
            foreach (var blackCell in blackCells)
            {
                m_Grid.Cells[blackCell[0]][blackCell[1]] = CellState.Black;
            }

            var rowConstraints = gridData.GetRowConstraints();
            m_RowOptions = CalculateLineOptions(rowConstraints, m_Grid.RowCount);

            var columnConstraints = gridData.GetColumnConstraints();
            m_ColumnOptions = CalculateLineOptions(columnConstraints, m_Grid.ColumnCount);

            bool moreSweepsNeeded = true;
            while (moreSweepsNeeded)
            {
                RemoveLineOptionsIncompatibleWithTheGrid();
                moreSweepsNeeded = UpdateCellsWithOnlyOnePossibleValue();
            }

            return m_Grid;
        }

        private bool UpdateCellsWithOnlyOnePossibleValue()
        {
            bool updatedAnyCell = false;
            for (int i = 0; i < m_Grid.RowCount; i++)
            {
                for (int j = 0; j < m_Grid.ColumnCount; j++)
                {
                    if (m_Grid.Cells[i][j] == CellState.Unknown)
                    {
                        bool blackRow = m_RowOptions[i].Any(x => x[j] == CellState.Black);
                        if (!blackRow)
                        {
                            m_Grid.Cells[i][j] = CellState.White;
                            updatedAnyCell = true;
                        }
                        bool whiteRow = m_RowOptions[i].Any(x => x[j] == CellState.White);
                        if (!whiteRow)
                        {
                            m_Grid.Cells[i][j] = CellState.Black;
                            updatedAnyCell = true;
                        }
                    }
                    if (m_Grid.Cells[j][i] == CellState.Unknown)
                    {
                        bool blackColumn = m_ColumnOptions[i].Any(x => x[j] == CellState.Black);
                        if (!blackColumn)
                        {
                            m_Grid.Cells[j][i] = CellState.White;
                            updatedAnyCell = true;
                        }
                        bool whiteColumn = m_ColumnOptions[i].Any(x => x[j] == CellState.White);
                        if (!whiteColumn)
                        {
                            m_Grid.Cells[j][i] = CellState.Black;
                            updatedAnyCell = true;
                        }
                    }
                }
            }
            return updatedAnyCell;
        }

        private void RemoveLineOptionsIncompatibleWithTheGrid()
        {
            for (int i = 0; i < m_Grid.RowCount; i++)
            {
                for (int j = 0; j < m_Grid.ColumnCount; j++)
                {
                    if (m_Grid.Cells[i][j] != CellState.Unknown)
                    {
                        m_RowOptions[i] = m_RowOptions[i].Where(option => m_Grid.Cells[i][j] == option[j]).ToList();
                    }
                    if (m_Grid.Cells[j][i] != CellState.Unknown)
                    {
                        m_ColumnOptions[i] = m_ColumnOptions[i].Where(option => m_Grid.Cells[j][i] == option[j]).ToList();
                    }
                }
            }
        }

        private List<CellState[]>[] CalculateLineOptions(int[][] lineConstraints, int lineLength)
        {
            List<CellState[]>[] lineOptions = new List<CellState[]>[lineLength];
            for (int i = 0; i < lineLength; i++) //For each of the lines in the grid
            {
                lineOptions[i] = new List<CellState[]>();
                List<int[]> runStartingPositionSets = CalculatePossibleRunStartingPositions(lineConstraints[i], lineLength);
                foreach (var runStartingPositionSet in runStartingPositionSets) //for every option for that line
                {
                    var lineOption = GetPopulatedLineFromRunStartingPositions(runStartingPositionSet, lineConstraints[i], lineLength);
                    lineOptions[i].Add(lineOption);
                }
            }
            return lineOptions;
        }

        private CellState[] GetPopulatedLineFromRunStartingPositions(int[] runStartingPositionSet, int[] lineConstraint, int lineLength)
        {
            CellState[] lineOption = new CellState[lineLength];
            for (int position = 0; position < lineLength; position++)
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

        private List<int[]> CalculatePossibleRunStartingPositions(int[] constraint, int lineLength)
        {
            List<int[]> acceptableArrangements = new List<int[]>();
            int[] arrangement = new int[constraint.Length];
            int[] furthestAccpetablePositions = new int[constraint.Length];

            //Find right-most acceptable positions
            furthestAccpetablePositions[constraint.Length - 1] = lineLength - constraint[constraint.Length - 1];
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