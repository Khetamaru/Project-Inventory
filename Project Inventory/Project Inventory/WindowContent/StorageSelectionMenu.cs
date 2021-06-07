using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public class StorageSelectionMenu : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventHandler[] topSwitchEvents;
        private Storage[] bottomGridButtons;
        private RoutedEventHandler[] bottomSwitchEvents;

        private int widthLimit;

        public StorageSelectionMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualStorageId, int _actualDataId)
            : base(toolBox, _router, requestCenter, _actualStorageId, _actualDataId)
        {
            topGridButtons = new string[] { "Create a Storage", "Return" };

            topSwitchEvents = new RoutedEventHandler[] { GetEventHandler("Add Storage"), GetEventHandler("MainMenu") };

            LoadBDDInfos();
            
            widthLimit = 5;
        }

        public void LoadBDDInfos()
        {
            bottomGridButtons = JsonCenter.LoadStorageSelectionInfos(requestCenter);
            bottomSwitchEvents = JsonCenter.SetEventHandlerTab(bottomGridButtons.Length, GetEventHandler("storageViewerPage"));
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 2, "TopStretch", "HeightTenPercent");

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, 
                                                   topGridButtons, 
                                                   topSwitchEvents, 
                                                   new string[] { "StandartLittleMargin", "StandartLittleMargin" }, 
                                                   new string[] { "TopLeft", "TopRight" });
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            ButtonPlacer(bottomGrid, bottomGridButtons.Length, widthLimit, "BottomStretch", "HeightNintyPercent");

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, RoutedIdSetup(bottomGridButtons), bottomSwitchEvents, "standart", "CenterCenter");
        }

        public RoutedEventHandler[] RoutedIdSetup(Storage[] storageLibrary)
        {
            var i = 0;
            RoutedEventHandler[] RoutedIds = new RoutedEventHandler[storageLibrary.Length];

            foreach(Storage storage in storageLibrary)
            {
                RoutedIds[i] = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    IDSetup(sender, e, storage.id);
                });
                i++;
            }

            return RoutedIds;
        }

        public void IDSetup(object sender, RoutedEventArgs e, int id)
        {
            actualStorageId = id;
        }
    }
}
