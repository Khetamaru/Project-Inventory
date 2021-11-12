using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System.ComponentModel;
using System.Windows;

namespace Project_Inventory
{
    public partial class MainWindow : Window
    {
        public ToolBox toolBox;
        private double titleBarHeight;

        private Router router;
        private RequestCenter requestCenter;

        private WindowsName actualWindow;

        private MainMenu mainMenu;
        private StorageSelectionMenu storageSelectionMenu;
        private FormPage formPage;
        private StorageViewerPage storageViewerPage;
        private ListMenu listMenu;
        private ListViewerPage listViewerPage;
        private LogsMenu logsMenu;
        private UserMenu userMenu;
        private GlobalStorageResearch globalStorageResearch;
        private DataDetailsPage dataDetailsPage;
        private StorageTransfertSelection storageTransfertSelection;
        private DataTransfert dataTransfert;
        private BugReportedView bugReportedView;
        private DatabaseModifMenu databaseModifMenu;

        private int actualUserId;
        private int actualStorageId;
        private int actualDataId;
        private int actualCustomListId;

        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();

            titleBarHeight = SystemParameters.WindowCaptionHeight;

            toolBox = new ToolBox(titleBarHeight, this);

            Width = toolBox.windowWidth;
            Height = toolBox.windowHeight;

            router = InitRouters();
            requestCenter = new RequestCenter();

            actualUserId = -1;
            actualStorageId = -1;
            actualDataId = -1;
            actualCustomListId = -1;

            this.SizeChanged += new SizeChangedEventHandler((object sender, SizeChangedEventArgs e) => { SizeChangeResizeEvent(sender, e); });
            this.Closing += new CancelEventHandler((object sender, CancelEventArgs e) => { ClosePopUp(sender, e); });

            Init();
        }

        private void SizeChangeResizeEvent(object sender, SizeChangedEventArgs e)
        {
            switch(this.WindowState == WindowState.Maximized)
            {
                case true:
                    WpfScreen wpfScreen = WpfScreen.GetScreenFrom(this);

                    toolBox.windowWidth = wpfScreen.PrimaryScreenSizeWidth();
                    toolBox.windowHeight = wpfScreen.PrimaryScreenSizeHeight();
                    break;

                case false:
                    toolBox.windowWidth = Width;
                    toolBox.windowHeight = Height;
                    break;
            }
            ReloadView(sender, e);
        }

        private void Init()
        {
            MainMenuInit();
        }

