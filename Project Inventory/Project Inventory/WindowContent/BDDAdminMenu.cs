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
    public class BDDAdminMenu : WindowContent
    {
        private string[] topGridButtons;
        private RoutedEventLibrary[] topSwitchEvents;

        private string[] bottomGridButtons;
        private RoutedEventLibrary[] switchEvents;

        private int widthLimit;

        public BDDAdminMenu(ToolBox toolBox, Router _router, RequestCenter requestCenter, int _actualUserId, int _actualStorageId, int _actualDataId, int _actualCustomListId)
            : base(toolBox, _router, requestCenter, _actualUserId, _actualStorageId, _actualDataId, _actualCustomListId)
        {
            topGridButtons = new string[] { "Retour" };

            topSwitchEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(topSwitchEvents);

            topSwitchEvents[0].changePageEvent = GetEventHandler(WindowsName.MainMenu);

            bottomGridButtons = new string[] { "Syncronisation de la Base De Données" };

            switchEvents = new RoutedEventLibrary[1];
            RoutedEventLibrariesInit(switchEvents);
            switchEvents[0].optionalEventOne += new RoutedEventHandler((object sender, RoutedEventArgs e) => { CastDataBase(sender, e); });

            widthLimit = 5;
        }

        public new void TopGridInit(Grid topGrid)
        {
            toolBox.SetUpGrid(topGrid, 1, 1, SkinLocation.TopStretch, SkinSize.HeightOneTier);
            toolBox.CreateSwitchButtonsToGridByTab(topGrid, topGridButtons, topSwitchEvents,
                                                   SkinName.StandartLittleMargin,
                                                   SkinLocation.TopRight);
        }

        public new void BottomGridInit(Grid bottomGrid)
        {
            toolBox.ButtonPlacer(bottomGrid, bottomGridButtons.Length, widthLimit, SkinLocation.BottomStretch, SkinSize.HeightTwoTier);

            toolBox.CreateSwitchButtonsToGridByTab(bottomGrid, bottomGridButtons, switchEvents, SkinName.Standart, SkinLocation.CenterCenter);
        }

        private void CastDataBase(object sender, RoutedEventArgs e)
        {
            if (PopUpCenter.ActionValidPopup())
            {
                requestCenter.PostRequest(BDDTabsName.LogLibraries.ToString(), new Log(actualUserId, "Base De Données synchronisée.").ToJson());
                requestCenter.OptionRequest(BDDTabsName.Save.ToString() + "/Cast");

                PopUpCenter.MessagePopup("Base De Données synchronisée.");
            }
        }
    }
}
