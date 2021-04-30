using Project_Inventory.Tools;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_Inventory
{
    public class ToolBox
    {
        private MainWindow context;
        private WpfScreen wpfScreen;

        private double windowWidth;
        private double windowHeight;

        public ToolBox(MainWindow _context, double titleBarHeight)
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

            LoadButtonPosition(temp, skinPosition);
            LoadButtonSkin(temp, skinName);

            return temp;
        }

        public Button CreateSwitchButton(string content,
                                         RoutedEventHandler router, 
                                         string skinName,
                                         string skinPosition)
        {
            Button temp = CreateButton(content, skinName, skinPosition);
            AddOnClickButton(temp, router);

            return temp;
        }

        private void AddOnClickButton(Button button, RoutedEventHandler router)
        {
            button.Click += router;
        }
        private void LoadButtonPosition(Button button, string skinPosition)
        {
            switch (skinPosition)
            {
                case "TopLeft":
                    ButtonSkins.TopLeft(button);
                    break;

                case "TopCenter":
                    ButtonSkins.TopCenter(button);
                    break;

                case "TopRight":
                    ButtonSkins.TopRight(button);
                    break;

                case "CenterLeft":
                    ButtonSkins.CenterLeft(button);
                    break;

                case "CenterCenter":
                    ButtonSkins.CenterCenter(button);
                    break;

                case "CenterRight":
                    ButtonSkins.CenterRight(button);
                    break;

                case "BottomLeft":
                    ButtonSkins.BottomLeft(button);
                    break;

                case "BottomCenter":
                    ButtonSkins.BottomCenter(button);
                    break;

                case "BottomRight":
                    ButtonSkins.BottomRight(button);
                    break;
            }
        }

        private void LoadButtonSkin(Button button, string skinName) 
        {
            switch(skinName) 
            {
                case "standart":
                    ButtonSkins.StandartButtonSkin(button);
                    break;

                case "StandartLittleMargin":
                    ButtonSkins.StandartLittleMargin(button);
                    break;
            }
        }

        // GRID PART //

        public void SetUpGrid(Grid grid,
                               int rowNb,
                               int columnNb,
                               string skinName,
                               string lengthName)
        {
            EmptyGrid(grid);

            CreateRowsInGrid(grid, rowNb);
            CreateColumnsInGrid(grid, columnNb);

            LoadGridLocation(grid, skinName);
            LoadGridLength(grid, lengthName);
        }

        public void EmptyGrid(Grid grid)
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
        }

        public void LoadGridLocation(Grid grid, string skinName)
        {
            switch (skinName)
            {
                case "TopLeft":
                    GridSkins.TopLeft(grid);
                    break;

                case "StretchLeft":
                    GridSkins.StretchLeft(grid);
                    break;

                case "BottomLeft":
                    GridSkins.BottomLeft(grid);
                    break;

                case "TopStretch":
                    GridSkins.TopStretch(grid);
                    break;

                case "BottomStretch":
                    GridSkins.BottomStretch(grid);
                    break;

                case "StretchStretch":
                    GridSkins.StretchStretch(grid);
                    break;

                case "CenterCenter":
                    GridSkins.CenterCenter(grid);
                    break;

                case "StretchCenter":
                    GridSkins.StretchCenter(grid);
                    break;

                case "CenterStretch":
                    GridSkins.CenterStretch(grid);
                    break;

                case "TopRight":
                    GridSkins.TopRight(grid);
                    break;

                case "StretchRight":
                    GridSkins.StretchRight(grid);
                    break;

                case "BottomRight":
                    GridSkins.BottomRight(grid);
                    break;
            }
        }

        public void LoadGridLength(Grid grid, string lengthName)
        {
            switch (lengthName)
            {
                case "WidthOneTier":
                    GridSkins.WidthOneTier(grid, windowWidth);
                    break;

                case "WidthTwoTier":
                    GridSkins.WidthTwoTier(grid, windowWidth);
                    break;

                case "HeightOneTier":
                    GridSkins.HeightOneTier(grid, windowHeight);
                    break;

                case "HeightTwoTier":
                    GridSkins.HeightTwoTier(grid, windowHeight);
                    break;

                case "HeightTenPercent":
                    GridSkins.HeightTenPercent(grid, windowHeight);
                    break;

                case "HeightEightPercent":
                    GridSkins.HeightEightPercent(grid, windowHeight);
                    break;

                case "HeightNintyPercent":
                    GridSkins.HeightNintyPercent(grid, windowHeight);
                    break;
            }
        }

        public void AddButtonToGrid(Grid grid, Button button, int rowNb, int columnNb)
        {
            Grid.SetRow(button, rowNb);
            Grid.SetColumn(button, columnNb);
            grid.Children.Add(button);
        }

        public void CreateButtonToGrid(Grid grid,
                                       string content,
                                       int rowNb,
                                       int columnNb, 
                                       string skinName,
                                       string skinPosition)
        {
            Button button = CreateButton(content, skinName, skinPosition);

            AddButtonToGrid(grid, button, rowNb, columnNb);
        }

        public void CreateSwitchButtonToGrid(Grid grid,
                                               string content,
                                               RoutedEventHandler router,
                                               int rowNb,
                                               int columnNb, 
                                               string skinName,
                                               string skinPosition)
        {
            Button button = CreateSwitchButton(content, router, skinName, skinPosition);

            AddButtonToGrid(grid, button, rowNb, columnNb);
        }

        public void CreateRowsInGrid(Grid grid, int nbRows)
        {
            int i;

            for (i = 0; i < nbRows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        public void CreateColumnsInGrid(Grid grid, int nbColumns)
        {
            int i;

            for (i = 0; i < nbColumns; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        public void CreateButtonsToGridByTab(Grid grid, string[] buttonsTab, string buttonsSkin, string skinPosition)
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
                        CreateButtonToGrid(grid, buttonsTab[k], i, j, buttonsSkin, skinPosition);
                        k++;
                    }
                }
            }
        }

        public void CreateSwitchButtonsToGridByTab(Grid grid, string[] buttonsTab, RoutedEventHandler[] routerTab, string buttonsSkin, string skinPosition)
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
                        CreateSwitchButtonToGrid(grid, buttonsTab[k], routerTab[k], i, j, buttonsSkin, skinPosition);
                        k++;
                    }
                }
            }
        }

        public void CreateFormToGridByTab(Grid grid, string[] formElements, string[] labels)
        {
            Label label;
            UIElement uIElement;
            int i = 0;

            grid.ColumnDefinitions[0].Width = new GridLength(20, GridUnitType.Star);
            grid.ColumnDefinitions[1].Width = new GridLength(80, GridUnitType.Star);

            foreach (string name in formElements)
            {
                label = new Label();
                FormSkin.LabelSkin(label, labels[i]);
                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
                grid.Children.Add(label);

                switch (name)
                {
                    case ("TextBox"):

                        uIElement = new TextBox();
                        FormSkin.TextBoxSkin(uIElement as TextBox);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as TextBox);

                        break;

                    case ("TextBoxNumber"):

                        uIElement = new TextBox();
                        FormSkin.TextBoxSkin(uIElement as TextBox);
                        FormSkin.TextBoxNumberSkin(uIElement as TextBox);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as TextBox);

                        break;

                    case ("DatePicker"):

                        uIElement = new DatePicker();
                        FormSkin.DatePickerSkin(uIElement as DatePicker);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as DatePicker);

                        break;

                    case ("ListBox"):

                        uIElement = new ListBox();
                        FormSkin.ListBoxSkin(uIElement as ListBox, new string[] { "Option N°1", "Option N°2", "Option N°3" });
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as ListBox);

                        break;
                }

                i++;
            }
        }

        public void CreateTabToGrid(Grid grid, string[,] stringTab, string[,] indicTab, string stringSkin, string skinPosition)
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
                    if (stringTab.Length >= (i + 1) * (j + 1))
                    {
                        CreateTabCellToGrid(grid, stringTab[i, j], indicTab[i, j], i, j, stringSkin, skinPosition);
                        k++;
                    }
                }
            }
        }

        public void CreateTabCellToGrid(Grid grid, string text, string indication, int row, int column, string stringSkin, string skinPosition)
        {
            Label label = new Label();
            label.Content = text;

            StorageViewerSkin.LoadLabelSkin(label, stringSkin);
            StorageViewerSkin.LoadLabelSkinPosition(label, skinPosition);

            // insert potential clickEvent

            Grid.SetRow(label, row);
            Grid.SetColumn(label, column);

            grid.Children.Add(label);
        }

        // StorageViewer //

        public void CreateScrollableGrid(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, string gridSkin, string skinHeight, string tabSkin, string tabPos, string[,] stringTab, string[,] indicTab)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, gridSkin, "");

            ScrollGridInit(embededGrid, gridRowTwo, gridColumnTwo, scrollViewer);

            CreateTabToGrid(embededGrid, stringTab, indicTab, tabSkin, tabPos);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

        public void SetUpNewGrid(Grid grid,
                               int rowNb,
                               int columnNb,
                               string skinName,
                               string lengthName)
        {
            CreateRowsInGrid(grid, rowNb);
            CreateColumnsInGrid(grid, columnNb);

            LoadGridLocation(grid, skinName);
            LoadGridLength(grid, lengthName);
        }

        public void EmbedScrollableGrid(Grid grid, Grid embededGrid, ScrollViewer scrollViewer)
        {
            scrollViewer.Content = embededGrid;
            grid.Children.Add(scrollViewer);
        }

        public void ScrollGridInit(Grid embededGrid, int gridRowTwo, int gridColumnTwo, ScrollViewer scrollViewer)
        {
            if(gridRowTwo > 5)
            {
                if(gridColumnTwo > 5) 
                { 
                    ScrollViewerBoth(scrollViewer);
                    SetScrollGridHeight(embededGrid);
                    SetScrollGridWidth(embededGrid);
                }
                else 
                { 
                    ScrollViewerVertical(scrollViewer);
                    SetScrollGridHeight(embededGrid);
                }
            }
            else
            {
                if (gridColumnTwo > 5) 
                { 
                    ScrollViewerHorizontal(scrollViewer);
                    SetScrollGridWidth(embededGrid); 
                    GridSkins.RowHeightTenPercent(embededGrid.RowDefinitions[0], windowHeight);
                    GridSkins.HeightNintyPercent(embededGrid, windowHeight);
                }
                else 
                { 
                    ScrollViewerNoOne(scrollViewer);
                    GridSkins.RowHeightTenPercent(embededGrid.RowDefinitions[0], windowHeight);
                    GridSkins.HeightNintyPercent(embededGrid, windowHeight);
                }
            }
        }

        public void ScrollViewerBoth(ScrollViewer scrollViewer)
        {
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        public void ScrollViewerVertical(ScrollViewer scrollViewer)
        {
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        public void ScrollViewerHorizontal(ScrollViewer scrollViewer)
        {
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        public void ScrollViewerNoOne(ScrollViewer scrollViewer)
        {
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }

        public void SetScrollGridWidth(Grid grid)
        {
            foreach(ColumnDefinition column in grid.ColumnDefinitions) 
            { 
                GridSkins.ColumnHeightTwentyPercent(column, windowWidth);
            }
        }

        public void SetScrollGridHeight(Grid grid)
        {
            foreach (RowDefinition row in grid.RowDefinitions)
            {
                if (row.Equals(grid.RowDefinitions[0]))
                {
                    GridSkins.RowHeightTenPercent(row, windowHeight);
                }
                else
                {
                    GridSkins.RowHeightTwentyPercent(row, windowHeight);
                }
            }
        }
    }
}
