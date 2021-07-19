using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    /// <summary>
    /// Page to create a storage or select one to see details of it
    /// </summary>
    public class StorageSelectionMenu : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;
        private Storage[] bottomGridButtons;
        private RoutedEventLibrary[] bottomSwitchEvents;
        private RoutedEventHandler reloadEvent;

        private int widthLimit;

        private enum status {
            VIEWER,
            MODIFIER
        }

        private status viewerStatus;

        private Grid capGrid;
        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        public StorageSelectionMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualStorageId, int _actualDataId, RoutedEventHandler _reloadEvent)
            : base(toolBox, _router, requestCenter, _actualStorageId, _actualDataId)
        {
            viewerStatus = status.VIEWER;
            reloadEvent = _reloadEvent;

            topGridButtons = new string[] { "Modify", "Return" };
            saveButton = new string[] { "Save" };

            topSwitchEvents = new RoutedEventLibrary[2];
            RoutedEventLibrariesInit(topSwitchEvents);

            topSwitchEvents[0].resetPageEvent = reloadEvent;
            topSwitchEvents[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SwitchStatus(sender, e); });
            topSwitchEvents[1].changePageEvent = GetEventHandler(WindowsName.MainMenu);

            saveEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(saveEvents);
            saveEvents[0].resetPageEvent = reloadEvent;
            saveEvents[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SwitchStatus(sender, e); });
            saveEvents[0].optionalEventTwo = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SaveDatas(sender, e); });

            capGrid = new Grid();

            LoadBDDInfos();
            
            widthLimit = 5;
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void LoadBDDInfos()
        {
            bottomGridButtons = JsonCenter.LoadStorageSelectionInfos(requestCenter);
            bottomSwitchEvents = JsonCenter.SetEventHandlerTab(bottomGridButtons.Length, GetEventHandler(WindowsName.StorageViewerPage));
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 2, SkinsName.TopStretch, SkinsName.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, 
                                                   topGridButtons, 
                                                   topSwitchEvents, 
                                                   new SkinsName[] { SkinsName.StandartLittleMargin, SkinsName.StandartLittleMargin }, 
                                                   new SkinsName[] { SkinsName.TopLeft, SkinsName.TopRight });
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            switch (viewerStatus)
            {
                case status.VIEWER:

                    LoadBDDInfos();

                    toolBox.SetUpGrid(bottomGrid, 1, 1, SkinsName.BottomStretch, SkinsName.HeightNintyPercent);
                    capGrid = new Grid();

                    toolBox.ButtonPlacer(capGrid, bottomGridButtons.Length, widthLimit, SkinsName.BottomStretch, SkinsName.HeightNintyPercent);
                    RoutedIdSetup(bottomGridButtons);

                    toolBox.CreateSwitchButtonsToGridByTab(capGrid, bottomGridButtons, bottomSwitchEvents, SkinsName.Standart, SkinsName.CenterCenter);
                    bottomGrid.Children.Add(capGrid);
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

                    LoadBDDInfos();

                    toolBox.SetUpGrid(centerGrid, 1, 1, SkinsName.StretchStretch, SkinsName.HeightEightPercent);
                    capGrid = new Grid();

                    toolBox.ButtonPlacer(capGrid, bottomGridButtons.Length + 1, widthLimit, SkinsName.BottomStretch, SkinsName.HeightEightPercent);
                    RoutedIdSetup(bottomGridButtons);

                    toolBox.CreateTabToGrid(capGrid, bottomGridButtons, SkinsName.Center);
                    centerGrid.Children.Add(capGrid);

                    AddDeleteButtons();

                    break;
            }
        }

        /// <summary>
        /// Insert the stored procedure in the button
        /// </summary>
        /// <param name="storageLibrary"></param>
        public void RoutedIdSetup(Storage[] storageLibrary)
        {
            var i = 0;

            foreach(Storage storage in storageLibrary)
            {
                bottomSwitchEvents[i].updateIdEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    IDSetup(sender, e, storage.id);
                });
                i++;
            }
        }

        /// <summary>
        /// Load actuel storage id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        public void IDSetup(object sender, RoutedEventArgs e, int id)
        {
            actualStorageId = id;
        }

        /// <summary>
        /// Switch between the two modes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchStatus(object sender, RoutedEventArgs e)
        {
            switch (viewerStatus)
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
            List<int> changesList = toolBox.GetUIElements(capGrid, bottomGridButtons);

            Storage optionnalAdd = new Storage(42, string.Empty);

            foreach (int change in changesList)
            {
                requestCenter.PutRequest(BDDTabsName.StorageLibraries.ToString() + "/" + bottomGridButtons[change].id, bottomGridButtons[change].ToJsonId());
            }

            if (toolBox.OptionnalAdd(capGrid, bottomGridButtons, optionnalAdd))
            {
                requestCenter.PostRequest(BDDTabsName.StorageLibraries.ToString(), optionnalAdd.ToJson());
            }
        }

        /// <summary>
        /// Create buttons to add that delete selected storage
        /// </summary>
        /// <returns></returns>
        private void AddDeleteButtons()
        {
            List<Button> buttonList = new List<Button>();
            Button tempButton;
            RoutedEventLibrary tempRouter;

            int i = bottomGridButtons.Length;
            int j = 1;

            while (i >= 5)
            {
                i -= 5;
                j++;
            }

            int rowNb = j;

            for (i = 0; i < rowNb; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    if (bottomGridButtons.Length > j + (i * 5))
                    {
                        tempRouter = new RoutedEventLibrary();
                        var storage = bottomGridButtons[j + (i * 5)];
                        tempRouter.optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                        {
                            DeleteStorage(sender, e, storage.id);
                        });
                        tempRouter.resetPageEvent = reloadEvent;

                        tempButton = toolBox.CreateSwitchButtonImage(ImagesName.RedCross, tempRouter, SkinsName.StandartLittleMargin, SkinsName.CenterLeft, ImageSizesName.Small);

                        Grid.SetRow(tempButton, i);
                        Grid.SetColumn(tempButton, j);

                        capGrid.Children.Add(tempButton);
                    }
                }
            }
        }

        /// <summary>
        /// Stored procedure for storage delete
        /// </summary>
        private void DeleteStorage(object sender, RoutedEventArgs e, int StorageId)
        {
            if (ConfirmPopup())
            {
                requestCenter.DeleteRequest(BDDTabsName.DataLibraries.ToString() + "/storage/" + StorageId);
                requestCenter.DeleteRequest(BDDTabsName.StorageLibraries.ToString() + "/" + StorageId);
            }
        }

        /// <summary>
        /// A yes/no pop up to be sure user want to delete
        /// </summary>
        /// <returns></returns>
        private bool ConfirmPopup()
        {
            string sMessageBoxText = "Are you sure ?";
            string sCaption = "Validation Pop Up";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    return true;

                case MessageBoxResult.No:
                    break;

                case MessageBoxResult.Cancel:
                    break;
            }

            return false;
        }
    }
}
