﻿using Project_Inventory.Tools;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public partial class MainWindow : Window
    {
        public ToolBox toolBox;
        private double titleBarHeight;

        private Router router;
        private RequestCenter requestCenter;

        private MainMenu mainMenu;
        private StorageSelectionMenu storageSelectionMenu;
        private FormPage formPage;
        private StorageViewerPage storageViewerPage;

        public MainWindow()
        {
            DataContext = this;
            ResizeMode = ResizeMode.CanMinimize;

            InitializeComponent();

            titleBarHeight = SystemParameters.WindowCaptionHeight;

            toolBox = new ToolBox(this, titleBarHeight);

            router = InitRouters();
            requestCenter = new RequestCenter();

            Init();
        }

        private void Init()
        {
            MainMenuInit();
        }

        public void MainMenuInit()
        {
            mainMenu = new MainMenu(toolBox, router, requestCenter);
            mainMenu.TopGridInit(topGrid);
            mainMenu.CenterGridInit(centerGrid);
            mainMenu.BottomGridInit(bottomGrid);
        }

        public void StorageSelectionMenuInit()
        {
            storageSelectionMenu = new StorageSelectionMenu(toolBox, router, requestCenter);
            storageSelectionMenu.TopGridInit(topGrid);
            storageSelectionMenu.CenterGridInit(centerGrid);
            storageSelectionMenu.BottomGridInit(bottomGrid);
        }

        public void FormPageInit()
        {
            formPage = new FormPage(toolBox, router, requestCenter);
            formPage.TopGridInit(topGrid);
            formPage.CenterGridInit(centerGrid);
            formPage.BottomGridInit(bottomGrid);
        }

        public void storageViewerPageInit()
        {
            storageViewerPage = new StorageViewerPage(toolBox, router, requestCenter);
            storageViewerPage.TopGridInit(topGrid);
            storageViewerPage.CenterGridInit(centerGrid);
            storageViewerPage.BottomGridInit(bottomGrid);
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

                case ("FormPage"):
                    FormPageInit();
                    break;

                case ("storageViewerPage"):
                    storageViewerPageInit();
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
            string[] routersName = new string[] 
            { 
                "MainMenu", 
                "StorageSelectionMenu", 
                "FormPage",
                "storageViewerPage"
            };

            RoutedEventHandler[] routersRouter = EnventHandlerGeneratorByTab(routersName);

            return new Router(routersName, routersRouter);
        }
    }
}
