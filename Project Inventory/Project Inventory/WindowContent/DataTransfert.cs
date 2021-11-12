using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Project_Inventory.BDD;
using Project_Inventory.Tools;

namespace Project_Inventory
{
    class DataTransfert : WindowContent
    {
        private Grid[] capGrid;

        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private Data data;
        private Data newData;

        List<List<ListOption>> listOptions;
        List<int> customListIds;
        Data header;
        Data newHeader;

        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        private RoutedEventHandler reloadEvent;

        public DataTransfert(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomId, RoutedEventHandler _reloadEvent)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomId)
        {
            topGridButtons = new string[] { "", "Retour" };
            saveButton = new string[] { "Sauvegarder" };

            reloadEvent = _reloadEvent;

            topSwitchEvents = new RoutedEventLibrary[2];
            RoutedEventLibrariesInit(topSwitchEvents);

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
            data = JsonCenter.LoadDataTransfertPageInfos(requestCenter, actualDataId, actualStorageId, out newData, out listOptions, out customListIds, out header, out newHeader);

            TryLoadData();
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 2, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid,
                                                   topGridButtons,
                                                   topSwitchEvents,
                                                   new SkinName[] {     SkinName.StandartLittleMargin, SkinName.StandartLittleMargin },
                                                   new SkinLocation[] { SkinLocation.None,             SkinLocation.TopRight });

            Label leftTitle = new Label();
            leftTitle.Content = "ANCIENNE DONNÉE";

            Label rightTitle = new Label();
            rightTitle.Content = "NOUVELLE DONNÉE";

            toolBox.InsertUIElementInGrid(topGrid, leftTitle,  0, 0, UIElementsName.Label, SkinLocation.CenterCenter);
            toolBox.InsertUIElementInGrid(topGrid, rightTitle, 0, 1, UIElementsName.Label, SkinLocation.CenterCenter);
        }

        public new void CenterGridInit(Grid centerGrid)
        {
            capGrid = new Grid[2];

            toolBox.SetUpGrid(centerGrid, 1, 1, SkinLocation.StretchStretch, SkinSize.HeightEightPercent);

            toolBox.DataTransfertGrid(centerGrid, out capGrid[0], out capGrid[1], data, newData, header, newHeader, listOptions, customListIds);
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
                if (toolBox.GetUIElements(toolBox.ExtractFormInfos(capGrid[1]), newData, out output, listOptions))
                {
                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "Une donnée du stockage (" + JsonCenter.GetStorage(requestCenter, actualStorageId).Name + ") a été modifiée.").ToJson());
                    requestCenter.PostRequest(BDDTabsName.DataLibraries.ToString(), output.ToJson());

                    requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "Une donnée du stockage (" + JsonCenter.GetStorage(requestCenter, data.StorageId).Name + ") a été supprimée.").ToJson());
                    requestCenter.DeleteRequest(BDDTabsName.DataLibraries.ToString() + "/" + data.id);

                    GetEventHandler(WindowsName.StorageSelectionMenu).Invoke(sender, e);
                }
            }
        }

        private void TryLoadData()
        {
            int i, j = 0;
            int[] newDataTypeIndex = new int[newData.DataType.Count];

            for (i = 0; i < newDataTypeIndex.Length; i++)
            {
                newDataTypeIndex[i] = -1;
            }

            for (i = 0 ; i < data.DataText.Count ; i++)
            {
                while(newData.DataType.Count > j && (newData.DataType[j] != data.DataType[i] || newDataTypeIndex[j] != -1))
                {
                    j++;
                }
                if (newData.DataType.Count > j)
                {
                    newData.DataText[j] = data.DataText[i];
                    newDataTypeIndex[j] = 1;
                }

                j = 0;
            }

            newData.CodeBar = data.CodeBar;
        }
    }
}
