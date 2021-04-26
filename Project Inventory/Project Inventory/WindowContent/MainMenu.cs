using System;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public class MainMenu : WindowContent
    {
        private string[] topGridButtons;
        private string[] bottomGridButtons;
        private RoutedEventHandler[] switchEvents;

        private int widthLimit;

        public MainMenu(VisualElements_ToolBox visualElements_ToolBox, Router _router)
            : base(visualElements_ToolBox, _router)
        {
            topGridButtons = new string[] { "Logo Application" };
            bottomGridButtons = new string[] { "Menu n°1", "Menu n°2" };
            switchEvents = new RoutedEventHandler[] { GetEventHandler("StorageSelectionMenu"), GetEventHandler("FormPage") };

            widthLimit = 5;
        }

        public new Grid TopGridInit(Grid topGrid)
        {
            topGrid = toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightOneTier");

            topGrid = toolBox.CreateButtonsToGridByTab(topGrid, topGridButtons, "standart", "CenterCenter");

            return topGrid;
        }

        public new Grid BottomGridInit(Grid bottomGrid)
        {
            bottomGrid = ButtonPlacer(bottomGrid, bottomGridButtons.Length, widthLimit, "BottomStretch", "HeightTwoTier");

            bottomGrid = toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, switchEvents, "standart", "CenterCenter");

            return bottomGrid;
        }
    }
}
