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
        private string[,] stringTab;
        private string[,] indicTab;

        public StorageViewerPage(ToolBox ToolBox, Router _router)
            : base(ToolBox, _router)
        {
            topGridButtons = new string[] { "Return" };

            topSwitchEvents = new RoutedEventHandler[] { GetEventHandler("MainMenu") };

            capGrid = new Grid();

            stringTab = new string[,] { { "Nom de la console", "Date de sortie", "nombre de manettes", "connectique", "possédé" },
                                        { "PSP", "03/12/1997", "1", "USB", "true" },
                                        { "PS2", "05/03/2002", "4", "USB", "false" },
                                        { "PS3", "25/07/2015", "4", "USB", "true" },
                                        { "PS15", "18/02/2048", "8", "ZURGLUK", "true" } };
            indicTab = new string[,] {  { "title", "title", "title", "title", "title" },
                                        { "string", "date", "int", "string", "boolean" },
                                        { "string", "date", "int", "string", "boolean" },
                                        { "string", "date", "int", "string", "boolean" },
                                        { "string", "date", "int", "string", "boolean" } };
            /*stringTab = new string[,] { { "Nom de la console", "Date de sortie", "nombre de manettes", "connectique", "possédé", "possédé", "possédé", "possédé", "possédé", "possédé" },
                                        { "PSP", "03/12/1997", "1", "USB", "true", "possédé", "possédé", "possédé", "possédé", "possédé" },
                                        { "PS2", "05/03/2002", "4", "USB", "false", "possédé", "possédé", "possédé", "possédé", "possédé" },
                                        { "PS3", "25/07/2015", "4", "USB", "true", "possédé", "possédé", "possédé", "possédé", "possédé" },
                                        { "PS15", "18/02/2048", "8", "ZURGLUK", "true", "possédé", "possédé", "possédé", "possédé", "possédé" },
                                        { "PS15", "18/02/2048", "8", "ZURGLUK", "true", "possédé", "possédé", "possédé", "possédé", "possédé" },
                                        { "PS15", "18/02/2048", "8", "ZURGLUK", "true", "possédé", "possédé", "possédé", "possédé", "possédé" },
                                        { "PS15", "18/02/2048", "8", "ZURGLUK", "true", "possédé", "possédé", "possédé", "possédé", "possédé" } };
            indicTab = new string[,] {  { "title", "title", "title", "title", "title", "title", "title", "title", "title", "title" },
                                        { "string", "date", "int", "string", "boolean", "title", "title", "title", "title", "title" },
                                        { "string", "date", "int", "string", "boolean", "title", "title", "title", "title", "title" },
                                        { "string", "date", "int", "string", "boolean", "title", "title", "title", "title", "title" },
                                        { "string", "date", "int", "string", "boolean", "title", "title", "title", "title", "title" },
                                        { "string", "date", "int", "string", "boolean", "title", "title", "title", "title", "title" },
                                        { "string", "date", "int", "string", "boolean", "title", "title", "title", "title", "title" },
                                        { "string", "date", "int", "string", "boolean", "title", "title", "title", "title", "title" } };*/
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
