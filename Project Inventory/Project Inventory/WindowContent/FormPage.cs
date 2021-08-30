using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    /// <summary>
    /// Page use to proceduraly generated forms
    /// </summary>
    class FormPage : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private Grid capGrid;
        private UIElementsName[] formElements;
        private string[] labels;
        private WindowsName formType;
        private ComboBoxNames listBoxNames;

        private int bottomColumnNb;
        private string[] bottomGridButtons;
        private RoutedEventLibrary[] formValidButton;

        private RoutedEventHandler reloadEvent;

        private CustomList[] customList;

        public FormPage(ToolBox toolBox, Router _router, RequestCenter requestCenter, WindowsName _formType, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomListId, RoutedEventHandler _reloadEvent)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomListId)
        {
            capGrid = new Grid();

            formType = _formType;

            reloadEvent = _reloadEvent;

            formConfiguration();
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 1, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents, SkinName.StandartLittleMargin, SkinLocation.TopRight);
        }

        public new void CenterGridInit(Grid centerGrid)
        {
            switch (formType)
            {
                default:

                    toolBox.CreateScrollableForm(centerGrid, capGrid,
                                                 1, 1,
                                                 formElements.Length, 2,
                                                 SkinLocation.StretchStretch, SkinSize.HeightEightPercent,
                                                 formElements, labels, listBoxNames,
                                                 customList);
                    break;

                case WindowsName.CreditPage:

                    toolBox.CreateScrollableForm(centerGrid, capGrid,
                                                 1, 1,
                                                 formElements.Length, 2,
                                                 SkinLocation.BottomStretch, SkinSize.HeightNintyPercent,
                                                 formElements, labels, listBoxNames,
                                                 customList);
                    break;
            }
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            switch(formType)
            {
                default:

                    toolBox.SetUpGrid(bottomGrid, 1, bottomColumnNb, SkinLocation.BottomStretch, SkinSize.HeightTenPercent);

                    toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, formValidButton, SkinName.StandartLittleMargin, SkinLocation.CenterCenter);
                    break;
                case WindowsName.CreditPage:
                    toolBox.EmptyGrid(bottomGrid);
                    break;
            }
        }

        /// <summary>
        /// Verify all fields are correctly fill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void formValidation(object sender, RoutedEventArgs e)
        {
            string[] uiElements = new string[capGrid.Children.Count / 2];

            toolBox.GetUiElementResult(capGrid, uiElements, formElements);

            if (toolBox.FormResultValidation(uiElements, formElements))
            {
                switch(formType)
                {
                    case WindowsName.InitStorage:

                        InitStorage();
                        break;
                }
            }
            else
            {
                PopUpCenter.MessagePopup("Some Fields are empty or wrongly filled.");
            }
        }

        /// <summary>
        /// Create the originale data line that define all the storage structure
        /// </summary>
        /// <param name="uIElements"></param>
        public void InitStorage()
        {
            UIElementsName[] uIElementsNames = new UIElementsName[formElements.Length / 2];
            List<string> columnNames = new List<string>();
            List<string> dataType = new List<string>();
            List<CustomList> customLists;

            int j = 0;

            GetInitStorageUIElement(uIElementsNames, columnNames, customList, out customLists);

            for (int i = 0 ; i < uIElementsNames.Length ; i++)
            {
                if (uIElementsNames[i] == UIElementsName.None)
                {
                    dataType.Add(customLists[j].id.ToString());
                    j++;
                }
                else
                {
                    dataType.Add(uIElementsNames[i].ToString());
                }
            }

            Data data = new Data(actualStorageId, columnNames, dataType, true);

            string json = data.ToJson();

            requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "(" + JsonCenter.GetStorage(requestCenter, actualStorageId).Name + ") Storage has been initialised.").ToJson());
            requestCenter.PostRequest(BDDTabsName.DataLibraries.ToString(), json);

            StorageXCustomList temp;

            foreach (CustomList customList in customLists)
            {
                temp = new StorageXCustomList(actualStorageId, customList.id);

                requestCenter.PostRequest(BDDTabsName.StorageLibrariesXCustomListLibraries.ToString(), temp.ToJson());
            }
        }

        /// <summary>
        /// Add a column to storage structure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void InitStorageReload(object sender, RoutedEventArgs e)
        {
            string[] uIElementsNames = new string[formElements.Length / 2];
            List<string> columnNames = new List<string>();

            GetInitStorageUIElement(uIElementsNames, columnNames);

            ExtendLengthInitStorage();

            capGrid = new Grid();
            reloadEvent.Invoke(sender, e);

            InjectInitStorageUIElement(uIElementsNames, columnNames);
        }
        
        /// <summary>
         /// Give all elements filled in the form
         /// </summary>
         /// <param name="uIElementsNames"></param>
         /// <param name="columnNames"></param>
        private void GetInitStorageUIElement(string[] uIElementsNames, List<string> columnNames)
        {
            int i = 0;
            int k = 0;
            string temp;

            foreach (UIElement uIElement in capGrid.Children)
            {
                if (i % 2 != 0)
                {
                    if ((i + 1) % 4 == 0)
                    {
                        columnNames.Add((uIElement as TextBox).Text);
                    }
                    else
                    {
                        if ((uIElement as ComboBox).SelectedItem != null)
                        {
                            temp = (uIElement as ComboBox).SelectedItem.ToString();
                        }
                        else
                        {
                            temp = UIElementsName.TextBox.ToString();
                        }
                        uIElementsNames[k] = temp;

                        k++;
                    }
                }

                i++;
            }
        }

        /// <summary>
        /// Give all elements filled in the form
        /// </summary>
        /// <param name="uIElementsNames"></param>
        /// <param name="columnNames"></param>
        private void GetInitStorageUIElement(UIElementsName[] uIElementsNames, List<string> columnNames, CustomList[] customList, out List<CustomList> customLists)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            customLists = new List<CustomList>();
            string temp;

            foreach(UIElement uIElement in capGrid.Children)
            {
                if (i%2 != 0)
                {
                    if ((i+1)%4 == 0)
                    {
                        columnNames.Add((uIElement as TextBox).Text);
                    }
                    else
                    {
                        if ((uIElement as ComboBox).SelectedItem != null)
                        {
                            temp = (uIElement as ComboBox).SelectedItem.ToString();
                        }
                        else
                        {
                            temp = UIElementsName.TextBox.ToString();
                        }
                        uIElementsNames[k] = GetUIElementName(temp);

                        if (uIElementsNames[k] == UIElementsName.None)
                        {
                            for (j = 0; j < customList.Length; j++)
                            {
                                if (customList[j].Name == temp)
                                {
                                    customLists.Add(customList[j]);
                                }
                            }
                        }

                        k++;
                    }
                }

                i++;
            }
        }

        /// <summary>
        /// Inject infos in fields already filled
        /// </summary>
        /// <param name="uIElementsNames"></param>
        /// <param name="columnNames"></param>
        private void InjectInitStorageUIElement(string[] uIElementsNames, List<string> columnNames)
        {
            int j = 0;
            int k = 0;
            string temp = string.Empty;

            for (int i = 0 ; i < (capGrid.Children.Count - 4) ; i++)
            {
                if (i % 2 != 0)
                {
                    if ((i + 1) % 4 == 0)
                    {
                        (capGrid.Children[i] as TextBox).Text = columnNames[j];
                        j++;
                    }
                    else
                    {
                        (capGrid.Children[i] as ComboBox).SelectedItem = uIElementsNames[k];
                        k++;
                    }
                }
            }
        }

        private UIElementsName GetUIElementName(string name)
        {
            switch (name)
            {
                case "TextBox":
                    return UIElementsName.TextBox;
                case "TextBoxNumber":
                    return UIElementsName.TextBoxNumber;
                case "DatePicker":
                    return UIElementsName.DatePicker;
                case "ComboBox":
                    return UIElementsName.ComboBox;
            }
            return UIElementsName.None;
        }

        /// <summary>
        /// Extend of two the number of fields in the form
        /// </summary>
        private void ExtendLengthInitStorage()
        {
            UIElementsName[] formElementsTemp = new UIElementsName[formElements.Length + 2];
            string[] labelsTemp = new string[labels.Length + 2];

            int i = 0;

            foreach (UIElementsName element in formElements)
            {
                formElementsTemp[i] = element;
                labelsTemp[i] = labels[i];
                i++;
            }

            formElementsTemp[formElements.Length] = UIElementsName.ComboBox;
            formElementsTemp[formElements.Length + 1] = UIElementsName.TextBox;

            labelsTemp[labels.Length] = "Information Type";
            labelsTemp[labels.Length + 1] = "Information Title";

            formElements = formElementsTemp;
            labels = labelsTemp;
        }

        /// <summary>
        /// Use form type name to generate the good form
        /// </summary>
        public void formConfiguration()
        {
            switch(formType)
            {
                case (WindowsName.InitStorage):

                    listBoxNames = ComboBoxNames.UIElementsType;

                    customList = JsonCenter.LoadInitStorageInfos(requestCenter);

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

                    formElements = new UIElementsName[] { UIElementsName.ComboBox, UIElementsName.TextBox };
                    labels = new string[] { "Information Type", "Information Title" };

                    formValidButton[1].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                    {
                        formValidation(sender, e);
                    });

                    break;

                case (WindowsName.CreditPage):

                    topGridButtons = new string[] { "Return" };

                    topSwitchEvents = new RoutedEventLibrary[1];
                    RoutedEventLibrariesInit(topSwitchEvents);
                    topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.MainMenu);

                    formValidButton = new RoutedEventLibrary[2];
                    RoutedEventLibrariesInit(formValidButton);
                    formValidButton[0].optionalEventOne = new RoutedEventHandler((object sender, RoutedEventArgs e) => InitStorageReload(sender, e));
                    formValidButton[1].changePageEvent = GetEventHandler(WindowsName.StorageViewerPage);

                    formElements = new UIElementsName[] { UIElementsName.None, UIElementsName.None, UIElementsName.None };
                    labels = new string[] { "Enterprise : Docteur Ordianteur Laval", "Project Manager : ETAIX Vincent", "Code Author : LASSERRE Anthony" };

                    break;
            }
        }
    }
}
