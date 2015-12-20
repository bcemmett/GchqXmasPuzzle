namespace GchqXmasPuzzle
{
    class Grid
    {
        public CellState[][] Cells { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }

        public Grid(int rows, int columns)
        {
            Cells = new CellState[rows][];
            for (int i = 0; i < rows; i++)
            {
                Cells[i] = new CellState[columns];
            }
            RowCount = rows;
            ColumnCount = columns;
        }
    }
}