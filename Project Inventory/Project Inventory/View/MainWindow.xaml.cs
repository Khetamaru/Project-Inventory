using Project_Inventory.BDD;
using Project_Inventory.Tools;
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

        private int actualStorageId;
        private int actualDataId;

        public MainWindow()
        {
            DataContext = this;
            ResizeMode = ResizeMode.CanMinimize;

            InitializeComponent();

            titleBarHeight = SystemParameters.WindowCaptionHeight;

            toolBox = new ToolBox(this, titleBarHeight);

            router = InitRouters();
            requestCenter = new RequestCenter();

            actualStorageId = 42;
            actualDataId = 42;

            Init();
        }

        private void Init()
        {
            MainMenuInit();
        }

        public void MainMenuInit()
        {
            mainMenu = new MainMenu(toolBox, router, requestCenter, actualStorageId, actualDataId);
            actualWindow = WindowsName.MainMenu;
            mainMenu.TopGridInit(topGrid);
            mainMenu.CenterGridInit(centerGrid);
            mainMenu.BottomGridInit(bottomGrid);
        }

        public void StorageSelectionMenuInit()
        {
            storageSelectionMenu = new StorageSelectionMenu(toolBox, router, requestCenter, actualStorageId, actualDataId);
            actualWindow = WindowsName.StorageSelectionMenu;
            storageSelectionMenu.TopGridInit(topGrid);
            storageSelectionMenu.CenterGridInit(centerGrid);
            storageSelectionMenu.BottomGridInit(bottomGrid);
        }

        public void FormPageInit(WindowsName formType)
        {
            RoutedEventHandler reloadEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => ReloadView(sender, e));

            formPage = new FormPage(toolBox, router, requestCenter, formType, actualStorageId, actualDataId, reloadEvent);
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
                storageViewerPage = new StorageViewerPage(toolBox, router, requestCenter, actualStorageId, actualDataId);
                actualWindow = WindowsName.StorageViewerPage;
                storageViewerPage.TopGridInit(topGrid);
                storageViewerPage.CenterGridInit(centerGrid);
                storageViewerPage.BottomGridInit(bottomGrid);
            }
        }

        public void IDBackups()
        {
            switch(actualWindow)
            {
                case (WindowsName.MainMenu):
                    actualStorageId = mainMenu.StorageIDBackups();
                    actualDataId = mainMenu.DataIDBackups();
                    break;
                case (WindowsName.StorageSelectionMenu):
                    actualStorageId = storageSelectionMenu.StorageIDBackups();
                    actualDataId = storageSelectionMenu.DataIDBackups();
                    break;
                case (WindowsName.FormPage):
                    actualStorageId = formPage.StorageIDBackups();
                    actualDataId = formPage.DataIDBackups();
                    break;
                case (WindowsName.StorageViewerPage):
                    actualStorageId = storageViewerPage.StorageIDBackups();
                    actualDataId = storageViewerPage.DataIDBackups();
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

                case WindowsName.StorageViewerPage:
                    storageViewerPageInit();
                    break;

                case WindowsName.AddStorage:
                    FormPageInit(windowName);
                    break;

                case WindowsName.InitStorage:
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
                WindowsName.StorageViewerPage,
                WindowsName.FormPage
            };

            WindowsName[] formRoutersName = new WindowsName[]
            {
                WindowsName.AddStorage,
                WindowsName.InitStorage
            };

            WindowsName[] routersNameF = new WindowsName[routersName.Length - 1 + formRoutersName.Length];


            RoutedEventHandler[] routersRouter = EnventHandlerGeneratorByTab(routersName, formRoutersName, routersNameF);

            return new Router(routersNameF, routersRouter);
        }

        public void ReloadView(object sender, RoutedEventArgs e)
        {
            switch (actualWindow)
            {
                case (WindowsName.MainMenu):
                    mainMenu.TopGridInit(topGrid);
                    mainMenu.CenterGridInit(centerGrid);
                    mainMenu.BottomGridInit(bottomGrid);
                    break;

                case (WindowsName.StorageSelectionMenu):
                    storageSelectionMenu.TopGridInit(topGrid);
                    storageSelectionMenu.CenterGridInit(centerGrid);
                    storageSelectionMenu.BottomGridInit(bottomGrid);
                    break;

                case (WindowsName.FormPage):
                    formPage.TopGridInit(topGrid);
                    formPage.CenterGridInit(centerGrid);
                    formPage.BottomGridInit(bottomGrid);
                    break;

                case (WindowsName.StorageViewerPage):
                    storageViewerPage.TopGridInit(topGrid);
                    storageViewerPage.CenterGridInit(centerGrid);
                    storageViewerPage.BottomGridInit(bottomGrid);
                    break;
            }
        }
    }
}
