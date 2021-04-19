﻿using System;
using System.Windows.Controls;

namespace Project_Inventory
{
    public class MainMenu : WindowContent
    {
        public MainMenu(VisualElements_ToolBox visualElements_ToolBox)
            : base(visualElements_ToolBox)
        {

        }

        public new Grid TopGridInit(Grid topGrid)
        {
            topGrid = toolBox.SetUpGrid(topGrid, 1, 1, "TopStretch", "HeightOneTier");

            string[] topGridButtons = new string[] { "Logo Application" };

            topGrid = toolBox.CreateButtonsToGridByTab(topGrid, topGridButtons, "standart", "CenterCenter");

            return topGrid;
        }

        public new Grid BottomGridInit(Grid bottomGrid)
        {
            bottomGrid = toolBox.SetUpGrid(bottomGrid, 1, 2, "BottomStretch", "HeightTwoTier");

            string[] bottomGridButtons = new string[] { "Menu n°1", "Menu n°2" };
            Type[] rederectType = new Type[] { typeof(StorageSelectionPage), typeof(StorageSelectionPage) };

            bottomGrid = toolBox.CreateRederectButtonsToGridByTab(bottomGrid, bottomGridButtons, rederectType, "standart", "CenterCenter");

            return bottomGrid;
        }
    }
}
