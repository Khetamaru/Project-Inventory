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
            windowGrid = toolBox.ChangeGrid(windowGrid, 1, 1);

            windowGrid = toolBox.CreateButtonToGrid(windowGrid, "ceci est un text à la con", HorizontalAlignment.Center, VerticalAlignment.Top);
            windowGrid = toolBox.CreateButtonToGrid(windowGrid, "lui aussi", HorizontalAlignment.Center, VerticalAlignment.Center);
        }
    }
}
