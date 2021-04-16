using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project_Inventory
{
    public static class GridSkins
    {
        static double tierMultiplier = 3.07;
        static double tenPercentMultiplier = 10;

        // Grid Location //

        public static Grid TopLeft(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.HorizontalAlignment = HorizontalAlignment.Left;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid StretchLeft(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.HorizontalAlignment = HorizontalAlignment.Left;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid BottomLeft(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Left;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }

        public static Grid TopStretch(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid BottomStretch(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid StretchStretch(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid CenterCenter(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.HorizontalAlignment = HorizontalAlignment.Center;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }

        public static Grid TopRight(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.HorizontalAlignment = HorizontalAlignment.Right;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid StretchRight(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.HorizontalAlignment = HorizontalAlignment.Right;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid BottomRight(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Right;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid TopCenter(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.HorizontalAlignment = HorizontalAlignment.Center;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid BottomCenter(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Center;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid CenterRight(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.HorizontalAlignment = HorizontalAlignment.Right;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid CenterLeft(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.HorizontalAlignment = HorizontalAlignment.Left;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }

        // Grid Length //

        public static Grid WidthOneTier(Grid grid, double screenWidth)
        {
            grid.Width = screenWidth / tierMultiplier;

            return grid;
        }
        public static Grid WidthTwoTier(Grid grid, double screenWidth)
        {
            grid.Width = screenWidth / tierMultiplier * 2;

            return grid;
        }

        public static Grid HeightOneTier(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight / tierMultiplier;

            return grid;
        }

        public static Grid HeightTwoTier(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight / tierMultiplier * 2;

            return grid;
        }

        public static Grid HeightTenPercent(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight / tenPercentMultiplier;

            return grid;
        }

        public static Grid HeightNintyPercent(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight / tenPercentMultiplier * 9;

            return grid;
        }
    }
}
