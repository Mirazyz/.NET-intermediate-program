using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLife
{
    class Grid
    {
        private readonly int sizeX;
        private readonly int sizeY;
        private readonly Cell[,] cells;
        private readonly Cell[,] nextGenerationCells;
        private static Random rnd;
        private readonly Canvas drawCanvas;
        private readonly Ellipse[,] cellsVisuals;

        public Grid(Canvas c)
        {
            drawCanvas = c;
            rnd = new Random();
            sizeX = (int) (c.Width / 5);
            sizeY = (int)(c.Height / 5);
            cells = new Cell[sizeX, sizeY];
            nextGenerationCells = new Cell[sizeX, sizeY];
            cellsVisuals = new Ellipse[sizeX, sizeY];
 
            InitializeCells();
            SetRandomPattern();
            InitCellsVisuals();
            UpdateGraphics();   
        }

        public void Clear()
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    cells[i, j] = new Cell(i, j, 0, false);
                    nextGenerationCells[i, j] = new Cell(i, j, 0, false);
                    cellsVisuals[i, j].Fill = Brushes.Gray;
                }
            }
        }

        void InitializeCells()
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    cells[i, j] = new Cell(i, j, 0, false);
                    nextGenerationCells[i, j] = new Cell(i, j, 0, false);
                }
            }
        }

        void MouseMove(object sender, MouseEventArgs e)
        {
            var cellVisual = sender as Ellipse;
            
            int i = (int) cellVisual.Margin.Left / 5;
            int j = (int) cellVisual.Margin.Top / 5;
            
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!cells[i, j].IsAlive)
                {
                    cells[i, j].IsAlive = true;
                    cells[i, j].Age = 0;
                    cellVisual.Fill = Brushes.White;
                }
            }
        }

        public void UpdateGraphics()
        {
            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                    cellsVisuals[i, j].Fill = cells[i, j].IsAlive
                                                  ? (cells[i, j].Age < 2 ? Brushes.White : Brushes.DarkGray)
                                                  : Brushes.Gray;
        }

        public void InitCellsVisuals()
        {
            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                {
                    cellsVisuals[i, j] = new Ellipse();
                    cellsVisuals[i, j].Width = cellsVisuals[i, j].Height = 5;
                    double left = cells[i, j].PositionX;
                    double top = cells[i, j].PositionY;
                    cellsVisuals[i, j].Margin = new Thickness(left, top, 0, 0);
                    cellsVisuals[i, j].Fill = Brushes.Gray;
                    drawCanvas.Children.Add(cellsVisuals[i, j]);

                    cellsVisuals[i, j].MouseMove += MouseMove;
                    cellsVisuals[i, j].MouseLeftButtonDown += MouseMove;
                 }
            UpdateGraphics();
                    
        }
        
        public static bool GetRandomBoolean()
        {
            return rnd.NextDouble() > 0.8;
        }

        public void SetRandomPattern()
        {
            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                    cells[i, j].IsAlive = GetRandomBoolean();
        }
        
        public void UpdateToNextGeneration()
        {
            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                {
                    cells[i, j].IsAlive = nextGenerationCells[i, j].IsAlive;
                    cells[i, j].Age = nextGenerationCells[i, j].Age;
                }

            UpdateGraphics();
        }
        
        public void Update()
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    nextGenerationCells[i, j] = CalculateNextGeneration(i,j);          // OPTIMIZED
                }
            }

            UpdateToNextGeneration();
        }

        public Cell CalculateNextGeneration(int row, int column)    // UNOPTIMIZED
        {
            bool isAlive = cells[row, column].IsAlive;
            int neightborsCount = CountNeighbors(row, column);

            if (isAlive && neightborsCount < 2)
            {
                return new Cell(row, column, 0, false);
            }

            if (isAlive && (neightborsCount == 2 || neightborsCount == 3))
            {
                cells[row, column].Age++;
                return new Cell(row, column, cells[row, column].Age, true);
            }

            if (!isAlive && neightborsCount == 3)
            {
                return new Cell(row, column, 0, true);
            }

            if (isAlive && neightborsCount > 3)
            {
                return new Cell(row, column, 0, false);
            }

            return new Cell(row, column, 0, false);
        }

        public void CalculateNextGeneration(int row, int column, ref bool isAlive, ref int age)     // OPTIMIZED
        {
            isAlive = cells[row, column].IsAlive;
            age = cells[row, column].Age;

            int count = CountNeighbors(row, column);

            if (isAlive && count < 2)
            {
                isAlive = false;
                age = 0;
            }

            if (isAlive && (count == 2 || count == 3))
            {
                cells[row, column].Age++;
                isAlive = true;
                age = cells[row, column].Age;
            }

            if (!isAlive && count == 3)
            {
                isAlive = true;
                cells[row, column].IsAlive = true;
                age = 0;
            }

            if (isAlive && count > 3)
            {
                isAlive = false;
                cells[row, column].IsAlive = false;
                age = 0;
            }
        }

        public int CountNeighbors(int i, int j)
        {
            int count = 0;

            if (i != sizeX - 1 && cells[i + 1, j].IsAlive) count++;
            if (i != sizeX - 1 && j != sizeY - 1 && cells[i + 1, j + 1].IsAlive) count++;
            if (j != sizeY - 1 && cells[i, j + 1].IsAlive) count++;
            if (i != 0 && j != sizeY - 1 && cells[i - 1, j + 1].IsAlive) count++;
            if (i != 0 && cells[i - 1, j].IsAlive) count++;
            if (i != 0 && j != 0 && cells[i - 1, j - 1].IsAlive) count++;
            if (j != 0 && cells[i, j - 1].IsAlive) count++;
            if (i != sizeX - 1 && j != 0 && cells[i + 1, j - 1].IsAlive) count++;

            return count;
        }
    }
}