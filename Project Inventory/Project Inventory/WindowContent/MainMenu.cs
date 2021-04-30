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

        public MainMenu(ToolBox toolBox, Router _router)
            : base(toolBox, _router)
        {
            topGridButtons = new string[] { "Logo Application" };
            bottomGridButtons = new string[] { "Storage Selection", "Formulaire Type", "Storage Viewer" };
            switchEvents = new RoutedEventHandler[] { GetEventHandler("StorageSelectionMenu"), GetEventHandler("FormPage"), GetEventHandler("storageViewerPage") };

            widthLimit = 5;
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightOneTier");

            toolBox.CreateButtonsToGridByTab(topGrid, topGridButtons, "standart", "CenterCenter");
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            ButtonPlacer(bottomGrid, bottomGridButtons.Length, widthLimit, "BottomStretch", "HeightTwoTier");

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, switchEvents, "standart", "CenterCenter");
        }
    }
}
