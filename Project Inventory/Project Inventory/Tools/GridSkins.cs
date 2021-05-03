using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project_Inventory
{
    public static class GridSkins
    {
        static double tierMultiplier = 3.07;
        static double tenPercentMultiplier = 10;

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

        public static void WidthOneTier(Grid grid, double screenWidth)
        {
            grid.Width = screenWidth / tierMultiplier;
        }
        public static void WidthTwoTier(Grid grid, double screenWidth)
        {
            grid.Width = screenWidth / tierMultiplier * 2;
        }

        public static void HeightOneTier(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight / tierMultiplier;
        }

        public static void HeightTwoTier(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight / tierMultiplier * 2;
        }

        public static void HeightTenPercent(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight / tenPercentMultiplier;
        }

        public static void HeightEightPercent(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight / tenPercentMultiplier * 8;
        }

        public static void HeightNintyPercent(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight / tenPercentMultiplier * 9;
        }

        public static void ColumnHeightTwentyPercent(ColumnDefinition column, double screenWidth)
        {
            column.Width = new GridLength(screenWidth / tenPercentMultiplier * 2, GridUnitType.Pixel);
        }

        public static void ColumnHeightFifteenPercent(ColumnDefinition column, double screenWidth)
        {
            column.Width = new GridLength(screenWidth / tenPercentMultiplier * 1.5, GridUnitType.Pixel);
        }

        public static void ColumnHeightTenPercent(ColumnDefinition column, double screenWidth)
        {
            column.Width = new GridLength(screenWidth / tenPercentMultiplier, GridUnitType.Pixel);
        }

        public static void RowHeightTenPercent(RowDefinition row, double screenHeight)
        {
            row.Height = new GridLength(screenHeight / tenPercentMultiplier * 0.5, GridUnitType.Pixel);
        }

        public static void RowHeightTwentyPercent(RowDefinition row, double screenHeight)
        {
            row.Height = new GridLength(screenHeight / tenPercentMultiplier * 2, GridUnitType.Pixel);
        }
        public static void RowHeightFifteenPercent(RowDefinition row, double screenHeight)
        {
            row.Height = new GridLength(screenHeight / tenPercentMultiplier * 1.5, GridUnitType.Pixel);
        }
    }
}
