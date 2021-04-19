using System;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public partial class MainWindow : Window
    {
        public VisualElements_ToolBox toolBox;
        private double titleBarHeight;

        private MainMenu mainMenu;
        private StorageSelectionMenu storageSelectionMenu;

        public MainWindow()
        {
            DataContext = this;
            ResizeMode = ResizeMode.CanMinimize;

            InitializeComponent();

            titleBarHeight = SystemParameters.WindowCaptionHeight;

            toolBox = new VisualElements_ToolBox(this, titleBarHeight);

            mainMenu = new MainMenu(toolBox);
            storageSelectionMenu = new StorageSelectionMenu(toolBox);

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

        public void WindowSwitch(string windowName)
        {
            switch(windowName)
            {
                case ("MainMneu"):
                    MainMenuInit();
                    break;

                case ("StorageSelectionMenu"):
                    StorageSelectionMenuInit();
                    break;
            }
        }
    }
}
