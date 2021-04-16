using System;
using System.Windows;

namespace Project_Inventory
{
    public partial class MainWindow : Window
    {
        private VisualElements_ToolBox toolBox;
        private double titleBarHeight;

        public MainWindow()
        {
            DataContext = this;
            ResizeMode = ResizeMode.CanMinimize;

            InitializeComponent();

            titleBarHeight = SystemParameters.WindowCaptionHeight;

            toolBox = new VisualElements_ToolBox(this, titleBarHeight);

            Init();
        }

        private void Init()
        {
            TopGridInit();
            BottomGridInit();
        }

        private void TopGridInit()
        {
            topGrid = toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightOneTier");

            string[] topGridButtons = new string[] { "Logo Application" };

            topGrid = toolBox.CreateButtonsToGridByTab(topGrid, topGridButtons, "standart", "CenterCenter");
        }

        private void BottomGridInit()
        {
            bottomGrid = toolBox.SetUpGrid(bottomGrid, 1, 2, "BottomStretch", "HeightTwoTier");

            string[] bottomGridButtons = new string[] {"Menu n°1", "Menu n°2"};
            Type[] rederectType = new Type[] { typeof(StorageSelectionPage), typeof(StorageSelectionPage) };

            bottomGrid = toolBox.CreateRederectButtonsToGridByTab(bottomGrid, bottomGridButtons, rederectType, "standart", "CenterCenter");
        }
    }
}
