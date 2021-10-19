using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    /// <summary>
    /// Page to access all menus
    /// </summary>
    public class DatabaseModifMenu : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private int widthLimit;

        private List<string> requestTypes;
        private ComboBox requestComboBox;
        private TextBox requestTextBox;

        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        public DatabaseModifMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomListId)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomListId)
        {
            topGridButtons = new string[] { "Return" };
            saveButton = new string[] { "Validation" };

            topSwitchEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(topSwitchEvents);

            saveEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(saveEvents);
            saveEvents[0].optionalEventTwo = new RoutedEventHandler((object sender, RoutedEventArgs e) => { LaunchRequest(sender, e); });

            topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.MainMenu);

            requestFieldSetUp();

            widthLimit = 5;
        }

        private void requestFieldSetUp()
        {
            requestTypes = new List<string>();

            requestTypes.Add("Select an item");
            requestTypes.Add("Update New Column");

            requestComboBox = new ComboBox();
            requestComboBox.SelectionChanged += new SelectionChangedEventHandler((object sender, SelectionChangedEventArgs e) =>
            {
                LaunchRequestPatern(sender, e, requestComboBox.SelectedItem.ToString());
            });

            foreach(string str in requestTypes)
            {
                requestComboBox.Items.Add(str);
            }

            requestTextBox = new TextBox();

            requestComboBox.SelectedItem = requestComboBox.Items[0];

        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 1, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents,
                                                   new SkinName[] { SkinName.StandartLittleMargin },
                                                   new SkinLocation[] { SkinLocation.TopRight });
        }

        public new void CenterGridInit(Grid centerGrid)
        {
            toolBox.SetUpGrid(centerGrid, 1, 2, SkinLocation.CenterStretch, SkinSize.HeightEightPercent);

            toolBox.InsertUIElementInGrid(centerGrid, requestComboBox, 0, 0, UIElementsName.ComboBox, SkinLocation.CenterCenter);
            toolBox.InsertUIElementInGrid(centerGrid, requestTextBox, 0, 2, UIElementsName.TextBox, SkinLocation.CenterCenter);
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            toolBox.SetUpGrid(bottomGrid, 1, 1, SkinLocation.BottomStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, saveButton, saveEvents, SkinName.StandartLittleMargin, SkinLocation.BottomCenter);
        }

        private void LaunchRequest(object sender, RoutedEventArgs e)
        {
            requestCenter.OptionRequest(BDDTabsName.Save + "/Update/" + requestTextBox.Text);

            PopUpCenter.MessagePopup("Request goodly executed and saved.");
        }

        private void LaunchRequestPatern(object sender, SelectionChangedEventArgs e, string selectedOption)
        {
            switch(selectedOption)
            {
                case "Select an item":

                    requestTextBox.Text = string.Empty;
                    break;

                case "Update New Column":

                    requestTextBox.Text = "ALTER TABLE table_name " +
                                          "ADD column_name column_type;";
                    break;
            }
        }
    }
}
