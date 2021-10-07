using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    /// <summary>
    /// Page to access all menus
    /// </summary>
    public class MainMenu : WindowContent
    {
        private string[] bottomGridButtons;
        private RoutedEventLibrary[] switchEvents;

        private int widthLimit;

        private enum IsThereUser
        {
            YES,
            NO
        }

        private IsThereUser state;

        private List<User> userList;

        private RoutedEventHandler reloadEvent;

        public MainMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomListId, RoutedEventHandler _reloadEvent)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomListId)
        {
            bottomGridButtons = new string[] { "Storage Selection", "Custom Lists", "Logs", "User Menu", "Bug Report", "Bug Menu" };

            reloadEvent = _reloadEvent;

            switchEvents = new RoutedEventLibrary[6];
            RoutedEventLibrariesInit(switchEvents);
            switchEvents[0].changePageEvent = GetEventHandler(WindowsName.StorageSelectionMenu);
            switchEvents[1].changePageEvent = GetEventHandler(WindowsName.ListMenu);
            switchEvents[2].changePageEvent = GetEventHandler(WindowsName.LogsMenu);
            switchEvents[3].changePageEvent = GetEventHandler(WindowsName.UserMenu);
            switchEvents[4].changePageEvent = GetEventHandler(WindowsName.BugReportPage);
            switchEvents[5].changePageEvent = GetEventHandler(WindowsName.BugReportedView);

            widthLimit = 5;

            IsUserConnected();

            LoadBDDInfos();
        }

        public void IsUserConnected()
        {
            if (actualUserId >= 0)
            {
                state = IsThereUser.YES;
            }
            else
            {
                state = IsThereUser.NO;
            }
        }

        public void LoadBDDInfos()
        {
            if (state == IsThereUser.NO)
            {
                userList = JsonCenter.LoadMainMenuInfos(requestCenter);
            }
        }

        public new void TopGridInit(Grid topGrid)
        {
            switch (state)
            {
                case IsThereUser.YES:

                    toolBox.SetUpGrid(topGrid, 1, 1, SkinLocation.TopStretch, SkinSize.HeightOneTier);

                    Button button = toolBox.CreateSwitchButtonImage(ImagesName.logo, new RoutedEventLibrary(), SkinName.Standart, SkinLocation.CenterCenter, ImageSizesName.Logo);
                    button.Click += GetEventHandler(WindowsName.CreditPage);
                    Grid.SetRow(button, 0);
                    Grid.SetColumn(button, 0);
                    topGrid.Children.Add(button);

                    break;

                case IsThereUser.NO:

                    toolBox.EmptyGrid(topGrid);

                    break;
            }
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            switch (state)
            {
                case IsThereUser.YES:

                    toolBox.ButtonPlacer(bottomGrid, bottomGridButtons.Length, widthLimit, SkinLocation.BottomStretch, SkinSize.HeightTwoTier);

                    toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, switchEvents, SkinName.Standart, SkinLocation.CenterCenter);

                    break;

                case IsThereUser.NO:

                    toolBox.SetUpGrid(bottomGrid, 2, 1, SkinLocation.StretchStretch, SkinSize.HeightNintyPercent);

                    ComboBox comboBox = new ComboBox();
                    comboBox.Items.Add("Select A User");
                    foreach(User user in userList)
                    {
                        comboBox.Items.Add(user.Name);
                    }
                    comboBox.SelectedItem = comboBox.Items[0];
                    toolBox.InsertUIElementInGrid(bottomGrid, comboBox, 0, 0, UIElementsName.ComboBox, SkinLocation.CenterCenter);

                    Button button = new Button();
                    button.Content = "Sign In";
                    button.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                    {
                        IDSetup(sender, e, comboBox);
                    });
                    button.IsDefault = true;

                    toolBox.InsertUIElementInGrid(bottomGrid, button, 1, 0, UIElementsName.Button, SkinLocation.CenterCenter);

                    break;
            }
        }

        public void IDSetup(object sender, RoutedEventArgs e, ComboBox comboBox)
        {
            foreach(User user in userList)
            {
                if (user.Name == comboBox.SelectedItem.ToString())
                {
                    actualUserId = user.id;
                }
            }

            if (actualUserId >= 0)
            {
                reloadEvent.Invoke(sender, e);
            }
        }
    }
}
