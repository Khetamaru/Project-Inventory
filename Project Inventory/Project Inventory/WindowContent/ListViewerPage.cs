using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Project_Inventory.BDD;
using Project_Inventory.Tools;

namespace Project_Inventory
{
    class ListViewerPage : WindowContent
    {
        private Grid capGrid;

        private CustomList actualCustomList;

        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private RoutedEventHandler reloadEvent;

        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        public ListViewerPage(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualStorageId, int _actualDataId, int _actualCustomId, RoutedEventHandler _reloadEvent)
            : base(toolBox, _router, requestCenter, _actualStorageId, _actualDataId, _actualCustomId)
        {
            topGridButtons = new string[] { "Return" };
            saveButton = new string[] { "Save" };

            reloadEvent = _reloadEvent;

            topSwitchEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(topSwitchEvents);

            topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.ListMenu);

            saveEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(saveEvents);
            saveEvents[0].resetPageEvent = reloadEvent;
            saveEvents[0].optionalEventTwo = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SaveDatas(sender, e); });

            LoadBDDInfos();
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void LoadBDDInfos()
        {
            CustomList[] temp = JsonCenter.LoadListViewerPageInfos(requestCenter, actualCustomListId);

            actualCustomList = temp[0];
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 1, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonToGrid(topGrid,
                                             topGridButtons[0],
                                             topSwitchEvents[0],
                                             1, 1,
                                             SkinName.StandartLittleMargin,
                                             SkinLocation.TopRight);
        }

        public new void CenterGridInit(Grid centerGrid)
        {
            capGrid = new Grid();
            toolBox.SetUpGrid(centerGrid, 1, 1, SkinLocation.StretchStretch, SkinSize.HeightEightPercent);

            toolBox.CustomListViewer(centerGrid, capGrid, actualCustomList.Options, AddDeleteButtons(), AddUpArrowsButtons(), AddDownArrowsButtons());
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            toolBox.SetUpGrid(bottomGrid, 1, 1, SkinLocation.BottomStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonToGrid(bottomGrid,
                                             saveButton[0],
                                             saveEvents[0],
                                             1, 1,
                                             SkinName.StandartLittleMargin,
                                             SkinLocation.CenterCenter);
        }

        /// <summary>
        /// Apply changes indicated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDatas(object sender, RoutedEventArgs e)
        {
            string optionnalAdd;
            bool trigger = toolBox.GetUIElements(toolBox.ExtractFormInfos(capGrid), actualCustomList, out optionnalAdd);

            if (optionnalAdd != string.Empty)
            {
                actualCustomList.Options.Add(optionnalAdd);

                requestCenter.PutRequest(BDDTabsName.CustomListLibraries.ToString() + "/" + actualCustomList.id, actualCustomList.ToJsonId());
            }
            else if (trigger)
            {
                requestCenter.PutRequest(BDDTabsName.CustomListLibraries.ToString() + "/" + actualCustomList.id, actualCustomList.ToJsonId());
            }
        }

        /// <summary>
        /// Create buttons to add that delete selected option
        /// </summary>
        /// <returns></returns>
        private List<Button> AddDeleteButtons()
        {
            List<Button> buttonList = new List<Button>();
            Button tempButton;
            RoutedEventLibrary tempRouter;
            int i = 0;

            foreach (string option in actualCustomList.Options)
            {
                var index = i;
                tempRouter = new RoutedEventLibrary();
                tempRouter.optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    DeleteCustomList(sender, e, index);
                });
                tempRouter.resetPageEvent = reloadEvent;

                tempButton = toolBox.CreateSwitchButtonImage(ImagesName.RedCross, tempRouter, SkinName.Standart, SkinLocation.CenterCenter, ImageSizesName.Small);
                buttonList.Add(tempButton);

                i++;
            }

            return buttonList;
        }

        /// <summary>
        /// Stored procedure for option delete
        /// </summary>
        private void DeleteCustomList(object sender, RoutedEventArgs e, int index)
        {
            if (PopUpCenter.ActionValidPopup())
            {
                actualCustomList.Options.Remove(actualCustomList.Options[index]);

                requestCenter.PutRequest(BDDTabsName.CustomListLibraries.ToString() + "/" + actualCustomList.id, actualCustomList.ToJsonId());
            }
        }

        /// <summary>
        /// Create buttons to change options position in the list
        /// </summary>
        private List<Button> AddUpArrowsButtons()
        {
            List<Button> buttonList = new List<Button>();
            Button tempButton;
            RoutedEventLibrary tempRouter;
            int i = 0;

            foreach (string option in actualCustomList.Options)
            {
                var index = i;
                tempRouter = new RoutedEventLibrary();
                tempRouter.optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    MoveUp(sender, e, index);
                });
                tempRouter.resetPageEvent = reloadEvent;

                if (i != 0)
                {
                    tempButton = toolBox.CreateSwitchButtonImage(ImagesName.ArrowUp, tempRouter, SkinName.Standart, SkinLocation.CenterCenter, ImageSizesName.OneForTwoHorizontal);
                }
                else
                {
                    tempButton = toolBox.CreateSwitchButtonImage(ImagesName.None, tempRouter, SkinName.Standart, SkinLocation.CenterCenter, ImageSizesName.None);
                    tempButton.IsEnabled = false;
                    tempButton.Visibility = Visibility.Hidden;
                }
                buttonList.Add(tempButton);

                i++;
            }

            return buttonList;
        }

        private void MoveUp(object sender, RoutedEventArgs e, int index)
        {
            (actualCustomList.Options[index - 1], actualCustomList.Options[index]) = (actualCustomList.Options[index], actualCustomList.Options[index - 1]);

            requestCenter.PutRequest(BDDTabsName.CustomListLibraries.ToString() + "/" + actualCustomList.id, actualCustomList.ToJsonId());
        }

        /// <summary>
        /// Create buttons to change options position in the list
        /// </summary>
        private List<Button> AddDownArrowsButtons()
        {
            List<Button> buttonList = new List<Button>();
            Button tempButton;
            RoutedEventLibrary tempRouter;
            int i = 0;

            foreach (string option in actualCustomList.Options)
            {
                var index = i;
                tempRouter = new RoutedEventLibrary();
                tempRouter.optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    MoveDown(sender, e, index);
                });
                tempRouter.resetPageEvent = reloadEvent;

                if (i != actualCustomList.Options.Count -1)
                {
                    tempButton = toolBox.CreateSwitchButtonImage(ImagesName.ArrowDown, tempRouter, SkinName.Standart, SkinLocation.CenterCenter, ImageSizesName.OneForTwoHorizontal);
                }
                else
                {
                    tempButton = toolBox.CreateSwitchButtonImage(ImagesName.None, tempRouter, SkinName.Standart, SkinLocation.CenterCenter, ImageSizesName.None);
                    tempButton.IsEnabled = false;
                    tempButton.Visibility = Visibility.Hidden;
                }
                buttonList.Add(tempButton);

                i++;
            }

            return buttonList;
        }

        private void MoveDown(object sender, RoutedEventArgs e, int index)
        {
            (actualCustomList.Options[index + 1], actualCustomList.Options[index]) = (actualCustomList.Options[index], actualCustomList.Options[index + 1]);

            requestCenter.PutRequest(BDDTabsName.CustomListLibraries.ToString() + "/" + actualCustomList.id, actualCustomList.ToJsonId());
        }
    }
}
