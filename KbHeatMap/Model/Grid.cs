using System;

namespace KbHeatMap.Model
{
    public class Grid
    {
        private readonly ChromaColor[,] _grid = new ChromaColor[Constants.KbRows, Constants.KbColumns];

        public Grid()
        {
            SetAll(ChromaColor.Black);
        }

        public void Set(int row, int column, ChromaColor color)
        {
            if (row < 0 || row >= Constants.KbRows)
            {
                throw new ArgumentException($"Row out of range: {row}");
            }

            if (column < 0 || column >= Constants.KbColumns)
            {
                throw new ArgumentException($"Column out of range: {column}");
            }

            _grid[row, column] = color;
        }

        public void SetRow(int row, ChromaColor color)
        {
            if (row < 0 || row >= Constants.KbRows)
            {
                throw new ArgumentException($"Row out of range: {row}");
            }

            for (int i = 0; i < Constants.KbColumns; i++)
            {
                _grid[row, i] = color;
            }
        }

        public void SetColumn(int column, ChromaColor color)
        {
            if (column < 0 || column >= Constants.KbColumns)
            {
                throw new ArgumentException($"Column out of range: {column}");
            }

            for (int i = 0; i < Constants.KbRows; i++)
            {
                _grid[i, column] = color;
            }
        }

        public void SetAll(ChromaColor color)
        {
            for (int i = 0; i < Constants.KbRows; i++)
            {
                for (int j = 0; j < Constants.KbColumns; j++)
                {
                    _grid[i, j] = color;
                }
            }
        }

        public ChromaColor GetPosition(int row, int column)
        {
            if (row < 0 || row >= Constants.KbRows)
            {
                throw new ArgumentException($"Row out of range: {row}");
            }

            if (column < 0 || column >= Constants.KbColumns)
            {
                throw new ArgumentException($"Column out of range: {column}");
            }

            return _grid[row, column];
        }

        public void Serialize(uint[][] buffer)
        {
            for (int i = 0; i < Constants.KbRows; i++)
            {
                for (int j = 0; j < Constants.KbColumns; j++)
                {
                    buffer[i][j] = _grid[i, j].ToBgr();
                }
            }
        }
    }
}
