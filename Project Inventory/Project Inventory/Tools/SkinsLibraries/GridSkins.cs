using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project_Inventory
{
    /// <summary>
    /// Class to set up skin of grids
    /// </summary>
    public static class GridSkins
    {
        public static void SetUp(Grid grid)
        {
            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);
        }

        // Grid Location //

        public static void TopLeft(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.HorizontalAlignment = HorizontalAlignment.Left;

            SetUp(grid);
        }
        public static void StretchLeft(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.HorizontalAlignment = HorizontalAlignment.Left;

            SetUp(grid);
        }
        public static void BottomLeft(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Left;

            SetUp(grid);
        }

        public static void TopStretch(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            SetUp(grid);
        }
        public static void BottomStretch(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            SetUp(grid);
        }
        public static void StretchStretch(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            SetUp(grid);
        }
        public static void CenterCenter(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.HorizontalAlignment = HorizontalAlignment.Center;

            SetUp(grid);
        }

        public static void CenterStretch(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            SetUp(grid);
        }

        public static void StretchCenter(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.HorizontalAlignment = HorizontalAlignment.Center;

            SetUp(grid);
        }

        public static void TopRight(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.HorizontalAlignment = HorizontalAlignment.Right;

            SetUp(grid);
        }
        public static void StretchRight(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.HorizontalAlignment = HorizontalAlignment.Right;

            SetUp(grid);
        }
        public static void BottomRight(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Right;

            SetUp(grid);
        }
        public static void TopCenter(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.HorizontalAlignment = HorizontalAlignment.Center;

            SetUp(grid);
        }
        public static void BottomCenter(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Center;

            SetUp(grid);
        }
        public static void CenterRight(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.HorizontalAlignment = HorizontalAlignment.Right;

            SetUp(grid);
        }
        public static void CenterLeft(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.HorizontalAlignment = HorizontalAlignment.Left;

            SetUp(grid);
        }

        // Grid Length //

        public static void ColumnHeightTenPercent(ColumnDefinition column, double screenWidth)
        {
            column.Width = new GridLength(screenWidth * 0.10, GridUnitType.Pixel);
        }

        public static void ColumnHeightFifteenPercent(ColumnDefinition column, double screenWidth)
        {
            column.Width = new GridLength(screenWidth * 0.15, GridUnitType.Pixel);
        }

        public static void ColumnHeightTwentyPercent(ColumnDefinition column, double screenWidth)
        {
            column.Width = new GridLength(screenWidth * 0.20, GridUnitType.Pixel);
        }

        public static void WidthOneTier(Grid grid, double screenWidth)
        {
            grid.Width = screenWidth * 0.34;
        }
        public static void WidthTwoTier(Grid grid, double screenWidth)
        {
            grid.Width = screenWidth * 0.66;
        }

        public static void ColumnHeightTier(ColumnDefinition column, double screenWidth)
        {
            column.Width = new GridLength(screenWidth * 0.34, GridUnitType.Pixel);
        }

        public static void HeightTenPercent(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight * 0.10;
        }

        public static void RowHeightTenPercent(RowDefinition row, double screenHeight)
        {
            row.Height = new GridLength(screenHeight * 0.10, GridUnitType.Pixel);
        }

        public static void RowHeightFifteenPercent(RowDefinition row, double screenHeight)
        {
            row.Height = new GridLength(screenHeight * 0.15, GridUnitType.Pixel);
        }

        public static void RowHeightTwentyPercent(RowDefinition row, double screenHeight)
        {
            row.Height = new GridLength(screenHeight * 0.20, GridUnitType.Pixel);
        }

        public static void HeightOneTier(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight * 0.34;
        }

        public static void HeightTwoTier(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight * 0.66;
        }

        public static void HeightEightPercent(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight * 0.80;
        }

        public static void HeightNintyPercent(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight * 0.90;
        }
    }
}
