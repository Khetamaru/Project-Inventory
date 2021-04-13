using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project_Inventory
{
    public static class GridSkins
    {
        public static Grid RowGridSkin(Grid grid, double screenWidth)
        {
            grid.Width = screenWidth;

            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;

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
