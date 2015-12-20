using System.Collections.Generic;

namespace GchqXmasPuzzle
{
    class GridData
    {
        public int GetGridRowCount()
        {
            return 25;
        }

        public int GetGridColumnCount()
        {
            return 25;
        }

        public int[][] GetRowConstraints()
        {
            var constraints = new int[25][];
            constraints[0] = new int[] {7,3,1,1,7};
            constraints[1] = new int[] {1,1,2,2,1,1};
            constraints[2] = new int[] {1,3,1,3,1,1,3,1};
            constraints[3] = new int[] {1,3,1,1,6,1,3,1};
            constraints[4] = new int[] {1,3,1,5,2,1,3,1};
            constraints[5] = new int[] {1,1,2,1,1};
            constraints[6] = new int[] {7,1,1,1,1,1,7};
            constraints[7] = new int[] {3,3};
            constraints[8] = new int[] {1,2,3,1,1,3,1,1,2};
            constraints[9] = new int[] {1,1,3,2,1,1};
            constraints[10] = new int[] {4,1,4,2,1,2};
            constraints[11] = new int[] {1,1,1,1,1,4,1,3};
            constraints[12] = new int[] {2,1,1,1,2,5};
            constraints[13] = new int[] {3,2,2,6,3,1};
            constraints[14] = new int[] {1,9,1,1,2,1};
            constraints[15] = new int[] {2,1,2,2,3,1};
            constraints[16] = new int[] {3,1,1,1,1,5,1};
            constraints[17] = new int[] {1,2,2,5};
            constraints[18] = new int[] {7,1,2,1,1,1,3};
            constraints[19] = new int[] {1,1,2,1,2,2,1};
            constraints[20] = new int[] {1,3,1,4,5,1};
            constraints[21] = new int[] {1,3,1,3,10,2};
            constraints[22] = new int[] {1,3,1,1,6,6};
            constraints[23] = new int[] {1,1,2,1,1,2};
            constraints[24] = new int[] {7,2,1,2,5};
            return constraints;
        }

        public int[][] GetColumnConstraints()
        {
            var constraints = new int[25][];
            constraints[0] = new int[] {7,2,1,1,7};
            constraints[1] = new int[] {1,1,2,2,1,1};
            constraints[2] = new int[] {1,3,1,3,1,3,1,3,1};
            constraints[3] = new int[] {1,3,1,1,5,1,3,1};
            constraints[4] = new int[] {1,3,1,1,4,1,3,1};
            constraints[5] = new int[] {1,1,1,2,1,1};
            constraints[6] = new int[] {7,1,1,1,1,1,7};
            constraints[7] = new int[] {1,1,3};
            constraints[8] = new int[] {2,1,2,1,8,2,1};
            constraints[9] = new int[] {2,2,1,2,1,1,1,2};
            constraints[10] = new int[] {1,7,3,2,1};
            constraints[11] = new int[] {1,2,3,1,1,1,1,1};
            constraints[12] = new int[] {4,1,1,2,6};
            constraints[13] = new int[] {3,3,1,1,1,3,1};
            constraints[14] = new int[] {1,2,5,2,2};
            constraints[15] = new int[] {2,2,1,1,1,1,1,2,1};
            constraints[16] = new int[] {1,3,3,2,1,8,1};
            constraints[17] = new int[] {6,2,1};
            constraints[18] = new int[] {7,1,4,1,1,3};
            constraints[19] = new int[] {1,1,1,1,4};
            constraints[20] = new int[] {1,3,1,3,7,1};
            constraints[21] = new int[] {1,3,1,1,1,2,1,1,4};
            constraints[22] = new int[] {1,3,1,4,3,3};
            constraints[23] = new int[] {1,1,2,2,2,6,1};
            constraints[24] = new int[] {7,1,3,2,1,1};
            return constraints;
        }

        public List<int[]> GetBlackCells()
        {
            var cells = new List<int[]>();
            cells.Add(new[] {3, 3});
            cells.Add(new[] {3, 4});
            cells.Add(new[] {3, 12});
            cells.Add(new[] {3, 13});
            cells.Add(new[] {3, 21});
            cells.Add(new[] {8, 6});
            cells.Add(new[] {8, 7});
            cells.Add(new[] {8, 10});
            cells.Add(new[] {8, 14});
            cells.Add(new[] {8, 15});
            cells.Add(new[] {8, 18});
            cells.Add(new[] {16, 6});
            cells.Add(new[] {16, 11});
            cells.Add(new[] {16, 16});
            cells.Add(new[] {16, 20});
            cells.Add(new[] {21, 3});
            cells.Add(new[] {21, 4});
            cells.Add(new[] {21, 9});
            cells.Add(new[] {21, 10});
            cells.Add(new[] {21, 15});
            cells.Add(new[] {21, 20});
            cells.Add(new[] {21, 21});
            return cells;
        }
    }
}