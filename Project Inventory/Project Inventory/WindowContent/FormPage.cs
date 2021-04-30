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

        private string[] formElements;
        private string[] labels;

        private string[] bottomGridButtons;
        private RoutedEventHandler[] formValidButton;

        public FormPage(ToolBox toolBox, Router _router)
            : base(toolBox, _router)
        {
            topGridButtons = new string[] { "Return" };

            topSwitchEvents = new RoutedEventHandler[] { GetEventHandler("MainMenu") };

            formElements = new string[] { "TextBox", "TextBox", "TextBoxNumber", "DatePicker", "ListBox" };
            labels = new string[] { "Nom", "Prénom", "Numéro De Table", "Date De Naissance", "Choix Du Repas" };

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
            toolBox.SetUpGrid(centerGrid, formElements.Length, 2, "StretchStretch", "HeightEightPercent");

            toolBox.CreateFormToGridByTab(centerGrid, formElements, labels);
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            toolBox.SetUpGrid(bottomGrid, 1, 1, "BottomStretch", "HeightTenPercent");

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, formValidButton, "StandartLittleMargin", "CenterCenter");
        }
    }
}
