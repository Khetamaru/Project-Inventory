using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System.Windows.Controls;

namespace Project_Inventory
{
    class StorageViewerPage : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private enum status {
            VIEWER,
            MODIFIER
        }

        private status viewerStatus;

        private Grid capGrid;
        private Data[] dataTab;
        private string[,] stringTab;
        private string[,] indicTab;

        public StorageViewerPage(ToolBox ToolBox, Router _router, RequestCenter requestCenter, int _actualStorageId, int _actualDataId)
            : base(ToolBox, _router, requestCenter, _actualStorageId, _actualDataId)
        {
            viewerStatus = status.VIEWER;

            topGridButtons = new string[] { "Return" };

            topSwitchEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(topSwitchEvents);
            topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.MainMenu);

            capGrid = new Grid();

            LoadBDDInfos();
        }

        public void LoadBDDInfos()
        {
            dataTab = JsonCenter.LoadStorageViewerInfos(requestCenter, actualStorageId);

            int i;
            int j;

            stringTab = new string[dataTab.Length, dataTab[0].DataText.Length];
            indicTab = new string[dataTab.Length, dataTab[0].DataText.Length];

            for (i = 0; i < dataTab.Length; i++)
            {
                for (j = 0; j < dataTab[0].DataText.Length; j++)
                {
                    stringTab[i, j] = dataTab[i].DataText[j];
                    indicTab[i, j] = dataTab[i].DataType[j];
                }
            }
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 1, SkinsName.TopStretch, SkinsName.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents, SkinsName.StandartLittleMargin, SkinsName.TopRight);
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            toolBox.CreateScrollableGrid(bottomGrid, capGrid, 
                                         1, 1, 
                                         stringTab.GetLength(0), stringTab.GetLength(1),
                                         SkinsName.BottomStretch, SkinsName.HeightNintyPercent,
                                         SkinsName.Standart, SkinsName.Center, 
                                         stringTab, indicTab);
        }
    }
}
