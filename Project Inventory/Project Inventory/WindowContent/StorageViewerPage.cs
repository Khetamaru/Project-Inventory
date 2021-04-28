using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    class StorageViewerPage : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventHandler[] topSwitchEvents;

        private string[,] stringTab;
        private string[,] indicTab;

        public StorageViewerPage(VisualElements_ToolBox visualElements_ToolBox, Router _router)
            : base(visualElements_ToolBox, _router)
        {
            topGridButtons = new string[] { "Return" };

            topSwitchEvents = new RoutedEventHandler[] { GetEventHandler("MainMenu") };
        }

        public new Grid TopGridInit(Grid topGrid)
        {
            topGrid = toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightTenPercent");

            topGrid = toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents, "StandartLittleMargin", "TopRight");

            return topGrid;
        }

        public new Grid BottomGridInit(Grid bottomGrid)
        {
            bottomGrid = toolBox.SetUpGrid(bottomGrid, stringTab.GetLength(0), stringTab.GetLength(1), "BottomStretch", "HeightNintyPercent");

            bottomGrid = toolBox.CreateTabToGrid(bottomGrid, stringTab, indicTab, "standart", "center");

            return bottomGrid;
        }
    }
}
