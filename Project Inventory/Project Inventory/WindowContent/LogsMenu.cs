using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Project_Inventory.BDD;
using Project_Inventory.Tools;
using Project_Inventory.Tools.FonctionalityCerters;
using Project_Inventory.Tools.NamesLibraries;

namespace Project_Inventory
{
    public class LogsMenu : WindowContent
    {
        private Grid capGrid;

        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private List<Log> logsGridSave;
        private List<Log> logsGrid;
        private RoutedEventLibrary[] bottomSwitchEvents;

        private List<User> users;

        private RoutedEventHandler reloadEvent;

        private int widthLimit;

        public TextBox researchTextBox;
        public DatePicker preResearchDate;
        public DatePicker postResearchDate;
        public Button buttonLogDelete;

        public LogsMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomId, RoutedEventHandler _reloadEvent)
               : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomId)
        {
            topGridButtons = new string[] { "Research", "", "", "Return" };

            reloadEvent = _reloadEvent;

            topSwitchEvents = new RoutedEventLibrary[4];
            RoutedEventLibrariesInit(topSwitchEvents);

            topSwitchEvents[0].resetPageEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => { ResearchTrigger(sender, e, true); });
            topSwitchEvents[3].changePageEvent = GetEventHandler(WindowsName.MainMenu);

            researchTextBox = new TextBox();
            KeyPressedEventCenter.KeyPressedEventInjection(new RoutedEventHandler((object sender, RoutedEventArgs e) => { ResearchTrigger(sender, e, true); }), KeyPressedName.EnterKey, researchTextBox);

            preResearchDate = new DatePicker();
            preResearchDate.SelectedDate = DateTime.Now;
            postResearchDate = new DatePicker();
            buttonLogDelete = new Button();
            buttonLogDelete.Content = "Delete Logs";
            buttonLogDelete.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) => { DeleteLogs(sender, e); });

            capGrid = new Grid();

            LoadBDDInfos();

            widthLimit = 9;

            ResearchThree();
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void LoadBDDInfos()
        {
            logsGridSave = logsGrid = JsonCenter.LoadLogsMenuInfos(requestCenter, out users);
            bottomSwitchEvents = JsonCenter.SetEventHandlerTab(logsGridSave.Count, GetEventHandler(WindowsName.ListViewerPage));
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 4, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents,
                                                   new SkinName[] { SkinName.StandartLittleMargin, SkinName.None, SkinName.None, SkinName.StandartLittleMargin },
                                                   new SkinLocation[] { SkinLocation.TopCenter, SkinLocation.None, SkinLocation.None, SkinLocation.TopRight });

            toolBox.InsertUIElementInGrid(topGrid, researchTextBox, 0, 0, UIElementsName.TextBox, SkinLocation.CenterCenter);
            toolBox.InsertUIElementInGrid(topGrid, preResearchDate, 0, 1, UIElementsName.DatePicker, SkinLocation.CenterCenter);
            toolBox.InsertUIElementInGrid(topGrid, postResearchDate, 0, 2, UIElementsName.DatePicker, SkinLocation.CenterCenter);
            toolBox.InsertUIElementInGrid(topGrid, buttonLogDelete, 0, 3, UIElementsName.Button, SkinLocation.TopLeft);
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            capGrid = new Grid();

            toolBox.CreateScrollGrid(bottomGrid, capGrid, 1, 1, logsGrid.Count, 1, SkinLocation.BottomStretch, SkinSize.HeightNintyPercent, SkinLocation.CenterCenter, logsGrid, widthLimit, users);
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
        public void ResearchThree()
        {
            List<string> strResearchList = new List<string>();
            var logLibraryShorted = new List<Log>();

            var _stringTab = researchTextBox.Text.Split(new string[] { " " }, StringSplitOptions.None);

            foreach (string str in _stringTab)
            {
                if (str != "")
                {
                    strResearchList.Add(str);
                }
            }

            int[] trigger = new int[logsGridSave.Count];
            int i = 0;

            foreach (Log log in logsGridSave)
            {
                foreach (string str in strResearchList)
                {
                    if (log.Date.ToString().Contains(str) || log.Message.Contains(str) || toolBox.GetUser(log.UserId, users).Name.Contains(str))
                    {
                        trigger[i]++;
                    }
                    else
                    {
                        trigger[i] = strResearchList.Count + 1;
                    }

                    i++;
                }
            }

            for (i = 0; i < trigger.Length; i++)
            {
                if (trigger[i] == strResearchList.Count)
                {
                    logLibraryShorted.Add(logsGridSave[i]);
                }
            }

            logsGrid = new List<Log>();
            for (i = 0; i < logLibraryShorted.Count; i++)
            {
                logsGrid.Add(logLibraryShorted[i]);
            }

            if (preResearchDate.SelectedDate != null && postResearchDate.SelectedDate != null)
            {
                logsGrid = logsGrid.FindAll(
                    delegate(Log log)
                    {
                        return log.Date >= preResearchDate.SelectedDate && log.Date <= postResearchDate.SelectedDate;
                    }
                );
            }
            else if (preResearchDate.SelectedDate != null)
            {
                logsGrid = logsGrid.FindAll(
                    delegate (Log log)
                    {
                        return log.Date >= preResearchDate.SelectedDate;
                    }
                );
            }
            else if (postResearchDate.SelectedDate != null)
            {
                logsGrid = logsGrid.FindAll(
                    delegate (Log log)
                    {
                        return log.Date <= postResearchDate.SelectedDate;
                    }
                );
            }
        }

        public void DeleteLogs(object sender, RoutedEventArgs e)
        {
            if (PopUpCenter.ActionValidPopup("You will delete all logs, are you sure ?"))
            {
                requestCenter.DeleteRequest(BDDTabsName.LogLibraries.ToString());
                reloadEvent.Invoke(sender, e);
            }
        }
    }
}
