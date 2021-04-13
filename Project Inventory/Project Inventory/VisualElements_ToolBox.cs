using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Project_Inventory
{
    class VisualElements_ToolBox
    {
        private NavigationService navServiceContext;
        private Window context;
        private WpfScreen wpfScreen;


        public VisualElements_ToolBox(NavigationService navService, Window context)
        {
            navServiceContext = navService;
            this.context = context;
            wpfScreen = new WpfScreen();
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

        public Button CreateButtonWithNavigation(string content,
                                                 string nextPageName, 
                                                 string skinName)
        {

            Button temp = CreateButton(content, skinName);
            temp = AddOnClickButton(temp, nextPageName);

            return temp;

        }

        private Button AddOnClickButton(Button button, string nextPageName)
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

        public Grid ChangeGrid(Grid temp,
                               int columnNb,
                               int rowNb,
                               string skinName)
        {

            temp = LoadGridSkin(temp, skinName);

            temp = CreateRowsInGrid(temp, rowNb);
            temp = CreateColumnsInGrid(temp, columnNb);

            return temp;
        }

        public Grid LoadGridSkin(Grid grid, string skinName)
        {
            switch (skinName)
            {
                case "row":
                    grid = GridSkins.RowGridSkin(grid, wpfScreen.PrimaryScreenSizeWidth());
                    break;

                case "column":
                    grid = GridSkins.ColumnGridSkin(grid, wpfScreen.PrimaryScreenSizeHeight());
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

        public Grid CreateButtonNavigateToGrid(Grid grid,
                                               string content,
                                               string nextPageName,
                                               int rowNb,
                                               int columnNb, 
                                               string skinName)
        {
            Button button = CreateButtonWithNavigation(content, nextPageName, skinName);

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

        // OTHERS //

        private void PageNavigation(object sender, RoutedEventArgs e, string nextPageName)
        {
            Uri uri = new Uri(nextPageName, UriKind.Relative);
            navServiceContext.Navigate(uri);
            context.Close();
        }
    }
}
