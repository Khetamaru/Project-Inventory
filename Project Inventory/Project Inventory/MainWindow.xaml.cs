using System;
using System.Windows;
using System.Windows.Navigation;

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
            topGrid = toolBox.SetUpGrid(topGrid, 1, 1, "RowTopTier", "HeightOneTier");

            string[] topGridButtons = new string[1] { "Logo Application" };

            topGrid = toolBox.CreateButtonsToGridByTab(topGrid, topGridButtons, "standart");
        }

        private void BottomGridInit()
        {
            bottomGrid = toolBox.SetUpGrid(bottomGrid, 1, 2, "RowBottomTier", "HeightTwoTier");

            string[] bottomGridButtons = new string[2] {"Réserve n°1", "Réserve n°2"};
            Type[] rederectType = new Type[2] { typeof(StorageSelectionPage), typeof(StorageSelectionPage) };

            bottomGrid = toolBox.CreateRederectButtonsToGridByTab(bottomGrid, bottomGridButtons, rederectType, "standart");
        }
    }
}
