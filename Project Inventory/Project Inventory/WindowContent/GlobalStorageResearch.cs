using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Project_Inventory.BDD;
using Project_Inventory.Tools;
using Project_Inventory.Tools.FonctionalityCerters;
using Project_Inventory.Tools.NamesLibraries;

namespace Project_Inventory
{
    class GlobalStorageResearch : WindowContent
    {
        private Grid capGrid;

        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        public string researchKeyString;
        public TextBox researchTextBox;

        private List<Data> datas;
        private List<Button> buttons;
        private List<Storage> storages;

        public bool emptyInfoPopUp;

        private RoutedEventHandler reloadEvent;

        public GlobalStorageResearch(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomId, RoutedEventHandler _reloadEvent, string _researchKeyString)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomId)
        {
            topGridButtons = new string[] { "Chercher", "Retour" };

            researchKeyString = _researchKeyString;
            researchTextBox = new TextBox();
            researchTextBox.Text = _researchKeyString;

            reloadEvent = _reloadEvent;

            topSwitchEvents = new RoutedEventLibrary[2];
            RoutedEventLibrariesInit(topSwitchEvents);

            topSwitchEvents[0].resetPageEvent = reloadEvent;
            topSwitchEvents[1].changePageEvent = GetEventHandler(WindowsName.StorageSelectionMenu); 
            
            KeyPressedEventCenter.KeyPressedEventInjection(reloadEvent, KeyPressedName.EnterKey, researchTextBox);

            LoadBDDInfos();
        }

        /// <summary>
        /// Get all needed BDD infos
        /// </summary>
        public void LoadBDDInfos()
        {
            datas = JsonCenter.LoadGlobalStorageResearchInfos(requestCenter, out storages);

            if (datas == new List<Data>())
            {
                emptyInfoPopUp = true;
            }
            else
            {
                emptyInfoPopUp = false;
            }

            DataSourting();
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 2, SkinLocation.TopStretch, SkinSize.HeightTenPercent);

            toolBox.CreateSwitchButtonsToGridByTab(topGrid,
                                                   topGridButtons,
                                                   topSwitchEvents,
                                                   new SkinName[] { SkinName.StandartLittleMargin, SkinName.StandartLittleMargin },
                                                   new SkinLocation[] { SkinLocation.TopLeft, SkinLocation.TopRight });

            toolBox.InsertUIElementInGrid(topGrid, researchTextBox, 0, 0, UIElementsName.TextBox, SkinLocation.CenterCenter);
            researchTextBox.Focus();
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            capGrid = new Grid();

            toolBox.SetUpGrid(bottomGrid, 1, 1, SkinLocation.BottomStretch, SkinSize.HeightNintyPercent);

            toolBox.GlobalStorageResearchGrid(bottomGrid, capGrid, datas, buttons, storages);
        }

        /// <summary>
        /// Set Up Data to find by the research string
        /// </summary>
        private void DataSourting()
        {
            string[] strTab = researchKeyString.Split(' ');
            int index;
            List<Data> temp = new List<Data>();
            Button button;
            buttons = new List<Button>();

            foreach(Data data in datas)
            {
                index = 0;

                foreach (string str in strTab)
                {
                    if (data.DataText.Contains(str) || data.CodeBar.Contains(str))
                    {
                        index++;
                    }
                }

                if (index == strTab.Length)
                {
                    temp.Add(data);

                    button = new Button();
                    button.Content = "Voir details";
                    button.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) => { SeeDetails(sender, e, data.id); });

                    buttons.Add(button);
                }
            }

            if (buttons.Count < 1)
            {
                EmptyResearchResult();
            }

            datas = temp;
        }

        /// <summary>
        /// Launch Data details page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        private void SeeDetails(object sender, RoutedEventArgs e, int id)
        {
            actualDataId = id;

            GetEventHandler(WindowsName.DataDetailPage).Invoke(sender, e);
        }

        public void EmptyInfoPopUp()
        {
            PopUpCenter.MessagePopup("Aucune donnée n'a été trouvée.");
        }

        public void EmptyResearchResult()
        {
            PopUpCenter.MessagePopup("Aucun stockage n'a été trouvé.");
        }
    }
}
