using Project_Inventory.BDD;
using Project_Inventory.Tools.FonctionalityCerters;
using Project_Inventory.Tools.NamesLibraries;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Project_Inventory.Tools;

namespace Project_Inventory
{
    /// <summary>
    /// Page to create a storage or select one to see details of it
    /// </summary>
    public class StorageTransfertSelection : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private List<Storage> bottomGridButtons;
        private RoutedEventLibrary[] bottomSwitchEvents;

        private int widthLimit;

        private Grid capGrid;

        public StorageTransfertSelection(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomListId, RoutedEventHandler _reloadEvent)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomListId)
        {
            topGridButtons = new string[] { "Retour" };

            topSwitchEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(topSwitchEvents);

            topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.DataDetailPage);

            capGrid = new Grid();

            LoadBDDInfos();
            
            widthLimit = 5;
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void LoadBDDInfos()
        {
            bottomGridButtons = JsonCenter.LoadStorageTransfertSelectionInfos(requestCenter);
            bottomSwitchEvents = JsonCenter.SetEventHandlerTab(bottomGridButtons.Count, GetEventHandler(WindowsName.DataTransfert));
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 3, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, 
                                                   topGridButtons, 
                                                   topSwitchEvents, 
                                                   new SkinName[] { SkinName.StandartLittleMargin, SkinName.StandartLittleMargin, SkinName.StandartLittleMargin }, 
                                                   new SkinLocation[] { SkinLocation.TopLeft, SkinLocation.TopCenter, SkinLocation.TopRight });
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            LoadBDDInfos();

            toolBox.SetUpGrid(bottomGrid, 1, 1, SkinLocation.BottomStretch, SkinSize.HeightNintyPercent);
            capGrid = new Grid();

            toolBox.ButtonPlacer(capGrid, bottomGridButtons.Count, widthLimit, SkinLocation.BottomStretch, SkinSize.HeightNintyPercent);
            RoutedIdSetup(bottomGridButtons);

            toolBox.CreateSwitchButtonsToGridByTab(capGrid, bottomGridButtons.ToArray(), bottomSwitchEvents, SkinName.Standart, SkinLocation.CenterCenter);
            bottomGrid.Children.Add(capGrid);
        }

        /// <summary>
        /// Insert the stored procedure in the button
        /// </summary>
        /// <param name="storageLibrary"></param>
        public void RoutedIdSetup(List<Storage> storageLibrary)
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
