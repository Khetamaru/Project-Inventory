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
        private ListBoxNames listBoxNames;

        private int bottomColumnNb;
        private string[] bottomGridButtons;
        private RoutedEventLibrary[] formValidButton;

        private RoutedEventHandler reloadEvent;

        public FormPage(ToolBox toolBox, Router _router, RequestCenter requestCenter, WindowsName _formType, int _actualStorageId, int _actualDataId, RoutedEventHandler _reloadEvent)
            : base(toolBox, _router, requestCenter, _actualStorageId, _actualDataId)
        {
            capGrid = new Grid();

            formType = _formType;

            reloadEvent = _reloadEvent;

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
                                         formElements, labels, listBoxNames);
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            toolBox.SetUpGrid(bottomGrid, 1, bottomColumnNb, SkinsName.BottomStretch, SkinsName.HeightTenPercent);

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

                        InitStorage(uiElements);
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

        public void InitStorage()
        {
            return false; //A remplir
        }

        public void InitStorageReload(object sender, RoutedEventArgs e)
        {
            //Get UIElements Status

            //Extend By 1 Form Length

            reloadEvent.Invoke(sender, e);

            //Inject UiElements Status

            return false; //A remplir
        }

        public void formConfiguration()
        {
            switch(formType)
            {
                case (WindowsName.AddStorage):

                    listBoxNames = ListBoxNames.None;

                    topGridButtons = new string[] { "Return" };

                    topSwitchEvents = new RoutedEventLibrary[1];
                    RoutedEventLibrariesInit(topSwitchEvents);
                    topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.MainMenu);

                    bottomGridButtons = new string[] { "Valid" };
                    bottomColumnNb = 1;

                    formValidButton = new RoutedEventLibrary[1];
                    RoutedEventLibrariesInit(formValidButton);
                    formValidButton[0].changePageEvent = GetEventHandler(WindowsName.StorageSelectionMenu);

                    formElements = new UIElementsName[] { UIElementsName.TextBox };
                    labels = new string[] { "Storage's Name" };

                    break;

                case (WindowsName.InitStorage):

                    listBoxNames = ListBoxNames.UIElementsType;

                    topGridButtons = new string[] { "Return" };

                    topSwitchEvents = new RoutedEventLibrary[1];
                    RoutedEventLibrariesInit(topSwitchEvents);
                    topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.StorageSelectionMenu);

                    bottomGridButtons = new string[] { "Add Column", "Valid" };
                    bottomColumnNb = 2;

                    formValidButton = new RoutedEventLibrary[2];
                    RoutedEventLibrariesInit(formValidButton);
                    formValidButton[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) => InitStorageReload(sender, e));
                    formValidButton[1].changePageEvent = GetEventHandler(WindowsName.StorageViewerPage);

                    formElements = new UIElementsName[] { UIElementsName.ListBox, UIElementsName.TextBox };
                    labels = new string[] { "Information Type", "Information Title" };

                    break;
            }

            formValidButton[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                formValidation(sender, e);
            });
        }
    }
}
