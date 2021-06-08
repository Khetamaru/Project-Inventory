using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public class StorageSelectionMenu : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;
        private Storage[] bottomGridButtons;
        private RoutedEventLibrary[] bottomSwitchEvents;

        private int widthLimit;

        public StorageSelectionMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualStorageId, int _actualDataId)
            : base(toolBox, _router, requestCenter, _actualStorageId, _actualDataId)
        {
            topGridButtons = new string[] { "Create a Storage", "Return" };

            topSwitchEvents = new RoutedEventLibrary[2];
            RoutedEventLibrariesInit(topSwitchEvents);
            topSwitchEvents[0].changePageEvent = GetEventHandler("Add Storage");
            topSwitchEvents[1].changePageEvent = GetEventHandler("MainMenu");

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
            RoutedIdSetup(bottomGridButtons);

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, bottomSwitchEvents, "standart", "CenterCenter");
        }

        public void RoutedIdSetup(Storage[] storageLibrary)
        {
            var i = 0;

            foreach(Storage storage in storageLibrary)
            {
                bottomSwitchEvents[i].updateIdEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                {
                    IDSetup(sender, e, storage.id);
                });
                i++;
            }
        }

        public void IDSetup(object sender, RoutedEventArgs e, int id)
        {
            actualStorageId = id;
        }
    }
}
