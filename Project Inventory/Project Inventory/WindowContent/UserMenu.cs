using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Project_Inventory.BDD;
using Project_Inventory.Tools;

namespace Project_Inventory
{
    public class UserMenu : WindowContent
    {
        private Grid capGrid;

        private status viewerStatus;
        private enum status
        {
            VIEWER,
            MODIFIER
        }

        public bool emptyInfoPopUp;

        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private List<User> bottomGridButtons;

        private RoutedEventHandler reloadEvent;

        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        private int widthLimit;

        public UserMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomId, RoutedEventHandler _reloadEvent)
               : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomId)
        {
            viewerStatus = status.VIEWER;

            topGridButtons = new string[] { "Modifier", "Retour" };
            saveButton = new string[] { "Sauvegarde" };

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

            widthLimit = 5;
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void LoadBDDInfos()
        {
            bottomGridButtons = JsonCenter.LoadUserMenuInfos(requestCenter);

            if (bottomGridButtons == new List<User>())
            {
                emptyInfoPopUp = true;
            }
            else
            {
                emptyInfoPopUp = false;
            }
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

                    toolBox.ButtonPlacer(capGrid, bottomGridButtons.Count, widthLimit, SkinLocation.BottomStretch, SkinSize.HeightNintyPercent);

                    toolBox.CreateLabelToGridByTab(capGrid, bottomGridButtons, SkinLocation.CenterCenter);
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

                    toolBox.ButtonPlacer(capGrid, bottomGridButtons.Count + 1, widthLimit, SkinLocation.BottomStretch, SkinSize.HeightEightPercent);

                    toolBox.CreateTabToGrid(capGrid, bottomGridButtons, SkinLocation.CenterCenter, saveEvents[0]);
                    centerGrid.Children.Add(capGrid);

                    AddDeleteButtons();

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
            switch (viewerStatus)
            {
                case status.VIEWER:

                    viewerStatus = status.MODIFIER;
                    topGridButtons[0] = "Annuler";

                    break;

                case status.MODIFIER:

                    viewerStatus = status.VIEWER;
                    topGridButtons[0] = "Modifier";

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
            if (PopUpCenter.ActionValidPopup())
            {
                List<UIElement> elementList = toolBox.ExtractFormInfos(capGrid);

                User optionnalAdd = new User(string.Empty, 0, true);

                List<int> changesList = toolBox.GetUIElements(elementList, bottomGridButtons, out optionnalAdd);

                foreach (int change in changesList)
                {

                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "Certains noms d'utilisateurs ont été modifiés.").ToJson());
                    requestCenter.PutRequest(BDDTabsName.UserLibraries.ToString() + "/" + bottomGridButtons[change].id, bottomGridButtons[change].ToJsonId());
                }

                if (optionnalAdd != null)
                {
                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "Un nouvel utilisateur a été créé.").ToJson());
                    requestCenter.PostRequest(BDDTabsName.UserLibraries.ToString(), optionnalAdd.ToJson());
                }
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

            int i = bottomGridButtons.Count;
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
                    if (bottomGridButtons.Count > j + (i * 5))
                    {
                        tempRouter = new RoutedEventLibrary();
                        var user = bottomGridButtons[j + (i * 5)];
                        tempRouter.optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                        {
                            DeleteUser(sender, e, user.id);
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
        private void DeleteUser(object sender, RoutedEventArgs e, int UserId)
        {
            if (PopUpCenter.ActionValidPopup())
            {
                requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "L'utilisateur (" + JsonCenter.GetUser(requestCenter, UserId).Name + ") a été désactivé.").ToJson());
                requestCenter.DeleteRequest(BDDTabsName.UserLibraries.ToString() + "/" + UserId);
            }
        }

        public void EmptyInfoPopUp()
        {
            PopUpCenter.MessagePopup("Il n'y a pas d'utilisateur existant.");
        }
    }
}
