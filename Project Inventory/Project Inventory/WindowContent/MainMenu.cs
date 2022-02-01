using Project_Inventory.BDD;
using Project_Inventory.Tools;
using Project_Inventory.Tools.FonctionalityCerters;
using Project_Inventory.Tools.NamesLibraries;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        Action closeEvent;

        public MainMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomListId, RoutedEventHandler _reloadEvent, Action _closeEvent)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomListId)
        {
            bottomGridButtons = new string[] { "Bibliothèque de stockages", "Bibliothèque de listes custom", "Logs", "Menu utilisateur", "Signaler un bug", "Menu des bugs", "Menu de modification de la Base De Données" };

            reloadEvent = _reloadEvent;
            closeEvent = _closeEvent;

            switchEvents = new RoutedEventLibrary[7];
            RoutedEventLibrariesInit(switchEvents);
            switchEvents[0].changePageEvent = GetEventHandler(WindowsName.StorageSelectionMenu);
            switchEvents[1].changePageEvent = GetEventHandler(WindowsName.ListMenu);
            switchEvents[2].changePageEvent = GetEventHandler(WindowsName.LogsMenu);
            switchEvents[3].changePageEvent = GetEventHandler(WindowsName.UserMenu);
            switchEvents[4].changePageEvent = GetEventHandler(WindowsName.BugReportPage);
            switchEvents[5].changePageEvent = GetEventHandler(WindowsName.BugReportedView);
            switchEvents[6].changePageEvent = GetEventHandler(WindowsName.DatabaseModifMenu);

            widthLimit = 5;

            if (IsProgramUpToDate())
            {
                IsUserConnected();
                LoadBDDInfos();
            }
            else
            {
                closeEvent.Invoke();
            }
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

        public bool IsProgramUpToDate()
        {
            string lvn = ConfigurationManager.AppSettings["version"];
            SoftwareVersion svn = JsonCenter.GetVersion(requestCenter);

            if (string.Compare(lvn, svn.version) < 0)
            {
                PopUpCenter.MessagePopup("Votre version du logiciel n'est plus à jour.\nVeuillez contacter votre technicien pour mise à jour.\n\nVersion actuelle : " + lvn + "\nVersion nécéssaire : " + svn.version);
                return false;
            }
            if (string.Compare(lvn, svn.version) > 0)
            {
                requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(1, "Mise à jour Système vers la version (" + lvn + ")").ToJson());
                requestCenter.PostRequest(BDDTabsName.VersionLibraries.ToString(), new SoftwareVersion(lvn).ToJson());
            }
            return true;
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

                    toolBox.SetUpGrid(bottomGrid, 4, 1, SkinLocation.StretchStretch, SkinSize.HeightNintyPercent);

                    ComboBox comboBox = new ComboBox();
                    comboBox.Items.Add("Selectionnez un utilisateur");
                    foreach(User user in userList)
                    {
                        comboBox.Items.Add(user.Name);
                    }
                    comboBox.SelectedItem = comboBox.Items[0];
                    toolBox.InsertUIElementInGrid(bottomGrid, comboBox, 0, 0, UIElementsName.ComboBox, SkinLocation.CenterCenter);

                    Button button = new Button();
                    button.Content = "Identifiez-vous";
                    button.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                    {
                        IDSetup(sender, e, comboBox);
                    });
                    button.IsDefault = true;

                    toolBox.InsertUIElementInGrid(bottomGrid, button, 1, 0, UIElementsName.Button, SkinLocation.CenterCenter);

                    TextBox textBox = new TextBox();
                    toolBox.InsertUIElementInGrid(bottomGrid, textBox, 2, 0, UIElementsName.TextBox, SkinLocation.CenterCenter);
                    KeyPressedEventCenter.KeyPressedEventInjection(new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                    {
                        NewUserCreation(sender, e, textBox.Text);
                    }), KeyPressedName.EnterKey, textBox);
                    textBox.Focus();

                    Button button2 = new Button();
                    button2.Content = "Création utilisateur";
                    button2.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                    {
                        NewUserCreation(sender, e, textBox.Text);
                    });
                    toolBox.InsertUIElementInGrid(bottomGrid, button2, 3, 0, UIElementsName.Button, SkinLocation.CenterCenter);

                    break;
            }
        }

        private void NewUserCreation(object sender, RoutedEventArgs e, string textBoxText)
        {
            if (textBoxText.Replace(" ", "") != null)
            {
                requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(1, "Un nouvel utilisateur a été créé.").ToJson());
                requestCenter.PostRequest(BDDTabsName.UserLibraries.ToString(), new User(textBoxText, 0, true).ToJson());

                PopUpCenter.MessagePopup("Utilisateur (" + textBoxText + ") correctement créé !");
            }
            else
            {
                PopUpCenter.MessagePopup("Veuillez entrer un nom d'utilisateur avant d'en créer un.");
            }

            reloadEvent.Invoke(sender, e);
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
