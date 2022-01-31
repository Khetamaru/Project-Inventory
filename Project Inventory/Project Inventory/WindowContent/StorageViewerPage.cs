using Project_Inventory.BDD;
using Project_Inventory.Tools;
using Project_Inventory.Tools.FonctionalityCerters;
using Project_Inventory.Tools.NamesLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
        private List<Data> dataTab;
        private List<Data> dataTabSave;
        private List<List<ListOption>> listOptionsTab;
        private List<int> customListIds;
        private string[] indicTab;

        public bool sortingTrigger;
        private int buttonTriggeredIndex;
        private ImagesName buttonTriggeredImage;

        public bool emptyInfoPopUp;

        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        public TextBox researchTextBox;

        public StorageViewerPage(ToolBox ToolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomListId, RoutedEventHandler _reloadEvent)
            : base(ToolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomListId)
        {
            viewerStatus = status.VIEWER;
            reloadEvent = _reloadEvent;

            topGridButtons = new string[] { "Modifier", "Chercher", "Retour" };
            saveButton = new string[] { "Sauvegarder" };

            topSwitchEvents = new RoutedEventLibrary[3];
            RoutedEventLibrariesInit(topSwitchEvents);

            topSwitchEvents[0].resetPageEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => { ResearchTrigger(sender, e, false); });
            topSwitchEvents[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SwitchStatus(sender, e); });
            topSwitchEvents[1].resetPageEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => { ResearchTrigger(sender, e, true); });
            topSwitchEvents[2].changePageEvent = GetEventHandler(WindowsName.StorageSelectionMenu);

            saveEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(saveEvents);
            saveEvents[0].resetPageEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => { ResearchTrigger(sender, e, false); });
            saveEvents[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SwitchStatus(sender, e); });
            saveEvents[0].optionalEventTwo = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SaveDatas(sender, e); });

            researchTextBox = new TextBox();
            KeyPressedEventCenter.KeyPressedEventInjection(new RoutedEventHandler((object sender, RoutedEventArgs e) => { ResearchTrigger(sender, e, true); }), KeyPressedName.EnterKey, researchTextBox);

            sortingTrigger = false;
            buttonTriggeredIndex = -1;
            buttonTriggeredImage = ImagesName.None;

            capGrid = new Grid();

            LoadBDDInfos();
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void LoadBDDInfos()
        {
            dataTabSave = dataTab = JsonCenter.LoadStorageViewerInfos(requestCenter, actualStorageId, out listOptionsTab, out customListIds).ToList();

            if (dataTabSave == new List<Data>())
            {
                emptyInfoPopUp = true;
            }
            else
            {
                emptyInfoPopUp = false;
            }

            int j;

            indicTab = new string[dataTab.First().DataText.Count];

            for (j = 0; j < dataTab.First().DataText.Count; j++)
            {
                indicTab[j] = dataTab.First().DataType[j];
            }
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 3, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents,
                                                   new SkinName[] { SkinName.StandartLittleMargin, SkinName.StandartLittleMargin, SkinName.StandartLittleMargin },
                                                   new SkinLocation[] { SkinLocation.TopLeft, SkinLocation.TopRight, SkinLocation.TopRight });

            toolBox.InsertUIElementInGrid(topGrid, researchTextBox, 0, 1, UIElementsName.TextBox, SkinLocation.CenterCenter);
            researchTextBox.Focus();
        }

        public new void CenterGridInit(Grid centerGrid)
        {
            sortingTrigger = false;

            switch (viewerStatus)
            {
                case status.VIEWER:

                    toolBox.EmptyGrid(centerGrid);

                    break;

                case status.MODIFIER:

                    capGrid = new Grid();

                    toolBox.CreateScrollableGridModfiable(centerGrid, capGrid,
                                         1, 1,
                                         dataTab.Count + 2, dataTab.First().DataText.Count + 2,
                                         SkinLocation.CenterStretch, SkinSize.HeightEightPercent,
                                         SkinLocation.CenterCenter,
                                         dataTab.ToArray(), indicTab,
                                         AddDeleteButtons(),
                                         listOptionsTab, customListIds, SortButtonsGeneration());
                    break;
            }
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            switch (viewerStatus)
            {
                case status.VIEWER:

                    capGrid = new Grid();

                    toolBox.CreateScrollableGrid(bottomGrid, capGrid,
                                         1, 1,
                                         dataTab.Count + 1, dataTab.First().DataText.Count + 1,
                                         SkinLocation.BottomStretch, SkinSize.HeightNintyPercent,
                                         SkinLocation.CenterCenter,
                                         dataTab.ToArray(), indicTab,
                                         listOptionsTab, customListIds, SortButtonsGeneration());
                    break;

                case status.MODIFIER:
                    toolBox.SetUpGrid(bottomGrid, 1, 1, SkinLocation.BottomStretch, SkinSize.HeightTenPercent);

                    toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, saveButton, saveEvents, SkinName.StandartLittleMargin, SkinLocation.BottomCenter);
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
                Data optionnalAdd = null;

                List<int> changesList = toolBox.GetUIElements(toolBox.ExtractFormInfos(capGrid), dataTab.ToArray(), out optionnalAdd, listOptionsTab);

                foreach (int change in changesList)
                {
                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "Le stockage (" + JsonCenter.GetStorage(requestCenter, actualStorageId).Name + ") a été modifié.").ToJson());
                    requestCenter.PutRequest(BDDTabsName.DataLibraries.ToString() + "/" + dataTab[change].id, dataTab[change].ToJsonId());
                }

                if (optionnalAdd != null)
                {
                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "Le stockage (" + JsonCenter.GetStorage(requestCenter, actualStorageId).Name + ") a obtenu une nouvelle donnée.").ToJson());
                    requestCenter.PostRequest(BDDTabsName.DataLibraries.ToString(), optionnalAdd.ToJson());
                }
            }
        }

        /// <summary>
        /// Create buttons to add that delete selected data
        /// </summary>
        private List<Button> AddDeleteButtons()
        {
            List<Button> buttonList = new List<Button>();
            Button tempButton;
            RoutedEventLibrary tempRouter;

            foreach(Data data in dataTab)
            {
                tempRouter = new RoutedEventLibrary();
                tempRouter.optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    DeleteData(sender, e, data.id);
                });
                tempRouter.resetPageEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => { ResearchTrigger(sender, e, false); });

                tempButton = toolBox.CreateSwitchButtonImage(ImagesName.RedCross, tempRouter, SkinName.Standart, SkinLocation.CenterCenter, ImageSizesName.Small);
                buttonList.Add(tempButton);
            }

            return buttonList;
        }

        /// <summary>
        /// Stored procedure for data delete
        /// </summary>
        private void DeleteData(object sender, RoutedEventArgs e, int dataId)
        {
            if (PopUpCenter.ActionValidPopup())
            {
                requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "La donnée (" + JsonCenter.GetStorage(requestCenter, actualStorageId).Name + ") a été supprimée.").ToJson());
                requestCenter.DeleteRequest(BDDTabsName.DataLibraries.ToString() + "/" + dataId);
            }
        }

        /// <summary>
        /// Check if a research is tried else empty the research field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="v"></param>
        private void ResearchTrigger(object sender, RoutedEventArgs e, bool trigger)
        {
            if (!trigger)
            {
                researchTextBox.Text = string.Empty;
            }

            reloadEvent.Invoke(sender, e);
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void ResearchThree(string researchString)
        {
            List<string> strResearchList = new List<string>();
            var dataLibraryShorted = new List<Data>();

            var _stringTab = researchString.Split(new string[] { " " }, StringSplitOptions.None);

            foreach (string str in _stringTab)
            {
                if (str != "")
                {
                    strResearchList.Add(str);
                }
            }

            int[] trigger = new int[dataTabSave.Count];
            int i = 0;

            foreach (Data data in dataTabSave)
            {
                if (!data.IsHeader)
                {
                    foreach (string str in strResearchList)
                    {
                        foreach (string dataStr in data.DataText)
                        {
                            if (dataStr.Contains(str))
                            {
                                trigger[i]++;
                            }
                        }
                        if (data.CodeBar.Contains(str))
                        {
                            trigger[i]++;
                        }
                    }
                }
                else
                {
                    trigger[i] = strResearchList.Count;
                }

                i++;
            }

            for (i = 0; i < trigger.Length; i++)
            {
                if (trigger[i] == strResearchList.Count)
                {
                    dataLibraryShorted.Add(dataTabSave[i]);
                }
            }

            dataTab = new List<Data>(dataLibraryShorted.Count);
            for (i = 0; i < dataLibraryShorted.Count; i++)
            {
                dataTab.Add(dataLibraryShorted[i]);
            }

            indicTab = new string[dataTabSave.First().DataText.Count];

            for (i = 0; i < dataTabSave.First().DataText.Count; i++)
            {
                indicTab[i] = dataTabSave.First().DataType[i];
            }

            if (dataLibraryShorted.Count <= 1)
            {
                EmptyResearchResult();
            }
        }

        private List<Button> SortButtonsGeneration()
        {
            List<Button> sortButtons = new List<Button>();
            Button button;
            int customListId;
            int i;
            List<int> indexList = new List<int>();

            for (i = 0; i < dataTab.First().DataType.Count; i++)
            {
                indexList.Add(i);
            }

            foreach (int index in indexList)
            {
                if (buttonTriggeredIndex == index)
                {
                    button = toolBox.CreateSwitchButtonImage(buttonTriggeredImage, new RoutedEventLibrary(), SkinName.Standart, SkinLocation.CenterCenter, ImageSizesName.Small);

                    if (Int32.TryParse(dataTab.First().DataType[index], out customListId))
                    {
                        button.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) => { SortingDatas(sender, e, UIElementsName.ComboBox, index, button, sortButtons); });
                    }
                    else
                    {
                        button.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) => { SortingDatas(sender, e, toolBox.GetUIElementType(dataTab.First().DataType[index]), index, button, sortButtons); });
                    }
                }
                else
                {
                    button = toolBox.CreateSwitchButtonImage(ImagesName.ArrowNeutral, new RoutedEventLibrary(), SkinName.Standart, SkinLocation.CenterCenter, ImageSizesName.Small);

                    if (Int32.TryParse(dataTab.First().DataType[index], out customListId))
                    {
                        button.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) => { SortingDatas(sender, e, UIElementsName.ComboBox, index, button, sortButtons); });
                    }
                    else
                    {
                        button.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) => { SortingDatas(sender, e, toolBox.GetUIElementType(dataTab.First().DataType[index]), index, button, sortButtons); });
                    }
                }

                sortButtons.Add(button);
            }
            return sortButtons;
        }

        private void SortingDatas(object sender, RoutedEventArgs e, UIElementsName uiType, int index, Button button, List<Button> sortButtons)
        {
            Data Header = dataTab.First();
            dataTab.Remove(Header);

            bool triggerReverse = buttonTriggeredIndex == index && buttonTriggeredImage == ImagesName.ArrowDown ? true : false;

            List<Data> tempList = new List<Data>();

            if (triggerReverse)
            {
                switch (uiType)
                {
                    case UIElementsName.ComboBox:

                        tempList = dataTab.OrderByDescending(d => toolBox.GetDataTextColumn(requestCenter, Int32.Parse(d.DataText[index])) + 1).ToList();
                        break;

                    case UIElementsName.DatePicker:

                        tempList = dataTab.OrderByDescending(d => Convert.ToDateTime(d.DataText[index])).ToList();
                        break;

                    case UIElementsName.TextBox:

                        tempList = dataTab.OrderByDescending(d => d.DataText[index]).ToList();
                        break;

                    case UIElementsName.TextBoxNumber:

                        tempList = dataTab.OrderByDescending(d => d.DataText[index]).ToList();
                        break;
                }
            }
            else
            {
                switch (uiType)
                {
                    case UIElementsName.ComboBox:

                        tempList = dataTab.OrderBy(d => toolBox.GetDataTextColumn(requestCenter, Int32.Parse(d.DataText[index])) + 1).ToList();
                        break;

                    case UIElementsName.DatePicker:

                        tempList = dataTab.OrderBy(d => Convert.ToDateTime(d.DataText[index])).ToList();
                        break;

                    case UIElementsName.TextBox:

                        tempList = dataTab.OrderBy(d => d.DataText[index]).ToList();
                        break;

                    case UIElementsName.TextBoxNumber:

                        tempList = dataTab.OrderBy(d => d.DataText[index]).ToList();
                        break;
                }
            }

            dataTab = new List<Data>();
            dataTab.Add(Header);

            foreach(Data data in tempList)
            {
                dataTab.Add(data);
            }

            foreach (Button sortButton in sortButtons)
            {
                (sortButton.Content as Image).Source = new BitmapImage(new Uri(@"pack://application:,,,/" + Assembly.GetCallingAssembly().GetName().Name + ";component/Images/" + ImagesName.ArrowNeutral.ToString() + ".png", UriKind.Absolute));
            }

            if (triggerReverse)
            {
                (button.Content as Image).Source = new BitmapImage(new Uri(@"pack://application:,,,/" + Assembly.GetCallingAssembly().GetName().Name + ";component/Images/" + ImagesName.ArrowUp.ToString() + ".png", UriKind.Absolute));
                buttonTriggeredIndex = index;
                buttonTriggeredImage = ImagesName.ArrowUp;
            }
            else
            {
                (button.Content as Image).Source = new BitmapImage(new Uri(@"pack://application:,,,/" + Assembly.GetCallingAssembly().GetName().Name + ";component/Images/" + ImagesName.ArrowDown.ToString() + ".png", UriKind.Absolute));
                buttonTriggeredIndex = index;
                buttonTriggeredImage = ImagesName.ArrowDown;
            }

            sortingTrigger = true;

            reloadEvent.Invoke(sender, e);
        }

        public void EmptyInfoPopUp()
        {
            PopUpCenter.MessagePopup("Ce stockage est vide.");
        }

        public void EmptyResearchResult()
        {
            PopUpCenter.MessagePopup("Aucune donnée n'a été trouvée.");
        }
    }
}
