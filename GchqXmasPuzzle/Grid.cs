namespace GchqXmasPuzzle
{
    class Grid
    {
        public CellState[][] Cells { get; set; }
        public int Size { get; set; }

        public Grid(int size)
        {
            Cells = new CellState[size][];
            for (int i = 0; i < size; i++)
            {
                Cells[i] = new CellState[size];
            }
            Size = size;
        }
    }
}