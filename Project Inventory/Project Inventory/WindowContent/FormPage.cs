﻿using Project_Inventory.BDD;
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
        private string formType;

        private string[] bottomGridButtons;
        private RoutedEventHandler[] formValidButton;

        public FormPage(ToolBox toolBox, Router _router, RequestCenter requestCenter, 
                        string[] _formElements, string[] _labels, string _formType)
            : base(toolBox, _router, requestCenter)
        {
            topGridButtons = new string[] { "Return" };

            topSwitchEvents = new RoutedEventHandler[] { GetEventHandler("MainMenu") };

            capGrid = new Grid();

            bottomGridButtons = new string[] { "Valid" };

            formValidButton = new RoutedEventHandler[] { GetEventHandler("StorageSelectionMenu") };

            formElements = _formElements;
            labels = _labels;
            formType = _formType;
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
            (bottomGrid.Children[0] as Button).Click += new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                formValidation(sender, e);
            });
        }

        public void formValidation(object sender, RoutedEventArgs e)
        {
            string[] uiElements = new string[capGrid.Children.Count / 2];
            int i = 0;
            toolBox.GetUiElementResult(capGrid, uiElements, formElements);

            if (toolBox.FormResultValidation(uiElements, formElements))
            {
                switch(formType)
                {
                    case "Add Storage":

                        AddStorage(new Storage(uiElements[0]));
                        break;
                }
            }
            else
            {
                // POP UP REFUS
            }
        }

        public void AddStorage(Storage storage)
        {
            string json = storage.ToJson();

            requestCenter.PostRequest("StorageLibraries", json);
        }
    }
}
