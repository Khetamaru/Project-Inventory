using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Project_Inventory.BDD;
using Project_Inventory.Tools;

namespace Project_Inventory
{
    public class ListMenu : WindowContent
    {
        private Grid capGrid;

        private status viewerStatus;
        private enum status
        {
            VIEWER,
            MODIFIER
        }

        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private CustomList[] bottomGridButtons;
        private RoutedEventLibrary[] bottomSwitchEvents;

        private RoutedEventHandler reloadEvent;

        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        private int widthLimit;

        public ListMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualStorageId, int _actualDataId, int _actualCustomId, RoutedEventHandler _reloadEvent)
               : base(toolBox, _router, requestCenter, _actualStorageId, _actualDataId, _actualCustomId)
        {
            viewerStatus = status.VIEWER;

            topGridButtons = new string[] { "Modify", "Return" };
            saveButton = new string[] { "Save" };

            reloadEvent = _reloadEvent;

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
            bottomGridButtons = JsonCenter.LoadListMenuInfos(requestCenter);
            bottomSwitchEvents = JsonCenter.SetEventHandlerTab(bottomGridButtons.Length, GetEventHandler(WindowsName.ListViewerPage));
        }



        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 2, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid,
                                                   topGridButtons,
                                                   topSwitchEvents,
                                                   new SkinName[] { SkinName.StandartLittleMargin, SkinName.StandartLittleMargin },
                                                   new SkinLocation[] { SkinLocation.TopLeft, SkinLocation.TopRight });
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            switch (viewerStatus)
            {
                case status.VIEWER:

                    LoadBDDInfos();

                    toolBox.SetUpGrid(bottomGrid, 1, 1, SkinLocation.BottomStretch, SkinSize.HeightNintyPercent);
                    capGrid = new Grid();

                    toolBox.ButtonPlacer(capGrid, bottomGridButtons.Length, widthLimit, SkinLocation.BottomStretch, SkinSize.HeightNintyPercent);
                    RoutedIdSetup(bottomGridButtons);

                    toolBox.CreateSwitchButtonsToGridByTab(capGrid, bottomGridButtons, bottomSwitchEvents, SkinName.Standart, SkinLocation.CenterCenter);
                    bottomGrid.Children.Add(capGrid);
                    break;

                case status.MODIFIER:

                    toolBox.SetUpGrid(bottomGrid, 1, 1, SkinLocation.BottomStretch, SkinSize.HeightTenPercent);

                    toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, saveButton, saveEvents, SkinName.StandartLittleMargin, SkinLocation.BottomCenter);
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

                    toolBox.SetUpGrid(centerGrid, 1, 1, SkinLocation.StretchStretch, SkinSize.HeightEightPercent);
                    capGrid = new Grid();

                    toolBox.ButtonPlacer(capGrid, bottomGridButtons.Length + 1, widthLimit, SkinLocation.BottomStretch, SkinSize.HeightEightPercent);
                    RoutedIdSetup(bottomGridButtons);

                    toolBox.CreateTabToGrid(capGrid, bottomGridButtons, SkinLocation.CenterCenter);
                    centerGrid.Children.Add(capGrid);

                    AddDeleteButtons();

                    break;
            }
        }

        /// <summary>
        /// Insert the stored procedure in the button
        /// </summary>
        /// <param name="storageLibrary"></param>
        public void RoutedIdSetup(CustomList[] customListLibrary)
        {
            var i = 0;

            foreach (CustomList customList in customListLibrary)
            {
                bottomSwitchEvents[i].updateIdEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    IDSetup(sender, e, customList.id);
                });
                i++;
            }
        }

        /// <summary>
        /// Load actuel custom list id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        public void IDSetup(object sender, RoutedEventArgs e, int id)
        {
            actualCustomListId = id;
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
            List<UIElement> elementList = toolBox.ExtractFormInfos(capGrid);

            CustomList optionnalAdd = new CustomList(42, string.Empty, new List<string>());

            List<int> changesList = toolBox.GetUIElements(elementList, bottomGridButtons, out optionnalAdd);

            foreach (int change in changesList)
            {
                requestCenter.PutRequest(BDDTabsName.CustomListLibraries.ToString() + "/" + bottomGridButtons[change].id, bottomGridButtons[change].ToJsonId());
            }

            if (optionnalAdd != null)
            {
                requestCenter.PostRequest(BDDTabsName.CustomListLibraries.ToString(), optionnalAdd.ToJson());
            }
        }

        /// <summary>
        /// Create buttons to add that delete selected custom list
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
                        var customList = bottomGridButtons[j + (i * 5)];
                        tempRouter.optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                        {
                            DeleteCustomList(sender, e, customList.id);
                        });
                        tempRouter.resetPageEvent = reloadEvent;

                        tempButton = toolBox.CreateSwitchButtonImage(ImagesName.RedCross, tempRouter, SkinName.StandartLittleMargin, SkinLocation.CenterLeft, ImageSizesName.Small);

                        Grid.SetRow(tempButton, i);
                        Grid.SetColumn(tempButton, j);

                        capGrid.Children.Add(tempButton);
                    }
                }
            }
        }

        /// <summary>
        /// Stored procedure for custom list delete
        /// </summary>
        private void DeleteCustomList(object sender, RoutedEventArgs e, int CustomListId)
        {
            if (PopUpCenter.ActionValidPopup())
            {
                requestCenter.DeleteRequest(BDDTabsName.CustomListLibraries.ToString() + "/" + CustomListId);
            }
        }
    }
}
