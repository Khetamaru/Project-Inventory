using Project_Inventory.Tools;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_Inventory
{
    public class VisualElements_ToolBox
    {
        private MainWindow context;
        private WpfScreen wpfScreen;

        private double windowWidth;
        private double windowHeight;

        public VisualElements_ToolBox(MainWindow _context, double titleBarHeight)
        {
            context = _context;
            wpfScreen = new WpfScreen();

            windowWidth = wpfScreen.PrimaryScreenSizeWidth();
            windowHeight = wpfScreen.PrimaryScreenSizeHeight() - titleBarHeight;
        }

        // BUTTON PART //

        public Button CreateButton(string content, 
                                   string skinName, 
                                   string skinPosition)
        {
            Button temp = new Button();

            temp.Content = content;

            temp = LoadButtonPosition(temp, skinPosition);
            temp = LoadButtonSkin(temp, skinName);

            return temp;
        }

        public Button CreateSwitchButton(string content,
                                                 RoutedEventHandler router, 
                                                 string skinName,
                                                 string skinPosition)
        {
            Button temp = CreateButton(content, skinName, skinPosition);
            temp = AddOnClickButton(temp, router);

            return temp;

        }

        private Button AddOnClickButton(Button button, RoutedEventHandler router)
        {
            button.Click += router;
            return button;
        }
        private Button LoadButtonPosition(Button button, string skinPosition)
        {
            switch (skinPosition)
            {
                case "TopLeft":
                    button = ButtonSkins.TopLeft(button);
                    break;

                case "TopCenter":
                    button = ButtonSkins.TopCenter(button);
                    break;

                case "TopRight":
                    button = ButtonSkins.TopRight(button);
                    break;

                case "CenterLeft":
                    button = ButtonSkins.CenterLeft(button);
                    break;

                case "CenterCenter":
                    button = ButtonSkins.CenterCenter(button);
                    break;

                case "CenterRight":
                    button = ButtonSkins.CenterRight(button);
                    break;

                case "BottomLeft":
                    button = ButtonSkins.BottomLeft(button);
                    break;

                case "BottomCenter":
                    button = ButtonSkins.BottomCenter(button);
                    break;

                case "BottomRight":
                    button = ButtonSkins.BottomRight(button);
                    break;
            }

            return button;
        }

        private Button LoadButtonSkin(Button button, string skinName) 
        {
            switch(skinName) 
            {
                case "standart":
                    button = ButtonSkins.StandartButtonSkin(button);
                    break;

                case "StandartLittleMargin":
                    button = ButtonSkins.StandartLittleMargin(button);
                    break;
            }

            return button;
        }

        // GRID PART //

        public Grid SetUpGrid(Grid grid,
                               int rowNb,
                               int columnNb,
                               string skinName,
                               string lengthName)
        {
            grid = EmptyGrid(grid);

            grid = CreateRowsInGrid(grid, rowNb);
            grid = CreateColumnsInGrid(grid, columnNb);

            grid = LoadGridLocation(grid, skinName);
            grid = LoadGridLength(grid, lengthName);

            return grid;
        }

        public Grid EmptyGrid(Grid grid)
        {
            while (grid.Children.Count >= 1)
            {
                grid.Children.Remove(grid.Children[0]);
            }

            while (grid.RowDefinitions.Count >= 1)
            {
                grid.RowDefinitions.Remove(grid.RowDefinitions[0]);
            }

            while (grid.ColumnDefinitions.Count >= 1)
            {
                grid.ColumnDefinitions.Remove(grid.ColumnDefinitions[0]);
            }

            //grid.Width = 0;
            grid.Height = 0;

            return grid;
        }

        public Grid LoadGridLocation(Grid grid, string skinName)
        {
            switch (skinName)
            {
                case "TopLeft":
                    grid = GridSkins.TopLeft(grid);
                    break;

                case "StretchLeft":
                    grid = GridSkins.StretchLeft(grid);
                    break;

                case "BottomLeft":
                    grid = GridSkins.BottomLeft(grid);
                    break;

                case "TopStretch":
                    grid = GridSkins.TopStretch(grid);
                    break;

                case "BottomStretch":
                    grid = GridSkins.BottomStretch(grid);
                    break;

                case "StretchStretch":
                    grid = GridSkins.StretchStretch(grid);
                    break;

                case "CenterCenter":
                    grid = GridSkins.CenterCenter(grid);
                    break;

                case "StretchCenter":
                    grid = GridSkins.StretchCenter(grid);
                    break;

                case "CenterStretch":
                    grid = GridSkins.CenterStretch(grid);
                    break;

                case "TopRight":
                    grid = GridSkins.TopRight(grid);
                    break;

                case "StretchRight":
                    grid = GridSkins.StretchRight(grid);
                    break;

                case "BottomRight":
                    grid = GridSkins.BottomRight(grid);
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

                case "HeightTenPercent":
                    grid = GridSkins.HeightTenPercent(grid, windowHeight);
                    break;

                case "HeightEightPercent":
                    grid = GridSkins.HeightEightPercent(grid, windowHeight);
                    break;

                case "HeightNintyPercent":
                    grid = GridSkins.HeightNintyPercent(grid, windowHeight);
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

        public Grid CreateButtonToGrid(Grid grid,
                                       string content,
                                       int rowNb,
                                       int columnNb, 
                                       string skinName,
                                       string skinPosition)
        {
            Button button = CreateButton(content, skinName, skinPosition);

            grid = AddButtonToGrid(grid, button, rowNb, columnNb);

            return grid;
        }

        public Grid CreateSwitchButtonToGrid(Grid grid,
                                               string content,
                                               RoutedEventHandler router,
                                               int rowNb,
                                               int columnNb, 
                                               string skinName,
                                               string skinPosition)
        {
            Button button = CreateSwitchButton(content, router, skinName, skinPosition);

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

        public Grid CreateButtonsToGridByTab(Grid grid, string[] buttonsTab, string buttonsSkin, string skinPosition)
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
                    if (buttonsTab.Length >= (i + 1) * (j + 1))
                    {
                        grid = CreateButtonToGrid(grid, buttonsTab[k], i, j, buttonsSkin, skinPosition);
                        k++;
                    }
                }
            }

            return grid;
        }

        public Grid CreateSwitchButtonsToGridByTab(Grid grid, string[] buttonsTab, RoutedEventHandler[] routerTab, string buttonsSkin, string skinPosition)
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
                    if (buttonsTab.Length >= (i+1) * (j+1))
                    {
                        grid = CreateSwitchButtonToGrid(grid, buttonsTab[k], routerTab[k], i, j, buttonsSkin, skinPosition);
                        k++;
                    }
                }
            }

            return grid;
        }

        public Grid CreateFormToGridByTab(Grid grid, string[] formElements, string[] labels)
        {
            Label label;
            UIElement uIElement;
            int i = 0;

            grid.ColumnDefinitions[0].Width = new GridLength(20, GridUnitType.Star);
            grid.ColumnDefinitions[1].Width = new GridLength(80, GridUnitType.Star);

            foreach (string name in formElements)
            {
                label = new Label();
                label = FormSkin.LabelSkin(label, labels[i]);
                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
                grid.Children.Add(label);

                switch (name)
                {
                    case ("TextBox"):

                        uIElement = new TextBox();
                        uIElement = FormSkin.TextBoxSkin(uIElement as TextBox);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as TextBox);

                        break;

                    case ("TextBoxNumber"):

                        uIElement = new TextBox();
                        uIElement = FormSkin.TextBoxSkin(uIElement as TextBox);
                        uIElement = FormSkin.TextBoxNumberSkin(uIElement as TextBox);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as TextBox);

                        break;

                    case ("DatePicker"):

                        uIElement = new DatePicker();
                        uIElement = FormSkin.DatePickerSkin(uIElement as DatePicker);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as DatePicker);

                        break;

                    case ("ListBox"):

                        uIElement = new ListBox();
                        uIElement = FormSkin.ListBoxSkin(uIElement as ListBox, new string[] { "Option N°1", "Option N°2", "Option N°3" });
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as ListBox);

                        break;
                }

                i++;
            }

            return grid;
        }

        // FORM UIElements //
    }
}
