using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Project_Inventory.BDD;
using Project_Inventory.Tools;

namespace Project_Inventory
{
    public class BugReportedView : WindowContent
    {
        private Grid capGrid;

        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private List<Bug> bugsGridSave;
        private List<Bug> bugsGrid;

        private BrushConverter defaultButtonColor;

        private bool unhandleBugTrigger;
        private Button unhandleBugButton;

        private bool handleBugTrigger;
        private Button handleBugButton;

        private List<User> users;

        private RoutedEventHandler reloadEvent;

        private int widthLimit;

        public BugReportedView(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomId, RoutedEventHandler _reloadEvent)
               : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomId)
        {
            topGridButtons = new string[] { "", "", "Return" };

            reloadEvent = _reloadEvent;

            topSwitchEvents = new RoutedEventLibrary[3];
            RoutedEventLibrariesInit(topSwitchEvents);

            topSwitchEvents[2].changePageEvent = GetEventHandler(WindowsName.MainMenu);

            unhandleBugTrigger = true;
            handleBugTrigger = false;

            handleBugButton = new Button();
            unhandleBugButton = new Button();

            defaultButtonColor = new BrushConverter();
            unhandleBugButton.Background = Brushes.CadetBlue;


            unhandleBugButton.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) => { UnhandleButtonTrigger(sender, e); });
            handleBugButton.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) => { HandleButtonTrigger(sender, e); });

            capGrid = new Grid();

            bugsGrid = new List<Bug>();

            LoadBDDInfos();

            widthLimit = 9;
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void LoadBDDInfos()
        {
            bugsGridSave = JsonCenter.LoadBugReportedViewInfos(requestCenter, out users);

            BugSorting();

            if (bugsGrid.Count < 1)
            {
                EmptyResearchResult();
            }
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 3, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents,
                                                   new SkinName[] { SkinName.None, SkinName.None, SkinName.StandartLittleMargin },
                                                   new SkinLocation[] { SkinLocation.None, SkinLocation.None, SkinLocation.TopRight });

            handleBugButton.Content = "View Handle Bugs";
            unhandleBugButton.Content = "View Unhandle Bugs";

            toolBox.InsertUIElementInGrid(topGrid, handleBugButton, 0, 0, UIElementsName.Button, SkinLocation.CenterCenter);
            toolBox.InsertUIElementInGrid(topGrid, unhandleBugButton, 0, 1, UIElementsName.Button, SkinLocation.CenterCenter);
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            capGrid = new Grid();

            toolBox.CreateScrollGrid(bottomGrid, capGrid, 1, 1, bugsGrid.Count, 5, SkinLocation.BottomStretch, SkinSize.HeightNintyPercent, SkinLocation.CenterCenter, bugsGrid, widthLimit, users, AddDeleteButtons(), AddHandleButtons());
        }

        public void DeleteLogs(object sender, RoutedEventArgs e)
        {
            if (PopUpCenter.ActionValidPopup("You will delete all logs, are you sure ?"))
            {
                requestCenter.DeleteRequest(BDDTabsName.LogLibraries.ToString());
                GetEventHandler(WindowsName.LogsMenu).Invoke(sender, e);
            }
        }

        public void BugSorting()
        {
            bugsGrid = new List<Bug>();

            foreach (Bug bug in bugsGridSave)
            {
                if (handleBugTrigger && bug.Handled || unhandleBugTrigger && !bug.Handled)
                {
                    bugsGrid.Add(bug);
                }
            }
        }

        private void HandleButtonTrigger(object sender, RoutedEventArgs e)
        {
            handleBugTrigger = !handleBugTrigger;

            if (handleBugTrigger)
            {
                (sender as Button).Background = Brushes.CadetBlue;
            }
            else
            {
                (sender as Button).Background = (Brush)defaultButtonColor.ConvertFrom("#E0DCDC");
            }

            reloadEvent.Invoke(sender, e);
        }

        private void UnhandleButtonTrigger(object sender, RoutedEventArgs e)
        {
            unhandleBugTrigger = !unhandleBugTrigger;

            if (unhandleBugTrigger)
            {
                (sender as Button).Background = Brushes.CadetBlue;
            }
            else
            {
                (sender as Button).Background = (Brush)defaultButtonColor.ConvertFrom("#E0DCDC");
            }

            reloadEvent.Invoke(sender, e);
        }

        /// <summary>
        /// Create buttons to add that delete selected bug
        /// </summary>
        /// <returns></returns>
        private List<Button> AddDeleteButtons()
        {
            List<Button> buttonList = new List<Button>();
            Button tempButton;
            RoutedEventLibrary tempRouter;

            foreach (Bug bug in bugsGrid)
            {
                tempRouter = new RoutedEventLibrary();
                tempRouter.optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    DeleteBug(sender, e, bug.id);
                });
                tempRouter.resetPageEvent = reloadEvent;

                tempButton = toolBox.CreateSwitchButtonImage(ImagesName.RedCross, tempRouter, SkinName.Standart, SkinLocation.CenterCenter, ImageSizesName.Small);
                buttonList.Add(tempButton);
            }

            return buttonList;
        }

        private void DeleteBug(object sender, RoutedEventArgs e, int bugId)
        {
            if (PopUpCenter.ActionValidPopup())
            {
                requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "A bug has been marked resolved.").ToJson());
                requestCenter.DeleteRequest(BDDTabsName.BugLibraries.ToString() + "/" + bugId);
            }
        }

        /// <summary>
        /// Create a button to mark a bug handled.
        /// </summary>
        /// <returns></returns>
        private List<Button> AddHandleButtons()
        {
            List<Button> buttonList = new List<Button>();
            Button tempButton;
            RoutedEventLibrary tempRouter;

            foreach (Bug bug in bugsGrid)
            {
                tempRouter = new RoutedEventLibrary();
                tempRouter.optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    HandleBug(sender, e, bug);
                });
                tempRouter.resetPageEvent = reloadEvent;

                tempButton = toolBox.CreateSwitchButton("Mark Handled", tempRouter, SkinName.Standart, SkinLocation.CenterCenter);
                buttonList.Add(tempButton);
            }

            return buttonList;
        }
        private void HandleBug(object sender, RoutedEventArgs e, Bug bug)
        {
            bug.Handled = true;

            if (PopUpCenter.ActionValidPopup())
            {
                requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "A bug has been marked resolved.").ToJson());
                requestCenter.PutRequest(BDDTabsName.BugLibraries.ToString() + "/" + bug.id, bug.ToJsonId());
            }
        }

        public void EmptyResearchResult()
        {
            PopUpCenter.MessagePopup("No Bug has been found.");
        }
    }
}
