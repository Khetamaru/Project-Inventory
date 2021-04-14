using System.Windows;
using System.Windows.Navigation;

namespace Project_Inventory
{
    public partial class MainWindow : Window
    {
        private VisualElements_ToolBox toolBox;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            NavigationService navService = NavigationService.GetNavigationService(this);

            toolBox = new VisualElements_ToolBox(navService, this);

            Init();
        }

        private void Init()
        {
            topGrid = toolBox.SetUpGrid(topGrid, 1, 1, "RowTopTier");

            topGrid = toolBox.CreateButtonToGrid(topGrid, "ceci est une image", 0, 0, "standart");


            //centerGrid = toolBox.SetUpGrid(centerGrid, 1, 1, "RowCenterTier");


            bottomGrid = toolBox.SetUpGrid(bottomGrid, 1, 2, "RowBottomTier");

            bottomGrid = toolBox.CreateButtonToGrid(bottomGrid, "ceci est un text à la con", 0, 0, "standart");
            bottomGrid = toolBox.CreateButtonToGrid(bottomGrid, "lui aussi",                 0, 1, "standart");
        }
    }
}
