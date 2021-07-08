using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    /// <summary>
    /// Page to create a storage or select one to see details of it
    /// </summary>
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
            topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.AddStorage);
            topSwitchEvents[1].changePageEvent = GetEventHandler(WindowsName.MainMenu);

            LoadBDDInfos();
            
            widthLimit = 5;
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void LoadBDDInfos()
        {
            bottomGridButtons = JsonCenter.LoadStorageSelectionInfos(requestCenter);
            bottomSwitchEvents = JsonCenter.SetEventHandlerTab(bottomGridButtons.Length, GetEventHandler(WindowsName.StorageViewerPage));
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 2, SkinsName.TopStretch, SkinsName.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, 
                                                   topGridButtons, 
                                                   topSwitchEvents, 
                                                   new SkinsName[] { SkinsName.StandartLittleMargin, SkinsName.StandartLittleMargin }, 
                                                   new SkinsName[] { SkinsName.TopLeft, SkinsName.TopRight });
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            ButtonPlacer(bottomGrid, bottomGridButtons.Length, widthLimit, SkinsName.BottomStretch, SkinsName.HeightNintyPercent);
            RoutedIdSetup(bottomGridButtons);

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, bottomSwitchEvents, SkinsName.Standart, SkinsName.CenterCenter);
        }

        /// <summary>
        /// Insert the stored procedure in the button
        /// </summary>
        /// <param name="storageLibrary"></param>
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

        /// <summary>
        /// Load actuel storage id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        public void IDSetup(object sender, RoutedEventArgs e, int id)
        {
            actualStorageId = id;
        }
    }
}
