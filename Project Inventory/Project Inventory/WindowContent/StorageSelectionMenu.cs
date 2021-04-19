using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Project_Inventory
{
    public class StorageSelectionMenu : WindowContent
    {
        public StorageSelectionMenu(VisualElements_ToolBox visualElements_ToolBox)
            : base(visualElements_ToolBox)
        {

        }

        public new Grid TopGridInit(Grid topGrid)
        {
            topGrid = toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightTenPercent");

            string[] topGridButtons = new string[] { "Return" };
            Type[] rederectType = new Type[] { typeof(MainWindow) };

            topGrid = toolBox.CreateRederectButtonsToGridByTab(topGrid, topGridButtons, rederectType, "StandartLittleMargin", "TopRight");

            return topGrid;
        }

        public new Grid BottomGridInit(Grid bottomGrid)
        {
            bottomGrid = toolBox.SetUpGrid(bottomGrid, 5, 5, "BottomStretch", "HeightNintyPercent");

            string[] topGridButtons = new string[] { "Réserve N°1", "Réserve N°2", "Réserve N°3", "Réserve N°4", "Réserve N°5",
                                                     "Réserve N°6", "Réserve N°7", "Réserve N°8", "Réserve N°9", "Réserve N°10",
                                                     "Réserve N°11", "Réserve N°12", "Réserve N°13", "Réserve N°14", "Réserve N°15",
                                                     "Réserve N°16", "Réserve N°17", "Réserve N°18", "Réserve N°19", "Réserve N°20",
                                                     "Réserve N°21", "Réserve N°22", "Réserve N°23", "Réserve N°24"};
            Type[] rederectType = new Type[] { typeof(MainWindow), typeof(MainWindow), typeof(MainWindow), typeof(MainWindow), typeof(MainWindow),
                                               typeof(MainWindow), typeof(MainWindow), typeof(MainWindow), typeof(MainWindow), typeof(MainWindow),
                                               typeof(MainWindow), typeof(MainWindow), typeof(MainWindow), typeof(MainWindow), typeof(MainWindow),
                                               typeof(MainWindow), typeof(MainWindow), typeof(MainWindow), typeof(MainWindow), typeof(MainWindow),
                                               typeof(MainWindow), typeof(MainWindow), typeof(MainWindow), typeof(MainWindow), typeof(MainWindow)};

            bottomGrid = toolBox.CreateRederectButtonsToGridByTab(bottomGrid, topGridButtons, rederectType, "standart", "CenterCenter");

            return bottomGrid;
        }
    }
}
