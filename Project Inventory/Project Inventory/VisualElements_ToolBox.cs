using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Project_Inventory
{
    class VisualElements_ToolBox
    {
        private Window context;
        private WpfScreen wpfScreen;

        private double windowWidth;
        private double windowHeight;

        public VisualElements_ToolBox(Window context, double titleBarHeight)
        {
            this.context = context;
            wpfScreen = new WpfScreen();

            windowWidth = wpfScreen.PrimaryScreenSizeWidth();
            windowHeight = wpfScreen.PrimaryScreenSizeHeight() - titleBarHeight;
        }

        // BUTTON PART //

        public Button CreateButton(string content, string skinName)
        {
            Button temp = new Button();

            temp.Content = content;
            temp.HorizontalAlignment = HorizontalAlignment.Center;
            temp.VerticalAlignment = VerticalAlignment.Center;

            temp = LoadButtonSkin(temp, skinName);

            return temp;
        }

        public Button CreateRederectButton(string content,
                                                 Type nextPageName, 
                                                 string skinName)
        {

            Button temp = CreateButton(content, skinName);
            temp = AddOnClickButton(temp, nextPageName);

            return temp;

        }

        private Button AddOnClickButton(Button button, Type nextPageName)
        {
            button.Click += (object sender, RoutedEventArgs e) => 
            {
                PageNavigation(sender, e, nextPageName); 
            };
            return button;
        }

        private Button LoadButtonSkin(Button button, string skinName) 
        {
            switch(skinName) 
            {
                case "standart":
                    button = ButtonSkins.StandartButtonSkin(button);
                    break;
            }

            return button;
        }

        // GRID PART //

        public Grid SetUpGrid(Grid temp,
                               int rowNb,
                               int columnNb,
                               string skinName,
                               string lengthName)
        {
            temp = CreateRowsInGrid(temp, rowNb);
            temp = CreateColumnsInGrid(temp, columnNb);

            temp = LoadGridLocation(temp, skinName);
            temp = LoadGridLength(temp, lengthName);

            return temp;
        }

        public Grid LoadGridLocation(Grid grid, string skinName)
        {
            switch (skinName)
            {
                case "RowTopTier":
                    grid = GridSkins.RowTopTier(grid);
                    break;

                case "RowCenterTier":
                    grid = GridSkins.RowCenterTier(grid);
                    break;

                case "RowBottomTier":
                    grid = GridSkins.RowBottomTier(grid);
                    break;

                case "column":
                    grid = GridSkins.ColumnGridSkin(grid);
                    break;
            }

            return grid;
        }

        public Grid LoadGridLength(Grid grid, string lengthName)
        {
            switch (lengthName)
            {
                case "WidthOneTier":
                    grid = GridSkins.WidthOneTier(grid, windowWidth);
                    break;

                case "WidthTwoTier":
                    grid = GridSkins.WidthTwoTier(grid, windowWidth);
                    break;

                case "HeightOneTier":
                    grid = GridSkins.HeightOneTier(grid, windowHeight);
                    break;

                case "HeightTwoTier":
                    grid = GridSkins.HeightTwoTier(grid, windowHeight);
                    break;
            }

            return grid;
        }

        public Grid AddButtonToGrid(Grid grid, Button button, int rowNb, int columnNb)
        {
            Grid.SetRow(button, rowNb);
            Grid.SetColumn(button, columnNb);
            grid.Children.Add(button);

            return grid;
        }
        public Grid AddButtonToGrid(Grid grid, Button button, int rowNb)
        {
            Grid.SetRow(button, rowNb);
            Grid.SetColumnSpan(button, grid.ColumnDefinitions.Count);
            grid.Children.Add(button);

            return grid;
        }

        public Grid CreateButtonToGrid(Grid grid,
                                       string content,
                                       int rowNb,
                                       int columnNb, 
                                       string skinName)
        {
            Button button = CreateButton(content, skinName);

            grid = AddButtonToGrid(grid, button, rowNb, columnNb);

            return grid;
        }
        public Grid CreateButtonToGrid(Grid grid,
                                      string content,
                                      int rowNb,
                                      string skinName)
        {
            Button button = CreateButton(content, skinName);

            grid = AddButtonToGrid(grid, button, rowNb);

            return grid;
        }

        public Grid CreateRederectButtonToGrid(Grid grid,
                                               string content,
                                               Type nextPageName,
                                               int rowNb,
                                               int columnNb, 
                                               string skinName)
        {
            Button button = CreateRederectButton(content, nextPageName, skinName);

            grid = AddButtonToGrid(grid, button, rowNb, columnNb);

            return grid;
        }

        public Grid CreateRowsInGrid(Grid grid, int nbRows)
        {
            int i;

            for (i = 0; i < nbRows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            return grid;
        }

        public Grid CreateColumnsInGrid(Grid grid, int nbColumns)
        {
            int i;

            for (i = 0; i < nbColumns; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            return grid;
        }

        public Grid CreateButtonsToGridByTab(Grid grid, string[] buttonsTab, string buttonsSkin)
        {
            int i;
            int j;
            int k = 0;

            int rowNb = grid.RowDefinitions.Count;
            int columnNb = grid.ColumnDefinitions.Count;

            for (i = 0; i < rowNb; i++)
            {
                for (j = 0; j < columnNb; j++)
                {
                    if (buttonsTab[k] != null)
                    {
                        grid = CreateButtonToGrid(grid, buttonsTab[k], i, j, buttonsSkin);
                        k++;
                    }
                }
            }

            return grid;
        }

        public Grid CreateRederectButtonsToGridByTab(Grid grid, string[] buttonsTab, Type[] rederectTab, string buttonsSkin)
        {
            int i;
            int j;
            int k = 0;

            int rowNb = grid.RowDefinitions.Count;
            int columnNb = grid.ColumnDefinitions.Count;

            for (i = 0; i < rowNb; i++)
            {
                for (j = 0; j < columnNb; j++)
                {
                    if (buttonsTab[k] != null)
                    {
                        grid = CreateRederectButtonToGrid(grid, buttonsTab[k], rederectTab[k], i, j, buttonsSkin);
                        k++;
                    }
                }
            }

            return grid;
        }

        // OTHERS //

        private void PageNavigation(object sender, RoutedEventArgs e, Type nextPageName)
        {
            var nextWindow = Activator.CreateInstance(nextPageName);
            (nextWindow as Window).Show();
            context.Close();
        }
    }
}
