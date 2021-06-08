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

        private string actualWindow;

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
            actualWindow = "mainMenu";
            mainMenu.TopGridInit(topGrid);
            mainMenu.CenterGridInit(centerGrid);
            mainMenu.BottomGridInit(bottomGrid);
        }

        public void StorageSelectionMenuInit()
        {
            storageSelectionMenu = new StorageSelectionMenu(toolBox, router, requestCenter, actualStorageId, actualDataId);
            actualWindow = "storageSelectionMenu";
            storageSelectionMenu.TopGridInit(topGrid);
            storageSelectionMenu.CenterGridInit(centerGrid);
            storageSelectionMenu.BottomGridInit(bottomGrid);
        }

        public void FormPageInit(string formType)
        {
            formPage = new FormPage(toolBox, router, requestCenter, formType, actualStorageId, actualDataId);
            actualWindow = "formPage";
            formPage.TopGridInit(topGrid);
            formPage.CenterGridInit(centerGrid);
            formPage.BottomGridInit(bottomGrid);
        }

        public void storageViewerPageInit()
        {
            storageViewerPage = new StorageViewerPage(toolBox, router, requestCenter, actualStorageId, actualDataId);
            actualWindow = "storageViewerPage";
            storageViewerPage.TopGridInit(topGrid);
            storageViewerPage.CenterGridInit(centerGrid);
            storageViewerPage.BottomGridInit(bottomGrid);
        }

        public void IDBackups()
        {
            switch(actualWindow)
            {
                case ("mainMenu"):
                    actualStorageId = mainMenu.StorageIDBackups();
                    actualDataId = mainMenu.DataIDBackups();
                    break;
                case ("storageSelectionMenu"):
                    actualStorageId = storageSelectionMenu.StorageIDBackups();
                    actualDataId = storageSelectionMenu.DataIDBackups();
                    break;
                case ("formPage"):
                    actualStorageId = formPage.StorageIDBackups();
                    actualDataId = formPage.DataIDBackups();
                    break;
                case ("storageViewerPage"):
                    actualStorageId = storageViewerPage.StorageIDBackups();
                    actualDataId = storageViewerPage.DataIDBackups();
                    break;
            }
        }

        public void WindowSwitch(object sender, RoutedEventArgs e, string windowName) 
        {
            IDBackups();

            switch(windowName)
            {
                case ("MainMenu"):
                    MainMenuInit();
                    break;

                case ("StorageSelectionMenu"):
                    StorageSelectionMenuInit();
                    break;

                case ("storageViewerPage"):
                    storageViewerPageInit();
                    break;

                case ("Add Storage"):
                    FormPageInit(windowName);
                    break;
            }
        }

        public RoutedEventHandler EnventHandlerGenerator(string windowName)
        {
            RoutedEventHandler router = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                WindowSwitch(sender, e, windowName);
            });

            return router;
        }

        public RoutedEventHandler[] EnventHandlerGeneratorByTab(string[] windowName, string[] formRoutersName, string[] routersNameF)
        {
            RoutedEventHandler[] routers = new RoutedEventHandler[windowName.Length + formRoutersName.Length - 1];
            var i = 0;
            var j = 0;
            var string1 = string.Empty;

            foreach (string name in windowName)
            {
                if (name != "FormPage")
                {
                    routers[i] = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                    {
                        WindowSwitch(sender, e, name);
                    });
                    routersNameF[i] = name;
                }
                else
                {
                    foreach(string formName in formRoutersName)
                    {
                        string1 = formRoutersName[j];

                        routers[i + j] = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                        {
                            WindowSwitch(sender, e, string1);
                        });
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
            string[] routersName = new string[] 
            { 
                "MainMenu", 
                "StorageSelectionMenu",
                "storageViewerPage",
                "FormPage"
            };

            string[] formRoutersName = new string[]
            {
                "Add Storage"
            };

            string[] routersNameF = new string[routersName.Length - 1 + formRoutersName.Length];


            RoutedEventHandler[] routersRouter = EnventHandlerGeneratorByTab(routersName, formRoutersName, routersNameF);

            return new Router(routersNameF, routersRouter);
        }
    }
}
