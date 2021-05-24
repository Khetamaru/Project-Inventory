using Project_Inventory.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    class FormPage : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventHandler[] topSwitchEvents;

        private Grid capGrid;
        private string[] formElements;
        private string[] labels;

        private string[] bottomGridButtons;
        private RoutedEventHandler[] formValidButton;

        public FormPage(ToolBox toolBox, Router _router, RequestCenter requestCenter)
            : base(toolBox, _router, requestCenter)
        {
            topGridButtons = new string[] { "Return" };

            topSwitchEvents = new RoutedEventHandler[] { GetEventHandler("MainMenu") };

            capGrid = new Grid();
            formElements = new string[] { "TextBox", "TextBox", "TextBoxNumber", "DatePicker", "ListBox" };
            labels = new string[] { "Nom", "Prénom", "Numéro De Table", "Date De Naissance", "Choix Du Repas" };
            /*formElements = new string[] { "TextBox", "TextBox", "TextBoxNumber", "DatePicker", "ListBox", "DatePicker" };
            labels = new string[] { "Nom", "Prénom", "Numéro De Table", "Date De Naissance", "Choix Du Repas", "Date De Naissance" };*/

            bottomGridButtons = new string[] { "Valid" };

            formValidButton = new RoutedEventHandler[] { GetEventHandler("MainMenu") };
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightTenPercent");

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents, "StandartLittleMargin", "TopRight");
        }

        public new void CenterGridInit(Grid centerGrid)
        {
            toolBox.CreateScrollableForm(centerGrid, capGrid,
                                         1, 1,
                                         formElements.Length, 2,
                                         "StretchStretch", "HeightEightPercent",
                                         formElements, labels);
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            toolBox.SetUpGrid(bottomGrid, 1, 1, "BottomStretch", "HeightTenPercent");

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, formValidButton, "StandartLittleMargin", "CenterCenter");
        }
    }
}
