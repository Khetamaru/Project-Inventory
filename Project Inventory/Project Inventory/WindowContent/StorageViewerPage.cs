using Project_Inventory.BDD;
using Project_Inventory.Tools;
using Project_Inventory.Tools.FonctionalityCerters;
using Project_Inventory.Tools.NamesLibraries;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        private Data[] dataTabSave;
        private string[,] stringTab;
        private string[,] indicTab;

        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        public TextBox researchTextBox;

        public StorageViewerPage(ToolBox ToolBox, Router _router, RequestCenter requestCenter, int _actualStorageId, int _actualDataId, int _actualCustomListId, RoutedEventHandler _reloadEvent)
            : base(ToolBox, _router, requestCenter, _actualStorageId, _actualDataId, _actualCustomListId)
        {
            viewerStatus = status.VIEWER;
            reloadEvent = _reloadEvent;

            topGridButtons = new string[] { "Modify", "Research", "Return" };
            saveButton = new string[] { "Save" };

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

            capGrid = new Grid();

            LoadBDDInfos();
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void LoadBDDInfos()
        {
            dataTabSave = dataTab = JsonCenter.LoadStorageViewerInfos(requestCenter, actualStorageId);
            

            int i;
            int j;

            stringTab = new string[dataTab.Length, dataTab[0].DataText.Count];
            indicTab = new string[dataTab.Length, dataTab[0].DataText.Count];

            for (i = 0; i < dataTab.Length; i++)
            {
                for (j = 0; j < dataTab[0].DataText.Count; j++)
                {
                    stringTab[i, j] = dataTab[i].DataText[j];
                    indicTab[i, j] = dataTab[i].DataType[j];
                }
            }
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 3, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents,
                                                   new SkinName[] { SkinName.StandartLittleMargin, SkinName.StandartLittleMargin, SkinName.StandartLittleMargin },
                                                   new SkinLocation[] { SkinLocation.TopLeft, SkinLocation.TopRight, SkinLocation.TopRight });

            toolBox.InsertUIElementInGrid(topGrid, researchTextBox, 0, 1, UIElementsName.TextBox, SkinLocation.CenterCenter);
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
                                         SkinLocation.BottomStretch, SkinSize.HeightNintyPercent,
                                         SkinLocation.CenterCenter,
                                         stringTab, indicTab);
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

                    capGrid = new Grid();

                    toolBox.CreateScrollableGridModfiable(centerGrid, capGrid,
                                         1, 1,
                                         stringTab.GetLength(0) + 1, stringTab.GetLength(1) + 1,
                                         SkinLocation.CenterStretch, SkinSize.HeightEightPercent,
                                         SkinLocation.CenterCenter,
                                         stringTab, indicTab,
                                         AddDeleteButtons());
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

            Data optionnalAdd = new Data(42, actualStorageId, new List<string>(), dataTab[0].DataType, false);

            foreach(int change in changesList)
            {
                requestCenter.PutRequest(BDDTabsName.DataLibraries.ToString() + "/" + dataTab[change].id, dataTab[change].ToJsonId());
            }

            if (toolBox.OptionnalAdd(capGrid, dataTab, optionnalAdd))
            {
                requestCenter.PostRequest(BDDTabsName.DataLibraries.ToString(), optionnalAdd.ToJson());
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
                requestCenter.DeleteRequest("DataLibraries/" + dataId);
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

            int[] trigger = new int[dataTabSave.Length];
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

            dataTab = new Data[dataLibraryShorted.Count];
            for (i = 0; i < dataLibraryShorted.Count; i++)
            {
                dataTab[i] = dataLibraryShorted[i];
            }

            int j;

            stringTab = new string[dataTab.Length, dataTab[0].DataText.Count];
            indicTab = new string[dataTab.Length, dataTab[0].DataText.Count];

            for (i = 0; i < dataTab.Length; i++)
            {
                for (j = 0; j < dataTab[0].DataText.Count; j++)
                {
                    stringTab[i, j] = dataTab[i].DataText[j];
                    indicTab[i, j] = dataTab[i].DataType[j];
                }
            }
        }
    }
}
