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
            windowGrid = toolBox.ChangeGrid(windowGrid, 2, 2);

            
            windowGrid = toolBox.CreateButtonToGrid(windowGrid, "ceci est un text à la con", 1, 0, "standart");
            windowGrid = toolBox.CreateButtonToGrid(windowGrid, "lui aussi", 1, 1, "standart");

            windowGrid = toolBox.CreateButtonToGrid(windowGrid, "ceci est une image", 0, "standart");

        }
    }
}
