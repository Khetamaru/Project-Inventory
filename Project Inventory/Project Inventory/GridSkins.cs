using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project_Inventory
{
    public static class GridSkins
    {
        static double tierMultiplier = 3.07;

        // Grid Location //

        public static Grid RowTopTier(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid RowBottomTier(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid RowCenterTier(Grid grid)
        {
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }

        public static Grid ColumnGridSkin(Grid grid)
        {
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;

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
    }
}
