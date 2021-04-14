using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project_Inventory
{
    public static class GridSkins
    {
        public static Grid RowTopTier(Grid grid, double screenHeight)
        {
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid RowBottomTier(Grid grid, double screenHeight)
        {
            grid.VerticalAlignment = VerticalAlignment.Bottom;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
        public static Grid RowCenterTier(Grid grid, double screenHeight)
        {
            grid.VerticalAlignment = VerticalAlignment.Center;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }

        public static Grid ColumnGridSkin(Grid grid, double screenHeight)
        {
            grid.Height = screenHeight;

            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;

            grid.ShowGridLines = true;
            grid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            return grid;
        }
    }
}