        public void MainMenuInit()
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            mainMenu = new MainMenu(toolBox, router, requestCenter, actualUserId, actualStorageId, actualCustomListId, actualDataId, reloadEvent);
            actualWindow = WindowsName.MainMenu;
            mainMenu.TopGridInit(topGrid);
            mainMenu.CenterGridInit(centerGrid);
            mainMenu.BottomGridInit(bottomGrid);
        }

        public void StorageSelectionMenuInit()
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            storageSelectionMenu = new StorageSelectionMenu(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent);
            actualWindow = WindowsName.StorageSelectionMenu;
            storageSelectionMenu.TopGridInit(topGrid);
            storageSelectionMenu.CenterGridInit(centerGrid);
            storageSelectionMenu.BottomGridInit(bottomGrid);
            if (storageSelectionMenu.emptyInfoPopUp) { storageSelectionMenu.EmptyInfoPopUp(); }
        }

        public void DataTransfertInit()
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            dataTransfert = new DataTransfert(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent);
            actualWindow = WindowsName.DataTransfert;
            dataTransfert.TopGridInit(topGrid);
            dataTransfert.CenterGridInit(centerGrid);
            dataTransfert.BottomGridInit(bottomGrid);
        }

        public void StorageTransfertSelectionInit()
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            storageTransfertSelection = new StorageTransfertSelection(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent);
            actualWindow = WindowsName.StorageTransfertSelection;
            storageTransfertSelection.TopGridInit(topGrid);
            storageTransfertSelection.CenterGridInit(centerGrid);
            storageTransfertSelection.BottomGridInit(bottomGrid);
        }

        public void ListMenuInit()
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            listMenu = new ListMenu(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent);
            actualWindow = WindowsName.ListMenu;
            listMenu.TopGridInit(topGrid);
            listMenu.CenterGridInit(centerGrid);
            listMenu.BottomGridInit(bottomGrid);
            if (listMenu.emptyInfoPopUp) { listMenu.EmptyInfoPopUp(); }
        }

        public void UserMenuInit()
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            userMenu = new UserMenu(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent);
            actualWindow = WindowsName.UserMenu;
            userMenu.TopGridInit(topGrid);
            userMenu.CenterGridInit(centerGrid);
            userMenu.BottomGridInit(bottomGrid);
            if (userMenu.emptyInfoPopUp) { userMenu.EmptyInfoPopUp(); }
        }

        public void ListViewerPageInit()
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            listViewerPage = new ListViewerPage(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent);
            actualWindow = WindowsName.ListViewerPage;
            listViewerPage.TopGridInit(topGrid);
            listViewerPage.CenterGridInit(centerGrid);
            listViewerPage.BottomGridInit(bottomGrid);
            if (listViewerPage.emptyInfoPopUp) { listViewerPage.EmptyInfoPopUp(); }
        }

        public void GlobalStorageResearchInit()
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            globalStorageResearch = new GlobalStorageResearch(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent, storageSelectionMenu.researchTextBox.Text);
            actualWindow = WindowsName.GlobalStorageResearch;
            globalStorageResearch.TopGridInit(topGrid);
            globalStorageResearch.CenterGridInit(centerGrid);
            globalStorageResearch.BottomGridInit(bottomGrid);
            if (globalStorageResearch.emptyInfoPopUp) { globalStorageResearch.EmptyInfoPopUp(); }
        }

        public void DataDetailsPageInit()
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            dataDetailsPage = new DataDetailsPage(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent);
            actualWindow = WindowsName.DataDetailPage;
            dataDetailsPage.TopGridInit(topGrid);
            dataDetailsPage.CenterGridInit(centerGrid);
            dataDetailsPage.BottomGridInit(bottomGrid);
        }

        public void LogsMenuInit()
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            logsMenu = new LogsMenu(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent);
            actualWindow = WindowsName.LogsMenu;
            logsMenu.TopGridInit(topGrid);
            logsMenu.CenterGridInit(centerGrid);
            logsMenu.BottomGridInit(bottomGrid);
        }

        public void BugReportedViewInit()
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            bugReportedView = new BugReportedView(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent);
            actualWindow = WindowsName.BugReportedView;
            bugReportedView.TopGridInit(topGrid);
            bugReportedView.CenterGridInit(centerGrid);
            bugReportedView.BottomGridInit(bottomGrid);
        }

        public void DatabaseModifMenuInit()
        {
            databaseModifMenu = new DatabaseModifMenu(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId);
            actualWindow = WindowsName.DatabaseModifMenu;
            databaseModifMenu.TopGridInit(topGrid);
            databaseModifMenu.CenterGridInit(centerGrid);
            databaseModifMenu.BottomGridInit(bottomGrid);
        }

        public void FormPageInit(WindowsName formType)
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            formPage = new FormPage(toolBox, router, requestCenter, formType, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent);
            actualWindow = WindowsName.FormPage;
            formPage.TopGridInit(topGrid);
            formPage.CenterGridInit(centerGrid);
            formPage.BottomGridInit(bottomGrid);
        }

        public void storageViewerPageInit()
        {
            Data[] data = JsonCenter.LoadStorageViewerInfos(requestCenter, actualStorageId);

            if (data.Length < 1)
            {
                FormPageInit(WindowsName.InitStorage);
            }
            else
            {
                RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

                storageViewerPage = new StorageViewerPage(toolBox, router, requestCenter, actualUserId, actualStorageId, actualDataId, actualCustomListId, reloadEvent);
                actualWindow = WindowsName.StorageViewerPage;
                storageViewerPage.TopGridInit(topGrid);
                storageViewerPage.CenterGridInit(centerGrid);
                storageViewerPage.BottomGridInit(bottomGrid);

                if (storageViewerPage.emptyInfoPopUp) 
                { 
                    storageViewerPage.EmptyInfoPopUp(); 
                }
            }
        }

        public void IDBackups()
        {
            switch(actualWindow)
            {
                case WindowsName.MainMenu:
                    mainMenu.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.StorageSelectionMenu:
                    storageSelectionMenu.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.StorageTransfertSelection:
                    storageTransfertSelection.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.DataTransfert:
                    dataTransfert.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.ListMenu:
                    listMenu.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.UserMenu:
                    userMenu.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.ListViewerPage:
                    listViewerPage.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.DatabaseModifMenu:
                    databaseModifMenu.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.GlobalStorageResearch:
                    globalStorageResearch.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.DataDetailPage:
                    dataDetailsPage.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.LogsMenu:
                    logsMenu.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.BugReportedView:
                    bugReportedView.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.FormPage:
                    formPage.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;

                case WindowsName.StorageViewerPage:
                    storageViewerPage.IDBachUps(out actualUserId, out actualStorageId, out actualDataId, out actualCustomListId);
                    break;
            }
        }

        public void WindowSwitch(object sender, RoutedEventArgs e, WindowsName windowName) 
        {
            IDBackups();

            switch(windowName)
            {
                case WindowsName.MainMenu:
                    MainMenuInit();
                    break;

                case WindowsName.StorageSelectionMenu:
                    StorageSelectionMenuInit();
                    break;

                case WindowsName.StorageTransfertSelection:
                    StorageTransfertSelectionInit();
                    break;

                case WindowsName.DataTransfert:
                    DataTransfertInit();
                    break;

                case WindowsName.StorageViewerPage:
                    storageViewerPageInit();
                    break;

                case WindowsName.ListMenu:
                    ListMenuInit();
                    break;

                case WindowsName.UserMenu:
                    UserMenuInit();
                    break;

                case WindowsName.LogsMenu:
                    LogsMenuInit();
                    break;

                case WindowsName.BugReportedView:
                    BugReportedViewInit();
                    break;

                case WindowsName.DatabaseModifMenu:
                    DatabaseModifMenuInit();
                    break;

                case WindowsName.ListViewerPage:
                    ListViewerPageInit();
                    break;

                case WindowsName.GlobalStorageResearch:
                    GlobalStorageResearchInit();
                    break;

                case WindowsName.DataDetailPage:
                    DataDetailsPageInit();
                    break;

                case WindowsName.AddStorage:
                    FormPageInit(windowName);
                    break;

                case WindowsName.InitStorage:
                    FormPageInit(windowName);
                    break;

                case WindowsName.CreditPage:
                    FormPageInit(windowName);
                    break;

                case WindowsName.BugReportPage:
                    FormPageInit(windowName);
                    break;
            }
        }

        public RoutedEventHandler EnventHandlerGenerator(WindowsName windowName)
        {
            RoutedEventHandler router = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                WindowSwitch(sender, e, windowName);
            });

            return router;
        }

        public RoutedEventHandler[] EnventHandlerGeneratorByTab(WindowsName[] windowName, WindowsName[] formRoutersName, WindowsName[] routersNameF)
        {
            RoutedEventHandler[] routers = new RoutedEventHandler[windowName.Length + formRoutersName.Length - 1];
            var i = 0;
            var j = 0;
            WindowsName string1;

            foreach (WindowsName name in windowName)
            {
                if (name != WindowsName.FormPage)
                {
                    routers[i] = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                    {
                        WindowSwitch(sender, e, name);
                    });
                    routersNameF[i] = name;
                }
                else
                {
                    foreach(WindowsName formName in formRoutersName)
                    {
                        string1 = formRoutersName[j];

                        switch (j)
                        {
                            case 0:
                                routers[i + j] = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                                {
                                    WindowSwitch(sender, e, WindowsName.AddStorage);
                                });
                                break;
                            case 1:
                                routers[i + j] = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                                {
                                    WindowSwitch(sender, e, WindowsName.InitStorage);
                                });
                                break;
                            case 2:
                                routers[i + j] = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                                {
                                    WindowSwitch(sender, e, WindowsName.CreditPage);
                                });
                                break;
                            case 3:
                                routers[i + j] = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                                {
                                    WindowSwitch(sender, e, WindowsName.BugReportPage);
                                });
                                break;
                        }
                        routersNameF[i + j] = formName;

                        j++;
                    }
                }

                i++;
            }

            return routers;
        }

        public Router InitRouters()
        {
            WindowsName[] routersName = new WindowsName[] 
            { 
                WindowsName.MainMenu,
                WindowsName.StorageSelectionMenu,
                WindowsName.StorageTransfertSelection,
                WindowsName.DataTransfert,
                WindowsName.StorageViewerPage,
                WindowsName.ListMenu,
                WindowsName.ListViewerPage,
                WindowsName.GlobalStorageResearch,
                WindowsName.DataDetailPage,
                WindowsName.LogsMenu,
                WindowsName.UserMenu,
                WindowsName.BugReportedView,
                WindowsName.DatabaseModifMenu,
                WindowsName.FormPage
            };

            WindowsName[] formRoutersName = new WindowsName[]
            {
                WindowsName.AddStorage,
                WindowsName.InitStorage,
                WindowsName.CreditPage,
                WindowsName.BugReportPage
            };

            WindowsName[] routersNameF = new WindowsName[routersName.Length - 1 + formRoutersName.Length];


            RoutedEventHandler[] routersRouter = EnventHandlerGeneratorByTab(routersName, formRoutersName, routersNameF);

            return new Router(routersNameF, routersRouter);
        }

        public void ReloadView(object sender, RoutedEventArgs e)
        {
            switch (actualWindow)
            {
                case WindowsName.MainMenu:
                    mainMenu.IsUserConnected();
                    mainMenu.LoadBDDInfos();
                    mainMenu.TopGridInit(topGrid);
                    mainMenu.CenterGridInit(centerGrid);
                    mainMenu.BottomGridInit(bottomGrid);
                    break;

                case WindowsName.StorageSelectionMenu:
                    storageSelectionMenu.TopGridInit(topGrid);
                    storageSelectionMenu.CenterGridInit(centerGrid);
                    storageSelectionMenu.BottomGridInit(bottomGrid);

                    if (storageSelectionMenu.emptyInfoPopUp) 
                    { 
                        storageSelectionMenu.EmptyInfoPopUp(); 
                    }
                    break;

                case WindowsName.StorageTransfertSelection:
                    storageTransfertSelection.TopGridInit(topGrid);
                    storageTransfertSelection.CenterGridInit(centerGrid);
                    storageTransfertSelection.BottomGridInit(bottomGrid);
                    break;

                case WindowsName.DataTransfert:
                    dataTransfert.TopGridInit(topGrid);
                    dataTransfert.CenterGridInit(centerGrid);
                    dataTransfert.BottomGridInit(bottomGrid);
                    break;

                case WindowsName.FormPage:
                    formPage.GetUIElements();
                    formPage.TopGridInit(topGrid);
                    formPage.CenterGridInit(centerGrid);
                    formPage.BottomGridInit(bottomGrid);
                    formPage.DataInjection();
                    break;

                case WindowsName.ListMenu:
                    listMenu.TopGridInit(topGrid);
                    listMenu.CenterGridInit(centerGrid);
                    listMenu.BottomGridInit(bottomGrid);

                    if (listMenu.emptyInfoPopUp) 
                    { 
                        listMenu.EmptyInfoPopUp(); 
                    }
                    break;

                case WindowsName.UserMenu:
                    userMenu.TopGridInit(topGrid);
                    userMenu.CenterGridInit(centerGrid);
                    userMenu.BottomGridInit(bottomGrid);

                    if (userMenu.emptyInfoPopUp) 
                    { 
                        userMenu.EmptyInfoPopUp(); 
                    }
                    break;

                case WindowsName.ListViewerPage:
                    listViewerPage.LoadBDDInfos();
                    listViewerPage.TopGridInit(topGrid);
                    listViewerPage.CenterGridInit(centerGrid);
                    listViewerPage.BottomGridInit(bottomGrid);

                    if (listViewerPage.emptyInfoPopUp) 
                    { 
                        listViewerPage.EmptyInfoPopUp(); 
                    }
                    break;

                case WindowsName.GlobalStorageResearch:
                    globalStorageResearch.researchKeyString = globalStorageResearch.researchTextBox.Text;
                    globalStorageResearch.LoadBDDInfos();
                    globalStorageResearch.TopGridInit(topGrid);
                    globalStorageResearch.CenterGridInit(centerGrid);
                    globalStorageResearch.BottomGridInit(bottomGrid);

                    if (globalStorageResearch.emptyInfoPopUp) 
                    { 
                        globalStorageResearch.EmptyInfoPopUp(); 
                    }
                    break;

                case WindowsName.DataDetailPage:
                    dataDetailsPage.LoadBDDInfos();
                    dataDetailsPage.TopGridInit(topGrid);
                    dataDetailsPage.CenterGridInit(centerGrid);
                    dataDetailsPage.BottomGridInit(bottomGrid);
                    break;

                case WindowsName.DatabaseModifMenu:
                    databaseModifMenu.TopGridInit(topGrid);
                    databaseModifMenu.CenterGridInit(centerGrid);
                    databaseModifMenu.BottomGridInit(bottomGrid);
                    break;

                case WindowsName.BugReportedView:
                    bugReportedView.LoadBDDInfos();
                    bugReportedView.TopGridInit(topGrid);
                    bugReportedView.CenterGridInit(centerGrid);
                    bugReportedView.BottomGridInit(bottomGrid);
                    break;

                case WindowsName.LogsMenu:
                    if (logsMenu.researchTextBox.Text != string.Empty || logsMenu.preResearchDate.SelectedDate != null || logsMenu.postResearchDate.SelectedDate != null)
                    {
                        logsMenu.ResearchThree();
                    }
                    else
                    {
                        logsMenu.LoadBDDInfos();
                        logsMenu.TopGridInit(topGrid);
                    }
                    logsMenu.CenterGridInit(centerGrid);
                    logsMenu.BottomGridInit(bottomGrid);
                    break;

                case WindowsName.StorageViewerPage:
                    if (storageViewerPage.researchTextBox.Text != string.Empty)
                    {
                        storageViewerPage.ResearchThree(storageViewerPage.researchTextBox.Text);
                    }
                    else if (storageViewerPage.sortingTrigger) { }
                    else
                    {
                        storageViewerPage.LoadBDDInfos();
                        storageViewerPage.TopGridInit(topGrid);
                    }
                    storageViewerPage.CenterGridInit(centerGrid);
                    storageViewerPage.BottomGridInit(bottomGrid);

                    if (storageViewerPage.emptyInfoPopUp) 
                    { 
                        storageViewerPage.EmptyInfoPopUp(); 
                    }
                    break;
            }
        }

        private void ClosePopUp(object sender, CancelEventArgs e)
        {
            if(!PopUpCenter.ActionValidPopup())
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
