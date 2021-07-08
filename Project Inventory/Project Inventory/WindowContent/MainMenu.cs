using Project_Inventory.Tools;
using System.Windows.Controls;

namespace Project_Inventory
{
    /// <summary>
    /// Page to access all menus
    /// </summary>
    public class MainMenu : WindowContent
    {
        private string[] topGridButtons;
        private string[] bottomGridButtons;
        private RoutedEventLibrary[] switchEvents;

        private int widthLimit;

        public MainMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualStorageId, int _actualDataId)
            : base(toolBox, _router, requestCenter, _actualStorageId, _actualDataId)
        {
            topGridButtons = new string[] { "Logo Application" };
            bottomGridButtons = new string[] { "Storage Selection", "Formulaire Type", "Storage Viewer" };

            switchEvents = new RoutedEventLibrary[3];
            RoutedEventLibrariesInit(switchEvents);
            switchEvents[0].changePageEvent = GetEventHandler(WindowsName.StorageSelectionMenu);
            switchEvents[1].changePageEvent = GetEventHandler(WindowsName.AddStorage);
            switchEvents[2].changePageEvent = GetEventHandler(WindowsName.StorageViewerPage);

            widthLimit = 5;
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 1, SkinsName.TopStretch, SkinsName.HeightOneTier);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, SkinsName.Standart, SkinsName.CenterCenter);
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            ButtonPlacer(bottomGrid, bottomGridButtons.Length, widthLimit, SkinsName.BottomStretch, SkinsName.HeightTwoTier);

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, switchEvents, SkinsName.Standart, SkinsName.CenterCenter);
        }
    }
}
