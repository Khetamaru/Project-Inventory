using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    class FormPage : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private Grid capGrid;
        private string[] formElements;
        private string[] labels;
        private string formType;

        private string[] bottomGridButtons;
        private RoutedEventLibrary[] formValidButton;

        public FormPage(ToolBox toolBox, Router _router, RequestCenter requestCenter, string _formType, int _actualStorageId, int _actualDataId)
            : base(toolBox, _router, requestCenter, _actualStorageId, _actualDataId)
        {
            topGridButtons = new string[] { "Return" };

            capGrid = new Grid();

            bottomGridButtons = new string[] { "Valid" };

            formType = _formType;

            formConfiguration();
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

        public void formConfiguration()
        {
            switch(formType)
            {
                case ("Add Storage"):

                    topSwitchEvents = new RoutedEventLibrary[1];
                    RoutedEventLibrariesInit(topSwitchEvents);
                    topSwitchEvents[0].changePageEvent = GetEventHandler("MainMenu");

                    formValidButton = new RoutedEventLibrary[1];
                    RoutedEventLibrariesInit(formValidButton);
                    formValidButton[0].changePageEvent = GetEventHandler("StorageSelectionMenu");

                    formElements = new string[] { "TextBox" };
                    labels = new string[] { "Storage's Name" };

                    break;
            }

            formValidButton[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                formValidation(sender, e);
            });
        }
    }
}
