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
        private UIElementsName[] formElements;
        private string[] labels;
        private WindowsName formType;

        private string[] bottomGridButtons;
        private RoutedEventLibrary[] formValidButton;

        public FormPage(ToolBox toolBox, Router _router, RequestCenter requestCenter, WindowsName _formType, int _actualStorageId, int _actualDataId)
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
            toolBox.SetUpGrid(topGrid, 1, 1, SkinsName.TopStretch, SkinsName.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents, SkinsName.StandartLittleMargin, SkinsName.TopRight);
        }

        public new void CenterGridInit(Grid centerGrid)
        {
            toolBox.CreateScrollableForm(centerGrid, capGrid,
                                         1, 1,
                                         formElements.Length, 2,
                                         SkinsName.StretchStretch, SkinsName.HeightEightPercent,
                                         formElements, labels);
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            toolBox.SetUpGrid(bottomGrid, 1, 1, SkinsName.BottomStretch, SkinsName.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, formValidButton, SkinsName.StandartLittleMargin, SkinsName.CenterCenter);
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
                    case WindowsName.AddStorage:

                        AddStorage(new Storage(uiElements[0]));
                        break;

                    case WindowsName.InitStorage:

                        InitStorage();
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

        public void InitStorage(RoutedEventHandler reloadEvent)
        {
            return false; //A remplir
        }

        public void formConfiguration()
        {
            switch(formType)
            {
                case (WindowsName.AddStorage):

                    topSwitchEvents = new RoutedEventLibrary[1];
                    RoutedEventLibrariesInit(topSwitchEvents);
                    topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.MainMenu);

                    formValidButton = new RoutedEventLibrary[1];
                    RoutedEventLibrariesInit(formValidButton);
                    formValidButton[0].changePageEvent = GetEventHandler(WindowsName.StorageSelectionMenu);

                    formElements = new UIElementsName[] { UIElementsName.TextBox };
                    labels = new string[] { "Storage's Name" };

                    break;

                case (WindowsName.InitStorage):

                    topSwitchEvents = new RoutedEventLibrary[1];
                    RoutedEventLibrariesInit(topSwitchEvents);
                    topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.StorageSelectionMenu);

                    formValidButton = new RoutedEventLibrary[1];
                    RoutedEventLibrariesInit(formValidButton);
                    formValidButton[0].changePageEvent = GetEventHandler(WindowsName.StorageViewerPage);

                    formElements = new UIElementsName[] { UIElementsName.TextBoxNumber };
                    labels = new string[] { "Number Of Columns" };

                    break;
            }

            formValidButton[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                formValidation(sender, e);
            });
        }
    }
}
