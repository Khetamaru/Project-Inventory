using Project_Inventory.BDD;
using Project_Inventory.Tools;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Project_Inventory
{
    public class ToolBox
    {
        private WpfScreen wpfScreen;

        private double windowWidth;
        private double windowHeight;

        public ToolBox(double titleBarHeight)
        {
            wpfScreen = new WpfScreen();

            windowWidth = wpfScreen.PrimaryScreenSizeWidth();
            windowHeight = wpfScreen.PrimaryScreenSizeHeight() - titleBarHeight;
        }

        // BUTTON PART //

        public Button CreateButton(string content,
                                   SkinsName skinName,
                                   SkinsName skinPosition)
        {
            Button temp = new Button();

            temp.Content = content;

            LoadButtonPosition(temp, skinPosition);
            LoadButtonSkin(temp, skinName);

            return temp;
        }

        public Button CreateSwitchButton(string content,
                                         RoutedEventLibrary router,
                                         SkinsName skinName,
                                         SkinsName skinPosition)
        {
            Button temp = CreateButton(content, skinName, skinPosition);
            AddOnClickButton(temp, router);

            return temp;
        }

        private void AddOnClickButton(Button button, RoutedEventLibrary routedEventLibrary)
        {
            routedEventLibrary.EventInjection(button);
        }
        private void LoadButtonPosition(Button button, SkinsName skinPosition)
        {
            switch (skinPosition)
            {
                case SkinsName.TopLeft:
                    ButtonSkins.TopLeft(button);
                    break;

                case SkinsName.TopCenter:
                    ButtonSkins.TopCenter(button);
                    break;

                case SkinsName.TopRight:
                    ButtonSkins.TopRight(button);
                    break;

                case SkinsName.CenterLeft:
                    ButtonSkins.CenterLeft(button);
                    break;

                case SkinsName.CenterCenter:
                    ButtonSkins.CenterCenter(button);
                    break;

                case SkinsName.CenterRight:
                    ButtonSkins.CenterRight(button);
                    break;

                case SkinsName.BottomLeft:
                    ButtonSkins.BottomLeft(button);
                    break;

                case SkinsName.BottomCenter:
                    ButtonSkins.BottomCenter(button);
                    break;

                case SkinsName.BottomRight:
                    ButtonSkins.BottomRight(button);
                    break;
            }
        }

        private void LoadButtonSkin(Button button, SkinsName skinName)
        {
            switch (skinName)
            {
                case SkinsName.Standart:
                    ButtonSkins.StandartButtonSkin(button);
                    break;

                case SkinsName.StandartLittleMargin:
                    ButtonSkins.StandartLittleMargin(button);
                    break;
            }
        }

        // GRID PART //

        public void SetUpGrid(Grid grid,
                               int rowNb,
                               int columnNb,
                               SkinsName skinName,
                               SkinsName lengthName)
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

        public void LoadGridLocation(Grid grid, SkinsName skinName)
        {
            switch (skinName)
            {
                case SkinsName.TopLeft:
                    GridSkins.TopLeft(grid);
                    break;

                case SkinsName.StretchLeft:
                    GridSkins.StretchLeft(grid);
                    break;

                case SkinsName.BottomLeft:
                    GridSkins.BottomLeft(grid);
                    break;

                case SkinsName.TopStretch:
                    GridSkins.TopStretch(grid);
                    break;

                case SkinsName.BottomStretch:
                    GridSkins.BottomStretch(grid);
                    break;

                case SkinsName.StretchStretch:
                    GridSkins.StretchStretch(grid);
                    break;

                case SkinsName.CenterCenter:
                    GridSkins.CenterCenter(grid);
                    break;

                case SkinsName.StretchCenter:
                    GridSkins.StretchCenter(grid);
                    break;

                case SkinsName.CenterStretch:
                    GridSkins.CenterStretch(grid);
                    break;

                case SkinsName.TopRight:
                    GridSkins.TopRight(grid);
                    break;

                case SkinsName.StretchRight:
                    GridSkins.StretchRight(grid);
                    break;

                case SkinsName.BottomRight:
                    GridSkins.BottomRight(grid);
                    break;
            }
        }

        public void LoadGridLength(Grid grid, SkinsName lengthName)
        {
            switch (lengthName)
            {
                case SkinsName.WidthOneTier:
                    GridSkins.WidthOneTier(grid, windowWidth);
                    break;

                case SkinsName.WidthTwoTier:
                    GridSkins.WidthTwoTier(grid, windowWidth);
                    break;

                case SkinsName.HeightOneTier:
                    GridSkins.HeightOneTier(grid, windowHeight);
                    break;

                case SkinsName.HeightTwoTier:
                    GridSkins.HeightTwoTier(grid, windowHeight);
                    break;

                case SkinsName.HeightTenPercent:
                    GridSkins.HeightTenPercent(grid, windowHeight);
                    break;

                case SkinsName.HeightEightPercent:
                    GridSkins.HeightEightPercent(grid, windowHeight);
                    break;

                case SkinsName.HeightNintyPercent:
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
                                       SkinsName skinName,
                                       SkinsName skinPosition)
        {
            Button button = CreateButton(content, skinName, skinPosition);

            AddButtonToGrid(grid, button, rowNb, columnNb);
        }

        public void CreateSwitchButtonToGrid(Grid grid,
                                               string content,
                                               RoutedEventLibrary router,
                                               int rowNb,
                                               int columnNb,
                                               SkinsName skinName,
                                               SkinsName skinPosition)
        {
            Button button = CreateSwitchButton(content, router, skinName, skinPosition);

            AddButtonToGrid(grid, button, rowNb, columnNb);
        }

        public void CreateSwitchButtonToGrid(Grid grid,
                                               Storage storage,
                                               RoutedEventLibrary router,
                                               int rowNb,
                                               int columnNb,
                                               SkinsName skinName,
                                               SkinsName skinPosition)
        {
            Button button = CreateSwitchButton(storage.Name, router, skinName, skinPosition);

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

        public void CreateSwitchButtonsToGridByTab(Grid grid, string[] buttonsTab, SkinsName buttonsSkin, SkinsName skinPosition)
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

        public void CreateSwitchButtonsToGridByTab(Grid grid, string[] buttonsTab, RoutedEventLibrary[] routerTab, SkinsName buttonsSkin, SkinsName skinPosition)
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
                        CreateSwitchButtonToGrid(grid, buttonsTab[k], routerTab[k], i, j, buttonsSkin, skinPosition);
                        k++;
                    }
                }
            }
        }

        public void CreateSwitchButtonsToGridByTab(Grid grid, Storage[] buttonsTab, RoutedEventLibrary[] routerTab, SkinsName buttonsSkin, SkinsName skinPosition)
        {
            int i;
            int j;
            int k = 0;
            int z;

            int rowNb = grid.RowDefinitions.Count;
            int columnNb = grid.ColumnDefinitions.Count;

            for (i = 0; i < rowNb; i++)
            {
                for (j = 0; j < columnNb; j++)
                {
                    if (i >= 1)
                    {
                        if (j >= 1) { z = (i * 5) + j; }
                        else { z = i * 5; }
                    }
                    else
                    {
                        if (j >= 1) { z = j; }
                        else { z = 1; }
                    }

                    if (buttonsTab.Length > z)
                    {
                        CreateSwitchButtonToGrid(grid, buttonsTab[k], routerTab[k], i, j, buttonsSkin, skinPosition);
                        k++;
                    }
                }
            }
        }

        public void CreateSwitchButtonsToGridByTab(Grid grid, string[] buttonsTab, RoutedEventLibrary[] routerTab, SkinsName[] buttonsSkin, SkinsName[] skinPosition)
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
                        CreateSwitchButtonToGrid(grid, buttonsTab[k], routerTab[k], i, j, buttonsSkin[k], skinPosition[k]);
                        k++;
                    }
                }
            }
        }

        public void CreateFormToGridByTab(Grid grid, UIElementsName[] formElements, string[] labels, ComboBoxNames listBoxName)
        {
            Label label;
            UIElement uIElement;
            int i = 0;

            grid.ColumnDefinitions[0].Width = new GridLength(windowWidth * 0.2, GridUnitType.Pixel);
            grid.ColumnDefinitions[1].Width = new GridLength(windowWidth * 0.8, GridUnitType.Pixel);

            foreach (UIElementsName name in formElements)
            {
                label = new Label();
                UIElementSkin.LabelSkinForm(label, labels[i]);
                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
                grid.Children.Add(label);

                switch (name)
                {
                    case (UIElementsName.TextBox):

                        uIElement = new TextBox();
                        UIElementSkin.TextBoxSkinForm(uIElement as TextBox);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as TextBox);

                        break;

                    case (UIElementsName.TextBoxNumber):

                        uIElement = new TextBox();
                        UIElementSkin.TextBoxSkinForm(uIElement as TextBox);
                        UIElementSkin.TextBoxNumberSkinForm(uIElement as TextBox);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as TextBox);

                        break;

                    case (UIElementsName.DatePicker):

                        uIElement = new DatePicker();
                        UIElementSkin.DatePickerSkinForm(uIElement as DatePicker);
                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as DatePicker);

                        break;

                    case (UIElementsName.ComboBox):

                        uIElement = new ComboBox();

                        switch (listBoxName)
                        {
                            case ComboBoxNames.UIElementsType:
                                string[] comboBoxStrings = comboBoxStrings = new string[] { "TextBox", "TextBoxNumber", "DatePicker", "ComboBox" };
                                UIElementSkin.ComboBoxSkinForm(uIElement as ComboBox, comboBoxStrings);
                                break;
                        }

                        Grid.SetRow(uIElement, i);
                        Grid.SetColumn(uIElement, 1);
                        grid.Children.Add(uIElement as ComboBox);

                        break;
                }

                i++;
            }
        }

        public void CreateTabToGrid(Grid grid, string[,] stringTab, SkinsName skinPosition)
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
                        CreateTabCellToGrid(grid, stringTab[i, j], i, j, skinPosition);
                        k++;
                    }
                }
            }
        }

        public void CreateTabToGrid(Grid grid, string[,] stringTab, string[,] indicTab, SkinsName skinPosition)
        {
            int i;
            int j;
            int k = 0;

            int rowNb = grid.RowDefinitions.Count - 1;
            int columnNb = grid.ColumnDefinitions.Count;

            string[] addElemString = new string[columnNb];

            for (i = 0; i < addElemString.Length; i++)
            {
                addElemString[i] = string.Empty;
            }

            for (i = 0; i < rowNb; i++)
            {
                if (i == 0)
                {
                    for (j = 0; j < columnNb; j++)
                    {
                        if (stringTab.Length >= (i + 1) * (j + 1))
                        {
                            CreateHeaderToGrid(grid, stringTab[i, j], i, j, skinPosition);
                            k++;
                        }
                    }
                }
                else
                {
                    for (j = 0; j < columnNb; j++)
                    {
                        if (stringTab.Length >= (i + 1) * (j + 1))
                        {
                            CreateTabCellToGrid(grid, stringTab[i, j], indicTab[i, j], i, j, skinPosition);
                            k++;
                        }
                    }
                }
            }

            for (int l = 0; l < addElemString.Length; l++)
            {
                CreateTabCellToGrid(grid, addElemString[l], indicTab[0, l], rowNb + 1, l, skinPosition);
            }
        }

        public void CreateTabCellToGrid(Grid grid, string text, string indication, int row, int column, SkinsName skinPosition)
        {
            if (indication == UIElementsName.DatePicker.ToString())
            {
                DatePicker uiElement = new DatePicker();
                UIElementSkin.DatePickerSkinForm(uiElement);
                if (text != "")
                {
                    uiElement.SelectedDate = DateTime.Parse(text);
                }

                UIElementSkin.DatePickerSkinModify(uiElement);
                StorageViewerSkin.LoadSkinPosition(uiElement, skinPosition);

                // insert potential clickEvent

                Grid.SetRow(uiElement, row);
                Grid.SetColumn(uiElement, column);

                grid.Children.Add(uiElement);
            }

            if (indication == UIElementsName.TextBox.ToString())
            {
                TextBox uiElement = new TextBox();
                uiElement.Text = text;

                UIElementSkin.TextBoxSkinModify(uiElement);
                StorageViewerSkin.LoadSkinPosition(uiElement, skinPosition);

                // insert potential clickEvent

                Grid.SetRow(uiElement, row);
                Grid.SetColumn(uiElement, column);

                grid.Children.Add(uiElement);
            }

            if (indication == UIElementsName.TextBoxNumber.ToString())
            {
                TextBox uiElement = new TextBox();
                UIElementSkin.TextBoxNumberValidationHandler(uiElement);
                uiElement.Text = text;

                UIElementSkin.TextBoxNumberSkinModify(uiElement);
                StorageViewerSkin.LoadSkinPosition(uiElement, skinPosition);

                // insert potential clickEvent

                Grid.SetRow(uiElement, row);
                Grid.SetColumn(uiElement, column);

                grid.Children.Add(uiElement);
            }

            if (indication == UIElementsName.ComboBox.ToString())
            {
                ComboBox uiElement = new ComboBox();
                string[] comboBoxStrings = new string[] { "TextBox", "TextBoxNumber", "DatePicker", "ComboBox" };
                UIElementSkin.ComboBoxSkinForm(uiElement, comboBoxStrings);

                if (text != null)
                {
                    uiElement.SelectedItem = text;
                }
                else
                {
                    uiElement.SelectedItem = uiElement.Items[0];
                }

                UIElementSkin.ComboBoxSkinModify(uiElement);
                StorageViewerSkin.LoadSkinPosition(uiElement, skinPosition);

                // insert potential clickEvent

                Grid.SetRow(uiElement, row);
                Grid.SetColumn(uiElement, column);

                grid.Children.Add(uiElement);
            }
        }

        public void CreateTabCellToGrid(Grid grid, string text, int row, int column, SkinsName skinPosition)
        {
            Label label = new Label();
            label.Content = text;

            UIElementSkin.LabelSkinModify(label);
            StorageViewerSkin.LoadSkinPosition(label, skinPosition);

            // insert potential clickEvent

            Grid.SetRow(label, row);
            Grid.SetColumn(label, column);

            grid.Children.Add(label);
        }

        public void CreateHeaderToGrid(Grid grid, string text, int row, int column, SkinsName skinPosition)
        {
            TextBox textbox = new TextBox();
            textbox.Text = text;

            UIElementSkin.TextBoxSkinModify(textbox);
            StorageViewerSkin.LoadSkinPosition(textbox, skinPosition);

            Grid.SetRow(textbox, row);
            Grid.SetColumn(textbox, column);

            grid.Children.Add(textbox);
        }

        // Form //

        public void CreateScrollableForm(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, SkinsName gridSkin, SkinsName skinHeight, UIElementsName[] stringTab, string[] indicTab, ComboBoxNames listBoxNames)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, SkinsName.TopLeft, SkinsName.None);

            ScrollFormInit(embededGrid, gridRowTwo, scrollViewer);

            CreateFormToGridByTab(embededGrid, stringTab, indicTab, listBoxNames);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

        public void ScrollFormInit(Grid embededGrid, int gridRowTwo, ScrollViewer scrollViewer)
        {
            SetScrollFormHeight(embededGrid);

            if (gridRowTwo > 5)
            {
                ScrollViewerVertical(scrollViewer);
            }
            else
            {
                ScrollViewerNoOne(scrollViewer);
            }
        }

        public void SetScrollFormHeight(Grid grid)
        {
            foreach (RowDefinition row in grid.RowDefinitions)
            {
                GridSkins.RowHeightFifteenPercent(row, windowHeight);
            }
        }

        public void GetUiElementResult(Grid grid, string[] uiReselt, UIElementsName[] formElements)
        {
            int i = 0;
            int j = 0;

            foreach (UIElement element in grid.Children)
            {
                if (i % 2 != 0)
                {
                    switch (formElements[j])
                    {
                        case (UIElementsName.TextBox):

                            uiReselt[j] = (element as TextBox).Text;

                            break;

                        case (UIElementsName.TextBoxNumber):

                            uiReselt[j] = (element as TextBox).Text;

                            break;

                        case (UIElementsName.DatePicker):

                            uiReselt[j] = (element as DatePicker).Text;

                            break;

                        case (UIElementsName.ComboBox):

                            uiReselt[j] = (element as ComboBox).SelectedItem.ToString();

                            break;
                    }

                    j++;
                }

                i++;
            }
        }

        public bool FormResultValidation(string[] uiElements, UIElementsName[] formElements)
        {
            int i = 0;
            int j = 0;

            foreach (string element in uiElements)
            {
                switch (formElements[i])
                {
                    case (UIElementsName.TextBox):

                        for (j = 0; j < element.Length; j++)
                        {
                            if (element[j] == '~' ||
                               element[j] == ',' ||
                               element[j] == '{' ||
                               element[j] == '}')
                            {
                                element.Remove(j, 1);
                            }
                        }

                        break;

                    case (UIElementsName.TextBoxNumber):

                        break;

                    case (UIElementsName.DatePicker):

                        break;

                    case (UIElementsName.ComboBox):

                        break;
                }

                if (element == "" || element == null)
                {
                    return false;
                }

                i++;
            }

            return true;
        }

        // StorageViewer //

        public void CreateScrollableGrid(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, SkinsName gridSkin, SkinsName skinHeight, SkinsName tabPos, string[,] stringTab, string[,] indicTab)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, SkinsName.StretchStretch, skinHeight);

            ScrollGridInit(embededGrid, gridRowTwo, gridColumnTwo, scrollViewer);

            CreateTabToGrid(embededGrid, stringTab, tabPos);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

        public void CreateScrollableGridModfiable(Grid grid, Grid embededGrid, int gridRowOne, int gridColumnOne, int gridRowTwo, int gridColumnTwo, SkinsName gridSkin, SkinsName skinHeight, SkinsName tabPos, string[,] stringTab, string[,] indicTab)
        {
            ScrollViewer scrollViewer = new ScrollViewer();

            SetUpGrid(grid, gridRowOne, gridColumnOne, gridSkin, skinHeight);

            SetUpNewGrid(embededGrid, gridRowTwo, gridColumnTwo, SkinsName.StretchStretch, skinHeight);

            ScrollGridInit(embededGrid, gridRowTwo, gridColumnTwo, scrollViewer);

            CreateTabToGrid(embededGrid, stringTab, indicTab, tabPos);

            EmbedScrollableGrid(grid, embededGrid, scrollViewer);
        }

        public void SetUpNewGrid(Grid grid,
                           int rowNb,
                           int columnNb,
                           SkinsName skinName,
                           SkinsName lengthName)
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
            SetScrollGridHeight(embededGrid);
            SetScrollGridWidth(embededGrid);

            if (gridRowTwo > 6)
            {
                if (gridColumnTwo > 9)
                {
                    ScrollViewerBoth(scrollViewer);
                }
                else
                {
                    ScrollViewerVertical(scrollViewer);
                }
            }
            else
            {
                if (gridColumnTwo > 9)
                {
                    ScrollViewerHorizontal(scrollViewer);
                }
                else
                {
                    ScrollViewerNoOne(scrollViewer);
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
            foreach (ColumnDefinition column in grid.ColumnDefinitions)
            {
                GridSkins.ColumnHeightTenPercent(column, windowWidth);
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
                    GridSkins.RowHeightFifteenPercent(row, windowHeight);
                }
            }
        }

        public List<int> GetUIElements(Grid grid, Data[] data, string[,] indicationTab)
        {
            int rowNb = data.Length;
            int columnNb = data[0].DataText.Length;

            int k = 0;

            string[] dataText;
            List<int> changesList = new List<int>();
            bool trigger;

            for ( int i = 0; i < rowNb; i++) 
            {
                dataText = new string[columnNb];
                trigger = false;

                for ( int j = 0 ; j < columnNb ; j++ )
                {
                    if (i == 0)
                    {
                        dataText[j] = (grid.Children[k] as TextBox).Text;
                    }
                    else if (indicationTab[i, j] == UIElementsName.TextBox.ToString())
                    {

                        dataText[j] = (grid.Children[k] as TextBox).Text;
                    }
                    else if (indicationTab[i, j] == UIElementsName.TextBoxNumber.ToString())
                    {

                        dataText[j] = (grid.Children[k] as TextBox).Text;
                    }
                    else if (indicationTab[i, j] == UIElementsName.DatePicker.ToString())
                    {

                        dataText[j] = (grid.Children[k] as DatePicker).Text;
                    }
                    else if (indicationTab[i, j] == UIElementsName.ComboBox.ToString())
                    {

                        dataText[j] = (grid.Children[k] as ComboBox).SelectedItem.ToString();
                    }

                    if (dataText[j] != data[i].DataText[j])
                    {
                        trigger = true;
                    }

                    k++;
                }

                if (trigger)
                {
                    changesList.Add(i);
                    data[i].DataText = dataText;
                }
            }

            return changesList;
        }

        public bool OptionnalAdd(Grid grid, Data[] data, Data optionnalAdd)
        {
            int j = data.Length * data[0].DataText.Length;
            bool trigger = false;

            for (int i = 0; i < optionnalAdd.DataText.Length; i++)
            {
                if (optionnalAdd.DataType[i] == UIElementsName.TextBox.ToString())
                {

                    optionnalAdd.DataText[i] = (grid.Children[j + i] as TextBox).Text;
                }
                else if (optionnalAdd.DataType[i] == UIElementsName.TextBoxNumber.ToString())
                {

                    optionnalAdd.DataText[i] = (grid.Children[j + i] as TextBox).Text;
                }
                else if (optionnalAdd.DataType[i] == UIElementsName.DatePicker.ToString())
                {

                    optionnalAdd.DataText[i] = (grid.Children[j + i] as DatePicker).Text;
                }
                else if (optionnalAdd.DataType[i] == UIElementsName.ComboBox.ToString())
                {

                    optionnalAdd.DataText[i] = (grid.Children[j + i] as ComboBox).SelectedItem.ToString();
                }

                if (optionnalAdd.DataText[i] != string.Empty)
                {
                    trigger = true;
                }
            }

            return trigger;
        }
    }
}
