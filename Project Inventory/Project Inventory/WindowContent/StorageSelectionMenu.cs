using Project_Inventory.BDD;
using Project_Inventory.Tools.FonctionalityCerters;
using Project_Inventory.Tools.NamesLibraries;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Project_Inventory.Tools;

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

        public bool emptyInfoPopUp;

        private int widthLimit;

        private enum status {
            VIEWER,
            MODIFIER
        }

        private status viewerStatus;

        private Grid capGrid;
        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        public TextBox researchTextBox;

        public StorageSelectionMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomListId, RoutedEventHandler _reloadEvent)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomListId)
        {
            viewerStatus = status.VIEWER;
            reloadEvent = _reloadEvent;

            topGridButtons = new string[] { "Modifier", "Chercher", "Retour" };
            saveButton = new string[] { "Sauvegarder" };

            topSwitchEvents = new RoutedEventLibrary[3];
            RoutedEventLibrariesInit(topSwitchEvents);

            topSwitchEvents[0].resetPageEvent = reloadEvent;
            topSwitchEvents[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SwitchStatus(sender, e); });
            topSwitchEvents[1].changePageEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => { GlobalResearch(sender, e); });
            topSwitchEvents[2].changePageEvent = GetEventHandler(WindowsName.MainMenu);

            saveEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(saveEvents);
            saveEvents[0].resetPageEvent = reloadEvent;
            saveEvents[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SwitchStatus(sender, e); });
            saveEvents[0].optionalEventTwo = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SaveDatas(sender, e); });

            researchTextBox = new TextBox();
            KeyPressedEventCenter.KeyPressedEventInjection(new RoutedEventHandler((object sender, RoutedEventArgs e) => { GlobalResearch(sender, e); }), KeyPressedName.EnterKey, researchTextBox);

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
            toolBox.SetUpGrid(topGrid, 1, 3, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, 
                                                   topGridButtons, 
                                                   topSwitchEvents, 
                                                   new SkinName[] { SkinName.StandartLittleMargin, SkinName.StandartLittleMargin, SkinName.StandartLittleMargin },
                                                   new SkinLocation[] { SkinLocation.TopLeft, SkinLocation.TopCenter, SkinLocation.TopRight });

            toolBox.InsertUIElementInGrid(topGrid, researchTextBox, 0, 1, UIElementsName.TextBox, SkinLocation.CenterCenter);
            researchTextBox.Focus();
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

                    toolBox.CreateTabToGrid(capGrid, bottomGridButtons, SkinLocation.CenterCenter, saveEvents[0]);
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
        /// Launch Data research on all Storages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GlobalResearch(object sender, RoutedEventArgs e)
        {
            if (researchTextBox.Text.Replace(" ", string.Empty) != string.Empty)
            {
                GetEventHandler(WindowsName.GlobalStorageResearch).Invoke(sender, e);
            }
            else
            {
                PopUpCenter.MessagePopup("Vous devez écrire quelque chose dans la barre de recherche.");
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
                Storage optionnalAdd = new Storage(42, string.Empty);

                List<int> changesList = toolBox.GetUIElements(toolBox.ExtractFormInfos(capGrid), bottomGridButtons, out optionnalAdd);

                foreach (int change in changesList)
                {
                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "Le nom d'un stockage a été modifié.").ToJson());
                    requestCenter.PutRequest(BDDTabsName.StorageLibraries.ToString() + "/" + bottomGridButtons[change].id, bottomGridButtons[change].ToJsonId());
                }

                if (optionnalAdd != null)
                {
                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "Un nouveau stockage a été créé.").ToJson());
                    requestCenter.PostRequest(BDDTabsName.StorageLibraries.ToString(), optionnalAdd.ToJson());
                }
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

                        tempButton = toolBox.CreateSwitchButtonImage(ImagesName.RedCross, tempRouter, SkinName.StandartLittleMargin, SkinLocation.CenterLeft, ImageSizesName.Small);

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
            if (PopUpCenter.ActionValidPopup())
            {
                requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "Le stockage (" + JsonCenter.GetStorage(requestCenter, StorageId).Name + ") a été supprimé.").ToJson());
                requestCenter.DeleteRequest(BDDTabsName.StorageLibrariesXCustomListLibraries.ToString() + "/storage/" + StorageId);
                requestCenter.DeleteRequest(BDDTabsName.DataLibraries.ToString() + "/storage/" + StorageId);
                requestCenter.DeleteRequest(BDDTabsName.StorageLibraries.ToString() + "/" + StorageId);
            }
        }

        public void EmptyInfoPopUp()
        {
            PopUpCenter.MessagePopup("Il n'y a pas de stockage.");
        }
    }
}
