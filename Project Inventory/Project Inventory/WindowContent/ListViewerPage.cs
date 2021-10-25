using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Project_Inventory.BDD;
using Project_Inventory.Tools;

namespace Project_Inventory
{
    class ListViewerPage : WindowContent
    {
        private Grid capGrid;

        private List<ListOption> listOptions;

        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private RoutedEventHandler reloadEvent;

        public bool emptyInfoPopUp;

        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        public ListViewerPage(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomId, RoutedEventHandler _reloadEvent)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomId)
        {
            topGridButtons = new string[] { "Retour" };
            saveButton = new string[] { "Sauvegarder" };

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
            listOptions = JsonCenter.LoadListViewerPageInfos(requestCenter, actualCustomListId);

            if(listOptions.Count > 0)
            {
                listOptions = listOptions.OrderBy(option => option.Index).ToList();
                emptyInfoPopUp = false;
            }
            else
            {
                emptyInfoPopUp = true;
            }
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

            toolBox.CustomListViewer(centerGrid, capGrid, listOptions, AddDeleteButtons(), AddUpArrowsButtons(), AddDownArrowsButtons());
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
            if (PopUpCenter.ActionValidPopup())
            {
                string optionnalAdd;
                bool trigger = toolBox.GetUIElements(toolBox.ExtractFormInfos(capGrid), listOptions, out optionnalAdd);

                if (optionnalAdd != string.Empty)
                {
                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "La liste custom (" + JsonCenter.GetCustomList(requestCenter, actualCustomListId).Name + ") a été modifié.").ToJson());
                    requestCenter.PostRequest(BDDTabsName.ListOptionLibraries.ToString(), (new ListOption(actualCustomListId, listOptions.Count, optionnalAdd)).ToJson());

                    foreach (ListOption option in listOptions)
                    {
                        requestCenter.PutRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + option.id, option.ToJsonId());
                    }
                }
                else if (trigger)
                {
                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "La liste custom (" + JsonCenter.GetCustomList(requestCenter, actualCustomListId).Name + ") a été modifié.").ToJson());
                    foreach (ListOption option in listOptions)
                    {
                        requestCenter.PutRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + option.id, option.ToJsonId());
                    }
                }
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

            foreach (ListOption option in listOptions)
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
            if (listOptions.Count > 1)
            {
                if (PopUpCenter.ActionValidPopup())
                {
                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "La liste custom (" + JsonCenter.GetCustomList(requestCenter, actualCustomListId).Name + ") a été modifié.").ToJson());
                    requestCenter.DeleteRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + listOptions[index].id);
                }
            }
            else
            {
                PopUpCenter.MessagePopup("Une liste custom ne peut pas être vide. Vous ne pouvez pas supprimer la dernière option.");
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

            foreach (ListOption option in listOptions)
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
            (listOptions[index - 1].Index, listOptions[index].Index) = (listOptions[index].Index, listOptions[index - 1].Index);

            requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "L'option (" + JsonCenter.GetListOption(requestCenter, listOptions[index].id).Name + ") et l'option (" + JsonCenter.GetListOption(requestCenter, listOptions[index - 1].id).Name + ") de la liste custom (" + JsonCenter.GetCustomList(requestCenter, actualCustomListId).Name + ") ont été échangés.").ToJson());
            requestCenter.PutRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + listOptions[index - 1].id, listOptions[index - 1].ToJsonId());
            requestCenter.PutRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + listOptions[index].id,     listOptions[index].ToJsonId());
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

            foreach (ListOption option in listOptions)
            {
                var index = i;
                tempRouter = new RoutedEventLibrary();
                tempRouter.optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    MoveDown(sender, e, index);
                });
                tempRouter.resetPageEvent = reloadEvent;

                if (i != listOptions.Count -1)
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
            (listOptions[index + 1].Index, listOptions[index].Index) = (listOptions[index].Index, listOptions[index + 1].Index);

            requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "L'option (" + JsonCenter.GetListOption(requestCenter, listOptions[index].id).Name + ") et l'option (" + JsonCenter.GetListOption(requestCenter, listOptions[index + 1].id).Name + ") de la liste custom (" + JsonCenter.GetCustomList(requestCenter, actualCustomListId).Name + ") ont été échangés.").ToJson());
            requestCenter.PutRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + listOptions[index + 1].id, listOptions[index + 1].ToJsonId());
            requestCenter.PutRequest(BDDTabsName.ListOptionLibraries.ToString() + "/" + listOptions[index].id, listOptions[index].ToJsonId());
        }

        public void EmptyInfoPopUp()
        {
            PopUpCenter.MessagePopup("Il n'y a pas d'option dans cette liste.");
        }
    }
}
