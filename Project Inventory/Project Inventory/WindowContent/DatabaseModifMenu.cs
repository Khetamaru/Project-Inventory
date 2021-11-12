using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public class DatabaseModifMenu : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private List<string> requestTypes;
        private ComboBox requestComboBox;
        private TextBox requestTextBox;

        private string[] saveButton;
        private RoutedEventLibrary[] saveEvents;

        public DatabaseModifMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomListId)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomListId)
        {
            topGridButtons = new string[] { "Retour" };
            saveButton = new string[] { "Validation", "Synchronisation" };

            topSwitchEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(topSwitchEvents);

            saveEvents = new RoutedEventLibrary[2];
            RoutedEventLibrariesInit(saveEvents);
            saveEvents[0].optionalEventTwo = new RoutedEventHandler((object sender, RoutedEventArgs e) => { LaunchRequest(sender, e); });
            saveEvents[1].optionalEventTwo = new RoutedEventHandler((object sender, RoutedEventArgs e) => { BDDStructSynchro(sender, e); });

            topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.MainMenu);

            requestFieldSetUp();
        }

        private void requestFieldSetUp()
        {
            requestTypes = new List<string>();

            requestTypes.Add("Selectionnez une option");
            requestTypes.Add("Update Ajout Colonne");
            requestTypes.Add("Update Suppression Colonne");

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
            toolBox.SetUpGrid(bottomGrid, 1, 2, SkinLocation.BottomStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, saveButton, saveEvents, 
                                                   new SkinName[] { SkinName.StandartLittleMargin, SkinName.StandartLittleMargin },
                                                   new SkinLocation[] { SkinLocation.BottomCenter, SkinLocation.BottomCenter });
        }

        private void LaunchRequest(object sender, RoutedEventArgs e)
        {
            if(PopUpCenter.ActionValidPopup("Cette Action est irréversible et peut affecter sévèrement la base de données. Êtes-vous sûr ?"))
            {
                requestCenter.PutRequest(BDDTabsName.Save + "/Update", new RequestMySQL(requestTextBox.Text).ToJson());
                requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "BDD modifiée.").ToJson());
                PopUpCenter.MessagePopup("Requête correctement exécutée et sauvegardée.");
            }
        }

        private void BDDStructSynchro(object sender, RoutedEventArgs e)
        {
            if (PopUpCenter.ActionValidPopup("Cette action ne doit être effectuée que et uniquement suite à une mise à jour. Êtes-vous sûr ?"))
            {
                requestCenter.OptionRequest(BDDTabsName.Save + "/Cast");
                requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "BDD synchronisée.").ToJson());
                PopUpCenter.MessagePopup("Structure de la base de donnée synchronisée.");
            }
        }

        private void LaunchRequestPatern(object sender, SelectionChangedEventArgs e, string selectedOption)
        {
            switch(selectedOption)
            {
                case "Selectionnez une option":

                    requestTextBox.Text = string.Empty;
                    break;

                case "Update Ajout Colonne":

                    requestTextBox.Text = "ALTER TABLE table_name " +
                                          "ADD column_name column_type;";
                    break;

                case "Update Suppression Colonne":

                    requestTextBox.Text = "ALTER TABLE table_name " +
                                          "DROP COLUMN column_name;";
                    break;
            }
        }
    }
}
