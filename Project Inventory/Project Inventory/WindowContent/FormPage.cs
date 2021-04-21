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

        private UIElement[] formElements;
        private Type[] formElementsType;
        private RoutedEventHandler formValidButton;

        public FormPage(VisualElements_ToolBox visualElements_ToolBox, Router _router)
            : base(visualElements_ToolBox, _router)
        {
            topGridButtons = new string[] { "Return" };

            topSwitchEvents = new RoutedEventHandler[] { GetEventHandler("MainMenu") };
        }

        public new Grid TopGridInit(Grid topGrid)
        {
            topGrid = toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightTenPercent");

            topGrid = toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents, "StandartLittleMargin", "TopRight");

            return topGrid;
        }

        public new Grid BottomGridInit(Grid bottomGrid)
        {
            bottomGrid = toolBox.SetUpGrid(bottomGrid, formElements.Length, 1, "BottomStretch", "HeightNintyPercent");

            bottomGrid = toolBox.CreateUIElementsToGridByTab(bottomGrid, formElements, formElementsType);

            return bottomGrid;
        }
    }
}
