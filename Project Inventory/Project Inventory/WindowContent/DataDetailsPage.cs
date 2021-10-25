using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Project_Inventory.BDD;
using Project_Inventory.Tools;

namespace Project_Inventory
{
    class DataDetailsPage : WindowContent
    {
        private Grid capGrid;

        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private Data data;
        private Data header;
        private List<List<ListOption>> listOptions;
        private List<int> customListIds;

        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        private RoutedEventHandler reloadEvent;

        public DataDetailsPage(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomId, RoutedEventHandler _reloadEvent)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomId)
        {
            topGridButtons = new string[] { "Transfert", "Retour" };
            saveButton = new string[] { "Sauvegarder" };

            reloadEvent = _reloadEvent;

            topSwitchEvents = new RoutedEventLibrary[2];
            RoutedEventLibrariesInit(topSwitchEvents);

            topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.StorageTransfertSelection);
            topSwitchEvents[0].updateIdEvent = new RoutedEventHandler((object sender, RoutedEventArgs e) => { UpdateDataId(sender, e); });
            topSwitchEvents[1].changePageEvent = GetEventHandler(WindowsName.GlobalStorageResearch);

            saveEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(saveEvents);
            saveEvents[0].optionalEventTwo = new RoutedEventHandler((object sender, RoutedEventArgs e) => { SaveDatas(sender, e); });

            LoadBDDInfos();
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void LoadBDDInfos()
        {
            data = JsonCenter.LoadDataDetailsPageInfos(requestCenter, actualDataId, out listOptions, out customListIds, out header);
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 2, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid,
                                                   topGridButtons,
                                                   topSwitchEvents,
                                                   new SkinName[] { SkinName.StandartLittleMargin, SkinName.StandartLittleMargin },
                                                   new SkinLocation[] { SkinLocation.TopLeft, SkinLocation.TopRight });
        }

        public new void CenterGridInit(Grid centerGrid)
        {
            capGrid = new Grid();

            toolBox.SetUpGrid(centerGrid, 1, 1, SkinLocation.StretchStretch, SkinSize.HeightEightPercent);

            toolBox.GlobalDataDetailsGrid(centerGrid, capGrid, data, listOptions, customListIds, header);
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            toolBox.SetUpGrid(bottomGrid, 1, 1, SkinLocation.BottomStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, saveButton, saveEvents, SkinName.StandartLittleMargin, SkinLocation.BottomCenter);
        }

        /// <summary>
        /// Apply changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDatas(object sender, RoutedEventArgs e)
        {
            if (PopUpCenter.ActionValidPopup())
            {
                Data output;
                if (toolBox.GetUIElements(toolBox.ExtractFormInfos(capGrid), data, out output, listOptions))
                {
                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "Une donnée du stockage (" + JsonCenter.GetStorage(requestCenter, output.StorageId).Name + ") a été modifiée.").ToJson());
                    requestCenter.PutRequest(BDDTabsName.DataLibraries.ToString() + "/" + output.id, output.ToJsonId());
                }

                reloadEvent.Invoke(sender, e);
            }
        }

        private void UpdateDataId(object sender, RoutedEventArgs e)
        {
            actualDataId = data.id;
        }
    }
}
