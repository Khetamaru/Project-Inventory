using System;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public partial class MainWindow : Window
    {
        public VisualElements_ToolBox toolBox;
        private double titleBarHeight;

        private Router router;

        private MainMenu mainMenu;
        private StorageSelectionMenu storageSelectionMenu;

        public MainWindow()
        {
            DataContext = this;
            ResizeMode = ResizeMode.CanMinimize;

            InitializeComponent();

            titleBarHeight = SystemParameters.WindowCaptionHeight;

            toolBox = new VisualElements_ToolBox(this, titleBarHeight);

            router = InitRouters();

            mainMenu = new MainMenu(toolBox, router);
            storageSelectionMenu = new StorageSelectionMenu(toolBox, router);

            Init();
        }

        private void Init()
        {
            MainMenuInit();
        }

        public void MainMenuInit()
        {
            topGrid = mainMenu.TopGridInit(topGrid);
            centerGrid = mainMenu.CenterGridInit(centerGrid);
            bottomGrid = mainMenu.BottomGridInit(bottomGrid);
        }

        public void StorageSelectionMenuInit()
        {
            topGrid = storageSelectionMenu.TopGridInit(topGrid);
            centerGrid = storageSelectionMenu.CenterGridInit(centerGrid);
            bottomGrid = storageSelectionMenu.BottomGridInit(bottomGrid);
        }

        public void WindowSwitch(object sender, RoutedEventArgs e, string windowName) 
        {
            switch(windowName)
            {
                case ("MainMenu"):
                    MainMenuInit();
                    break;

                case ("StorageSelectionMenu"):
                    StorageSelectionMenuInit();
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

        public RoutedEventHandler[] EnventHandlerGeneratorByTab(string[] windowName)
        {
            RoutedEventHandler[] routers = new RoutedEventHandler[windowName.Length];
            var i = 0;

            foreach(string name in windowName)
            {
                routers[i] = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    WindowSwitch(sender, e, name);
                });

                i++;
            }

            return routers;
        }

        public Router InitRouters()
        {
            string[] routersName = new string[] { "MainMenu", "StorageSelectionMenu" };

            RoutedEventHandler[] routersRouter = EnventHandlerGeneratorByTab(routersName);

            return new Router(routersName, routersRouter);
        }
    }
}
