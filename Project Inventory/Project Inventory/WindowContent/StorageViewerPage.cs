using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    class StorageViewerPage : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventHandler[] topSwitchEvents;

        private Grid capGrid;
        private Data[] dataTab;
        private string[,] stringTab;
        private string[,] indicTab;

        public StorageViewerPage(ToolBox ToolBox, Router _router, RequestCenter requestCenter, int _actualStorageId, int _actualDataId)
            : base(ToolBox, _router, requestCenter, _actualStorageId, _actualDataId)
        {
            topGridButtons = new string[] { "Return" };

            topSwitchEvents = new RoutedEventHandler[] { GetEventHandler("MainMenu") };

            capGrid = new Grid();

            LoadBDDInfos();
        }

        public void LoadBDDInfos()
        {
            dataTab = JsonCenter.LoadStorageViewerInfos(requestCenter, actualStorageId);

            int i;
            int j;

            stringTab = new string[dataTab.Length, dataTab[0].DataText.Length];
            indicTab = new string[dataTab.Length, dataTab[0].DataText.Length];

            for (i = 0; i < dataTab.Length; i++)
            {
                for (j = 0; j < dataTab[0].DataText.Length; j++)
                {
                    stringTab[i, j] = dataTab[i].DataText[j];
                    indicTab[i, j] = dataTab[i].DataType[j];
                }
            }
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightTenPercent");

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents, "StandartLittleMargin", "TopRight");
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            toolBox.CreateScrollableGrid(bottomGrid, capGrid, 
                                         1, 1, 
                                         stringTab.GetLength(0), stringTab.GetLength(1), 
                                         "BottomStretch", "HeightNintyPercent", 
                                         "standart", "center", 
                                         stringTab, indicTab);
        }
    }
}
