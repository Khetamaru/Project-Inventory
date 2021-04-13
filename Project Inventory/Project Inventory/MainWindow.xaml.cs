using System.Windows;
using System.Windows.Controls;
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
            topGrid = toolBox.ChangeGrid(topGrid, 1, 1, "row");
            bottomGrid = toolBox.ChangeGrid(bottomGrid, 2, 1, "row");

            topGrid = toolBox.CreateButtonToGrid(topGrid,       "ceci est une image",        0, 0, "standart");
            bottomGrid = toolBox.CreateButtonToGrid(bottomGrid, "ceci est un text à la con", 0, 0, "standart");
            bottomGrid = toolBox.CreateButtonToGrid(bottomGrid, "lui aussi",                 1, 0, "standart");
        }
    }
}
