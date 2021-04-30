using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public class StorageSelectionMenu : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventHandler[] topSwitchEvents;
        private string[] bottomGridButtons;
        private RoutedEventHandler[] bottomSwitchEvents;

        private int widthLimit;

        public StorageSelectionMenu(ToolBox toolBox, Router _router)
            : base(toolBox, _router)
        {
            topGridButtons = new string[] { "Return" };

            topSwitchEvents = new RoutedEventHandler[] { GetEventHandler("MainMenu") };

            bottomGridButtons = new string[] { "Réserve N°1", "Réserve N°2", "Réserve N°3", "Réserve N°4", "Réserve N°5",
                                                     "Réserve N°6", "Réserve N°7", "Réserve N°8", "Réserve N°9", "Réserve N°10",
                                                     "Réserve N°11", "Réserve N°12", "Réserve N°13", "Réserve N°14", "Réserve N°15",
                                                     "Réserve N°16", "Réserve N°17", "Réserve N°18", "Réserve N°19", "Réserve N°20",
                                                     "Réserve N°21", "Réserve N°22", "Réserve N°23", "Réserve N°24"};

            bottomSwitchEvents = new RoutedEventHandler[] { GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"),
                                                                           GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"),
                                                                           GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"),
                                                                           GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"),
                                                                           GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu"), GetEventHandler("MainMenu")};

            widthLimit = 5;
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightTenPercent");

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents, "StandartLittleMargin", "TopRight");
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            ButtonPlacer(bottomGrid, bottomGridButtons.Length, widthLimit, "BottomStretch", "HeightNintyPercent");

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, bottomSwitchEvents, "standart", "CenterCenter");
        }
    }
}
