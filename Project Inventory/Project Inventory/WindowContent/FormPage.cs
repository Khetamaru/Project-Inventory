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

        private RoutedEventHandler formValidButton;

        public FormPage(VisualElements_ToolBox visualElements_ToolBox, Router _router)
            : base(visualElements_ToolBox, _router)
        {
            topGridButtons = new string[] { "Return" };

            topSwitchEvents = new RoutedEventHandler[] { GetEventHandler("MainMenu") };

            formElements = new string[] { "TextBox", "TextBox", "TextBoxNumber", "DatePicker", "ListBox" };
            labels = new string[] { "Nom", "Prénom", "Numéro De Table", "Date De Naissance", "Choix Du Repas" };
        }

        public new Grid TopGridInit(Grid topGrid)
        {
            topGrid = toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightTenPercent");

            topGrid = toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents, "StandartLittleMargin", "TopRight");

            return topGrid;
        }

        public new Grid BottomGridInit(Grid bottomGrid)
        {
            bottomGrid = toolBox.SetUpGrid(bottomGrid, formElements.Length, 2, "BottomStretch", "HeightNintyPercent");

            bottomGrid = toolBox.CreateFormToGridByTab(bottomGrid, formElements, labels);

            return bottomGrid;
        }
    }
}
