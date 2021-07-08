using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    /// <summary>
    /// Page to see what is in a specific storage and interact with it
    /// </summary>
    class StorageViewerPage : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;
        private RoutedEventHandler reloadEvent;

        /// <summary>
        /// Use to know whitch is the current status of the page
        /// </summary>
        private enum status {
            VIEWER,
            MODIFIER
        }

        private status viewerStatus;

        private Grid capGrid;
        private Data[] dataTab;
        private string[,] stringTab;
        private string[,] indicTab;

        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        public StorageViewerPage(ToolBox ToolBox, Router _router, RequestCenter requestCenter, int _actualStorageId, int _actualDataId, RoutedEventHandler _reloadEvent)
            : base(ToolBox, _router, requestCenter, _actualStorageId, _actualDataId)
        {
            viewerStatus = status.VIEWER;
            reloadEvent = _reloadEvent;

            topGridButtons = new string[] { "Modify", "Return" };
            saveButton = new string[] { "Save" };

            topSwitchEvents = new RoutedEventLibrary[2];
            RoutedEventLibrariesInit(topSwitchEvents);

            topSwitchEvents[0].resetPageEvent = reloadEvent;
            topSwitchEvents[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SwitchStatus(sender, e); });
            topSwitchEvents[1].changePageEvent = GetEventHandler(WindowsName.StorageSelectionMenu);

            saveEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(saveEvents);
            saveEvents[0].resetPageEvent = reloadEvent;
            saveEvents[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SwitchStatus(sender, e); });
            saveEvents[0].optionalEventTwo = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SaveDatas(sender, e); });

            capGrid = new Grid();

            LoadBDDInfos();
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
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
            toolBox.SetUpGrid(topGrid, 1, 2, SkinsName.TopStretch, SkinsName.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents,
                                                   new SkinsName[] { SkinsName.StandartLittleMargin, SkinsName.StandartLittleMargin },
                                                   new SkinsName[] { SkinsName.TopLeft, SkinsName.TopRight });
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            switch (viewerStatus)
            {
                case status.VIEWER:

                    capGrid = new Grid();

                    toolBox.CreateScrollableGrid(bottomGrid, capGrid,
                                         1, 1,
                                         stringTab.GetLength(0), stringTab.GetLength(1),
                                         SkinsName.BottomStretch, SkinsName.HeightNintyPercent,
                                         SkinsName.Center,
                                         stringTab, indicTab);
                    break;

                case status.MODIFIER:
                    toolBox.SetUpGrid(bottomGrid, 1, 1, SkinsName.BottomStretch, SkinsName.HeightTenPercent);

                    toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, saveButton, saveEvents, SkinsName.StandartLittleMargin, SkinsName.BottomCenter);
                    break;
            }
        }

        public new void CenterGridInit(Grid centerGrid)
        {
            switch (viewerStatus)
            {
                case status.VIEWER:

                    centerGrid = null;

                    break;

                case status.MODIFIER:

                    capGrid = new Grid();

                    toolBox.CreateScrollableGridModfiable(centerGrid, capGrid,
                                         1, 1,
                                         stringTab.GetLength(0) + 1, stringTab.GetLength(1),
                                         SkinsName.StretchStretch, SkinsName.HeightEightPercent,
                                         SkinsName.Center,
                                         stringTab, indicTab);
                    break;
            }
        }

        /// <summary>
        /// Switch between the two modes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchStatus(object sender, RoutedEventArgs e)
        {
            switch(viewerStatus)
            {
                case status.VIEWER:

                    viewerStatus = status.MODIFIER;
                    topGridButtons[0] = "Cancel";

                    break;

                case status.MODIFIER:

                    viewerStatus = status.VIEWER;
                    topGridButtons[0] = "Modify";

                    break;
            }
        }

        /// <summary>
        /// Apply changes indicated in the "Modify" mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDatas(object sender, RoutedEventArgs e)
        {
            List<int> changesList = toolBox.GetUIElements(capGrid, dataTab, indicTab);

            Data optionnalAdd = new Data(42, actualStorageId, new string[dataTab[0].DataText.Length], dataTab[0].DataType, false);

            foreach(int change in changesList)
            {
                requestCenter.PutRequest("DataLibraries/" + dataTab[change].id, dataTab[change].ToJsonId());
            }

            if (toolBox.OptionnalAdd(capGrid, dataTab, optionnalAdd))
            {
                requestCenter.PostRequest("DataLibraries", optionnalAdd.ToJson());
            }
        }
    }
}
