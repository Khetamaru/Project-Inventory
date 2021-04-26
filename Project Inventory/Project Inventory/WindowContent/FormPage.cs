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

        public FormPage(VisualElements_ToolBox visualElements_ToolBox, Router _router)
            : base(visualElements_ToolBox, _router)
        {
            topGridButtons = new string[] { "Return" };

            topSwitchEvents = new RoutedEventHandler[] { GetEventHandler("MainMenu") };

            formElements = new string[] { "TextBox", "TextBox", "TextBoxNumber", "DatePicker", "ListBox" };
            labels = new string[] { "Nom", "Prénom", "Numéro De Table", "Date De Naissance", "Choix Du Repas" };

            bottomGridButtons = new string[] { "Valid" };

            formValidButton = new RoutedEventHandler[] { GetEventHandler("MainMenu") };
        }

        public new Grid TopGridInit(Grid topGrid)
        {
            topGrid = toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightTenPercent");

            topGrid = toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents, "StandartLittleMargin", "TopRight");

            return topGrid;
        }

        public new Grid CenterGridInit(Grid centerGrid)
        {
            centerGrid = toolBox.SetUpGrid(centerGrid, formElements.Length, 2, "StretchStretch", "HeightEightPercent");

            centerGrid = toolBox.CreateFormToGridByTab(centerGrid, formElements, labels);

            return centerGrid;
        }

        public new Grid BottomGridInit(Grid bottomGrid)
        {
            bottomGrid = toolBox.SetUpGrid(bottomGrid, 1, 1, "BottomStretch", "HeightTenPercent");

            bottomGrid = toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, formValidButton, "StandartLittleMargin", "CenterCenter");

            return bottomGrid;
        }
    }
}
